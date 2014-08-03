using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Controls
{
    /// <summary>
    /// Interaction logic for WindowTitleControl.xaml
    /// </summary>
    public partial class WindowTitleControl : UserControl
    {
        private Window Window
        {
            get { return Parent as Window; }
        }

        public WindowTitleControl()
        {
            InitializeComponent();
        }

        private void DraggableBar(object sender, MouseButtonEventArgs e)
        {
            Window.DragMove();
        }
    }
}
