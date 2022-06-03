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
        public string type; // סוג הדמות: מכשול/דמות
        public bool CountJump = true;

        public BaseClass(double placeX, double placeY, Canvas arena, double width, double height)
        {
            this.speedX = 0;

            this.image = new Image(); //יצירת תמונה חדשה
            this.image.Width = width; // מקנים לה רוחב
            this.image.Height = height; // מקנים לה גובה

            this.arena = arena; // מורים על הקאנבאס עליו צריך לשים את הדמות
            Canvas.SetLeft(this.image, placeX); // מיקום הדמות על המסך
            Canvas.SetTop(this.image, placeY); // מיקופ הדמות על המסך
            this.arena.Children.Add(this.image); // מוסיפים את הדמות לקנבס

            position = new Point(placeX, placeY); // גישה קלה יותר למיקום
            size = new Size(width,height); // גישה נוחה יותר לגודל


            this.moveTimer = new DispatcherTimer(); 
            this.moveTimer.Interval = TimeSpan.FromMilliseconds(4);
            this.moveTimer.Start();
        }
       /// <summary>
       /// מקבלת את המיקום והגודל של מלבן
       /// </summary>
       /// <returns></returns>
        public Rect GetRectangle()
        {
            return new Rect(this.position.X, this.position.Y, this.image.Width, this.image.Height);
        }
       /// <summary>
       /// מקבלת תמונה
       /// </summary>
       /// <returns></returns>
        public Image GetImage()
        {
            return this.image;
        }
        /// <summary>
        /// מחזירה האם המכשול הוא מסוג קיר או מסוג שלט
        /// </summary>
        /// <returns></returns>
        public string GetBackground()
        {
            return type;
        }
    }

}
