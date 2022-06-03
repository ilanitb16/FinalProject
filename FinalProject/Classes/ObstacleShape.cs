using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace FinalProject.Classes
{
    public class ObstacleShape: BaseClass
    {
        public ObstacleShape(Canvas arena, string bg) : base(1500, arena.ActualHeight*0.7, arena, 100, 150)
        {
            if (bg == "sign")
                image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri("ms-appx:///Assets/Obstacle/sign.png"));
            if (bg == "Wall")
                image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri("ms-appx:///Assets/Obstacle/WhiteWall.png"));
            
            base.type = bg;  // סוג הדמות: מכשול
        }
    }
}
