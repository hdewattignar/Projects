using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroidsClone
{
    public class Drawer
    {
        Graphics graphics;
        SolidBrush brush;
        Pen pen;
        Pen testPen;

        public Drawer(Graphics graphics)
        {
            this.graphics = graphics;                    
            brush = new SolidBrush(Color.White);
            pen = new Pen(brush);
            pen.Width = 2;
            testPen = new Pen(new SolidBrush(Color.Red));
            testPen.Width = 5;
        }

        public void drawShip(PointF[] points)
        {
            graphics.DrawPolygon(pen, points);            
        }

        public void drawAsteroid(PointF[] points)
        {
            points[0] = points[1];
            graphics.DrawPolygon(pen, points);
            graphics.DrawRectangle(testPen, points[0].X, points[0].Y, 1, 1);
            drawHitBox(points[0]);
        }

        public void drawHitBox(PointF center)
        {
            graphics.DrawEllipse(testPen, center.X - 100, center.Y - 100, 200, 200);
        }
    }
}
