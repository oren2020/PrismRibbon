using Infrastructure;
using System.Windows.Controls.Ribbon;

namespace ModuleA.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewBTab.xaml
    /// </summary>
    public partial class ViewBTab : RibbonTab, ISupportDataContext
    {
        public ViewBTab()
        {
            InitializeComponent();
        }
    }
}
