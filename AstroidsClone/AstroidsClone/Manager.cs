using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstroidsClone
{
    public class Manager
    {
        Graphics graphics;
        Ship ship;
        Asteroid[] asteroids;
        Drawer drawer;
        PointF canvasSize;
        Random rnd;

        public Manager(Graphics graphics, PointF canvasSize)
        {
            this.graphics = graphics;
            drawer = new Drawer(graphics);
            this.canvasSize = canvasSize;
            ship = new Ship(new PointF(canvasSize.X / 2, canvasSize.Y / 2));
            rnd = new Random();
            asteroids = new Asteroid[100];

            createAsteroids();            
        }

        public void Run()
        {
            ship.rotate();
            ship.Heading += ship.RotationAngle;
            Draw();
        }           

        public void Draw()
        {
            drawer.drawShip(ship.getShipDimensions());

            for (int i = 0; i < asteroids.Length; i++)
            {
                drawer.drawShip(asteroids[i].getShape());
            }
            
        }

        public void createAsteroids()
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                int newX = rnd.Next((int)canvasSize.X * 3);
                int newY = rnd.Next((int)canvasSize.Y * 3);
                Point newPoint = new Point(newX, newY);
                asteroids[i] = new Asteroid(newPoint, rnd);
            }            
        }

        public void Move()
        {
            PointF velocity = ship.Thrust();

            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].Move(velocity);
            }
        }

        public void RotateShip(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
            {
                ship.RotationAngle = -10;                
            }
            if (e.KeyCode == Keys.D)
            {
                ship.RotationAngle = 10;                
            }            
        }

        public void stopRotation()
        {
            ship.RotationAngle = 0;
        }
    }
}
