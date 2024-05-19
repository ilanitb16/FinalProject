using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FinalProject.Classes
{
    // כל שינויי הדמויות והרקעים מתבצעים באמצעות קלאס גלובלי "גלובל דטה" שאליו נמסרים השמות של הדמויות והרקעים. 
    // גם הסאונד משתנה באמצעות הקלאס הזה
    class GlobalData
    {
        // קישור למצבים השונים של הדמות ברירת מחדל
        
        public static string CharacterRun = "ms-appx:///Assets/Characters/Gifs/Dinosaur/run-right.gif"; 
        public static string CharacterJump = "ms-appx:///Assets/Characters/Gifs/Dinosaur/Jump.gif";
        public static string CharacterIdle = "ms-appx:///Assets/Characters/Gifs/Dinosaur/idle-right.gif";
        public static string CharacterDead = "ms-appx:///Assets/Characters/Gifs/Dinosaur/dead.gif";

        public static string BackgroundFileName = "bg2.jpg"; // קישור לרקע ברירת מחדש

        public static bool sound = true; // הסאונד- אוטומטית  מופעל
    }
}
