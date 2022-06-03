using DataBaseProject.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using Windows.Storage;

namespace DataBaseProject
{
    public static class DataBaseMethods
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path; // Specifies the path to the database
        private static string connectionString = "Filename=" + dbPath + "\\DBGame.db"; // מיקום הדטה בייס

       /// <summary>
       /// פעולה שמוציאה את המשתמש מהדטה בייס על בסיס סיסמה ושם משתמש
       /// </summary>
       /// <param name="username">השם משתמש</param>
       /// <param name="password">הסיסמה</param>
       /// <returns>היוזר עם הנתונים האלו</returns>
        public static User GetUser(string username, string password)
        {
            string query = $"SELECT * FROM [Users] WHERE UserName= '{username}' AND Password = '{password}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open(); //פתיחת החיבור
                SqliteCommand command = new SqliteCommand(query, connection); //הפקודה
                SqliteDataReader reader = command.ExecuteReader(); //ביצוע הפקודה
                if (reader.HasRows) //האם יש נתונים
                {
                    reader.Read(); // קריאת שורה אחת
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        Coins = reader.GetInt32(reader.GetOrdinal("Coins")),
                        CurrentCharacter = reader.GetInt32(4),
                        Score = reader.GetInt32(5),
                        Mail = reader.GetString(6),
                    };
                    return user;
                }
            }
            return null; // אם המשתמש לא קיים, נחזיר נאל 
        }
        /// <summary>
        /// פעולה מוסיפה מתשמש למאגר ומחזירה אותו אם התהליך צלח
        /// </summary>
        /// <param name="name">שם מתשמש</param>
        /// <param name="password">סיסמה</param>
        /// <param name="mail">כתובת מייל</param>
        /// <returns>המשתמש שנוסף</returns>
        public static User AddUser(string name, string password, string mail)
        {
            User user = GetUser(name, password);  //נסיון שליפת המשתמש במאגר
            if (user != null) // משמע המשתמש כבר קיים
                return null; //הגעת למקום הלא נכון, אתה משתמש קיים גש להתחברות
            string query = $"INSERT INTO [Users] (UserName, Password, Coins, CurrentCharacter, Score, Mail) VALUES ('{name}', '{password}', {0}, {1}, {0}, '{mail}')";
            Execute(query); // המשתמש החדש ברגע זה מתווסף למאגר המשתמשים  הקיימים
            user = GetUser(name, password); // קבלת המשתמש שהתווסף כרגע
            return (user);
        }
        /// <summary>
        ///query פעולה שמתחברת לדטה בייס ומוציאה לפועל פקודה שרשומה בתוך   
        /// </summary>
        /// <param name="query">הוראה לאופן פעולה במאגר נתונים</param>
        public static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
         /// <summary>
      /// מחזיר רשימה מלאה של כל הרכישות
      /// </summary>
      /// <param name="userId">איי די של היוזר</param>
      /// <returns> רשימה</returns>
        public static List<Purchase> GetPurchases(int userId)
        {
            List<Purchase> products = new List<Purchase>();
            string query = $"SELECT * FROM Purchases WHERE UserId = {userId} ORDER BY ProductSerialNumber";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Purchase purchase = new Purchase
                        {
                            UserId = reader.GetInt32(0),
                            ProductSeriaNumber = reader.GetInt32(1),
                            ProductType= reader.GetString(2),
                        };
                        products.Add(purchase);
                    }
                }
            }
            return products;
        }
        /// <summary>
        /// מבצע את הרכישה של המוצר המבוקש
        /// </summary>
        /// <param name="user">שם משתמש</param>
        /// <param name="id">מספר סידורי</param>
        /// <param name="product">מוצר</param>
        /// <param name="productType">סוג מוצר</param>
        public static void ExecutePurchase(User user, int id, int product, string productType)
        {
            string query;
            query = $"UPDATE [Users] SET Coins = {user.Coins} WHERE Id= {id}";
            Execute(query); // המשתמש החדש ברגע זה מתווסף למאגר המשתמשים  הקיימים
            query = $"INSERT INTO [Purchases] (UserId, ProductSerialNumber, ProductType ) VALUES ({id}, {product}, '{productType}')";
            Execute(query); // המשתמש החדש ברגע זה מתווסף למאגר המשתמשים  הקיימים
        }
       /// <summary>
       /// קבלת המשתמש לפי שם משתמש וכתובת מייל 
       /// </summary>
       /// <param name="userName">שם מתשמש</param>
       /// <param name="Mail">סיסמה</param>
       /// <returns>משתמש</returns>
        public static User GetUserForgotPassword(string userName, string Mail)
        {
            string query = $"SELECT * FROM [Users] WHERE UserName='{userName}' AND Mail='{Mail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        Coins = reader.GetInt32(3),
                        CurrentCharacter = reader.GetInt32(4),
                        Score = reader.GetInt32(5),
                        Mail = reader.GetString(6),
                    }; 
                    return user;
                }
                return null;  
            }
        }
        /// <summary>
        /// מקבל את המשתמש לפי שם משתמש וסיסמה
        /// </summary>
        /// <param name="userName">שם מתשמש</param>
        /// <param name="Password">סיסמה</param>
        /// <returns></returns>
        public static User GetUserChangePassword(string userName, string Password)
        {
            string query = $"SELECT * FROM [Users] WHERE UserName='{userName}' AND Password='{Password}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        Coins = reader.GetInt32(3),
                        CurrentCharacter = reader.GetInt32(4),
                        Score = reader.GetInt32(5),
                        Mail = reader.GetString(6),
                    }; return user;
                }
                return null;
            }
        }
       /// <summary>
       /// קבלת מערך של שלושת המשתמשים בעלי הניקוד הגבוה ביותר
       /// </summary>
       /// <returns></returns>
        public static User [] GetTopThree()
        {
            List<User> allUsers = new List<User>();
            string query = $"SELECT * FROM Users";
            using (SqliteConnection connection = new SqliteConnection(connectionString)) // קבלת כל המשתמשים מהדטה בייס
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User // הכנסת המשתמשים לתוך המערך 
                        {
                            Id = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            Coins = reader.GetInt32(3),
                            CurrentCharacter = reader.GetInt32(4),
                            Score = reader.GetInt32(5),
                            Mail = reader.GetString(6),
                        };
                        allUsers.Add(user);
                    }
                }
            }

            User [] TopThreeUsers = new User[3]; // יצירת מערך עם שלושה מקומות

            int maxscore = 0; // ניקוד מינימלי
            User helpUser = null; // משתנה עזר
            int index = 0; // משתנה עזר

            while(index < 3) // לולאה שרצה 3 פעמים על הרשימה ומכניסה למערך את שלושת המשתמשים בעלי הניקוד הכי גבוה
            {
                foreach (User user in allUsers)
                {
                    if (user.Score > maxscore) // אם הניקוד גבוהה מהניקוד המקסימלי
                    {
                        maxscore = user.Score;// שינוי ערך המקסימום
                        helpUser = user; // השמת ערך למשתנה עזר
                    }
                }

                TopThreeUsers[index] = helpUser; //הוספת המשתמש למערך

                allUsers.Remove(helpUser); // הסרת המשתמש מהרשימה
                helpUser = null; // משתנה עזר נאל
                maxscore = 0; // משתנה עזר 0
                index++;
            }
            return TopThreeUsers; // מחזיר מערך עם שלושה משתמשים בעלי ניקוד מקסימלי
        }
        /// <summary>
        /// פעולה שמעדכנת את המטבעות של המשתמש
        /// </summary>
        /// <param name="user">משתמש</param>

        public static void UpdateCoins( User user) 
        {
            string query = $"UPDATE [Users] SET Coins = {user.Coins} WHERE Id = {user.Id}" ;
            Execute(query);
        }
        /// <summary>
        /// פעולה שמעדכנת את כמות הניקוד של המשתמש בתום המשחק
        /// </summary>
        /// <param name="user">משתמש</param>
        public static void UpdateScore(User user)
        {
            string query = $"UPDATE [Users] SET Score = {user.Score} WHERE Id = {user.Id}";
            Execute(query);
        }
    } 

} 

        
  
