using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FRADataStructs;
using FRADataStructs.DataStructs;
namespace Tool_DataStructsTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var data = new LSArray();
            data.Allocate(2, 3);
            data.ForEach((ti,tj)=>{
                var tls = new LocalStructure();
                tls.Add("STR", new StringData() { Value = ti + ":" + tj });
                tls.PrimaryDataKey = "STR";
                data.ValueSet(ti, tj, tls);
            });
            data.Present(null, "SGA");
        }
    }
}
