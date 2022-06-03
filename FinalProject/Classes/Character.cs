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
    class Character : BaseCharacter
    {
        ImageBrush Background = new ImageBrush(); // רקע הדמות
        
        public Character(double placeX, double placeY, Canvas arena,
           double width, double height) : base(placeX, placeY, arena, width, height) 
        {
            this.state = StateType.runRight;
            MatchGif();
            base.moveTimer.Tick += MoveTimer_Tick;
            base.moveTimer.Start();
        } // פעולה בונה
        /// <summary>
        /// //קובע לפי המצב של הדמות, איזה רקע לשים לה
        /// </summary>
        public override void MatchGif() 
        {
            switch (this.state) // קובע לפי המצב של הדמות, איזה רקע לשים לה
            {
                case StateType.runRight:
                    this.image.Source = new BitmapImage(new Uri(GlobalData.CharacterRun));
                    break;
                case StateType.jumpRight:
                    this.image.Source = new BitmapImage(new Uri(GlobalData.CharacterJump));
                    break;
                case StateType.idle:
                    this.image.Source = new BitmapImage(new Uri(GlobalData.CharacterIdle));
                    break;
                case StateType.dead:
                    this.image.Source = new BitmapImage(new Uri(GlobalData.CharacterDead));
                    break;
            }
        }
       /// <summary>
       /// הזזת הדמות
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void MoveTimer_Tick(object sender, object e)
        {

        }
    }
}
