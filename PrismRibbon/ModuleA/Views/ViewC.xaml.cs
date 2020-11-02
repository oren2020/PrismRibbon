using Prism.Regions;
using System.Windows.Controls;

namespace ModuleA.Views
{
    /// <summary>
    /// Interaction logic for ViewC.xaml
    /// </summary>
    public partial class ViewC : UserControl, IRegionMemberLifetime
    {
        public ViewC()
        {
            InitializeComponent();
        }

        ~ViewC()
        {
            int x = 1;
        }

        public bool KeepAlive => false;
    }
}
