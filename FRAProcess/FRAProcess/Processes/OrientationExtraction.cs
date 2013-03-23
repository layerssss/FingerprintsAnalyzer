using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess.Processes
{
    public abstract class OrientationExtraction:FRAProcess
    {
        public GrayLevelImage InputImage;
        public OrientationGraph Orientation = new OrientationGraph();
        public override void Input(FRAProcessInputter inputter)
        {
            this.InputImage = inputter.GetArg<GrayLevelImage>("增强图像");
        }
        public override void Output(FRAProcessOutputter outputter)
        {
            outputter.PutArg<OrientationGraph>("初步提取", Orientation);
        }
        public  override void Procedure()
        {
            this.Orientation.Allocate(InputImage.Width, InputImage.Height);
        }
    }
}
