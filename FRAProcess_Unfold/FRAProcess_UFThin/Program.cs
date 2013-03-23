using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_UFThin
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_UFThinProcess(), args);
        }
    }
    class FRAProcess_UFThinProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntegerArraySeq IAS;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            IAS = inputter.GetArg<IntegerArraySeq>("二值图");
        }
        
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("未消除毛刺的骨架", IAS);
        }

        public override void Procedure()
        {
            bool left;
            do
            {
                left = Sink(0) || Sink(1);
            } while (left);
            for (int x = 0; x < this.IAS.Count; x++)
            {
                
                int y = this.IAS[x].Count - 1;
                int last=this.IAS[x][y];
                while (this.IAS[x][y-1] == this.IAS[x].Last())
                {
                    this.IAS[x].RemoveAt(y);
                    y--;
                }
            }
        }
        public bool Sink(int valueToSink)
        {
            int valueSinking = valueToSink == 1 ? 0 : 1;
            bool changed = false;
            for (int x = 0; x < this.IAS.Count; x++)
            {
                for (int y = 2; y < this.IAS[x].Count; y++)
                {
                    if (this.IAS[x][y] != valueToSink || this.IAS[x][y - 1] != valueSinking)
                    {
                        continue;
                    }
                    if (this.IAS[x][y - 2] == valueToSink)//下方侦测
                    {
                        continue;
                    }
                    int x2l = (x + this.IAS.Count - 1) % this.IAS.Count;
                    int x2r = (x + 1) % this.IAS.Count;
                    if (y - 1 < this.IAS[x2l].Count)
                    {
                        if (this.IAS[x2l][y - 1] == valueSinking)//左
                        {
                            if (this.IAS[x2l][y - 2] == valueToSink)
                            {
                                continue;
                            }
                        }
                    }
                    if (y - 1 < this.IAS[x2r].Count)
                    {
                        if (this.IAS[x2r][y - 1] == valueSinking)
                        {
                            if (this.IAS[x2r][y - 2] == valueToSink)
                            {
                                continue;
                            }
                        }
                    }
                    this.IAS[x][y - 1] = valueToSink;
                    changed = true;
                }
            }
            return changed;
        }
        
    }

}
