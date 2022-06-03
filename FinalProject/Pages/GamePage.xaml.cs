using DataBaseProject.Models;
using FinalProject.Classes;
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
    public sealed partial class GamePage : Page
    {
        public User user = null;
        Manager manager;
        public GamePage()
        {
            this.InitializeComponent();
            BackButton.Visibility = Visibility.Visible;
        }
       /// <summary>
       /// מקשר בין התוכן של המנגר לפעולת המשחק
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.manager = new Manager(arena,this.user, mediaPlayer);   
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) 
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter; // קבלת משתמש
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e) //לחיצה על הכפתור "חזרה לאחור" מחזירה לדף הראשי
        {
         Frame.Navigate(typeof(MainPage), this.user);
        }
        private void PlaySound(string FilePath)
        {
            
            //MediaElement PlayMusic = new MediaElement(); 
            //StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //Folder = await Folder.GetFolderAsync("Assets");
            ////StorageFile sf = await Folder.GetFileAsync(FilePath);
            ////PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            //GlobalData.player.Play();
            

        }
    }
}
