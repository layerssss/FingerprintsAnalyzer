using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
namespace FRADataStructs.DataStructs
{
    public class IntergerGraph : Abstract.Graph<int>, IDataStruct
    {
        public IntergerGraph() { }
        public IntergerGraph(Abstract.Graph<int> graph){
            this.Width = graph.Width;
            this.Height = graph.Height;
            this.Value = graph.Value;
        }
        public new IntergerGraph Clone()
        {
            return new IntergerGraph(base.Clone());
        }
        public IntergerGraph Clone(Func<int, int> func)
        {
            return new IntergerGraph(base.Clone(func));
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Width);
            writer.Write(this.Height);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    writer.Write(this.Value[i][j]);
                }
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Allocate(reader.ReadInt32(), reader.ReadInt32());
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    this.Value[i][j] = reader.ReadInt32();
                }
            }
        }

        public virtual bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormIntergerGraph f = new Controls.FormIntergerGraph();
            f.LoadData(this, originalImg, dataIdent);
            List<int> all = new List<int>();
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (!all.Contains(this.Value[i][j]))
                    {
                        all.Add(this.Value[i][j]);
                    }
                }
            }
            all.Sort();
            foreach (int i in all)
            {
                int j = (int)(((double)(i  - all.Min() )) / (all.Max() - all.Min()) * (255));
                f.AddCheckBox(i, System.Drawing.Color.FromArgb(
                    j,j,j
                    ));
            }
            return f.ShowDialog()==System.Windows.Forms.DialogResult.OK;
        }

        #endregion

        #region IDataStruct 成员


        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
