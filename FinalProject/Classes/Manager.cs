using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FinalProject.Classes
{
    class Manager
    {
        private User user = null; // המשתמש שכרגע משחק במשחק
        public Canvas arena; //שם הקנבס
        public BaseCharacter character; //הדמות המרכזית שמופיעה על המסך
        public Background background; // קביעת הרקע של המשחק

        public BaseClass intersectObstacle; // משתנה שיכיל את המכשול שפגע בדמות, יאפשר מחיקה מהמסך והרשימה
        bool isIntersect = false; // משתנה שמסייע לדעת האם שני גופים נפגשים

        public List<BaseClass> coinlist = new List<BaseClass>(); // רשימה שתחיל את כל המטבעות
        public BaseClass removeCoin; // משתנה שיכיל את המכשול שיצא מהמסך, יאפשר מחיקתו מהרשימה
        public BaseClass intersectCoin; // משתנה שיכיל את המכשול שפגע בדמות, יאפשר מחיקה מהמסך והרשימה
        bool isIntersectCoin = false; // משתנה שעוזר לקבוע האם יש התנגשות בין מטבע לדמות

        private DispatcherTimer renderTimer; // טיימר שאחראי על יצירת מכשולים והוספתם לרשימה ואז למסך, כל פרק זמן רנדומלי
        private DispatcherTimer generatorTimer; // טיימר שמזיז את המכשולים אחד אחד על המסך 
        private DispatcherTimer heartTimer; // טיימר שאחראי על הזזת הלבבות שאפשר לתפוס

        List<BaseClass> obstacleList = new List<BaseClass>(); // רשימה שתחיל את כל המכשולים
       
        public TextBlock ScoreText; // טקסט בלוק שאחראי על שינוי הניקוד
        public TextBlock coinTextBox; // טקסט בלוק שאחראי על ערך המטבעות  
        public List<Heart> lives = new List<Heart>(); // רשימה שעוזרת למנות את החיים שנותרו  
        public List<Heart> AddLives = new List<Heart>(); //רשימת עזר- שתפקידה לשמור את הלבבות שניתנים לתפיסה

        DispatcherTimer backgroundTimer; // טיימר שאחראי על הזזת הרקע עצמו 

        int score = 0; // מונה הניקוד
        double speed = 10; // מהירות התחלתית 
        int coinCounter = 0; // מונה המטבעות
        int repeat = 2; // משתנה שמאפשר להוסיף רק עוד שני חיים במידת הצורך, כדי שהמשחק לא יימשך לנצח

        public MediaElement player; //פללייר
        public bool isGameOver = false; //האם המשחק נגמר
        public Manager(Canvas arena, User user, MediaElement player)
        {
            this.arena = arena;
            this.user = user;
            this.player = player;

            background = new Background(GlobalData.BackgroundFileName, arena, speed); //יצירת רקע

            this.character = new Character(0.3*arena.ActualWidth, 0.7*arena.ActualHeight, arena,150,150); //יצירת הדמות והצגתה על המסך
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; //בודקת האם נלחץ כפתור

            InitHearts(); // ממקמת את הלבבות במערך על המסך (שלושת הלבבות למעלה) ב

            ScoreText = new TextBlock(); // יוצרים את הבלוק שאחראי על ייצוג הניקוד
            ScoreText.Foreground = new SolidColorBrush(Colors.Black);
            ScoreText.FontSize = 25;
            ScoreText.Text = "Score: 0";
            arena.Children.Add(ScoreText);

            coinTextBox = new TextBlock(); // יוצרים את הבלוק שאחראי על ייצוג כמות המטבעות שברשות השחקן
            coinTextBox.Foreground = new SolidColorBrush(Colors.Black);
            coinTextBox.FontSize = 25;
            coinTextBox.Text = "Coins: 0";
            Canvas.SetLeft(coinTextBox, 100);
            arena.Children.Add(coinTextBox);

            this.renderTimer = new DispatcherTimer();
            this.renderTimer.Interval = TimeSpan.FromMilliseconds(5); // טיימר שיפעל כל 5 מילי שניות
            this.renderTimer.Tick += renderTimer_Tick;
            this.renderTimer.Start();

            this.generatorTimer = new DispatcherTimer();
            this.generatorTimer.Interval = TimeSpan.FromMilliseconds(5000); //טיימר שיפעל כל 5 שניות
            this.generatorTimer.Tick += generatorTimer_Tick;
            this.generatorTimer.Start();

            this.heartTimer = new DispatcherTimer();
            this.heartTimer.Interval = TimeSpan.FromMilliseconds(5); // טיימר שיפעל כל שנייה
            this.heartTimer.Tick += HeartTimer_Tick;
            this.heartTimer.Start();

            this.backgroundTimer = new DispatcherTimer();
            this.backgroundTimer.Start();
            this.backgroundTimer.Interval = TimeSpan.FromMilliseconds(15);
            this.backgroundTimer.Tick += BackgroundTimer_Tick;

            if (!GlobalData.sound)
                player.Stop();

        }

        //private  void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (e.Parameter != null && e.Parameter.ToString() != "")
        //    {
        //        this.user = (User)e.Parameter; // קבלת משתמש
        //    }
        //}
        private async void PlaySound(string FilePath)
        {
            player.Stop();
            MediaElement PlayMusic = new MediaElement();
            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(FilePath);
            PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            player.Play();
        }
        /// <summary>
        /// מוסיפה 3 לבבות לרשימה המייצגת את כמות החיים שברשותו של השחקן
        /// </summary>
        private void InitHearts() // מוסיפה 3 לבבות לרשימה המייצגת את כמות החיים שברשותו של השחקן
        {
            for (int i = 0; i < 3; i++)
            {
                Heart heart = new Heart(0, 0, arena); //הוספת הלבבות על המסך
                lives.Add(heart);
            }

            renderHearts();
        }
        private void renderHearts() // פעולה שאחראית על מיקום הלבבות שנתפסו למעלה על המסך
        {
            int i = 0;

            foreach (Heart heart in lives) //מיקום החיים הקיימים על המסך
            {
                heart.position.X = arena.ActualWidth * (0.93 - i * 0.05);
                heart.position.Y = arena.ActualHeight * 0.01;
                Canvas.SetTop(heart.image, heart.position.Y);
                Canvas.SetLeft(heart.image, heart.position.X);
                i++;
            }
        }
        /// <summary>
        /// /טיימר שאחראי על הזזת הלבבות הניתנים לתפיסה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeartTimer_Tick(object sender, object e) //טיימר שאחראי על הזזת הלבבות הניתנים לתפיסה
        {
            Heart removeHeart = null; // הלב שנסיר ממערך העזר ונכניס למערך החיים הקיימים במידה וייתפס

            foreach (Heart heart in AddLives) // מזיזים את הלב שניתן לתפוס על המסך, אם הלב נתפס מוסיפים אותו לרשימת החיים 
            {
                heart.position.X -= speed; // מקנים ללב מהירות, כך שהלב יזוז יחד הם הרקע
                Canvas.SetLeft(heart.image, heart.position.X);
                Canvas.SetTop(heart.image, heart.position.Y);

                if (IntersectsWith(character, heart)) // אם יש פגישה בין הלב לדמות
                {
                    removeHeart = heart;  //השמה
                }
                else
                {
                    if (heart.position.X + heart.size.Width < 0) // אם הלב יצא מן המסך ולא נתפס
                    {
                        arena.Children.Remove(heart.image); // מסירים את הלב מן המסך
                        AddLives.Remove(heart); // הסרת הלב ממערך העזר, הוא לא נתפס על ידי הדמות
                        break;
                    }
                }
            }

            if (removeHeart != null) // אם הלב נתפס
            {
                arena.Children.Remove(removeHeart.image); // מסירים את הלב מן המסך
                AddLives.Remove(removeHeart); // מסירים אותו מן המערך שמסייע להוסיף חיים
                lives.Add(new Heart(0, 0, arena)); // מוסיפים למעלה את הלב החדש
                renderHearts(); //ממקמים את הלב במקום הנכון
            }
        }
        private async void LivesControl(BaseClass obstacle, BaseCharacter character) // מבצע בדיקה על כמות החיים שנותרה ואם השחקן חורג ממנה, המשחק נגמר
        {
            Heart heart = lives[0];
            arena.Children.Remove(heart.image);
            lives.Remove(heart);
            arena.Children.Remove(obstacle.image);
            obstacleList.Remove(obstacle);
            renderHearts();

            if(lives.Count == 0)
            {
                GameOver();
                isGameOver = true;

                if(GlobalData.sound)
                    PlaySound("loser.m4a");

                StackPanel panel = new StackPanel // פאנל שיכיל את הכל
                {
                    Width = 200
                };
                panel.Orientation = Orientation.Vertical;

                ContentDialog EndGamePopUp = new ContentDialog() 
                {
                    Title = "GAME OVER!!",
                    Background = new SolidColorBrush(Colors.Gray),
                    Content=panel,
                    Width = 400,
                    PrimaryButtonText = "Ok",
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontFamily = new FontFamily("Times New Roman"),
                };
                await EndGamePopUp.ShowAsync();
            }
        }
        /// <summary>
        /// בצע בדיקה על כמות החיים שנותרה ואם השחקן חורג ממנה, המשחק נגמר
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generatorTimer_Tick(object sender, object e) //פעולה שיוצרת מכשול ומוסיפה אותו לרשימת המכשולים
        {
            string bg; // מחרוזת שתאפשר בחירת רקע רנדומלי ממבחר המכשולים האפשריים
            Random rand = new Random();
            int nextTimeout = rand.Next(3000, 3500); // רנדום שיגדיר את משך הזמן בין מכשול למכשול

            Random bgRandom = new Random();
            if (bgRandom.Next(1, 3) == 1) //רנדום הקובע איזה מכשול יבחר
                bg = "sign";
            else
                bg = "Wall"; 

            double positionX = rand.Next(Convert.ToInt32(0.6 * arena.ActualWidth), Convert.ToInt32(0.8 * arena.ActualWidth)); // הצבה רנדומלית של המכשול על המסך בטווח מיקומים הנתון
            double positionY = rand.Next(Convert.ToInt32(0.4 * arena.ActualHeight), Convert.ToInt32(0.5 * arena.ActualHeight)); // הצבה רנדומלית של המכשול על המסך בטווח מיקומים הנתון

            ObstacleShape obstacle = new ObstacleShape(arena, bg); // הגדרת המכשול  
            Coin coin = new Coin(positionX, positionY, arena, 50, 50); // הגדרת המכשול  
            obstacleList.Add(obstacle); // הוספת המכשול לרשימת מכשולים
            coinlist.Add(coin);

 
            if (lives.Count == 1) // ברגע שנותר חיים אחד בלבד, נוספת הזדמנות לתפוס לב חדש
            {
                if (repeat > 0)
                {
                    Heart heart = new Heart(arena.ActualWidth * 0.6, 0.5 * arena.ActualHeight, arena);
                    AddLives.Add(heart); // מוסיפים לרשימת עזר- שתפקידה לשמור את הלבבות שניתנים לתפיסה, לב נוסף
                    repeat--;
                }
            }            

            // הוספת המכשול על המסך מתבצעת על ידי הפעולה שנמצאת בתוך הקלאס

            this.generatorTimer.Interval = TimeSpan.FromMilliseconds(nextTimeout); // הפעלת הטיימר מחדש
        }
        /// <summary>
        /// הפעולה עוברת על מערך המכשולים ומזיזה אותם על המסך
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderTimer_Tick(object sender, object e) //הפעולה עוברת על מערך המכשולים ומזיזה אותם על המסך
        {
            BaseClass removeObstacle = null; 

            foreach (BaseClass obstacle in obstacleList) // כל מכשול בתוך רשימת המכשולים מוזז בהתאם למהירות של הרקע
            {
                obstacle.position.X-=speed;
                Canvas.SetLeft(obstacle.image, obstacle.position.X);
                Canvas.SetTop(obstacle.image, obstacle.position.Y);

                if (obstacle.position.X + obstacle.size.Width < 0) //אם המכשול יצא מן המסך
                    removeObstacle = obstacle; // הצבת המשתנה שיימחק בתוך משתנה עזר, כדי שנוכל למחוק אותו אחרי הלולאה אחרת הקוד נופל

                if (IntersectsWith(character, obstacle)) 
                {
                    isIntersect = true;
                    intersectObstacle = obstacle;
                    obstacle.CountJump = false;
                }
                else
                {
                    if (!isIntersect && obstacle.position.X*2 + obstacle.size.Width < character.position.X + character.size.Width && obstacle.CountJump) // אם התרחש מפגש
                    {
                        if (obstacle.GetBackground() == "sign") // מתווסף ניקוד בהתאם
                            score += 10;
                        if (obstacle.GetBackground() == "Wall")
                            score += 5;
                        ScoreText.Text = "score: " + score;
                        obstacle.CountJump = false;

                        if (score >= 20 && score < 40 && background.Speed != 15) // אם הניקוד בטווחים האלו, נוספת מהירות
                        {
                            speed = 15;
                            background.Speed = speed;
                        }

                        if (score >= 40 && background.Speed != 25) // אם השחקן צבר עוד יותר ניקוד המהירות תגדל עוד יותר (קשה יותר)
                        {
                            speed = 25;
                            background.Speed = speed;
                        }
                    }
                }
            }

            obstacleList.Remove(removeObstacle); // מחיקת המכשול מהרשימה לאחר שיצא מהמסך

            if(isIntersect) //אם יש פגיעה- כלומר אם המכשול והדמות נפגשו
                LivesControl(intersectObstacle, character); 

            isIntersect = false;

            foreach (BaseClass coin in coinlist) //כל מטבע בתוך רשימת המטבעות זז בהתאם למהירות של הרקע
            {
                coin.position.X -= speed;
                Canvas.SetLeft(coin.image, coin.position.X); 
                Canvas.SetTop(coin.image, coin.position.Y);

                if (coin.position.X + coin.size.Width < 0) //אם המטבע יצא מהמסך
                    removeCoin = coin; // הצבת המשתנה שיימחק בתוך משתנה עזר, כדי שנוכל למחוק אותו אחרי הלולאה אחרת הקוד נופל

                if (IntersectsWith(character, coin)) // אם יש חיתוך בין הדמות למטבע
                {
                    intersectCoin = coin; // השמת המטבע שאיתו יש חיתוך, על מנת שנוכל לבצע עליו מניפולציות בהמשך
                    isIntersectCoin = true; // יש פגיעה
                    coin.CountJump = false;
                    coinCounter += 1;
                    coinTextBox.Text = "coins: " + coinCounter;
                    user.Coins++;
                }
            }

            coinlist.Remove(removeCoin); // הסרת המטבע מרשימת המטבעות

            if (isIntersectCoin) // אם אכן יש חיתוך
            {
                arena.Children.Remove(intersectCoin.image); // הסרת המטבע מהמסך
                coinlist.Remove(intersectCoin); // הסרת המטבע מהרשימה
                isIntersectCoin = false; // השמת הערך חזרה לערך שקר
            }
        }
        private void BackgroundTimer_Tick(object sender, object e)
        {

            background.MoveBackground(); // פעולה פנימית שקימת לקלאס מסוג רקע

        }
        /// <summary>
        ///  The function checks if two objects from type Base Class intersect.
        ///  פעולה בודקת אם שני גופים נפגשים
        /// </summary>
        /// <param name="character"> the main character</param>
        /// <param name="object2">obstacle</param>
        /// <returns></returns>
        private bool IntersectsWith(BaseClass character, BaseClass object2) // אובייקט אחד דינוזאור, אובייקט שתיים מכשול
        {
            Size s2 = object2.size; // גודל המכשול
            Point p2 = object2.position; //מיקום המכשול

            if (character.position.X + character.size.Width  < p2.X || character.position.X > p2.X + s2.Width) // אם קצוות הדמות בציר האיקס לא נמצאות בטווח של המכשול
                return false;

            if (character.position.Y < p2.Y - s2.Height || character.position.Y - character.size.Height > p2.Y) // האם קצוות הדמות בציר הוואי אינן נמצאות בטווח המכשול
                return false;

            return true; // אם קצוות הדמות פוגעות בקצוות המכשול
        }
        /// <summary>
        /// פעולה בודקת האם נלחץ כפתור הרווח
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args) // פעולה המאפשרת את קפיצת הדמות
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Up)
            {
                this.character.Jump();
            }
        }
        /// <summary>
        /// פעולה שאחראיצ על סיום המשחק
        /// </summary>
        public void GameOver()
        {
            DataBaseMethods.UpdateCoins(user);
            user.Score += score;
            DataBaseMethods.UpdateScore(user);

            this.character.Die();
            this.character.moveTimer.Stop();

            this.heartTimer.Stop();
            this.renderTimer.Stop();
            this.generatorTimer.Stop();
            this.backgroundTimer.Stop();
            this.background.Speed = 0;
            player.Stop();
        }
        /// <summary>
        /// מחזיר את המשתמש חזרה לדף הראשי
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
