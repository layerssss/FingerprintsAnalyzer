using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAProcess
{
    public class FRAProcessInputter
    {
        public bool Processing;
        public string Plan;
        System.IO.StreamWriter output;
        public FRADataStructs.DataStructTypes DataStructTypes;
        public FRAProcessInputter(bool processing, System.IO.StreamWriter output, FRADataStructs.DataStructTypes dataStructTypes,string plan)
        {
            this.Processing = processing;
            this.output = output;
            this.DataStructTypes = dataStructTypes;
            this.Plan = plan;
        }
        public T GetArg<T>(string argName)
            where T : FRADataStructs.IDataStruct, new()
        {
            return (T)this.GetArg(this.DataStructTypes.GetName<T>(), argName);
        }
        public FRADataStructs.IDataStruct GetArg(string argType,string argName)
        {
            if (Processing)
            {
                FRADataStructs.IDataStruct emptydata = this.DataStructTypes.GetNew(argType);
                System.IO.BinaryReader reader = new System.IO.BinaryReader(System.IO.File.OpenRead(argName + "." + argType));
                emptydata.Deserialize(reader);
                reader.Close();
                if (this.Plan != null && this.Plan == "test")
                {
                    try
                    {
                        emptydata.Present(FRADataStructs.DataStructs.GrayLevelImage.TryGetOrigin(), argName + "." + argType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("无法显示输入数据 " + argName + "." + argType + " 原因：" + ex.Message);
                    }
                    if (emptydata.GetType() == typeof(FRADataStructs.DataStructs.LocalStructure)&&(emptydata as FRADataStructs.DataStructs.LocalStructure).Values.First().GetType()==typeof(FRADataStructs.DataStructs.LocalStructure))
                    {
                        emptydata = (emptydata as FRADataStructs.DataStructs.LocalStructure).Values.First();
                    }
                }
                return emptydata;
            }
            else
            {
                output.WriteLine("input");
                output.WriteLine(argType);
                output.WriteLine(argName);
                return null;
            }
        }
    }
}
