using DataBaseProject;
using DataBaseProject.Models;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OptionsPage : Page
    {
        private User user = null;

        public OptionsPage()
        {
            this.InitializeComponent();
        }

        private void soundButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.sound = false;

            muteSoundButton.IsEnabled = true;
            muteSoundButton.Visibility = Visibility.Visible;

            soundButton.IsEnabled = false;
            soundButton.Visibility = Visibility.Collapsed;

        }
        private void musicButton_Click(object sender, RoutedEventArgs e)
        {

        }
        protected override void OnNavigatedTo(NavigationEventArgs e) // 
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter; // קבלת משתמש
            }
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage), this.user);
        }  
        private void HomeButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(MainPage), this.user);
        } // חזרה לדף הבית
        private void SettingsButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(OptionsPage), this.user);
        }// קישור לדף הגדרות
        private void StoreButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(StorePage), this.user);
        } // קישור לחנות
        private void LoginButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(LoginPage), this.user);
        } //קישור לדף התחברות
        private void HelpButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(HelpPage), this.user);
        } // קישור לדף עזרה
       /// <summary>
       /// שינוי סיסמה
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private async void ChangePassword_Click(object sender, RoutedEventArgs e) // שנה סיסמה
        {
            string Password1; // סיסמה ישנה
            string Password2; // סיסמה חדשה
            var oldPassword = new TextBox // תיבת טקסט שתכיל סיסמה ישנה
            {
                Width = 150,
                Height = 30,
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black),
                PlaceholderText = "Enter old password",


            };
            var newPassword = new TextBox // תיבת טקסט שתקלוט סיסמה חדשה
            {
                Width = 150,
                Height = 30,
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black),
                PlaceholderText = "Enter new password",


            };
            var passBlock = new TextBlock
            {
                Width = 300,
                Height = 30,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            StackPanel panel = new StackPanel // פאנל שיכיל את הכל
            {
                Width = 200
            };
            panel.Orientation = Orientation.Vertical;
            panel.Children.Add(oldPassword);
            panel.Children.Add(newPassword);
            ContentDialog firstPopUp = new ContentDialog() // פופ אפ עובר סיסמה ראשונה
            {
                Title = "Enter your old password: ",
                Content = panel,
                Background = new SolidColorBrush(Colors.Gray),
                Width = 400,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel",
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),

            };
            ContentDialog secondPopUp = new ContentDialog() // פופ אפ עבור סיסמה שניה
            {
                Title = "Enter your new password: ",
                Content = passBlock,
                Background = new SolidColorBrush(Colors.Gray),
                Width = 200,
                PrimaryButtonText = "Ok",
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),


            };
            var answer = await firstPopUp.ShowAsync();
            if (answer == ContentDialogResult.Primary) // אם נלחץ OK 
            {
                TextBox Pass1 = (TextBox)((StackPanel)firstPopUp.Content).Children[0]; // קליטת סיסמה ישנה
                TextBox Pass2 = (TextBox)((StackPanel)firstPopUp.Content).Children[1]; // קליטת סיסמה חדשה
                Password1 = Pass1.Text; // העברתה למשתנה מיוחד
                Password2 = Pass2.Text; // העברתה למשתנה מיוחד
                User user = DataBaseMethods.GetUserChangePassword(this.user.UserName, Password1); // אם ישנו משתמש עם סיסמה כזו, היוזר יכנס למשתנה המיועד
                if (user != null) // אם קיים משתמש כזה
                {
                    this.user.Password = Password2; // השמת סיסמה חדשה
                    string query;
                    query = $"UPDATE [Users] SET Password = '{Password2}' WHERE UserName= '{user.UserName}'"; // עדכון הסיסמה במערכת
                    DataBaseMethods.Execute(query); // המשתמש החדש ברגע זה מתווסף למאגר המשתמשים  הקיימים
                    ((TextBlock)secondPopUp.Content).Text = "Your password has been successfully changed"; // הצגת הודעה מתאימה
                }
                else 
                    ((TextBlock)secondPopUp.Content).Text = "The data you entered is incorrect"; // לא קיים משתמש כזה, הסיסמה שהוזנה לא נכונה
                await secondPopUp.ShowAsync();
            }
        } // 
        /// <summary>
        /// השתקת הסאונד במשחק
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void muteSoundButton_Click(object sender, RoutedEventArgs e) // פעולה שאחראית על השתקת הסאונד במשחק
        {
           // GlobalData.sound = true; // לחיצה פעם ראשונה: יש סאונד

            if (!GlobalData.sound)
            {
                muteSoundButton.IsEnabled = false; // 
                muteSoundButton.Visibility = Visibility.Collapsed;
                GlobalData.sound = false;

            }
            else
            {
                soundButton.IsEnabled = true;
                soundButton.Visibility = Visibility.Visible;
                GlobalData.sound = true;
            }
        }
    } 
}
