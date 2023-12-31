﻿using Note.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Note.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowVM(this);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private bool isFullScreen = false;
        private double storedLeft, storedTop, storedWidth, storedHeight;

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as LoginWindowVM).InputUserPassword = (sender as PasswordBox).Password;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (isFullScreen)
            {
                // If already in full-screen mode, restore to normal size
                RestoreWindowSize();
            }
            else
            {
                // If not in full-screen mode, switch to full-screen
                GoFullScreen();
            }

            // Toggle the full-screen state
            isFullScreen = !isFullScreen;
        }
        private void GoFullScreen()
        {
            // Save the current window state
            storedLeft = Left;
            storedTop = Top;
            storedWidth = Width;
            storedHeight = Height;

            // Set the window to full-screen
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.NoResize;

            // Maximize the window to cover the entire screen
            WindowState = WindowState.Maximized;
            // Topmost = true;
        }

        private void RestoreWindowSize()
        {
            // Restore the original window size and state
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.CanMinimize;

            // Return to the original position and size
            Left = storedLeft;
            Top = storedTop;
            Width = storedWidth;
            Height = storedHeight;
            // Topmost = false;
        }
    }
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Field is required.")
                : ValidationResult.ValidResult;
        }
    }
}
