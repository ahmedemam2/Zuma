using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zumeok
{
    public class Circle
    {
        public int XC, YC, Rad;
        public int inC = 1;
        public float StartTh = 0, th, x, y;
        public float EndTh = 45;
        public float thRadian2;
        public float xs2,ys2,xe2,ye2,currX,currY;
        int Speed = 10;
        public bool  isTravelling = true;
        //public void SetVals(float a, float b, float c, float d) //x1,y1,x2,y2
        //{

        //    xs2 = a;
        //    ys2 = b;
        //    xe2 = c;
        //    ye2 = d;

        //    //////////////////
        //    dx = xe2 - xs2; // /_\dx
        //    dy = ye2 - ys2; // /_\dy
        //    m = dy / dx;  // m
        //    invM = dx / dy;
        //    /////////////////
        //    currX = xs2; //current x of the ball
        //    currY = ys2;
        //    currx2 = xs2;
        //    curry2 = ys2;
        //}
        public void DrawYourSelf()
        {

            float thRadian;


            //int i = 0;
            //for (float th = StartTh; th < EndTh; th += inC, i++)
            //{
            //    thRadian = (float)(th * Math.PI / 180);
            //    x = (float)(Rad * Math.Cos(thRadian) + XC);
            //    y = (float)(Rad * Math.Sin(thRadian) + YC);
            //    g.FillEllipse(Brushes.Black, x - 5, y - 5, 10, 10);

            //}
            thRadian = (float)(th * Math.PI / 180);
            x = (float)(Rad * Math.Cos(thRadian) + XC);
            y = (float)(Rad * Math.Sin(thRadian) + YC);

        }
        public void drawok()
        {


            float thRadian, thRadianOuter;


            int i = 0;

            thRadian = (float)(th * Math.PI / 180);
            x = (float)(Rad * Math.Cos(thRadian) + XC);
            y = (float)(Rad * Math.Sin(thRadian) + YC);


        }
        /*
        if (Math.Abs(fballs[i].dx) > Math.Abs(fballs[i].dy))
					{
						if (xs < fballs[i].shootx && ys < fballs[i].shooty)
						{
							fballs[i].x += speed;
							fballs[i].y += fballs[i].m * speed;
						}
						else if (xs < fballs[i].shootx && ys > fballs[i].shooty)
						{
							fballs[i].x += speed;
							fballs[i].y += fballs[i].m * speed;
						}
						else if (xs > fballs[i].shootx && ys < fballs[i].shooty)
						{
							fballs[i].x -= speed;
							fballs[i].y -= fballs[i].m * speed;
						}
						else if (xs > fballs[i].shootx && ys > fballs[i].shooty)
						{
							fballs[i].x -= speed;
							fballs[i].y -= fballs[i].m * speed;
						}
					}
					else
					{
						if (xs < fballs[i].shootx && ys < fballs[i].shooty)
						{
							fballs[i].y += speed;
							fballs[i].x += 1 / fballs[i].m * speed;
						}
						else if (xs < fballs[i].shootx && ys > fballs[i].shooty)
						{
							fballs[i].y -= speed;
							fballs[i].x -= 1 / fballs[i].m * speed;
						}
						else if (xs > fballs[i].shootx && ys < fballs[i].shootx)
						{
							fballs[i].y += speed;
							fballs[i].x += 1 / fballs[i].m * speed;
						}
						else if (xs > fballs[i].shootx && ys > fballs[i].shooty)
						{
							fballs[i].y -= speed;
							fballs[i].x -= 1 / fballs[i].m * speed;
						}
					}

        */
        public void MoveStep(float xs , float xe, float ys, float ye,float crX,float crY,float m,float invM
            ,float dx,float dy)
        {
            float cx = currX;
            float cy = currY;
            if (Math.Abs(dx) > Math.Abs(dy)) //|dy|,|dx|
            {
                if (xs < xe)
                {
                    cx += Speed;
                    cy += m * Speed;

                    if (cx >= xe)
                    {
                        isTravelling = false;
                    }

                }
                else
                {

               
                    cx -= Speed;
                    cy -= m * Speed;
                    if (cx <= xe)
                    {
                        isTravelling = false;
                    }
                }
            }
            else if (Math.Abs(dx) < Math.Abs(dy))
            {

                
                if (ys < ye)
                {
                    cy += Speed;
                    cx += invM * Speed;
                    //    currx2 += Speed;
                    //   cy2 += invM * Speed;
                    if (cy >= ye)
                    {
                        isTravelling = false;
                    }
                }
                else
                {
                    cy -= Speed;
                    cx -= invM * Speed;
                    //   cx2 -= Speed;
                    //   cy2 -= invM * Speed;
                    if (cy <= ye)
                    {
                        isTravelling = false;
                    }
                }
            }
            currY = cy;
            currX= cx;
        }
    }
}