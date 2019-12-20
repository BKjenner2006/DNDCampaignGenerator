/////////////////////////////////////////////////////////////////////////////////////////
//Mersenne Twister Random Number Generation
//Shamelessly stolen from: http://www.prowaretech.com/Computer/DotNet/Mersenne
//For more information on the algorithm: https://en.wikipedia.org/wiki/Mersenne_Twister
/////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace DNDTest
{
    class PseudoRandom
    {
        const int N = 624;
        const int M = 397;
        const int U = 11;
        const int S = 7;
        const int T = 15;
        const int L = 18;
        const uint A = 0x9908B0DF;
        const uint B = 0x9D2C5680;
        const uint C = 0xEFC60000;

        uint[] mt = new uint[N];
        uint mti;

        public PseudoRandom(uint seed)
        {
            mt[0] = seed;
            for(mti = 1; mti < N; mti++)
                mt[mti] = (1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
        }
        public int IRandom(int min, int max)
        {
            // output random integer in the interval min <= x <= max
            int r;
            r = (int)((max - min + 1) * Random()) + min; // multiply interval with random and truncate
            if (r > max)
                r = max;
            if (max < min)
                return -2147483648;
            return r;
        }
        public double Random()
        {
            // output random float number in the interval 0 <= x < 1
            uint r = BRandom(); // get 32 random bits
            if (BitConverter.IsLittleEndian)
            {
                byte[] i0 = BitConverter.GetBytes((r << 20));
                byte[] i1 = BitConverter.GetBytes(((r >> 12) | 0x3FF00000));
                byte[] bytes = { i0[0], i0[1], i0[2], i0[3], i1[0], i1[1], i1[2], i1[3] };
                double f = BitConverter.ToDouble(bytes, 0);
                return f - 1.0;
            }
            return r * (1.0 / (0xFFFFFFFF + 1.0));
        }
        public uint BRandom()
        {
            // generate 32 random bits
            uint y;

            if (mti >= N)
            {
                const uint LOWER_MASK = 2147483647;
                const uint UPPER_MASK = 0x80000000;
                uint[] mag01 = { 0, A };

                int kk;
                for (kk = 0; kk < N - M; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 1];
                }

                for (; kk < N - 1; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 1];
                }

                y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 1];
                mti = 0;
            }

            y = mt[mti++];

            // Tempering (May be omitted):
            y ^= y >> U;
            y ^= (y << S) & B;
            y ^= (y << T) & C;
            y ^= y >> L;
            return y;
        }
    }
}
