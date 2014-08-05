using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls
{
    /// <summary>
    /// Interaction logic for WindowTitleControl.xaml
    /// </summary>
    public partial class WindowTitleControl : INotifyPropertyChanged
    {
        public static int CaptionHeight { get { return SystemInformation.CaptionHeight; } }

        public delegate void ShowOptionsRequestedEventHandler();

        public event ShowOptionsRequestedEventHandler ShowOptionsRequested;

        public static readonly DependencyProperty ShowOptionsProperty = DependencyProperty.Register("ShowOptions",
            typeof(bool), typeof(WindowTitleControl), new PropertyMetadata(false));

        public bool ShowOptions
        {
            get { return (bool)GetValue(ShowOptionsProperty); }
            set
            {
                if (ShowOptions != value)
                {
                    SetValue(ShowOptionsProperty, value);
                    OnPropertyChanged();
                }
            }
        }

        #region Button Brushes

        public static readonly DependencyProperty RegularButtonMouseOverBackgroundBrushProperty = DependencyProperty.Register(
            "RegularButtonMouseOverBackgroundBrush", typeof (Brush), typeof (WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(238, 238, 238))));

        public Brush RegularButtonMouseOverBackgroundBrush
        {
            get { return (Brush) GetValue(RegularButtonMouseOverBackgroundBrushProperty); }
            set { SetValue(RegularButtonMouseOverBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonMouseOverBackgroundBrushProperty = DependencyProperty.Register(
            "CloseButtonMouseOverBackgroundBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(224, 67, 67))));

        public Brush CloseButtonMouseOverBackgroundBrush
        {
            get { return (Brush) GetValue(CloseButtonMouseOverBackgroundBrushProperty); }
            set { SetValue(CloseButtonMouseOverBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty RegularButtonMouseDownBackgroundBrushProperty = DependencyProperty.Register(
            "RegularButtonMouseDownBackgroundBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(78, 166, 234))));

        public Brush RegularButtonMouseDownBackgroundBrush
        {
            get { return (Brush) GetValue(RegularButtonMouseDownBackgroundBrushProperty); }
            set { SetValue(RegularButtonMouseDownBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonMouseDownBackgroundBrushProperty = DependencyProperty.Register(
            "CloseButtonMouseDownBackgroundBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(153, 61, 61))));

        public Brush CloseButtonMouseDownBackgroundBrush
        {
            get { return (Brush) GetValue(CloseButtonMouseDownBackgroundBrushProperty); }
            set { SetValue(CloseButtonMouseDownBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty StaticButtonIconBrushProperty = DependencyProperty.Register(
            "StaticButtonIconBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(204, 204, 204))));

        public Brush StaticButtonIconBrush
        {
            get { return (Brush) GetValue(StaticButtonIconBrushProperty); }
            set { SetValue(StaticButtonIconBrushProperty, value); }
        }

        public static readonly DependencyProperty RegularButtonIconMouseHoverBrushProperty = DependencyProperty.Register(
            "RegularButtonIconMouseHoverBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

        public Brush RegularButtonIconMouseHoverBrush
        {
            get { return (Brush) GetValue(RegularButtonIconMouseHoverBrushProperty); }
            set { SetValue(RegularButtonIconMouseHoverBrushProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonIconMouseHoverBrushProperty = DependencyProperty.Register(
            "CloseButtonIconMouseHoverBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        public Brush CloseButtonIconMouseHoverBrush
        {
            get { return (Brush) GetValue(CloseButtonIconMouseHoverBrushProperty); }
            set { SetValue(CloseButtonIconMouseHoverBrushProperty, value); }
        }

        public static readonly DependencyProperty RegularButtonIconMouseDownBrushProperty = DependencyProperty.Register(
            "RegularButtonIconMouseDownBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        public Brush RegularButtonIconMouseDownBrush
        {
            get { return (Brush) GetValue(RegularButtonIconMouseDownBrushProperty); }
            set { SetValue(RegularButtonIconMouseDownBrushProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonIconMouseDownBrushProperty = DependencyProperty.Register(
            "CloseButtonIconMouseDownBrush", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        public Brush CloseButtonIconMouseDownBrush
        {
            get { return (Brush) GetValue(CloseButtonIconMouseDownBrushProperty); }
            set { SetValue(CloseButtonIconMouseDownBrushProperty, value); }
        }

        #endregion

        protected virtual void OnShowOptionsRequested()
        {
            var handler = ShowOptionsRequested;
            if (handler != null) handler();
        }

        public WindowTitleControl()
        {
            InitializeComponent();

            Loaded += WindowTitleControl_Loaded;
        }

        void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Window.DragMove();
        }

        void WindowTitleControl_Loaded(object sender, RoutedEventArgs e)
        {

            //take control of dragging.
            Window.MouseLeftButtonDown += Window_MouseLeftButtonDown;
        }

        private Window Window
        {
            get { return FindAncestor<Window>(this); }
        }

        private static T FindAncestor<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        private void OptionsClick(object sender, RoutedEventArgs e)
        {
            OnShowOptionsRequested();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Window.Close();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }
    }
}
