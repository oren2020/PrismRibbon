using Infrastructure;
using ModuleA.RibbonTabs;
using Prism.Regions;
using System.Windows.Controls;

namespace ModuleA.Views
{
    /// <summary>
    /// Interaction logic for ViewB.xaml
    /// </summary>
    [DependentView(typeof(ViewBTab), RegionNames.RibbonTabRegion)]
    [DependentView(typeof(ViewC), RegionNames.SubRegion)]
    public partial class ViewB : UserControl, ISupportDataContext, IRegionMemberLifetime
    {
        public ViewB()
        {
            InitializeComponent();
        }

        ~ViewB()
        {
            int x = 1;
        }

        public bool KeepAlive => false;
    }
}
