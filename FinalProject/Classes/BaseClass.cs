using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FinalProject.Classes
{
    public class BaseClass
    {
        public Image image; //מראה הדמות 
        protected Canvas arena; //תנועה
        public DispatcherTimer moveTimer; //יממש תנועה 
        protected double speedX; //מהירות אופקית

        public Point position; // מיקום הדמות: שילוב הפרמטרים איקס וואי
        public Size size; // גודל הדמות: שילוב הגובה והאורך
        public string type;
        public bool CountJump = true;

        public BaseClass(double placeX, double placeY, Canvas arena, double width, double height)
        {
            this.speedX = 0;

            this.image = new Image();
            this.image.Width = width;
            this.image.Height = height;

            this.arena = arena;
            Canvas.SetLeft(this.image, placeX);
            Canvas.SetTop(this.image, placeY);
            this.arena.Children.Add(this.image);

            position = new Point(placeX, placeY);
            size = new Size(width,height);


            this.moveTimer = new DispatcherTimer();
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(4);
            this.moveTimer.Start();
        }
        public Rect GetRectangle()
        {
            return new Rect(this.position.X, this.position.Y, this.image.Width, this.image.Height);
        }
        public Image GetImage()
        {
            return this.image;
        }
        public string GetBackground()
        {
            return type;
        }
    }

}
