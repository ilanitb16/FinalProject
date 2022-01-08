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
            double width, double height) : base(placeX,placeY,arena,width,height)
        {
            this.acceleration = 0;
            this.speedY = 0;
            this.state = StateType.runRight;
            base.moveTimer.Tick += moveTimer_Tick;

        }
        public void Jump()
        {
            if ( this.state == StateType.runRight)
            {
                this.state = StateType.jumpRight;
                MatchGif();
                this.speedY = -20;
                this.acceleration = 0.8;
            }
        }
        internal void Rest()
        {
            if (this.state != StateType.jumpRight)
            {
                this.state = StateType.idle;
                MatchGif();
                this.speedX = 0;
            }
        }
        internal void Die()
        {
            this.state = StateType.dead;
            MatchGif();
        }
        private void moveTimer_Tick(object sender, object e) 
        {
            this.position.Y += this.speedY;
            this.speedY += acceleration;
            Canvas.SetTop(this.image, this.position.Y);
            Canvas.SetLeft(this.image, this.position.X);

            if (this.position.Y >= 0.705* arena.ActualHeight)
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

        public virtual void MatchGif()
        {

        }

        public enum StateType
        {
             runRight, jumpRight, idle, dead
        }
    }

}
