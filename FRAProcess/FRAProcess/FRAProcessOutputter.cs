using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAProcess
{

    public class FRAProcessOutputter
    {
        public bool Processing;
        public string Plan;
        System.IO.StreamWriter output;
        public FRADataStructs.DataStructTypes DataStructTypes;
        public FRAProcessOutputter(bool processing, System.IO.StreamWriter output, FRADataStructs.DataStructTypes dataStructTypes,string plan)
        {
            this.Processing = processing;
            this.output = output;
            this.DataStructTypes = dataStructTypes;
            this.Plan = plan;
        }
        public void PutArg<T>(string argName, T data)
            where T : FRADataStructs.IDataStruct, new()
        {
            this.PutArg(DataStructTypes.GetName<T>(), argName, data);
        }
        public void PutArg(string argType, string argName,FRADataStructs.IDataStruct data)
        {
            if (Processing)
            {
                System.IO.BinaryWriter writer = new System.IO.BinaryWriter(System.IO.File.Create(argName + "." + argType));
                data.Serialize(writer);
                writer.Close();
                if (this.Plan != null && this.Plan == "test")
                {
                    try
                    {
                        data.Present(FRADataStructs.DataStructs.GrayLevelImage.TryGetOrigin(), argName + "." + argType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("无法显示输出数据 " + argName + "." + argType + " 原因：" + ex.Message);
                    }
                }
            }
            else
            {
                output.WriteLine("output");
                output.WriteLine(argType);
                output.WriteLine(argName);
            }
        }
    }
}
