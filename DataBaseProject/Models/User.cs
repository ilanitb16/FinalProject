using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class User
    {
        public int Id { get; set; } //מספר סידורי
        public string UserName { get; set; } // שם משתמש
        public string Password { get; set; } // סיסמה
        public int CurrentCharacter { get; set; } // דמות בשימוש כעת
        public int Coins { get; set; } // מטבעות
        public int Score { get; set; } // ניקוד
        public string Mail { get; set; } // כתובת מייל
        public int Backgrounds { get; set; } // רקעים
        /// <summary>
        /// מחיזרה את המספר סידורי, מטבעות, מייל וניקוד שיש לו
        /// </summary>
        /// <returns> תכונות בסטרינג</returns>
        public override string ToString() 
        {
            return $"Id: {Id} \br Coins: {Coins} \br Score: {Score} \br Mail: {Mail}";
        }
    }
    
}
