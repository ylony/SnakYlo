using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Main
{
    class Snake
    {
        private List<Square> core = new List<Square>();
        private int nbPts = 0;
        public Snake(Point init)
        {
            Ellipse newE = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(255, 255, 255);
            newE.Fill = brush;
            newE.Width = 10;
            newE.Height = 10;
            Canvas.SetTop(newE, init.Y);
            Canvas.SetLeft(newE, init.X);
            core.Add(new Square(newE, init));
        }

        public Ellipse eatCandy()
        {
            Ellipse newE = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(255, 255, 255);
            newE.Fill = brush;
            newE.Width = 10;
            newE.Height = 10;
            Canvas.SetTop(newE, MainWindow.CurrentPos.Y); //Fix bug affichage en 0;0 quand mangé
            Canvas.SetLeft(newE, MainWindow.CurrentPos.X);
            core.Add(new Square(newE, MainWindow.CurrentPos));
            return core.ElementAt(core.Count - 1).one;
        }

        public List<Square> getCore()
        {
            return this.core;
        }

        public void addPts(int nbPts)
        {
            this.nbPts += nbPts;
        }

        public int getNbPts()
        {
            return this.nbPts;
        }
    }
}
