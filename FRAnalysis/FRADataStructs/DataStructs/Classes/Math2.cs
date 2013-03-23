using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Classes
{
    public class Math2
    {
        /// <summary>
        /// 得到一个虚数的辐角主值
        /// </summary>
        /// <param name="realPart">The real part.</param>
        /// <param name="imaginePart">The virtual part.</param>
        /// <returns></returns>
        static public double Arg(double realPart, double imaginePart)
        {
            return System.Math.Atan2(-imaginePart, -realPart) + Math.PI;
        }
        static public double MidDF(double w1, double theta1, double w2, double theta2)
        {
            return (
                (w1 * theta1 + w2 * theta2 +
                Math2.Floor(Math.Abs(theta1 - theta2), Math.PI)
                )) % (2 * Math.PI);
        }
        static public double MidOF(double w1, double theta1, double w2, double theta2)
        {
            return (
                (w1 * theta1 + w2 * theta2 +
                Math2.Floor(Math.Abs(theta1 - theta2), 0.5 * Math.PI)
                )) % (Math.PI);
        }
        static public double DifOF(double theta1, double theta2)
        {
            return (theta1 - theta2 + 1.5 * Math.PI) % Math.PI - 0.5 * Math.PI;
        }
        static public double DifDf(double theta1, double theta2)
        {
            return (theta1 - theta2 + 3 * Math.PI) % (Math.PI * 2) - Math.PI;
        }
        static public double OrthogonalOF(double oF)
        {
            return (oF + 0.5 * Math.PI) % Math.PI;
        }
        static public double Floor(double target,double b)
        {
            return b * Math.Floor(target / b);
        }
        static public void DDALineEach(int x0, int y0, int x1, int y1, Action<int, int> act)
        {
            int dx, dy, epsl;
            float x, y, xIncre, yIncre;
            dx = x1 - x0;
            dy = y1 - y0;
            x = x0;
            y = y0;
            epsl = Math.Max(Math.Abs(dx), Math.Abs(dy));
            xIncre = (float)dx / (float)epsl;
            yIncre = (float)dy / (float)epsl;
            for (int k = 0; k <= epsl; k++)
            {
                act((int)(x + 0.5), (int)(y + 0.5));
                x += xIncre;
                y += yIncre;
            }
        }
        static public void Rotate(double x, double y,out double newX,out double newY, double theta, double x0, double y0)
        {
            double dx = x - x0;
            double dy = y - y0;
            double cos = Math.Cos(theta);
            double sin = Math.Sin(theta);
            newX = x0 + cos * dx - sin * dy;
            newY = y0 + sin * dx + cos * dy;
        }
        static public void Rotate(ref int i, ref int j, double theta, int baseI, int baseJ)
        {
            var deltaI = i - baseI;
            var deltaJ = j - baseJ;
            var cos = Math.Cos(theta);
            var sin = Math.Sin(theta);
            i = (int)Math.Round(baseI + cos * deltaI - sin * deltaJ);
            j = (int)Math.Round(baseJ + sin * deltaI + cos * deltaJ);
        }
        static public void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
        static public double PI(double times)
        {
            return Math.PI * times;
        }
        static public double Sqr(double val)
        {
            return val * val;
        }
    }
}
