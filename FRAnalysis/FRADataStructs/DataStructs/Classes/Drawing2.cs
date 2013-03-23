using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace FRADataStructs.DataStructs.Classes
{
    public class Drawing2
    {
        /// <summary>
        /// 画出走向来
        /// </summary>
        /// <param name="g">绘图对象.</param>
        /// <param name="pen">笔刷.</param>
        /// <param name="centerPoint">绘制区域左上角</param>
        /// <param name="r">绘制区域半径</param>
        /// <param name="orientation">走向值[0~PI).</param>
        /// <param name="boudsPen">绘制边界用的笔刷,为null时不绘制.</param>
        public static void DrawOrientation(Graphics g,Pen pen,Point centerPoint,int r,double orientation,Pen boudsPen)
        {
            g.DrawLine(pen,
                Point.Add(centerPoint, new Size((int)(Math.Cos(orientation) * r), (int)(-Math.Sin(orientation) * r))),
                Point.Add(centerPoint, new Size((int)(-Math.Cos(orientation) * r), (int)(Math.Sin(orientation) * r))));
            
        }
        /// <summary>
        /// Draws the concentric circles.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <param name="deltaR">The delta R.</param>
        /// <param name="n">The n.</param>
        public static void DrawConcentricCircles(Graphics g, Pen pen,Point centerPoint, int deltaR, int n)
        {
            int r=0;
            while (n != 0)
            {
                r += deltaR;
                g.DrawEllipse(pen, new Rectangle(Point.Subtract(centerPoint, new Size(-r, -r)), new Size(r, r)));
                n--;
            }
        }
        public static int[] FourConnectionAreaI = new int[] { -1, +0, +1, +0 };
        public static int[] FourConnectionAreaJ = new int[] { +0, +1, +0, -1 };
        public static int[] EightConnectionAreaI = new int[] { -1, -1, -1, +0, +1, +1, +1, +0 };
        public static int[] EightConnectionAreaJ = new int[] { -1, +0, +1, +1, +1, +0, -1, -1 };
        public static void ConnectionArea(int[] caI,int[] caJ,Action<int, int, int> act)
        {
            for (var k = 0; k < caI.Length; k++)
            {
                act(caI[k], caJ[k], k);
            }
        }
        public static void EightConnectionArea(Action<int, int, int> act)
        {
            ConnectionArea(EightConnectionAreaI, EightConnectionAreaJ, act);
        }
        public static void FourConnectionArea(Action<int, int, int> act)
        {
            ConnectionArea(FourConnectionAreaI, FourConnectionAreaJ, act);
        }

        /// <summary>
        /// 在正常的直角坐标系里画直线
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="height">The height.</param>
        public static void DrawLine(Graphics g, Pen pen, int x1, int y1, int x2, int y2, int x0, int y0, int height)
        {
            g.DrawLine(pen, x1 + x0, height - y0 - y1, x2 + x0, height - y0 - y2);
        }
        public static void DrawPoint(Graphics g, Pen pen, int x1, int y1, int x0, int y0, int height)
        {
            DrawLine(g, pen, x1 - 10, y1 - 10, x1 + 10, y1 + 10, x0, y0, height);
            DrawLine(g, pen, x1 + 10, y1 - 10, x1 - 10, y1 + 10, x0, y0, height);
        }
        /// <summary>
        /// 检验在一个点是否能符合一个细化模板的定义.
        /// </summary>
        /// <param name="hitAct">The hit act.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public static bool HitThiningTemlate(int i, int j, ThiningTemplate template, Func<int, int, bool> hitAct)
        {
            foreach (var tmp in template.Safe)
            {
                if (hitAct(i + tmp.I, j + tmp.J))
                {
                    return false;
                }
            }
            foreach (var tmp in template.Targets)
            {
                if (!hitAct(i + tmp.I, j + tmp.J))
                {
                    return false;
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 定义一个细化模板
    /// </summary>
    public struct ThiningTemplate{
        public PointLocation[] Targets;
        public PointLocation[] Safe;
    }
}

