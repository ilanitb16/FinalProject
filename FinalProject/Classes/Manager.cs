using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FinalProject.Classes
{
    class Manager
    {
        public Canvas arena; //שם הקנבס
        public BaseCharacter character; //הדמות המרכזית שמופיעה על המסך
        public Background background;

        //public Background background2;
        //public Background background3;
        public BaseClass intersectObstacle; // משתנה שיכיל את המכשול שפגע בדמות, יאפשר מחיקה מהמסך והרשימה
        bool isIntersect = false;

        public List<BaseClass> coinlist = new List<BaseClass>(); // רשימה שתחיל את כל המטבעות
        public BaseClass removeCoin; // משתנה שיכיל את המכשול שיצא מהמסך, יאפשר מחיקתו מהרשימה
        public BaseClass intersectCoin; // משתנה שיכיל את המכשול שפגע בדמות, יאפשר מחיקה מהמסך והרשימה
        bool isIntersectCoin = false;

        private DispatcherTimer renderTimer; // טיימר שאחראי על יצירת מכשולים והוספתם לרשימה ואז למסך, כל פרק זמן רנדומלי
        private DispatcherTimer generatorTimer; // טיימר שמזיז את המכשולים אחד אחד על המסך 
        private DispatcherTimer heartTimer;

        List<BaseClass> obstacleList = new List<BaseClass>(); // רשימה שתחיל את כל המכשולים
       
        public TextBlock tx;
        public TextBlock coinTextBox; 
        public List<Heart> lives = new List<Heart>();
        public List<Heart> AddLives = new List<Heart>();

        DispatcherTimer backgroundTimer;


        int score = 0;
        double speed = 10;
        int coinCounter = 0;

        public Manager(Canvas arena)
        {
            this.arena = arena;
            background = new Background(GlobalData.BackgroundFileName, arena, speed);

            this.character = new Dinosaur(0.3*arena.ActualWidth, 0.7*arena.ActualHeight, arena,150,150); //יצירת הדמות והצגתה על המסך
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; //בודקת האם נלחץ כפתור

            InitHearts();

            tx = new TextBlock();
            tx.Foreground = new SolidColorBrush(Colors.Black);
            tx.FontSize = 25;
            tx.Text = "Score: 0";
            arena.Children.Add(tx);

            coinTextBox = new TextBlock();
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

            if(GlobalData.sound)
                PlaySound("kahoot.mp3");
            
        }

        private async void PlaySound(string FilePath)
        {
            MediaElement PlayMusic = new MediaElement();
            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync("Assets");
            StorageFile sf = await Folder.GetFileAsync(FilePath);
            PlayMusic.SetSource(await sf.OpenAsync(FileAccessMode.Read), sf.ContentType);
            PlayMusic.Play();
        }

        private void InitHearts()
        {
            for (int i = 0; i < 3; i++)
            {
                Heart heart = new Heart(0, 0, arena); //הוספת הלבבות על המסך
                lives.Add(heart);
            }

            renderHearts();
        }


        private void renderHearts()
        {
            int i = 0;

            foreach (Heart heart in lives)
            {
                heart.position.X = arena.ActualWidth * (0.93 - i * 0.05);
                heart.position.Y = arena.ActualHeight * 0.01;
                Canvas.SetTop(heart.image, heart.position.Y);
                Canvas.SetLeft(heart.image, heart.position.X);
                i++;
            }
        }

        private void HeartTimer_Tick(object sender, object e)
        {
            Heart removeHeart = null;

            foreach (Heart heart in AddLives)
            {
                heart.position.X -= speed;
                Canvas.SetLeft(heart.image, heart.position.X);
                Canvas.SetTop(heart.image, heart.position.Y);

                if (IntersectsWith(character, heart))
                {
                    removeHeart = heart;
                }
                else
                {
                    if (heart.position.X + heart.size.Width < 0)
                    {
                        removeHeart = heart; // הצבת המשתנה שיימחק בתוך משתנה עזר, כדי שנוכל למחוק אותו אחרי הלולאה אחרת הקוד נופל
                        break;
                    }
                }
            }

            if (removeHeart != null)
            {
                arena.Children.Remove(removeHeart.image);
                AddLives.Remove(removeHeart);
                lives.Add(new Heart(0, 0, arena));
                renderHearts();
            }
        }

        private void LivesControl(BaseClass obstacle, BaseCharacter character) // מבצע בדיקה על כמות החיים שנותרה ואם השחקן חורג ממנה, המשחק נגמר
        {
            Heart heart = lives[0];
            arena.Children.Remove(heart.image);
            lives.Remove(heart);
            arena.Children.Remove(obstacle.image);
            obstacleList.Remove(obstacle);
            renderHearts();

            if(lives.Count == 0)
            {
                this.character.Die();
                this.character.moveTimer.Stop();

                this.heartTimer.Stop();
                this.renderTimer.Stop();
                this.generatorTimer.Stop();
                this.background.Speed = 0;
            }
        }

        private void generatorTimer_Tick(object sender, object e) //פעולה שיוצרת מכשול ומוסיפה אותו לרשימת המכשולים
        {
            string bg; // מחרוזת שתאפשר בחירת רקע רנדומלי ממבחר המכשולים האפשריים
            Random rand = new Random();
            int nextTimeout = rand.Next(3000, 8000); // רנדום שיגדיר את משך הזמן בין מכשול למכשול

            Random bgRandom = new Random();
            if (bgRandom.Next(1, 3) == 1) //רנדום הקובע איזה מכשול יבחר
                bg = "sign";
            else
                bg = "Wall";

            double positionX = rand.Next(Convert.ToInt32(0.6 * arena.ActualWidth), Convert.ToInt32(0.8 * arena.ActualWidth));
            double positionY = rand.Next(Convert.ToInt32(0.4 * arena.ActualHeight), Convert.ToInt32(0.5 * arena.ActualHeight));

            ObstacleShape obstacle = new ObstacleShape(arena, bg); // הגדרת המכשול  
            Coin coin = new Coin(positionX, positionY, arena, 50, 50); // הגדרת המכשול  
            obstacleList.Add(obstacle); // הוספת המכשול לרשימת מכשולים
            coinlist.Add(coin);

            if(lives.Count == 1)
            {
                Heart heart = new Heart(arena.ActualWidth * 0.6, 0.5 * arena.ActualHeight, arena);
                AddLives.Add(heart);
            }

            // הוספת המכשול על המסך מתבצעת על ידי הפעולה שנמצאת בתוך הקלאס

            this.generatorTimer.Interval = TimeSpan.FromMilliseconds(nextTimeout); // הפעלת הטיימר מחדש


        }
        private void renderTimer_Tick(object sender, object e) //הפעולה עוברת על מערך המכשולים ומזיזה אותם על המסך
        {
            BaseClass removeObstacle = null;

            foreach (BaseClass obstacle in obstacleList)
            {
                obstacle.position.X-=speed;
                Canvas.SetLeft(obstacle.image, obstacle.position.X);
                Canvas.SetTop(obstacle.image, obstacle.position.Y);

                if (obstacle.position.X + obstacle.size.Width < 0)
                    removeObstacle = obstacle; // הצבת המשתנה שיימחק בתוך משתנה עזר, כדי שנוכל למחוק אותו אחרי הלולאה אחרת הקוד נופל

                if (IntersectsWith(character, obstacle))
                {
                    isIntersect = true;
                    intersectObstacle = obstacle;
                    obstacle.CountJump = false;
                }

                if (!isIntersect && obstacle.position.X + obstacle.size.Width<character.position.X+character.size.Width && obstacle.CountJump)
                {
                    if (obstacle.GetBackground() == "sign")
                        score += 10;
                    if (obstacle.GetBackground() == "Wall")
                        score += 5;
                    tx.Text = "score: " + score;
                    obstacle.CountJump = false;

                    if (score >= 20 && score < 40 && background.Speed != 15)
                    {
                        speed = 15;
                        background.Speed = speed;
                    }

                    if (score >= 40 && background.Speed !=  25)
                    {
                        speed = 25;
                        background.Speed = speed;
                    }
                }
            }

            obstacleList.Remove(removeObstacle); // מחיקת המכשול מהרשימה לאחר שיצא מהמסך

            if(isIntersect)
                LivesControl(intersectObstacle, character);

            isIntersect = false;

            foreach (BaseClass coin in coinlist)
            {
                coin.position.X -= speed;
                Canvas.SetLeft(coin.image, coin.position.X); //test git
                Canvas.SetTop(coin.image, coin.position.Y);

                if (coin.position.X + coin.size.Width < 0)
                    removeCoin = coin; // הצבת המשתנה שיימחק בתוך משתנה עזר, כדי שנוכל למחוק אותו אחרי הלולאה אחרת הקוד נופל

                if (IntersectsWith(character, coin))
                {
                    intersectCoin = coin;
                    isIntersectCoin = true;
                }

                if(isIntersectCoin && coin.position.X + coin.size.Width < character.position.X + character.size.Width && coin.CountJump)
                {
                    coin.CountJump = false;
                    coinCounter += 1;
                    coinTextBox.Text = "coins: " + coinCounter;
                }
            }

            coinlist.Remove(removeCoin);
            if (isIntersectCoin)
            {
                arena.Children.Remove(intersectCoin.image);
                coinlist.Remove(intersectCoin);
                isIntersectCoin = false;
            }
        }

        private void BackgroundTimer_Tick(object sender, object e)
        {

            background.MoveBackground();

        }

        /// <summary>
        ///  The function checks if two objects from type Base Class intersect.
        /// </summary>
        /// <param name="character"> character</param>
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
        /// הפעולה מגרילה מספר רנדומלי של שניות בין 3 ל7, ויוצרת מכשול חדש על המסך כעבור כמות השניות שהוגרלה
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
    }
}
