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
    class Dinosaur : BaseCharacter
    {
        ImageBrush Background = new ImageBrush();

        public Dinosaur(double placeX, double placeY, Canvas arena,
           double width, double height) : base(placeX, placeY, arena, width, height)
        {
            this.state = StateType.runRight;
            MatchGif();
            base.moveTimer.Tick += MoveTimer_Tick;
            base.moveTimer.Start();
        }

       
        public override void MatchGif()
        {
            switch (this.state)
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

                //default:
                //    this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Gifs/Dinosaur/dino.png"));
                //    break;
            }
        }
        private void MoveTimer_Tick(object sender, object e)
        {

        }
    }
}
