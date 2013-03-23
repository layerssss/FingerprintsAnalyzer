using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Abstract
{
    public class Graph<TElement>
    {
        public TElement[][] Value;
        public TElement ValueGet(PointLocation pl)
        {
            return this.Value[pl.I][pl.J];
        }
        public void ValueSet(int i, int j, TElement val)
        {
            this.Value[i][j] = val;
        }
        public void ValueSet(PointLocation pl, TElement val)
        {
            this.ValueSet(pl.I, pl.J, val);
        }
        public virtual void Allocate(int width, int height,TElement defaultValue)
        {
            this.Width = width;
            this.Height = height;
            this.Value = new TElement[height][];
            for (int i = 0; i < height; i++)
            {
                this.Value[i] = new TElement[width];
                for (int j = 0; j < width; j++)
                {
                    this.Value[i][j] = defaultValue;
                }
            }
        }
        public virtual void Allocate(int width, int height)
        {
            this.Allocate(width, height, default(TElement));
        }
        public virtual void Allocate<T>(Graph<T> refGraph, TElement defaultValue)
        {
            this.Allocate(refGraph.Width, refGraph.Height,defaultValue);
        }
        public virtual void Allocate<T>(Graph<T> refGraph)
        {
            this.Allocate(refGraph.Width, refGraph.Height);
        }
        public bool IsInboundI(int i)
        {
            return i >= 0 && i < this.Height;
        }
        public bool IsInboundJ(int j)
        {
            return j >= 0 && j < this.Width;
        }
        public bool IsInbound(int i, int j)
        {
            return this.IsInboundI(i) && this.IsInboundJ(j);
        }
        public int Width;
        public int Height;
        public Graph<TElement> Clone()
        {
            return this.Clone(ti => ti);
        }
        public Graph<TElement2> Clone<TElement2>(Func<TElement, TElement2> func)
        {
            Graph<TElement2> ig = new Graph<TElement2>();
            ig.Allocate(this.Width, this.Height);
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    ig.Value[i][j] = func(this.Value[i][j]);
                }
            }
            return ig;
        }
        public Graph<TElement> Combine<TElement2>(Func<TElement, TElement2, TElement> func, Graph<TElement2> with)
        {
            Graph<TElement> ig = this.Clone();
            ig.Accumulate(func, with);
            return ig;
        }
        public void Accumulate<TElement2>(Func<TElement, TElement2, TElement> func, Graph<TElement2> with)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Value[i][j] = func(this.Value[i][j], with.Value[i][j]);
                }
            }
        }
        public bool FindAny(Func<int, int, bool> condition)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (condition(i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool FindAny(Func<int, int, TElement, bool> condition)
        {
            return FindAny((ti, tj) => condition(ti, tj, this.Value[ti][tj]));
        }
        public void ForEach(Action<int, int> act)
        {
            this.FindAny((ti, tj) =>
            {
                act(ti, tj);
                return false;
            });
        }
        public void ForEach(Action<int, int,TElement> act)
        {
            this.ForEach((ti, tj) => act(ti, tj, this.Value[ti][tj]));
        }
        /// <summary>
        /// 在指定窗口内寻找一个点，然后将其填充
        /// </summary>
        /// <param name="iMin">窗口 i min.</param>
        /// <param name="jMin">窗口 j min.</param>
        /// <param name="iMax">窗口 i max.</param>
        /// <param name="jMax">窗口 j max.</param>
        /// <param name="originalValue">要寻找并且填充的颜色</param>
        /// <param name="newValue">新颜色</param>
        /// <param name="counter">The counter.</param>
        /// <returns></returns>
        public bool FindAndFill(int iMin, int jMin, int iMax, int jMax, TElement originalValue, TElement newValue, ref int counter)
        {
            for (int i = iMin; i < iMax; i++)
            {
                for (int j = jMin; j < jMax; j++)
                {
                    if (this.Value[i][j].Equals(originalValue))
                    {
                        counter += Fill(i, j, iMin, jMin, iMax, jMax, originalValue, newValue);
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 计算结果图像上指定窗口的FC值.
        /// </summary>
        /// <param name="iMin">窗口 i min.</param>
        /// <param name="jMin">窗口 j min.</param>
        /// <param name="iMax">窗口 i max.</param>
        /// <param name="jMax">窗口 j max.</param>
        /// <param name="originalValue">The original value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="fCTemp">临时对象：计算FC时需要一个临时用的二值图，应跟原图一样大小</param>
        /// <returns>FC值</returns>
        public int FillCount(int iMin, int jMin, int iMax, int jMax, TElement originalValue, TElement newValue, Graph<TElement> fCTemp)
        {
            #region 将窗口内的值复制到FCTemp中的对应位置
            for (int i = iMin; i < iMax; i++)
            {
                for (int j = jMin; j < jMax; j++)
                {
                    fCTemp.Value[i][j] = this.Value[i][j];
                }
            }
            #endregion

            #region 重复填充并记录次数
            int count = 0;
            int counter = 0;
            while (fCTemp.FindAndFill(iMin, jMin, iMax, jMax, originalValue, newValue, ref counter))
            {
                count++;
            }
            #endregion
            return count;
        }
        public int AntiPoresFillCount(int iMin, int jMin, int iMax, int jMax, TElement originalValue, TElement newValue, Graph<TElement> fCTemp, int poreSizeMax)
        {
            #region 将窗口内的值复制到FCTemp中的对应位置
            for (int i = iMin; i < iMax; i++)
            {
                for (int j = jMin; j < jMax; j++)
                {
                    fCTemp.Value[i][j] = this.Value[i][j];
                }
            }
            #endregion

            #region 重复填充并记录次数
            int count = 0;
            int counter = 0;
            while (fCTemp.FindAndFill(iMin, jMin, iMax, jMax, originalValue, newValue, ref counter))
            {
                if (counter > poreSizeMax)
                {
                    count++;
                }
                counter = 0;
            }
            #endregion
            return count;
        }
        /// <summary>
        /// 从指定点开始进行四连通区域填充
        /// </summary>
        /// <param name="i">The ibase.</param>
        /// <param name="j">The jbase.</param>
        /// <param name="iMin">The i min.</param>
        /// <param name="jMin">The j min.</param>
        /// <param name="iMax">The i max(不可取).</param>
        /// <param name="jMax">The j max(不可取).</param>
        /// <param name="originalValue">被填充区域的颜色</param>
        /// <param name="newValue">新的颜色(警告：不能等于originalValue，否则会陷入死循环)</param>
        /// <returns>填充块的大小</returns>
        public int Fill(int i, int j, int iMin, int jMin, int iMax, int jMax, TElement originalValue, TElement newValue)
        {
            return FillAdvance(
                i,
                j,
                iMin,
                jMin,
                iMax,
                jMax,
                tv => tv.Equals(originalValue),
                (ti, tj, tv) => newValue);
        }
        public int FillAdvance(int i, int j, int iMin, int jMin, int iMax, int jMax, Func<TElement, bool> condition, Func<int, int, TElement, TElement> act)
        {
            return FillAdvance(i, j, iMin, jMin, iMax, jMax, condition, act, Classes.Drawing2.FourConnectionAreaI, Classes.Drawing2.FourConnectionAreaJ);
        }
        /// <summary>
        /// Fills the advance.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="iMin">The i min.</param>
        /// <param name="jMin">The j min.</param>
        /// <param name="iMax">The i max.</param>
        /// <param name="jMax">The j max.</param>
        /// <param name="condition">是否填充</param>
        /// <param name="act">填充时使用什么动作</param>
        /// <param name="connectionAreaI">The connection area I.</param>
        /// <param name="connectionAreaJ">The connection area J.</param>
        /// <returns></returns>
        public int FillAdvance(int i, int j, int iMin, int jMin, int iMax, int jMax, Func<TElement, bool> condition, Func<int, int, TElement, TElement> act, int[] connectionAreaI, int[] connectionAreaJ)
        {
            int count = 1;
            Stack<int> iStack = new Stack<int>();
            Stack<int> jStack = new Stack<int>();
            iStack.Push(i);
            jStack.Push(j);
            while (iStack.Count != 0)
            {
                i = iStack.Pop();
                j = jStack.Pop();
                this.Value[i][j] = act(i, j, this.Value[i][j]);
                for (int k = 0; k < connectionAreaI.Length; k++)//四周每一个点
                {
                    int nexti = i + connectionAreaI[k];
                    int nextj = j + connectionAreaJ[k];
                    if (nexti >= iMin && nexti < iMax && nextj >= jMin && nextj < jMax)//如果在范围内
                    {
                        if (condition(this.Value[nexti][nextj]))//要填充
                        {

                            this.Value[nexti][nextj] = act(nexti, nextj, this.Value[nexti][nextj]);
                            iStack.Push(nexti);
                            jStack.Push(nextj);
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        public bool FindRoute(int istart, int jstart, int iend, int jend, int maxRouteLen, out int routeLen, out UpTreeNode<PointLocation> routeNode, TElement path)
        {
            return FindRoute(istart, jstart, iend, jend, maxRouteLen, out routeLen, out routeNode, (ti, tj) => this.Value[ti][tj].Equals(path));
        }
        public bool FindRoute(int istart, int jstart, int iend, int jend, int maxRouteLen, out int routeLen, out UpTreeNode<PointLocation> routeNode, Func<int, int, bool> path)
        {
            return FindRoute(istart, jstart, iend, jend, maxRouteLen, out routeLen, out routeNode, path, Classes.Drawing2.EightConnectionAreaI, Classes.Drawing2.EightConnectionAreaJ);
        }
        public bool FindRoute(int istart, int jstart, int iend, int jend, int maxRouteLen, out int routeLen, out UpTreeNode<PointLocation> routeNode, Func<int, int, bool> path, int[] connAreaI, int[] connAreaJ)
        {
            BinaryGraph tmp = new BinaryGraph();
            routeLen = 0;
            tmp.Allocate(this.Width, this.Height);
            List<UpTreeNode<PointLocation>> curNodes = new List<UpTreeNode<PointLocation>>();
            PointLocation startObj = new PointLocation(istart, jstart);
            PointLocation endObj = new PointLocation(iend, jend);
            curNodes.Add(new UpTreeNode<PointLocation>(null, startObj));
            tmp.Value[startObj.I][startObj.J] = 1;
            while (curNodes.Count > 0)
            {
                routeLen++;
                if (routeLen > maxRouteLen)
                {
                    break;
                }
                List<UpTreeNode<PointLocation>> nextNodes = new List<UpTreeNode<PointLocation>>();
                foreach (UpTreeNode<PointLocation> node in curNodes)
                {
                    if (node.Obj.Equals(endObj))
                    {
                        routeNode = node;
                        return true;
                    }
                    #region node的周围
                    for (int k = 0; k < connAreaI.Length; k++)
                    {
                        int i = node.Obj.I + connAreaI[k];
                        int j = node.Obj.J + connAreaJ[k];
                        if (!path(i, j))
                        {
                            if (i != iend || j != jend)
                            {
                                continue;
                            }
                            else
                            {
                                routeLen++;
                                if (routeLen > maxRouteLen)
                                {
                                    break;
                                }
                                routeNode = new UpTreeNode<PointLocation>(node, endObj);
                                return true;
                            }
                        }
                        if (tmp.Value[i][j] == 1)
                        {
                            continue;
                        }
                        nextNodes.Add(new UpTreeNode<PointLocation>(node, new PointLocation(i, j)));
                        tmp.Value[i][j] = 1;
                    }
                    #endregion
                }
                curNodes.Clear();
                curNodes.AddRange(nextNodes);
            }
            routeNode = null;
            return false;
        }
    }
}
