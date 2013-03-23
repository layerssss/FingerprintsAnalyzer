using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MERemoveOverrichPoints
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MERemoveOverrichPointsProcess(), args);
        }
    }
    class FRAProcess_MERemoveOverrichPointsProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet points;
        Integer w;
        Integer maxCount;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.points = inputter.GetArg<PointLocationSet>("点坐标集");
            this.w = inputter.GetArg<Integer>("W");
            this.maxCount = inputter.GetArg<Integer>("窗口内点个数阀值");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("新点坐标集", points);
        }

        public override void Procedure()
        {
            List<PointLocation> pointsToBeDel = new List<PointLocation>();
            #region gdsagfdsaf
            foreach (PointLocation pl1 in points)
            {
                Window win = Window.GetSquare(pl1.I, pl1.J, w.Value);
                List<PointLocation> plsInWin = new List<PointLocation>();
                plsInWin.Add(pl1);
                foreach (PointLocation pl2 in points)
                {
                    if (pl2.Equals(pl1))
                    {
                        continue;
                    }
                    if (win.ContainPosition(pl2.I, pl2.J))
                    {
                        plsInWin.Add(pl2);
                    }
                }
                if (plsInWin.Count > maxCount.Value)
                {
                    foreach (PointLocation pl in plsInWin)
                    {
                        if (!pointsToBeDel.Contains(pl))
                        {
                            pointsToBeDel.Add(pl);
                        }
                    }
                }
            } 
            #endregion
            this.points.RemoveAll(tpl => pointsToBeDel.Contains(tpl));
        }
    }

}
