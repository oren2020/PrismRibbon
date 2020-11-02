using Infrastructure;
using ModuleA.RibbonTabs;
using System.Windows.Controls;

namespace ModuleA.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    [DependentView(typeof(ViewATab), RegionNames.RibbonTabRegion)]
    [DependentView(typeof(ViewATabExtra), RegionNames.RibbonTabRegion)]
    public partial class ViewA : UserControl, ISupportDataContext
    {
        public ViewA()
        {
            InitializeComponent();
        }
        
        ~ViewA()
        {
            int x = 1;
        }
    }
}
