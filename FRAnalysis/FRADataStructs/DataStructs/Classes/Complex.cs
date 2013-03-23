using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Classes
{
    public class Complex : ICloneable
    {
        public double R;
        public double I;
        //string ss;
        public Complex(double r, double i)
        {
            this.R = r;
            this.I = i;
        }
        public static Complex operator *(Complex source, Complex target)
        {
            return new Complex(source.R * target.R - source.I * target.I, source.R * target.I + source.I * target.R);
        }
        public static Complex operator *(Complex s, double d)
        {
            return new Complex(s.R * d, s.I * d);
        }
        public static Complex operator /(Complex s, double d)
        {
            return new Complex(s.R / d, s.I / d);
        }
        public static Complex operator +(Complex s, Complex t)
        {
            return new Complex(s.R + t.R, s.I + t.I);
        }
        public static Complex operator -(Complex s, Complex t)
        {
            return new Complex(s.R - t.R, s.I - t.I);
        }

        public object Clone()
        {
            return new Complex(this.R, this.I);
        }
    }
}
