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
        Drawer drawer;
        Random rnd;

        Ship ship;
        int throttlePosition = 0; // accelerating or decelerating

        List<Asteroid> asteroids;
        int asteroidCount = 200;        
        
        PointF canvasSize;
        PointF bounds;

        bool running = true;
        

        public Manager(Graphics graphics, PointF canvasSize)
        {
            this.graphics = graphics;
            drawer = new Drawer(graphics);
            rnd = new Random();

            this.canvasSize = canvasSize;
            bounds = new PointF(canvasSize.X * 5, canvasSize.Y * 5);
            ship = new Ship(new PointF(canvasSize.X / 2, canvasSize.Y / 2));                     
            
            asteroids = new List<Asteroid>();            

            createAsteroids();            
        }

        public void createAsteroids()
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                int newX = rnd.Next((int)bounds.X);
                int newY = rnd.Next((int)bounds.Y);
                Point newPoint = new Point(newX, newY);
                asteroids.Add(new Asteroid(newPoint, rnd));
            }
        }

        public void createSingleAsteroid()
        {
            int newX = rnd.Next((int)bounds.X);
            int newY = rnd.Next((int)bounds.Y);
            Point newPoint = new Point(newX, newY);
            asteroids.Add(new Asteroid(newPoint, rnd));
        }

        public bool Run()
        {
            ship.rotate();
            Move();
            collisionDetection();
            Draw();

            if (running == true)
            {
                return true;
            }
                        
            return false;            
        }           

        public void Draw()
        {
            drawer.drawShip(ship.getShipDimensions());

            for (int i = 0; i < asteroids.Count; i++)
            {
                if (boundsDetection(i))
                {
                    drawer.drawAsteroid(asteroids.ElementAt(i).getShape());
                }                
            }          
  
            if(asteroids.Count < asteroidCount)
            {
                createSingleAsteroid();
            }
        }        

        public void Move()
        {
            ship.AdjustSpeed(throttlePosition);
            PointF velocity = ship.Thrust();

            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids.ElementAt(i).Move(velocity);
            }
        }

        public void Throttle(int accdec)
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

            for (int i = 0; i < asteroids.Count; i++)
            {
                PointF center = asteroids.ElementAt(i).getCenter();
                PointF[] all = asteroids.ElementAt(i).getShape();
                int asteroidX = (int)center.X;
                int asteroidY = (int)center.Y;
                int asteroidRad = asteroids.ElementAt(i).Size;
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

        //decides if an asteroid should be draw
        //this should lower draw time
        public bool boundsDetection(int currentAsteroidIndex)
        {
            //ship location
            Point currentShipLocation = ship.getShortLocation();
            int x1 = currentShipLocation.X;
            int y1 = currentShipLocation.Y;
            int d = 3000;

            int x2 = (int)asteroids.ElementAt(currentAsteroidIndex).CurrentLocation.X;
            int y2 = (int)asteroids.ElementAt(currentAsteroidIndex).CurrentLocation.Y;

            double distance = Math.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));            

            if (distance < d)
            {
                return true;
            }

            return false;
        }

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }
    }
}
