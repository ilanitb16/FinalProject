using DataBaseProject;
using DataBaseProject.Models;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Record : Page
    {
        private User user = null;
        public Record()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            User[] user = DataBaseMethods.GetTopThree();
            firstPlace.Text = user[0].UserName;
            secondPlace.Text = user[1].UserName;
            thirdPlace.Text = user[2].UserName;

            Coins1.Text += user[0].Coins.ToString();
            Score1.Text += user[0].Score.ToString();

            Coins2.Text += user[1].Coins.ToString();
            Score2.Text += user[1].Score.ToString();

            Coins3.Text += user[2].Coins.ToString();
            Score3.Text += user[2].Score.ToString();

            PersonalScore.Text += this.user.Score + "";
        }
        private void HomeButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(MainPage),this.user);
        } // חזרה לדף הבית
        private void SettingsButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(OptionsPage),this.user);
        } // קישור לדף הגדרות
        private void StoreButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(StorePage),this.user);
        } // קישור לחנות
        private void LoginButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(LoginPage),this.user);
        } //קישור לדף התחברות
        private void HelpButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(HelpPage),this.user);
        } // קישור לדף עזרה
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter; // קבלת משתמש
            }
        }
    }
}
