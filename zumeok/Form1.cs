using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zumeok
{
    public partial class Form1 : Form
    {
        public class ball
        {
            public int id = 48;
            public int XS, YS;
            public float XD, YD, move = 0f;
            public int angleL, angleR;
            public bool flagm = false;
            public Bitmap img = new Bitmap("balls.png");
        }
        int yd = 0, MouseX = 0, MouseY = 0, ctm = 0, w = 10, currX, currY;
        float m, dx, dy, invm, curX, curY,xmove=0, ymove=0; 
        bool flagb = false, flagmove = false;
        PointF pt = new PointF(200, 200);
        dda objdda = new dda();
        Circle objcc = new Circle();
        float ys = 0, yf = 0.0f;
        List<RectangleF> rdestl = new List<RectangleF>();
        List<Rectangle> rsrcl = new List<Rectangle>();
        List<Rectangle> rdestl2 = new List<Rectangle>();
        List<Rectangle> rsrcl2 = new List<Rectangle>();
        Rectangle rsrc; RectangleF rdest; Rectangle rdest2, rsrc2;
        Bitmap off, frogR;
        PointF carPoint;
        curve obj = new curve();
        Circle objc = new Circle();
        ball objs = new ball(); ball objb = new ball();
        public int wx, wy, xx, yy,cx=590,cy=500, rx=500, ry=500;
        public ball world;
        public ball frog;
        Random Rc = new Random();
        int rd;
        List<ball> Lballs = new List<ball>();
        List<ball> fballs = new List<ball> ();
        List<Point> points;
        System.Windows.Forms.Timer tt = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.MouseMove += onclick;
            this.KeyDown += KeyClick;
            tt.Tick += Tic;
            tt.Start();
        }
        private void KeyClick(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                     case Keys.Right:
                    
             
                    objc.th += 10;
                    objcc.th += 10;
                    break;
                        //for (int i = 0; i < fballs.Count; i++)
                        //{
                        //    fballs[i].angleR = frog.angleR;
                        //    fballs[i].img = rotateImage(fballs[i].img, fballs[i].angleR);
                        //}
                    
                    case(Keys.Left):

                    objc.th -= 10;
                    objcc.th -= 10;
                    break;
                //use circle for bullet
                //for (int i = 0; i < fballs.Count; i++)
                //{
                //    fballs[i].angleL = frog.angleL;
                //    fballs[i].img = rotateImage(fballs[i].img, fballs[i].angleL);
                //}
                case (Keys.Space):
                    flagb = true;
                    fballs.RemoveAt(0);
                    flagmove = true;
                    createBullet();
                    break;
            }
        }
        void createBullet()
        {
            objb.id = Rc.Next(0, 3);
            objb.angleL = 0;
            objb.angleR = 0;
            if (objb.id == 0)
            {
                objb.img = new Bitmap("blue.bmp");
                objb.img = new Bitmap(objb.img, new Size(40, 40));
                objb.img.MakeTransparent();
            }
            if (objb.id == 1)
            {
                objb.img = new Bitmap("green.bmp");
                objb.img = new Bitmap(objb.img, new Size(40, 40));
                objb.img.MakeTransparent();
            }

            if (objb.id == 2)
            {
                objb.img = new Bitmap("yellow.bmp");
                objb.img = new Bitmap(objb.img, new Size(40, 40));
                objb.img.MakeTransparent();
            }

            if (objb.id == 3)
            {
                objb.img = new Bitmap("red.bmp");
                objb.img = new Bitmap(objb.img, new Size(40, 40));
                objb.img.MakeTransparent();
            }
            
            fballs.Add(objb);
        }
        List<Point> ReadPoints()
        {


            points = new List<Point>();


            string[] readText = File.ReadAllLines("points2.txt");
            int[] values = new int[readText.Length / 2];
            int ct = 0;
            foreach (string s in readText)
            {
                if (s != ",")
                {
                    values[ct] = Int32.Parse(s);
                    ct++;
                }


            }
            int j = 1;
            for (int i = 0; i < ct - 1; i += 2)
            {
                Point pn = new Point(values[i]*150/100, values[j]*150/100);

                j += 2;

                points.Add(pn);

            }

            return points;

        }

        void MakePath()
        {
            points = ReadPoints();

            for (int i = 0; i < points.Count; i++)
            {
                obj.SetControlPoint(points[i]);

            }
        }
       
       
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            objc.XC = ClientSize.Width/2+20;
            objc.YC = ClientSize.Height/2-40;
            objc.Rad = 110;
            objc.StartTh = 0;
            objc.EndTh = 360;
            objc.th = 95;
            //
            objcc.XC = ClientSize.Width / 2 + 20;
            objcc.YC = ClientSize.Height / 2 - 40;
            objcc.Rad = 300;
            objcc.StartTh = 0;
            objcc.EndTh = 360;
            objcc.th = 95;

            frog = new ball();
            frog.angleL = 0;
            frog.angleR = 0;
            frog.img = new Bitmap("frog.png");
            frog.img = new Bitmap(frog.img, new Size(320, 240));
            frog.img.MakeTransparent();
            world = new ball();
            world.img = new Bitmap("back.png");
            world.img = new Bitmap(world.img, new Size(off.Width, off.Height));
            world.img.MakeTransparent();
            wx = world.img.Width / 2; wy = world.img.Height / 2;
            xx = world.img.Width - 300;
            
            for (int i = 0; i < Rc.Next(10,20); i++)
            {
                objs = new ball();
                objs.XS = 48; objs.YS = 0; objs.YD = yd; objs.XD = 0;
                objs.img.MakeTransparent(objs.img.GetPixel(0, 0));
                objs.id = Rc.Next(1, 5);
                objs.XS *= objs.id;
                objs.flagm = false;
                objs.move = yf;
                Lballs.Add(objs);
                yf -= 0.01f;
                rdestl.Add(new Rectangle(0, 0, 0, 0));    rsrcl.Add(new Rectangle(0, 0, 0, 0));
            }
            MakePath();

        }

        private void onclick(object sender, MouseEventArgs e)
        {
            currX = e.X;
            currY = e.Y;
            RotateFrog(e.X, e.Y);
        }
        public void RotateFrog(int MouseX, int MouseY)
        {
            if (frog.img != null)
            {
                int xCenter = 630 + (frog.img.Width) / 2;
                int yCenter = 300 + (frog.img.Height) / 2;

                float diffY = (MouseY - yCenter);
                float diffX = (MouseX - xCenter);

                float m = diffY / diffX;
                m = 1 / m;
                double f = Math.Atan(m);
                float rad = (float)(f * 180 / Math.PI);
                if (MouseY < yCenter)
                    rad -= 180;
                frogR = rotateImage(frog.img, rad);
            }
            else
            {
                int xCenter = 630 + (frog.img.Width) / 2;
                int yCenter = 300 + (frog.img.Height) / 2;
                float m = (MouseY - yCenter) / (MouseX - xCenter);
                frogR = rotateImage(frog.img, (float)Math.Atan(1 / m));
            }
        }
        private void Tic(object sender, EventArgs e)
        {
            curX = objc.x; curY = objc.y;
            dx = objcc.x - objc.x;  dy = objcc.y - objc.y;
            m = dy / dx; invm = 1 / m;
            dx = objc.x - objcc.x; dy = objc.y - objcc.y;
            objc.MoveStep(objc.x, objc.y, objcc.x, objcc.y,curX,curY,m,invm,dx,dy);
            for (int i = 0; i < Lballs.Count; i++)
            {
                carPoint = obj.CalcCurvePointAtTime(Lballs[i].move);
                Lballs[i].XD = carPoint.X;
                Lballs[i].YD = carPoint.Y;
            }
            for (int i = 0; i < Lballs.Count; i++)
            {
                Lballs[i].move += 0.001f;
            }
            //ctm++;
            //if(ctm>10)
            //{
            //    if (fballs.Count > 0)
            //    {
            //        fballs.RemoveAt(w-1);
            //        w--;
            //        ctm = 0;
            //    }
            //}
            if(flagb==false)
            {
                flagb = true;
                createBullet();
            }
            if(flagmove==true)
            {
                xmove = objc.currX;
                ymove = objc.currY;
            }
            DrawDubb(CreateGraphics());

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
						else if (objc.x > objcc.x && ys > fballs[i].shooty)
						{
							fballs[i].y -= speed;
							fballs[i].x -= 1 / fballs[i].m * speed;
						}
					}

        */
        private Bitmap rotateImage(Bitmap b, float angle)
        {
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            Graphics g = Graphics.FromImage(returnBitmap);
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            g.DrawImage(b, new Point(0, 0));
            return returnBitmap;
        }
        //void AdjustBalls()
        //{
        //    for(int i=0;i<rdestl.Count;i++)
        //    {

        //    }
        //    float t = rdestl[i].
        //}
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            g.DrawImage(world.img, 0, 0);
            //g.DrawImage(frog.img, world.img.Width / 2 - 140, world.img.Height / 2 - 140);
            obj.DrawCurve(g);
        
            //ys = carPoint.Y;
            for (int i = 0; i < Lballs.Count; i++)
            {
                rdest = new RectangleF(Lballs[i].XD, Lballs[i].YD, 60, 60);
                rsrc = new Rectangle(Lballs[i].XS, Lballs[i].YS, 48, 48);
                rdestl[i] = new RectangleF(rdest.X, rdest.Y, 60, 60);
                rsrcl[i] = new Rectangle(rsrc.X, rsrc.Y, 48, 48);
                Lballs[i].YS += 48;
                if (Lballs[i].YS > 2399)
                {
                    Lballs[i].YS = 0;
                }
                g.DrawImage(Lballs[i].img, rdestl[i], rsrcl[i],
                        GraphicsUnit.Pixel);
            }
            g.DrawImage(frogR, ClientSize.Width/2-140,ClientSize.Height/2-140);
            if (flagb == true)
            {
                objc.DrawYourSelf();//rotate xs ys
                objcc.drawok();// rotate xe ye
                
            }

            g.DrawImage(fballs[0].img, objc.x -xmove, objc.y-ymove);//bullet

            //g.DrawImage(fballs[0].img, objcc.x - 10, objcc.y - 10);
            //uses object to rotate objc.DrawYourself()

            /////
            //for (int i = 0; i < fballs.Count; i++)
            //{
            //    rdest2 = new Rectangle(rdest2.X)
            //    rdestl2[i] = new Rectangle(rdest2.X, rdest2.Y, 40, 40);
            //    rsrcl2[i] = new Rectangle(rsrc2.X, rsrc2.Y, 23, 23);
            //    g.DrawImage(fballs[i].img, rdestl2[i], rsrcl2[i],
            //            GraphicsUnit.Pixel);
            //}
            //draw passed in image onto graphics object


        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}