using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tool_BinaryFileStream
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            if (args.Length != 0)
            {
                f.LoadFile(args[0]);
            }
            f.ShowDialog();
        }
    }
}
