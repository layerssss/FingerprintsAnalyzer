using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Abstract
{
    public class Array<T>:List<T>
    {
        /// <summary>
        /// Allocates the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="value">The value.</param>
        public void Allocate(int count, T value)
        {
            this.Clear();
            for (int i = 0; i < count; i++)
            {
                this.Add(value);
            }
        }
        /// <summary>
        /// Allocates the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void Allocate(int count)
        {
            Allocate(count, default(T));
        }
        public Array<T2> Clone<T2>(Func<T, T2> trans)
        {
            Array<T2> ta = new Array<T2>();
            this.ForEach(ti => { ta.Add(trans(ti)); });
            return ta;
        }
        public Array<T> Clone()
        {
            return this.Clone(ti => ti);
        }
    }
}
