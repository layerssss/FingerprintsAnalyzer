using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class GrayLevelImage : Abstract.DoubleFloatGraph
    {
        public static GrayLevelImage FromBitmap(System.Drawing.Bitmap bitmap)
        {
            GrayLevelImage img = new GrayLevelImage();
            img.Allocate(bitmap.Width,bitmap.Height);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    System.Drawing.Color c = bitmap.GetPixel(j,i);
                    img.Value[i][j] = 0;
                    img.Value[i][j] += (double)c.R / (255 * 3);
                    img.Value[i][j] += (double)c.G / (255 * 3);
                    img.Value[i][j] += (double)c.B / (255 * 3);
                }
            }
            return img;
        }
        public static GrayLevelImage TryGetOrigin()
        {
            GrayLevelImage img = new GrayLevelImage();
            System.IO.BinaryReader reader;
            try
            {
                reader = new System.IO.BinaryReader(System.IO.File.OpenRead("原图像.灰度图"));
            }
            catch
            {
                try
                {
                    reader = new System.IO.BinaryReader(System.IO.File.OpenRead("..\\..\\data\\原图像.灰度图"));
                }
                catch {
                    reader = null;
                }
            }
            if (reader != null)
            {
                img.Deserialize(reader);
                reader.Close();
                return img;
            }
            return null;
        }
        public new GrayLevelImage Clone()
        {
            return new GrayLevelImage(base.Clone());
        }
        public GrayLevelImage(Abstract.Graph<double> graph)
        {
            this.Value = graph.Value;
            this.Width = graph.Width;
            this.Height = graph.Height;
        }
        public GrayLevelImage() { }
        public void DrawImage(System.Drawing.Bitmap target)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    try
                    {
                        target.SetPixel(j, i, System.Drawing.Color.FromArgb((int)(Value[i][j] * 255), (int)(Value[i][j] * 255), (int)(Value[i][j] * 255)));
                    }
                    catch
                    {
                        target.SetPixel(j, i, System.Drawing.Color.Red);
                    }
                }
            }
        }
        public void Draw(GrayLevelImage img)
        {
            img.Value = this.Clone().Value;
        }

        public override bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormGrayScaleImage f = new Controls.FormGrayScaleImage(originalImg, this, dataIdent);
            f.ShowDialog();
            return false;
        }
    }
}
