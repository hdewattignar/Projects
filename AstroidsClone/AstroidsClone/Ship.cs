using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroidsClone
{
    public class Ship
    {
        PointF[] currentLocation;
        float rotationAngle;

        public Ship(PointF startPoint)
        {
            rotationAngle = (float)(5 * (Math.PI/180));

            currentLocation = new PointF[4];
            currentLocation[0] = startPoint;

            float brX = startPoint.X + 30;
            float brY = startPoint.Y + 50;
            PointF bottomRight = new PointF(brX, brY);

            float bmX = startPoint.X;
            float bmY = startPoint.Y + 40;
            PointF bottomMiddle = new PointF(bmX, bmY);

            float blX = startPoint.X - 30;
            float blY = startPoint.Y + 50;
            PointF bottomLeft = new PointF(blX, blY);

            currentLocation[1] = bottomRight;
            currentLocation[2] = bottomMiddle;
            currentLocation[3] = bottomLeft;
        }

        /// <summary>
        /// returns an array of pointF. Used for drawing to the canvas
        /// </summary>
        /// <returns></returns>
        public PointF[] getShipDimensions()
        {
            return currentLocation;
        }

        public void rotateRight()
        {
            for (int i = 1; i < currentLocation.Count(); i++)
            {
                float x = currentLocation[i].X;
                float y = currentLocation[i].Y;
                float startX = currentLocation[0].X;
                float startY = currentLocation[0].Y;

                currentLocation[i].X = (float)(startX + ((x - startX) * Math.Cos(-rotationAngle)) - ((y - startY) * Math.Sin(-rotationAngle)));
                currentLocation[i].Y = (float)(startY + ((x - startX) * Math.Sin(-rotationAngle)) + ((y - startY) * Math.Cos(-rotationAngle)));

            }
        }

        public void rotateLeft()
        {            
            for (int i = 1; i < currentLocation.Count(); i++)
            {
                float x = currentLocation[i].X;
                float y = currentLocation[i].Y;
                float startX = currentLocation[0].X;
                float startY = currentLocation[0].Y;

                currentLocation[i].X = (float)(startX + ((x - startX) * Math.Cos(rotationAngle)) - ((y - startY) * Math.Sin(rotationAngle)));
                currentLocation[i].Y = (float)(startY + ((x - startX) * Math.Sin(rotationAngle)) + ((y - startY) * Math.Cos(rotationAngle)));               

            }
        }
    }
}
