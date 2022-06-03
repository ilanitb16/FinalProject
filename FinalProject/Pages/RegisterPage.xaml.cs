using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class RegisterPage : Page
    {
        private User user;
        public RegisterPage()
        {
            this.InitializeComponent();
        }
       /// <summary>
       /// פעולה שארחאית על ניקוי כל השדות
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            UserName.Text = "";
            Password.Password = "";
            confirmPassword.Password = "";
            Gamil.Text = "";
        } //ניקוי השדות
       /// <summary>
       /// פעולה שמעבירה את המשתמש חזרה לדף הבית
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), this.user);

        }  // חזרה לדף הבית
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

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
       /// <summary>
       /// אם היוזר לא קיים, כל השדות תקינים אז הוא נוסף לרשימת יוזרים והמתשמש החדש נוצר
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text != "" && Gamil.Text != "" && Password.Password != "" && confirmPassword.Password != "" && Gamil.ToString().Contains('@')) // אם השדות אינם רקים
            {
                if (Password.Password.Equals(confirmPassword.Password) == true) // השוואה האם הסיסמאות שהוזנו זהות
                {
                    this.user = DataBaseMethods.AddUser(UserName.Text, Password.Password, Gamil.Text);  //   מוסיפים את הערכים רק אם המשתמש אינו קיים בדטה בייס

                    if (this.user != null) // אם יש ערכים
                    {
                        Frame.Navigate(typeof(MainPage), this.user);

                        string query;
                        query = $"INSERT INTO [Purchases] (UserId, ProductSerialNumber, ProductType ) VALUES ({this.user.Id}, {1}, '{"Character"}')";
                        DataBaseMethods.Execute(query); // הוספת דמות ברירת מחדל
                        query = $"INSERT INTO [Purchases] (UserId, ProductSerialNumber, ProductType ) VALUES ({this.user.Id}, {1}, '{"Background"}')";
                        DataBaseMethods.Execute(query); // הוספת רקע ברירת מחדל
                    }
                    else
                    {
                        var dialog = new MessageDialog("The user already exits!");
                        dialog.Title = "System notice";
                        dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    var dialog = new MessageDialog("Passwords are not the same!");
                    dialog.Title = "System notice";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new MessageDialog("One of fields is empty or incorrect!");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
                await dialog.ShowAsync();
            }

            if (!Gamil.ToString().Contains('@'))
            {
                var dialog = new MessageDialog("Gmail Address must contain @");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                await dialog.ShowAsync();
            }
        } //מבצעת ולידציה, אם היוזר מתאים אז הוא נוסף לדטה בייז
    }
}
