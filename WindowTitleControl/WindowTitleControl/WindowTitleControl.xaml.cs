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

        public static readonly DependencyProperty RegularButtonHoverBackgroundProperty = DependencyProperty.Register(
            "RegularButtonHoverBackground", typeof (Brush), typeof (WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(238, 238, 238))));

        public Brush RegularButtonHoverBackground
        {
            get { return (Brush) GetValue(RegularButtonHoverBackgroundProperty); }
            set { SetValue(RegularButtonHoverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonHoverBackgroundProperty = DependencyProperty.Register(
            "CloseButtonHoverBackground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(224, 67, 67))));

        public Brush CloseButtonHoverBackground
        {
            get { return (Brush) GetValue(CloseButtonHoverBackgroundProperty); }
            set { SetValue(CloseButtonHoverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty RegularButtonClickBackgroundProperty = DependencyProperty.Register(
            "RegularButtonClickBackground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(78, 166, 234))));

        public Brush RegularButtonClickBackground
        {
            get { return (Brush) GetValue(RegularButtonClickBackgroundProperty); }
            set { SetValue(RegularButtonClickBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonClickBackgroundProperty = DependencyProperty.Register(
            "CloseButtonClickBackground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(153, 61, 61))));

        public Brush CloseButtonClickBackground
        {
            get { return (Brush) GetValue(CloseButtonClickBackgroundProperty); }
            set { SetValue(CloseButtonClickBackgroundProperty, value); }
        }

        public static readonly DependencyProperty DefaultButtonForegroundProperty = DependencyProperty.Register(
            "DefaultButtonForeground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(204, 204, 204))));

        public Brush DefaultButtonForeground
        {
            get { return (Brush) GetValue(DefaultButtonForegroundProperty); }
            set { SetValue(DefaultButtonForegroundProperty, value); }
        }

        public static readonly DependencyProperty RegularButtonHoverForegroundProperty = DependencyProperty.Register(
            "RegularButtonHoverForeground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

        public Brush RegularButtonHoverForeground
        {
            get { return (Brush) GetValue(RegularButtonHoverForegroundProperty); }
            set { SetValue(RegularButtonHoverForegroundProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonHoverForegroundProperty = DependencyProperty.Register(
            "CloseButtonHoverForeground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        public Brush CloseButtonHoverForeground
        {
            get { return (Brush) GetValue(CloseButtonHoverForegroundProperty); }
            set { SetValue(CloseButtonHoverForegroundProperty, value); }
        }

        public static readonly DependencyProperty RegularButtonClickForegroundProperty = DependencyProperty.Register(
            "RegularButtonClickForeground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        public Brush RegularButtonClickForeground
        {
            get { return (Brush) GetValue(RegularButtonClickForegroundProperty); }
            set { SetValue(RegularButtonClickForegroundProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonClickForegroundProperty = DependencyProperty.Register(
            "CloseButtonClickForeground", typeof(Brush), typeof(WindowTitleControl), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        public Brush CloseButtonClickForeground
        {
            get { return (Brush) GetValue(CloseButtonClickForegroundProperty); }
            set { SetValue(CloseButtonClickForegroundProperty, value); }
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
