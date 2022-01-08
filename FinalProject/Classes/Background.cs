using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject.Classes
{
    public class Background
    {
        private double speed = -10;
        private Canvas arena;
        List<OneShow> backgrounds = new List<OneShow>();

        public Background(string fileName, Canvas arena, double speed)
        {
            this.arena = arena;
            this.speed = -1 * speed;
            this.backgrounds.Add(new OneShow(0, arena, 1500, 875, fileName));
            this.backgrounds.Add(new OneShow(1500, arena, 1500, 875, fileName));
            this.backgrounds.Add(new OneShow(3000, arena, 1500, 875, fileName));
        }

        public double Speed
        {
            get 
            {
                return this.speed;
            }

            set
            {
                this.speed = -1 * value;
            }
        }

        internal void MoveBackground()
        {
            SetFirstBackground();

            foreach (OneShow show in backgrounds)
            {
                show.PlaceX += speed;
                Canvas.SetLeft(show.Image, show.PlaceX);
            }
        }

        private void SetFirstBackground()
        {
            OneShow show = backgrounds[0];

            if (show.PlaceX + speed <= -1500)
            {
                backgrounds.RemoveAt(0);
                backgrounds.Add(show);

                OneShow prev = backgrounds[1];
                show.PlaceX = prev.PlaceX + prev.Image.ActualWidth;
            }
        }
    }
}
