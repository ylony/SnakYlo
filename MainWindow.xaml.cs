using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Main
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Snake snake;
        public static Point CurrentPos;
        private Candy candy = new Candy();
        public Random r = new Random();
        public DispatcherTimer timer;
        public TextBlock score;
        int game = 0; // 0 pas de jeu en cours 1 jeu en cours
        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        Direction move;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += GameArea_KeyDown;
            startGame();   
        }

        private void printScore()
        {
            score = new TextBlock();
            score.Text = snake.getNbPts().ToString();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(255, 255, 255);
            score.Foreground = brush;
            score.FontSize = 24;
            Canvas.SetTop(score, CurrentPos.Y - 20);
            Canvas.SetLeft(score, CurrentPos.X - 20);
            gameArea.Children.Add(score);
        }

        private void startGame()
        {
            gameArea.Children.Clear();
            move = Direction.DOWN;
            CurrentPos = new Point(Math.Round((gameArea.Width / 2 / 10)) * 10, Math.Round((gameArea.Height / 2 / 10)) * 10);
            this.snake = new Snake(CurrentPos);
            makeACandy();
            printCandy();
            printScore();
            gameArea.Children.Add(snake.getCore().ElementAt(0).one);
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 60);
            timer.Tick += new EventHandler(updateGame);
            timer.Start();
            game = 1;
        }

        private void GameArea_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Down && move != Direction.UP)
            {
                move = Direction.DOWN;
            }
            if (e.Key == Key.Left && move != Direction.RIGHT)
            {
                move = Direction.LEFT;
            }
            if (e.Key == Key.Right && move != Direction.LEFT)
            {
                move = Direction.RIGHT;
            }
            if (e.Key == Key.Up && move != Direction.DOWN)
            {
                move = Direction.UP;
            }
            if(e.Key == Key.Return && game == 0)
            {
                startGame();
            }
        }

        private void updateGame(object sender, EventArgs e)
        {
            this.paintSnake(CurrentPos);
            if(move == Direction.DOWN)
            {
                CurrentPos.Y += 10;
            }
            if (move == Direction.UP)
            {
                CurrentPos.Y -= 10;
            }
            if (move == Direction.RIGHT)
            {
                CurrentPos.X += 10;
            }
            if (move == Direction.LEFT)
            {
                CurrentPos.X -= 10;
            }
        }

        private void paintSnake(Point position)
        {
            if(position.X >= gameArea.Width - 20 || position.Y >= gameArea.Height - 10 || position.X < 0 || position.Y < 0)
            {
                gameOver();
            }
            check();
            int i = 0;
            int nbTrail = snake.getCore().Count;
            while (i < nbTrail)
            {             
                if(i + 1 == nbTrail)
                {       
                    Square one = snake.getCore().ElementAt(i);
                    one.updatePos(position);                    
                }
                else
                {
                    Square one = snake.getCore().ElementAt(i);
                    Square two = snake.getCore().ElementAt(i + 1);
                    one.updatePos(two.position);
                    two.updatePos(position);
                }
                i++;
            }
            if (position.X == candy.position.X && position.Y == candy.position.Y)
            {
                gameArea.Children.Add(snake.eatCandy());
                snake.addPts(10);
                score.Text = snake.getNbPts().ToString();
                printCandy();
            }
        }

        private void check()
        {
            int i = 1;
            int trail = snake.getCore().Count;
            while(i < trail)
            {
                if(CurrentPos.X == snake.getCore().ElementAt(i).position.X && CurrentPos.Y == snake.getCore().ElementAt(i).position.Y) //check qu'on se mange pas
                {
                    gameOver();
                }
                i++;
            }
        }

        private void makeACandy()
        {
            Ellipse candy = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(255, 0, 0);
            candy.Fill = brush;
            candy.Width = 10;
            candy.Height = 10;
            gameArea.Children.Add(candy);
            this.candy.eCandy = candy;
        }

        private void printCandy()
        {
            Point position = new Point();
            position.X = Math.Round((r.Next(0, 999999999) % (gameArea.Width - 20) / 10)) * 10;
            position.Y = Math.Round((r.Next(0, 999999999) % (gameArea.Height - 20) / 10)) * 10;
            int i = 0;
            int trail = snake.getCore().Count;
            while (i < trail)
            {
                if (position.X == snake.getCore().ElementAt(i).position.X && position.Y == snake.getCore().ElementAt(i).position.Y) //Pas de print candy sur la queue du snake
                {
                    position.X = Math.Round((r.Next(0, 999999999) % (gameArea.Width - 20) / 10)) * 10;
                    position.Y = Math.Round((r.Next(0, 999999999) % (gameArea.Height - 20) / 10)) * 10;
                    i = -1;
                }
                i++;
            }
            candy.position = position;
            Canvas.SetTop(candy.eCandy, position.Y);
            Canvas.SetLeft(candy.eCandy, position.X);                     
        }

        private void gameOver()
        {
            game = 0;        
            timer.Stop();
            MessageBox.Show("Fin de partie : " + snake.getNbPts() + " points.\n Appuyer sur entrée pour recommencer.");
        }
    }
}
