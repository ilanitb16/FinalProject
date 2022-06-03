using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject.Classes
{
    class BaseCharacter:BaseClass
    {
        protected double acceleration; //תאוצת התנועה
        public double speedY { set; get; } //מהירות אנכית 
        public StateType state { set; get; } //מצב הדמות 

        public BaseCharacter(double placeX, double placeY, Canvas arena,
            double width, double height) : base(placeX,placeY,arena,width,height) // יצירת דמות עם הנתונים שהיא צריכה
        {
            this.acceleration = 0; 
            this.speedY = 0;
            this.state = StateType.runRight; // מצב ברירת מחדל
            base.moveTimer.Tick += moveTimer_Tick;

        }
        /// <summary>
        /// גורם לדמות לקפוץ
        /// </summary>
        public void Jump() // אחראי על תהליך הקפיצה
        {
            if ( this.state == StateType.runRight)
            {
                this.state = StateType.jumpRight;
                MatchGif();
                this.speedY = -20;
                this.acceleration = 0.8;
            }
        }
        /// <summary>
          /// גורם לדמות לעמוד 
          /// </summary>
        internal void Rest() // אחראי על עמידה
        {
            if (this.state != StateType.jumpRight)
            {
                this.state = StateType.idle;
                MatchGif();
                this.speedX = 0;
            }
        }
        /// <summary>
        /// מצב הדמות בסוף משחק
        /// </summary>
        internal void Die() // מה שתמרחש כאשר המשחק נגמר 
        {
            this.state = StateType.dead;
            MatchGif();
            //this.position.Y = 0.705 * arena.ActualHeight;
            //this.speedX = 0;

        }
        /// <summary>
        /// תנועת הדמות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveTimer_Tick(object sender, object e)  // טיימר שאחראי על תנועת הדמות
        {
            this.position.Y += this.speedY; // הוספת מהירות למיקום- יוצר את ההזזה של הדמות
            this.speedY += acceleration;
            Canvas.SetTop(this.image, this.position.Y); // מיקום הדמות במקום הנכון 
            Canvas.SetLeft(this.image, this.position.X); // מיקום הדמות במקום הנכון

            if (this.position.Y >= 0.705* arena.ActualHeight) // הגדרת "רצפה" כלומר הדמות לא יכולה לרדת מתחת לרף שהוגדר
            {
                this.speedY = 0;
                this.speedX = 0;
                this.acceleration = 0;
                this.position.Y = 0.7 * arena.ActualHeight;
                Canvas.SetTop(this.image, this.position.Y);
                this.state = StateType.runRight;
                MatchGif();
            }
        }
        /// <summary>
        /// התאמת הרקע של הדמות למצבה
        /// </summary>
        public virtual void MatchGif()
        {

        }
        public enum StateType // המצבים האפשריים של הדמות
        {
             runRight, jumpRight, idle, dead
        }
    }
}
