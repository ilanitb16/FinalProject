using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject.Classes
{
    class Heart : BaseClass
    {
        public Heart(double placeX, double placeY, Canvas arena) : base(placeX, placeY, arena, 100, 100)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/heart.gif")); // הקישור לתמונת הלב
        }
    }
}
