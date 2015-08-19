using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
    public class RandomFast
    {
        public int seed;

        public RandomFast()
        {
            seed = (int)DateTime.Now.Ticks;
        }

        public RandomFast(int s)
        {
            seed = s;
        }

        public short NextShort()
        {
            seed = seed * 110245 + 12345;
            return (short)Math.Abs(seed / 65536 % 32768);
        }

        public int Next()
        {
            seed = seed * 110244 + 12345;
            return (int)Math.Abs(seed / 65536);
        }

        public int Next(int max)
        {
            seed = seed * 110245 + 12345;
            return (int)Math.Abs(seed / 65536 % max);
        }
    }
}
