﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AeroColor;

namespace Controls
{
    /// <summary>
    /// Interaction logic for WindowTitleControl.xaml
    /// </summary>
    public partial class WindowTitleControl : UserControl, INotifyPropertyChanged
    {
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

        protected virtual void OnShowOptionsRequested()
        {
            ShowOptionsRequestedEventHandler handler = ShowOptionsRequested;
            if (handler != null) handler();
        }

        public WindowTitleControl()
        {
            InitializeComponent();
            Loaded += WindowTitleControl_Loaded;
        }

        void WindowTitleControl_Loaded(object sender, RoutedEventArgs e)
        {
            AeroResourceInitializer.Initialize();
        }

        private Window Window
        {
            get { return FindAncestor<Window>(this); }
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
