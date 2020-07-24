using System;
using System.Drawing;

namespace captcha.Services.DrawCaptcha
{

    public class DrawCaptcha:IDrawCaptcha
    {
        
        public Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Chocolate,
                     Brushes.DarkOrchid };
        public Random random= new Random();
        public Captcha DrawsCaptcha()
        {
            string s= "qwertyuiopasdfghjklzxcvbnm1234567890";
            string res="";
            for(var i=0; i<8; i++){
                res+=s[random.Next(s.Length)];
            }
            Bitmap b = new Bitmap(250, 50);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.Gray);
                g.DrawString(res, new Font("Arial",30), Brushes.Green, new PointF(0, 0));
                g.DrawLine(new Pen(colors[random.Next(colors.Length-1)]), new Point(0, random.Next(b.Height)), new Point(b.Width,random.Next(b.Height)));
                g.DrawLine(new Pen(colors[random.Next(colors.Length-1)]), new Point(0, random.Next(b.Height)), new Point(b.Width,random.Next(b.Height)));
            }

            

            for(var y=0;y<b.Height;y++){
                for(var x=0; x<b.Width; x++){
                    if(random.Next()%20==0){
                        b.SetPixel(x,y, Color.White);
                    }
                }
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            string lol= Convert.ToBase64String(byteImage);
            ms.Dispose();
            b.Dispose();
            return new Captcha(){Key=res, Image=lol};
        }      
    }
}