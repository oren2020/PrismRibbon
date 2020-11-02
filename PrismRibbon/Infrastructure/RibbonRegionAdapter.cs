using Prism.Regions;
using System.Collections.Specialized;
using System.Windows.Controls.Ribbon;

namespace Infrastructure
{
    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddViewToRegion(view, regionTarget);
                    }

                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveViewFromRegion(view, regionTarget);
                    }
                }
            };
        }

        private void AddViewToRegion(object view, Ribbon regionTarget)
        {
            var ribbonTabItem = view as RibbonTab;
            if (ribbonTabItem != null)
                regionTarget.Items.Add(ribbonTabItem);
        }

        private void RemoveViewFromRegion(object view, Ribbon regionTarget)
        {
            var ribbonTabItem = view as RibbonTab;
            if (ribbonTabItem != null)
                regionTarget.Items.Remove(ribbonTabItem);
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
