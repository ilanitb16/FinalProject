using DataBaseProject;
using DataBaseProject.Models;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class StorePage : Page
    {
        private User user = null;

        public StorePage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// כפתור המבאפשר חזרה לדף הראשי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), this.user);
        }
        /// <summary>
        /// בכל פעם שהדף טוען בודקים האם המוצרים קיימים ברשותו של היוזר. אם כן, הכפתור משתנה ל"השתמש" אחרת הכפתור נשאר עם הכיתוב 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bool isExsist = false; // בדיקה האם המוצר קיים

            List<Purchase> p = DataBaseMethods.GetPurchases(this.user.Id); // קבלת רשימת הקניות של כל המשתמשים

            for (int i = 0; i < p.Count; i++) // בדיקה האם המוצר קיים בטבלה
            {
                if (p[i].ProductSeriaNumber == 2 && p[i].ProductType == "Character")
                    isExsist = true;
                if (isExsist)
                    dog_buy.Content = "use";
                isExsist = false; // השמת המשתנה לערכו הקודם
            }

            for (int i = 0; i < p.Count; i++) // בדיקה האם המוצר קיים בטבלה
            {
                if (p[i].ProductSeriaNumber == 3 && p[i].ProductType == "Character")
                    isExsist = true;
                if (isExsist)
                    Cat_buy.Content = "use";
                isExsist = false; // השמת המשתנה לערכו הקודם
            }

            for (int i = 0; i < p.Count; i++) // בדיקה האם המוצר קיים בטבלה
            {
                if (p[i].ProductSeriaNumber == 2 && p[i].ProductType == "Background")
                    isExsist = true;
                if (isExsist)
                    backgound2_buy.Content = "use";
                isExsist = false; // השמת המשתנה לערכו הקודם
            }

            for (int i = 0; i < p.Count; i++) // בדיקה האם המוצר קיים בטבלה
            {
                if (p[i].ProductSeriaNumber == 3 && p[i].ProductType == "Background")
                    isExsist = true;
                if (isExsist)
                    backgound3_buy.Content = "use";
                isExsist = false; // השמת המשתנה לערכו הקודם

            }

            coinTitle.Text = "coin:" + user.Coins; // כתיבת סה"כ המטבעות שנצברו
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.user = (User)e.Parameter;
            coinTitle.Text = "coins:" + user.Coins.ToString();

        }
       /// <summary>
       /// קניה ושימוש ברקע בהתאם
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void bg1Button_Click(object sender, RoutedEventArgs e)
        {
            if (BuyItem(0, user.Coins))
            {
                user.Coins -= 0;
            }
            GlobalData.BackgroundFileName = "bg2.jpg";
        }
        /// <summary>
        /// קניה ושימוש ברקע בהתאם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bg2Button_Click(object sender, RoutedEventArgs e)
        {
            bool isExsist = false;
            List<Purchase> p = DataBaseMethods.GetPurchases(this.user.Id);
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].ProductSeriaNumber == 2 && p[i].ProductType == "Background")
                    isExsist = true;
                if (isExsist)
                    backgound2_buy.Content = "use";
            }
            if (!isExsist)
            {
                if (BuyItem(300, user.Coins))
                {
                    user.Coins -= 300;
                    DataBaseMethods.ExecutePurchase(user, user.Id, 2, "Background");
                    backgound2_buy.Content = "use";
                    user.Backgrounds = 2;
                    GlobalData.BackgroundFileName = "snow.png";
                }
                if (!BuyItem(300, user.Coins))
                {
                    var dialog = new MessageDialog("Not enough coins");
                    dialog.Title = "System notice";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                    // await dialog.ShowAsync();
                }
            }
            else
            {
                GlobalData.BackgroundFileName = "snow.png";
            }
            coinTitle.Text = "coin:" + user.Coins;
        }
        /// <summary>
        /// קניה ושימוש ברקע בהתאם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bg3Button_Click(object sender, RoutedEventArgs e)
        {
            bool isExsist = false;
            List<Purchase> p = DataBaseMethods.GetPurchases(this.user.Id);
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].ProductSeriaNumber == 3 && p[i].ProductType == "Background")
                    isExsist = true;
                if (isExsist)
                    backgound3_buy.Content = "use";
            }
            if (!isExsist)
            {
                if (BuyItem(400, user.Coins))
                {
                    user.Coins -= 400;
                    DataBaseMethods.ExecutePurchase(user, user.Id, 3, "Background");
                    backgound3_buy.Content = "use";
                    user.Backgrounds = 3;
                    GlobalData.BackgroundFileName = "bg3.jpg";
                }
                if (!BuyItem(400, user.Coins))
                {
                    var dialog = new MessageDialog("Not enough coins");
                    dialog.Title = "System notice";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                    // await dialog.ShowAsync();
                }
            }
            else
            {
                GlobalData.BackgroundFileName = "bg3.jpg";
            }
            coinTitle.Text = "coin:" + user.Coins;
        }
        /// <summary>
        /// פעולה המסייעת ברגישת מוצר ובודקת האם הוא עומד בתקציב המשתמש
        /// </summary>
        /// <param name="price">מחיר המוצר</param>
        /// <param name="budget">תקציב</param>
        /// <returns></returns>
        public bool BuyItem(int price, int budget)
        {
            if (budget - price < 0)
                return false;
            return true;
        }
        /// <summary>
        /// קניה ושימוש בדמות בהתאם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dino_buy_Click(object sender, RoutedEventArgs e)
        {
            if (BuyItem(0, user.Coins))
            {
                user.Coins -= 0;
            }
        }
        /// <summary>
        /// קניה ושימוש בדמות בהתאם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dog_buy_Click(object sender, RoutedEventArgs e)
        {
            bool isExsist = false;
            List<Purchase> p = DataBaseMethods.GetPurchases(this.user.Id);
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].ProductSeriaNumber == 2 && p[i].ProductType== "Character")
                    isExsist = true;
                if(isExsist)
                    dog_buy.Content = "use";
            }
            if (!isExsist)
            {
                if (BuyItem(250, user.Coins))
                {
                    user.Coins -= 250;
                    DataBaseMethods.ExecutePurchase(user, user.Id, 2, "Character");
                    dog_buy.Content = "use";
                    user.CurrentCharacter = 2;

                    GlobalData.CharacterRun = "ms-appx:///Assets/Characters/Gifs/Dog/Run.gif";
                    GlobalData.CharacterJump = "ms-appx:///Assets/Characters/Gifs/Dog/Jump.gif";
                    GlobalData.CharacterIdle = "ms-appx:///Assets/Characters/Gifs/Dog/DogIdle.gif";
                    GlobalData.CharacterDead = "ms-appx:///Assets/Characters/Gifs/Dog/Dead.gif";
                }
                if (!BuyItem(250, user.Coins))
                {
                    var dialog = new MessageDialog("Not enough coins");
                    dialog.Title = "System notice";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                   // await dialog.ShowAsync();
                }
            }
            else
            {
                GlobalData.CharacterRun = "ms-appx:///Assets/Characters/Gifs/Dog/Run.gif";
                GlobalData.CharacterJump = "ms-appx:///Assets/Characters/Gifs/Dog/Jump.gif";
                GlobalData.CharacterIdle = "ms-appx:///Assets/Characters/Gifs/Dog/DogIdle.gif";
                GlobalData.CharacterDead = "ms-appx:///Assets/Characters/Gifs/Dog/Dead.gif";
            }
            coinTitle.Text = "coin:" + user.Coins;
        }
        /// <summary>
        /// קניה ושימוש בדמות בהתאם
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cat_buy_Click(object sender, RoutedEventArgs e)
        {
            bool isExsist = false;
            List<Purchase> p = DataBaseMethods.GetPurchases(this.user.Id);
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].ProductSeriaNumber == 3 && p[i].ProductType == "Character")
                    isExsist = true;
                if (isExsist)
                    Cat_buy.Content = "use";
            }
            if (!isExsist)
            {
                if (BuyItem(200, user.Coins))
                {
                    user.Coins -= 200;
                    DataBaseMethods.ExecutePurchase(user, user.Id, 3, "Character");
                    user.CurrentCharacter = 3;
                    Cat_buy.Content = "use";
                    GlobalData.CharacterRun = "ms-appx:///Assets/Characters/Gifs/Cat/RunningCat.gif";
                    GlobalData.CharacterJump = "ms-appx:///Assets/Characters/Gifs/Cat/JumpingCat.gif";
                    GlobalData.CharacterIdle = "ms-appx:///Assets/Characters/Gifs/Cat/IdleCat.gif";
                    GlobalData.CharacterDead = "ms-appx:///Assets/Characters/Gifs/Cat/DeadCat.gif";
                }
                if (!BuyItem(200, user.Coins))
                {
                    var dialog = new MessageDialog("Not enough coins");
                    dialog.Title = "System notice";
                }
            }
            else
            {
                GlobalData.CharacterRun = "ms-appx:///Assets/Characters/Gifs/Cat/RunningCat.gif";
                GlobalData.CharacterJump = "ms-appx:///Assets/Characters/Gifs/Cat/JumpingCat.gif";
                GlobalData.CharacterIdle = "ms-appx:///Assets/Characters/Gifs/Cat/IdleCat.gif";
                GlobalData.CharacterDead = "ms-appx:///Assets/Characters/Gifs/Cat/DeadCat.gif";
            }
            coinTitle.Text = "coin:" + user.Coins;

        }
        private void backgound1_buy_Click(object sender, RoutedEventArgs e)
        {
            if (BuyItem(0, user.Coins))
            {
                user.Coins -= 0;
            }
            GlobalData.BackgroundFileName = "bg2.jpg";
        }
    }
}
