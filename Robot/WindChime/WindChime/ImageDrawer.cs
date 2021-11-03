using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
namespace Drawer
{
    public class ImageDrawer
    {
        public static PointF PointHead = new PointF(10, 10);
        public static PointF PointName = new PointF(154, 6);
        public static PointF PointMoney = new PointF(194, 50);
        public static PointF PointHeart = new PointF(194, 94);
        public static PointF PointA = new PointF(73, 152);
        public static PointF PointB = new PointF(216, 152);
        public static PointF PointC = new PointF(73, 222);
        public static PointF PointD = new PointF(216, 222);
        public static PointF PointE = new PointF(43, 292);
        public static PointF PointF = new PointF(186, 292);
        public static PointF PointG = new PointF(73, 365);
        public static string DrawUserInfo(UserInfo usf)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("UserInfoImage/A.png");
            System.Drawing.Image img2 = System.Drawing.Image.FromFile("Head.png");
            Graphics g = Graphics.FromImage(img);
            using (Font f = new Font("SIMHEI", 20))
            {
                Font f2 = new Font("黑体", 15);
                using (Brush b = new SolidBrush(Color.White))
                {
                    g.DrawImage(img2, PointHead);
                    g.DrawString(usf.name, f, b, PointName);
                    g.DrawString(usf.money.ToString(), f, b, PointMoney);
                    g.DrawString(usf.heart.ToString(), f, b, PointHeart);
                    g.DrawString(usf.Courage.ToString(), f, b, PointA);
                    g.DrawString(usf.Cautious.ToString(), f, b, PointB);
                    g.DrawString(usf.Discipline.ToString(), f, b, PointC);
                    g.DrawString(usf.Justice.ToString(), f, b, PointD);
                    g.DrawString(GameManager.weapon[usf.EGOWeapon].Name + ".", f2, b, PointE);
                    g.DrawString(GameManager.armor[usf.EGOArmor].Name + ".", f2, b, PointF);
                    g.DrawString(usf.MoonCard.ToString(), f, b, PointG);
                }
            }
            img.Save($"UserInfoImage/{usf.id}.png", System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine(new FileInfo($"UserInfoImage/{usf.id}.png").FullName);
            return new FileInfo($"UserInfoImage/{usf.id}.png").FullName;
        }
    }
}