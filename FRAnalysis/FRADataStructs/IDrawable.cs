using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs
{
    public interface IDrawable
    {
        void Draw(System.Drawing.Bitmap img,System.Drawing.Font font);
        string Reverse(int x, int y);
    }
}
