using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DrawDoubleFloatArrSeq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DrawDoubleFloatArrSeqProcess(), args);
        }
    }
    class FRAProcess_DrawDoubleFloatArrSeqProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloatArraySeq seq;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.seq = inputter.GetArg<DoubleFloatArraySeq>("源序列");
        }
        BitmapFile bmp;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("序列预览", bmp);
        }

        public override void Procedure()
        {
            bmp = new BitmapFile();
            double a,b;
            int i;
            bmp.BitmapObject = seq.Draw(out a, out b, out i);
        }
    }

}
