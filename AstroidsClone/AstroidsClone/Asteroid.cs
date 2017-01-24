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
        PointF currentLocation;        //middle of the asteroid
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
            int rndX = rnd.Next(-5,10);
            int rndY = rnd.Next(-5,10);
            vector = new Point(rndX, rndY);
            shape[0] = currentLocation;

            //first point of the shape            
            float topX = shape[0].X;
            float topY = shape[0].Y + size;            
            shape[1] = new PointF(topX, topY);

            for(int i = 2; i < shape.Length; i++)
            {
                //point that needs to rotate
                float x = shape[i - 1].X;
                float y = shape[i - 1].Y;
                //point to pivot around
                float pivotX = shape[0].X;
                float pivotY = shape[0].Y;

                //get a random offset for both x and y
                rndX = 0;//rnd.Next(-10,10);
                rndY = 0;//rnd.Next(-10,10);

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
            //CurrentLocation = shape[0];
        }        

        public void Move(PointF thrust)
        {
            for (int i = 0; i < shape.Length; i++)
            {
                shape[i].X += (vector.X + thrust.X);
                shape[i].Y += (vector.Y + thrust.Y);
            }
            //CurrentLocation = shape[0];
        }        

        public PointF[] getShape()
        {            
            return shape;
        }

        public PointF getCenter()
        {
            return shape[0];
        }

        public PointF CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
