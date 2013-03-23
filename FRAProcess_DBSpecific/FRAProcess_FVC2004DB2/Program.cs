using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_FVC2004DB2
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_FVC2004DB2Process(), args);
        }
    }
    class FRAProcess_FVC2004DB2Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            img = inputter.GetArg<GrayLevelImage>("原图像");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("增强后图像", img);
        }

        public override void Procedure()
        {
            var random=new Random();
            for (int i = 0; i < img.Height; i++)
            {
                double d;
                int j = 0;
                while (img.Value[i][j] == 1)
                {
                    j++;
                }
                d = img.Value[i][j];
                for (; j >= 0; j--)
                {
                    img.Value[i][j] = random.NextDouble();//d;
                }
                j = img.Width - 1;
                while (img.Value[i][j] == 1)
                {
                    j--;
                }
                d = img.Value[i][j];
                for (; j <img.Width; j++)
                {
                    img.Value[i][j] = random.NextDouble();//d;
                }
            }
        }
    }

}
