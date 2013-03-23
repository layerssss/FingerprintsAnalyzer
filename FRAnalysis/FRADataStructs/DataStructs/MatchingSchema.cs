using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace FRADataStructs.DataStructs
{
    public class MatchingSchema : IDataStruct
    {
        public string AMacro = "";
        public string AResult = "";
        public string MMacro = "";
        public string MTemplateInput = "";
        public string MTargetInput= "";
        public string MResult = "";
        public string MThreshold = "";

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            try
            {
                writer.Write(AMacro);
                writer.Write(AResult);
                writer.Write(MMacro);
                writer.Write(MTemplateInput);
                writer.Write(MTargetInput);
                writer.Write(MResult);
                writer.Write(MThreshold);
            }
            catch { }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            try
            {
                this.AMacro = reader.ReadString();
                this.AResult = reader.ReadString();
                this.MMacro = reader.ReadString();
                this.MTemplateInput = reader.ReadString();
                this.MTargetInput = reader.ReadString();
                this.MResult = reader.ReadString();
                this.MThreshold = reader.ReadString();
            }
            catch { }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                WorkingDirectory = "..\\tools",
                FileName = "Tool_MachingPerformance.exe",
                Arguments = "..\\data\\" + dataIdent
            }).WaitForExit();
            return false;
        }

        public IDataStruct BuildInstance()
        {
            return new MatchingSchema();
        }

        #endregion
    }
}
