using DataBaseProject.Models;
using FinalProject.Classes;
using FinalProject.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private User user = null;
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        { 
            if(user != null) // אם המשתמש מחובר
            {
                LoginButton.IsEnabled = false;
                LogoutButton.IsEnabled = true;

                LoginButton.Visibility = Visibility.Collapsed;
                LogoutButton.Visibility = Visibility.Visible;
            }
            else //אם המשתמש אינו מחובר
            {
                LoginButton.IsEnabled = true;
                LogoutButton.IsEnabled = false;

                LoginButton.Visibility = Visibility.Visible;
                LogoutButton.Visibility = Visibility.Collapsed;

                this.PlayButton.IsEnabled = true;
                this.StorageButton.IsEnabled = true;
                this.StoreButton.IsEnabled = true;
                this.RecordButton.IsEnabled = true;
                this.SettingsButton.IsEnabled = true;
            }
        }
        private void StorageButton_Click(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(HelpPage),this.user);
        }  // קישור לדף עזרה
        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.user != null)
                Frame.Navigate(typeof(StorePage),this.user);
        } // קישור לדף החנות
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.user != null)
                Frame.Navigate(typeof(GamePage), this.user);
        } // קישור לדף משחקים
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        } // קישור לדף הרשמה
        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.user != null)
                Frame.Navigate(typeof(Record), this.user);

        } // קישור לף השיאים
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.user != null)
                Frame.Navigate(typeof(OptionsPage),this.user);

        } // קישור לדף ההגדרות
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter; // קבלת משתמש
                this.PlayButton.IsEnabled = true;
                this.StorageButton.IsEnabled = true;
                this.StoreButton.IsEnabled = true;
                this.LoginButton.IsEnabled = false;
                this.RecordButton.IsEnabled = true;
                this.SettingsButton.IsEnabled = true;

            }
            else
            {
                this.PlayButton.IsEnabled = false;
                this.StorageButton.IsEnabled = false;
                this.StoreButton.IsEnabled = false;
                this.RecordButton.IsEnabled = false;
                this.SettingsButton.IsEnabled = false;
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            user = null;

            this.PlayButton.IsEnabled = false;
            this.StorageButton.IsEnabled = false;
            this.StoreButton.IsEnabled = false;
            this.RecordButton.IsEnabled = false;
            this.SettingsButton.IsEnabled = false;

            LoginButton.IsEnabled = true;
            LogoutButton.IsEnabled = false;

            LoginButton.Visibility = Visibility.Visible;
            LogoutButton.Visibility = Visibility.Collapsed;
        } // אם המתשמש מחובר, הדף התחברות הופך לכפתור התנתקות
    }
}
