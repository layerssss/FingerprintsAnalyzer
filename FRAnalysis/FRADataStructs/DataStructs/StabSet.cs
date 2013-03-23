using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class StabSet :List<PointLocationSet>, IDataStruct
    {
        public bool IsOpen<T>(int i, int j, int stabNum, Abstract.Graph<T> bg, Func<T, bool> isClose, int closeTolarence)
        {
            int countClose = 0;
            return IsOpen(i, j, stabNum,bg,
                (ti, tj) =>
                {
                    if (isClose(bg.Value[ti][tj]))
                    {
                        countClose++;
                    }
                },
                () => countClose > closeTolarence);
        }
        public bool IsOpen<T>(int i, int j, int stabNum, Abstract.Graph<T> bg, Action<int, int> searchAction, Func<bool> closeCondition)
        {
            for (int l = 0; l < this[stabNum].Count; l++)
            {
                int ii = i + this[stabNum][l].I;
                int jj = j + this[stabNum][l].J;
                if (ii >= 0 && jj >= 0 && ii < bg.Height && jj < bg.Width)
                {
                    searchAction(ii, jj);
                }
                if (closeCondition())
                {
                    return false;
                }
            }
            return true;
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (PointLocationSet pls in this)
            {
                pls.Serialize(writer);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                PointLocationSet pls = new PointLocationSet();
                pls.Deserialize(reader);
                this.Add(pls);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormStabSet f = new Controls.FormStabSet();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
