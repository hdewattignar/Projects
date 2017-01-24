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

        public Drawer(Graphics graphics)
        {
            this.graphics = graphics;                    
            brush = new SolidBrush(Color.White);
            pen = new Pen(brush);
            pen.Width = 2;
        }

        public void drawShip(PointF[] points)
        {
            graphics.DrawPolygon(pen, points);            
        }

        public void drawAsteroid(PointF[] points)
        {
            points[0] = points[1];
            graphics.DrawPolygon(pen, points);
        }        
    }
}
