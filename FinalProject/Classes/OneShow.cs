using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject.Classes
{
    public class OneShow
    {
        public double PlaceX { get; set; } //מיקום אופקי
        public double PlaceY { get; set; } //מיקום אנכי
        public Image Image { get; set; }   //מראה הדמות 

        private Canvas arena;  //זירת המשחק   
        public DispatcherTimer moveTimer { get; }//יממש תנועה 
        public double SpeedX { get; set; } //מהירות אופקית
        
        public OneShow(double placeX, Canvas arena, double width, double height, string fileName) //פעולה בונה שיוצרת את הרקע
        {
            this.PlaceX = placeX;
            this.Image = new Image();
            this.Image.Width = width;
            this.Image.Height = height;
            this.arena = arena;
            this.SpeedX =-5;
            this.Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/" + fileName));

            Canvas.SetLeft(this.Image, this.PlaceX);
            Canvas.SetTop(this.Image, this.PlaceY);

            this.arena.Children.Add(this.Image);
        }
    }
}
