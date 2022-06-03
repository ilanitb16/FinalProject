using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
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
    public sealed partial class LoginPage : Page
    {
        private User user;
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e) //מעבר חזרה לדף התחברות
        {
            this.Frame.Navigate(typeof(RegisterPage));
        }
        private void HomeButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(MainPage), this.user);
        } // מעבר חזרה לדף הבית
        private void SettingsButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(OptionsPage), this.user);
        } // מעבר להגדרות
        private void StoreButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(StorePage), this.user);
        } // מעבר לחנות
        private void LoginButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(LoginPage), this.user);
        } // קישור לדף ההתחברות
        private void HelpButton_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            this.Frame.Navigate(typeof(HelpPage), this.user);
        } // קישור לדף עזרה
        private void submit_Click(object sender, RoutedEventArgs e) // בדיקה האם קיים משתמש עם השם והסיסמה שהוזנו, אם לא קופצת הודעה מתאימה
        {
            // string dbPath = ApplicationData.Current.LocalFolder.Path;
            this.user = DataBaseMethods.GetUser(username.Text, password.Password);
            if (this.user != null)
                Frame.Navigate(typeof(MainPage), this.user);
            else
            {
                // הצגת הודעה קופצת
                var dialog = new MessageDialog("Incorrect Username or Password. Try again?");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "ok", Id = 0 });
                dialog.ShowAsync();
            }
        }
        private async void ForgetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string userName;
            string userMail;
            var nameBox = new TextBox // יצירת תיבת טקטס שתכיל את ההודעה "הכנס שם משתמש"
            {
                Width = 150,
                Height = 30,
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),
                PlaceholderText= "Enter username",
                
            };
            var mailBox = new TextBox // יצירת תיבת טקסט שתכיל את ההודעה הזן כתובת מייל
            {
                Width = 150,
                Height = 30,
                FontSize = 15,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),
                PlaceholderText ="Enter mail",
            };
            var passBlock = new TextBlock // יצירת תיבה להזנת הטקסט
            {
                Width = 300,
                Height = 30,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),
            };
            StackPanel panel = new StackPanel // יצירת פאנל שיכיל את הכל
            {
                Width = 200
            };
            panel.Orientation = Orientation.Vertical;
            panel.Children.Add(nameBox); // הוספת התיבה לפאנל
            panel.Children.Add(mailBox); // הוספת התיבה לפאנל
            
            ContentDialog firstPopUp = new ContentDialog() // יצירת הפופ אפ
            {
                Title = "Enter your Username and your Email address: ", // כותרת ראשית
                Content = panel, 
                Background = new SolidColorBrush(Colors.Gray),
                Width = 400,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel",
                Foreground = new SolidColorBrush(Colors.Black),
                FontFamily = new FontFamily("Times New Roman"),

            };
            ContentDialog secondPopUp = new ContentDialog() // יצירת הפופ אפ שפולט את הסיסמה או הודעת שגיאה מתאימה
            {
                Title = "Your password is: ",
                Content = passBlock,
                Background = new SolidColorBrush(Colors.Gray),
                Width = 200,
                PrimaryButtonText = "Ok",
                Foreground = new SolidColorBrush(Colors.Black)

            };
            var answer = await firstPopUp.ShowAsync();
            if (answer == ContentDialogResult.Primary)
            {
                TextBox nameText = (TextBox)((StackPanel)firstPopUp.Content).Children[0];
                TextBox mailText = (TextBox)((StackPanel)firstPopUp.Content).Children[1];
                userName = nameText.Text;
                userMail = mailText.Text;
                User user = DataBaseMethods.GetUserForgotPassword(userName, userMail);
                if (user != null)
                    ((TextBlock)secondPopUp.Content).Text = user.Password;
                else
                    ((TextBlock)secondPopUp.Content).Text = "The data you entered is incorrect";
                await secondPopUp.ShowAsync();
            }
        } // שכחתי סיסמה: אם לוחצים ומזינים את השדות המבוקשים, נחשפת הסיסמה
    }
}
