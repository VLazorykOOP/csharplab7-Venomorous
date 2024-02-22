using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7CSharp
{
    public abstract class Figure
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

        public Figure(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public abstract void Draw(Graphics g);
    }

    public class RectangleFigure : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public RectangleFigure(int x, int y, Color color, int width, int height)
            : base(x, y, color)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(Graphics g)
        {
            // Draw a rectangle
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, X, Y, Width, Height);
            }
        }
    }

    public class Pentagon : Figure
    {
        public int Side { get; set; }

        public Pentagon(int x, int y, int side, Color color)
            : base(x, y, color)
        {
            Side = side;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = GetPentagonPoints();
            g.FillPolygon(brush, points);
            brush.Dispose();
        }

        private Point[] GetPentagonPoints()
        {
            Point[] points = new Point[5];
            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI / 5 * i;
                int x = (int)(X + Side * Math.Cos(angle));
                int y = (int)(Y + Side * Math.Sin(angle));
                points[i] = new Point(x, y);
            }
            return points;
        }
    }

    // Triangle class
    public class Triangle : Figure
    {
        public int SideA { get; set; }
        public int SideB { get; set; }

        public Triangle(int x, int y, Color color, int sideA, int sideB)
            : base(x, y, color)
        {
            SideA = sideA;
            SideB = sideB;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = GetTrianglePoints();
            g.FillPolygon(brush, points);
            brush.Dispose();
        }

        private Point[] GetTrianglePoints()
        {
            Point[] points = new Point[3];
            points[0] = new Point(X, Y);
            points[1] = new Point(X + SideA, Y);
            points[2] = new Point(X, Y + SideB);
            return points;
        }
    }

    // Rhombus class
    public class Rhombus : Figure
    {
        public int Diagonal1 { get; set; }
        public int Diagonal2 { get; set; }

        public Rhombus(int x, int y, Color color, int diagonal1, int diagonal2)
            : base(x, y, color)
        {
            Diagonal1 = diagonal1;
            Diagonal2 = diagonal2;
        }

        public override void Draw(Graphics g)
        {
            // Draw a rhombus based on the X, Y coordinates, Diagonal1, Diagonal2, and other properties
            // Adapt this method based on your drawing logic
            // Example:
            Brush brush = new SolidBrush(Color);
            Point[] points = GetRhombusPoints();
            g.FillPolygon(brush, points);
            brush.Dispose();
        }

        private Point[] GetRhombusPoints()
        {
            Point[] points = new Point[4];
            // Calculate the coordinates of the vertices based on X, Y, Diagonal1, Diagonal2
            // Example:
            points[0] = new Point(X, Y);
            points[1] = new Point(X + Diagonal1 / 2, Y + Diagonal2 / 2);
            points[2] = new Point(X + Diagonal1, Y);
            points[3] = new Point(X + Diagonal1 / 2, Y - Diagonal2 / 2);
            return points;
        }
    }
}
