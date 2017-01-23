using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroidsClone
{
    public class Asteroid
    {
        Point currentLocation;
        float angle;
        PointF[] shape;
        int size; //radius of asteroid
        
        PointF vector;

        public Asteroid(Point startPoint, Random rnd)
        {
            currentLocation = startPoint;
            angle = (float)(10 * (Math.PI/180));
            shape = new PointF[36];
            size = 100;

            createAsteroid(rnd);
        }

        public void createAsteroid(Random rnd)
        {
            int rndX = rnd.Next(20) - 10;
            int rndY = rnd.Next(20) - 10;
            vector = new Point(rndX, rndY);   

            //first point of the shape
            float topX = currentLocation.X;
            float topY = currentLocation.Y - size;
            shape[0] = new PointF(topX, topY);

            for(int i = 1; i < shape.Length; i++)
            {
                //point that needs to rotate
                float x = shape[i - 1].X;
                float y = shape[i - 1].Y;
                //point to pivot around
                float pivotX = currentLocation.X;
                float pivotY = currentLocation.Y;

                //get a random offset for both x and y
                rndX = rnd.Next(20) - 10;
                rndY = rnd.Next(20) - 10;

                //create new point to be added
                float newX = (float)(pivotX + ((x - pivotX) * Math.Cos(angle)) - ((y - pivotY) * Math.Sin(angle))) - rndX;
                float newY = (float)(pivotY + ((x - pivotX) * Math.Sin(angle)) + ((y - pivotY) * Math.Cos(angle))) - rndY;

                PointF newPoint = new PointF(newX, newY);
                shape[i] = newPoint;
            }
        }

        public void Move()
        {
            for (int i = 0; i < shape.Length; i++)
            {
                shape[i].X += vector.X;
                shape[i].Y += vector.Y;
            }
        }

        public PointF[] getShape()
        {
            Move();
            return shape;
        }
    }
}
