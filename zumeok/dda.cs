using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zumeok
{
    public class dda
    {
        public float xs, ys, xe, ye, dx, dy, m, invM, currX, currY, currx2, curry2;
        int Speed = 10;
        public float X, Y;
        //    Bitmap img = new Bitmap("1.bmp");
        public void SetVals(float a, float b, float c, float d) //x1,y1,x2,y2
        {
            xs = a;
            ys = b;
            xe = c;
            ye = d;

            //////////////////
            dx = xe - xs; // /_\dx
            dy = ye - ys; // /_\dy
            m = dy / dx;  // m
            invM = dx / dy;
            /////////////////
            currX = xs; //current x of the ball
            currY = ys;
            currx2 = xs;
            curry2 = ys;
        }

        public void MoveStep()
        {
            if (Math.Abs(dx) > Math.Abs(dy)) //|dy|,|dx|
            {
                if (xs < xe)
                {
                    currX += Speed;
                    currY += m * Speed;
                    //   currx2 += Speed;
                    //  curry2 += m * Speed;

                    if (currX >= xe)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
                else
                {
                    currX -= Speed;
                    currY -= m * Speed;
                    //   currx2 -= Speed;
                    //  curry2 -= m * Speed;
                    if (currY <= xe)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
            }
            else  //   if (Math.Abs(dx) <   Math.Abs(dy))
            {
                if (ys < ye)
                {
                    currY += Speed;
                    currX += invM * Speed;
                    //    currx2 += Speed;
                    //   curry2 += invM * Speed;
                    if (currY >= ye)
                    {
                        SetVals(xe, ye, xs, ys);
                    }




                }
                else
                {
                    currY -= Speed;
                    currX -= invM * Speed;
                    //   currx2 -= Speed;
                    //   curry2 -= invM * Speed;
                    if (currY <= ye)
                    {
                        SetVals(xe, ye, xs, ys);
                    }
                }
            }
        }

    }
}