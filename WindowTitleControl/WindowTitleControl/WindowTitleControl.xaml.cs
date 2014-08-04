using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls
{
    /// <summary>
    /// Interaction logic for WindowTitleControl.xaml
    /// </summary>
    public partial class WindowTitleControl : UserControl
    {
        private Window Window
        {
            get { return FindAncestor<Window>(this); }
        }

        public WindowTitleControl()
        {
            InitializeComponent();
        }

        private void DraggableBar(object sender, MouseButtonEventArgs e)
        {
            Window.DragMove();
        }

        private static T FindAncestor<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
 
            if (parent == null) return null;
 
            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }
    }
}
