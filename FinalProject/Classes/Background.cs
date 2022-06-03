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
        List<OneShow> backgrounds = new List<OneShow>(); // רשימה שמכילה את הרקע ( שלוש פעמים) לצורך יצירת התנועה
        public Background(string fileName, Canvas arena, double speed) // יצירת רקע עם הנתונים שהוא צריך
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
        /// <summary>
        /// הזזת רקע
        /// </summary>
        internal void MoveBackground() // פעולה שאחראית על הזזת הרקע
        {
            SetFirstBackground(); // השמת הרקע לתוך הרשימה

            foreach (OneShow show in backgrounds) //  הזזת הרקעים 
            {
                show.PlaceX += speed;
                Canvas.SetLeft(show.Image, show.PlaceX);
            }
        }
        /// <summary>
        /// ברגע שאחד הרקעים יוצא מן המסך, רקע אחר מתחלף במקומו והוא זז לסוף הרשימה
        /// </summary>
        private void SetFirstBackground() 
        {
            OneShow show = backgrounds[0];

            if (show.PlaceX + speed <= -1500) // אם הרקע יצא מן המסך
            {
                backgrounds.RemoveAt(0); // הסרת הרקע מהרשימה
                backgrounds.Add(show); // הוספת הרקע לרשימה מחדש

                OneShow prev = backgrounds[1]; 
                show.PlaceX = prev.PlaceX + prev.Image.ActualWidth; // מיקום הרקע שיצא מהמסך ישר אחרי הרקע שכרגע נמצא במסך
            }
        }
    }
}
