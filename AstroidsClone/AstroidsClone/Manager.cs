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
        bool running;
        int throttlePosition; // accelerating or decelerating

        public Manager(Graphics graphics, PointF canvasSize)
        {
            running = true;
            this.graphics = graphics;
            drawer = new Drawer(graphics);
            this.canvasSize = canvasSize;
            ship = new Ship(new PointF(canvasSize.X / 2, canvasSize.Y / 2));
            rnd = new Random();
            asteroids = new Asteroid[1];
            throttlePosition = 0;

            createAsteroids();            
        }

        public void createAsteroids()
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                int newX = rnd.Next((int)canvasSize.X *3);
                int newY = rnd.Next((int)canvasSize.Y *3);
                Point newPoint = new Point(newX, newY);
                asteroids[i] = new Asteroid(newPoint, rnd);
            }
        }

        public bool Run()
        {
            ship.rotate();
            
            Draw();
            Move();
            collisionDetection();

            if (running == true)
            {
                return true;
            }
            else
            {                
                return false;
            }            
        }           

        public void Draw()
        {
            drawer.drawShip(ship.getShipDimensions());

            for (int i = 0; i < asteroids.Length; i++)
            {
                drawer.drawAsteroid(asteroids[i].getShape());
            }            
        }        

        public void Move()
        {
            ship.AdjustSpeed(throttlePosition);
            PointF velocity = ship.Thrust();

            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].Move(velocity);
            }
        }

        public void Throtle(int accdec)
        {
            throttlePosition = accdec;
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

        public void collisionDetection()
        {
            PointF[] shipBounds = ship.getShipDimensions();

            for (int i = 0; i < asteroids.Length; i++)
            {                
                PointF center = asteroids[i].getCenter();
                PointF[] all = asteroids[i].getShape();
                int asteroidX = (int)center.X;
                int asteroidY = (int)center.Y;
                int asteroidRad = asteroids[i].Size;
                PointF[] shipDim = ship.getShipDimensions();
                int shipX = (int)shipDim[0].X;
                int shipY = (int)shipDim[0].Y;

                if (((asteroidX + asteroidRad > shipX) && (asteroidX - asteroidRad < shipX)) &&
                    ((asteroidY  + asteroidRad > shipY) && (asteroidY - asteroidRad < shipY)))
                {
                    running = false;
                }
            }
        }

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }
    }
}
