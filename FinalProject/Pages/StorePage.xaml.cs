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
    public sealed partial class StorePage : Page
    {
        public StorePage()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void CatButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.CharacterRun = "ms-appx:///Assets/Characters/Gifs/Cat/RunningCat.gif";
            GlobalData.CharacterJump = "ms-appx:///Assets/Characters/Gifs/Cat/JumpingCat.gif";
            GlobalData.CharacterIdle = "ms-appx:///Assets/Characters/Gifs/Cat/IdleCat.gif";
            GlobalData.CharacterDead = "ms-appx:///Assets/Characters/Gifs/Cat/DeadCat.gif";
    }

        private void DogButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DinoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bg1Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.BackgroundFileName = "bg2.jpg";
        }

        private void bg2Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.BackgroundFileName = "snow.png";

        }

        private void CatButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void bg3Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.BackgroundFileName = "bg3.jpg";
        }
    }
}
