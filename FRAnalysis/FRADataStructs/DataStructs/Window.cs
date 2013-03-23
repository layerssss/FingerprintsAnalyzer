using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class Window:IDataStruct
    {
        public int IMin;
        public int IMax;
        public int JMin;
        public int JMax;
        /// <summary>
        /// 判断指定坐标是否属于该窗口范围内.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>是否属于该窗口</returns>
        public bool ContainPosition(int i, int j)
        {
            return i >= IMin && i < IMax && j >= JMin && j < JMax;
        }
        public int GetSize()
        {
            return (IMax - IMin) * (JMax - JMin);
        }
        public int Width
        {
            get { return JMax - JMin; }
        }
        public int Height
        {
            get { return IMax - IMin; }
        }
        public static Window GetSquare(int i, int j, int w)
        {
            return new Window()
            {
                IMin = i - w,
                IMax = i + w + 1,
                JMin = j - w,
                JMax = j + w + 1
            };
        }
        public static Window GetSquare<T>(int i, int j, int w,Abstract.Graph<T> graph)
        {
            Window win = GetSquare(i, j, w);
            if (win.IMin < 0) { win.IMin = 0; }
            if (win.JMin < 0) { win.JMin = 0; }
            if (win.IMax > graph.Height) { win.IMax = graph.Height; }
            if (win.JMax > graph.Width) { win.JMax = graph.Width; }
            return win;
        }
        public void ForEach<T>(Abstract.Graph<T> graph, Func<T,bool> act)
        {
            this.ForEach((ti, tj) => act(graph.Value[ti][tj]));
        }
        public void ForEach<T>(Abstract.Graph<T> graph, Action<T> act)
        {
            this.ForEach(graph, tv => { act(tv); return false; });
        }
        public void ForEach(Func<int,int, bool> act)
        {
            for (int i = IMin; i < IMax; i++)
            {
                for (int j = JMin; j < JMax; j++)
                {
                    if (act(i,j))
                    {
                        return;
                    }
                }
            }
        }
        public void ForEach(Action<int, int> act)
        {
            ForEach((ti, tj) => { act(ti, tj); return false; });
        }
        public void ForEachOnBound(Func<int, int,bool> act)
        {
            this.ForEach((ti, tj) =>
            {
                if (ti != IMin && ti != IMax - 1)
                {
                    return false;
                }
                if (tj != JMin && tj != JMax - 1)
                {
                    return false;
                }
                return act(ti, tj);
            });
        }
        public void ForEachOnBound(Action<int, int> act)
        {
            ForEachOnBound((ti, tj) => { act(ti, tj); return false; });
        }
        public bool IsIn<T>(Abstract.Graph<T> graph)
        {

            return 0 <= IMin && graph.Height >= IMax && 0 <= JMin && graph.Width >= JMax;
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(IMin);
            writer.Write(IMax);
            writer.Write(JMin);
            writer.Write(JMax);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            IMin = reader.ReadInt32();
            IMax = reader.ReadInt32();
            JMin = reader.ReadInt32();
            JMax = reader.ReadInt32();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormWindow f = new Controls.FormWindow();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            return new Window()
            {
                IMin = 0,
                IMax = 0,
                JMin = 0,
                JMax = 0
            };
        }

        #endregion
    }
}