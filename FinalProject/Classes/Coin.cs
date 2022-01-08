using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace FinalProject.Classes
{
    class Coin:BaseClass
    {
        public List<BaseClass> coinlist = new List<BaseClass>(); // רשימה שתחיל את כל המטבעות


        Random random = new Random(); // רנדום שיגריל את מיקום המטבע על המסך
        
        public Coin(double placeX, double placeY, Canvas arena,
            double width, double height) : base(placeX, placeY, arena, width, height)
        {
            this.image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Coin.gif"));
        }
    }
}
