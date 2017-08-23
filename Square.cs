using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Main
{
    class Square
    {
        public Ellipse one;
        public Point position;
        public Square(Ellipse one, Point position)
        {
            this.one = one;
            this.position = position;
        }
        public void updatePos(Point position)
        {
            this.position = position;
            Canvas.SetTop(one, position.Y);
            Canvas.SetLeft(one, position.X);
        }
    }
}
