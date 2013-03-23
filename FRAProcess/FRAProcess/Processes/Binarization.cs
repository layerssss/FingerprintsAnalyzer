using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess.Processes
{
    public class Binarization:FRAProcess
    {
        public GrayLevelImage OriginalImg;
        public BinaryGraph Result = new BinaryGraph();
        public override void Input(FRAProcessInputter inputter)
        {
            OriginalImg = inputter.GetArg<GrayLevelImage>("输入图像");
        }
        
        public override void Output(FRAProcessOutputter outputter)
        {
            outputter.PutArg<BinaryGraph>("二值图", Result);
        }

        public override void Procedure()
        {
            Result.Allocate(OriginalImg.Width, OriginalImg.Height);
        }
    }
}
