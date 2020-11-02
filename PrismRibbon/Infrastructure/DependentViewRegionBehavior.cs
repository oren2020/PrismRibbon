using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Infrastructure
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        public const string BehaviorKey = "DependentViewRegionBehavior";

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newView in e.NewItems)
                {
                    var viewList = new List<DependentViewInfo>();

                    if (_dependentViewCache.ContainsKey(newView))
                    {
                        viewList = _dependentViewCache[newView];
                    }
                    else
                    {
                        foreach (var atr in GetCustomAttributes<DependentViewAttribute>(newView.GetType()))
                        {
                            var info = CreateDependentViewInfo(atr);

                            if (info.View is ISupportDataContext && newView is ISupportDataContext)
                                ((ISupportDataContext)info.View).DataContext = ((ISupportDataContext)newView).DataContext;

                            viewList.Add(info);
                        }

                        if (!_dependentViewCache.ContainsKey(newView))
                            _dependentViewCache.Add(newView, viewList);
                    }

                    viewList.ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Add(x.View));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        _dependentViewCache[oldView].ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Remove(x.View));

                        if (!ShouldKeepAlive(oldView))
                            _dependentViewCache.Remove(oldView);
                    }
                }
            }
        }

        private bool ShouldKeepAlive(object oldView)
        {
            IRegionMemberLifetime lifetime = GetItemOrContentLifetime(oldView);
            if (lifetime != null)
                return lifetime.KeepAlive;

            RegionMemberLifetimeAttribute lifetimeAttribute = GetItemOrContextLifetimeAttribute(oldView);
            if (lifetimeAttribute != null)
                return lifetimeAttribute.KeepAlive;

            return true;
        }

        private RegionMemberLifetimeAttribute GetItemOrContextLifetimeAttribute(object oldView)
        {
            var lifeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(oldView.GetType()).FirstOrDefault();
            if (lifeAttribute != null)
                return lifeAttribute;

            var frameworkElement = oldView as FrameworkElement;
            if (frameworkElement != null && frameworkElement.DataContext != null)
            {
                var dataContext = frameworkElement.DataContext;
                var contextLifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(dataContext.GetType()).FirstOrDefault();
                return contextLifetimeAttribute;
            }

            return null;
        }

        private IRegionMemberLifetime GetItemOrContentLifetime(object oldView)
        {
            var regionLifetime = oldView as IRegionMemberLifetime;
            if (regionLifetime != null)
                return regionLifetime;

            var framework = oldView as FrameworkElement;
            if (framework != null)
                return framework.DataContext as IRegionMemberLifetime;

            return null;
        }

        private DependentViewInfo CreateDependentViewInfo(DependentViewAttribute atr)
        {
            var info = new DependentViewInfo();

            info.TargetRegionName = atr.TargetRegionName;

            if (atr.Type != null)
                info.View = Activator.CreateInstance(atr.Type);

            return info;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

    internal class DependentViewInfo
    {
        public object View { get; set; }
        public string TargetRegionName { get; set; }
    }
}
