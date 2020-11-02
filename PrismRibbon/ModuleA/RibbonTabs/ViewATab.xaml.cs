using Infrastructure;
using System.Windows.Controls.Ribbon;

namespace ModuleA.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewATab.xaml
    /// </summary>
    public partial class ViewATab : RibbonTab, ISupportDataContext
    {
        public ViewATab()
        {
            InitializeComponent();
        }
    }
}
