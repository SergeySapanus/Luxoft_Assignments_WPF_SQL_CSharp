using System;
using System.Collections.Generic;

namespace MathHelpers
{
    public class WPFAssignmentHelper
    {
        #region Constants

        private const ulong COUNT_NUMBERS_PER_CHUNK = 10UL;

        #endregion Constants

        public static ulong Power2(ulong input)
        {
            unchecked
            {
                return (ulong)Math.Pow(input, 2);
            }
        }

        public static IEnumerable<ulong> GetPrimesByEratosthenesSieve(ulong sqrtLimit, ulong limit)
        {
            var rangeFrom = 0UL;

            var countChunks = limit / COUNT_NUMBERS_PER_CHUNK;
            if (limit % COUNT_NUMBERS_PER_CHUNK > 0)
                countChunks++;

            for (var p = 1UL; p <= countChunks; p++)
            {
                var rangeTo = p * COUNT_NUMBERS_PER_CHUNK;
                if (rangeTo > limit)
                    rangeTo = limit;

                foreach (var prime in GetPrimesByEratosthenesSieve(sqrtLimit, rangeFrom, rangeTo))
                    yield return prime;

                rangeFrom = rangeTo;
            }
        }

        private static IEnumerable<ulong> GetPrimesByEratosthenesSieve(ulong sqrtLimit, ulong rangeFrom, ulong rangeTo)
        {
            var countNumbersPerChunk = rangeTo - rangeFrom;
            if (countNumbersPerChunk <= 0UL)
                throw new ArgumentException($"{nameof(rangeFrom)}, {nameof(rangeTo)}");

            var composite = new bool[countNumbersPerChunk + 1];

            var i = 1UL;

            for (; i <= sqrtLimit; i++)
            {
                // Skip number 1.
                if (i == 1L)
                {
                    continue;
                }

                // Chunk's scope. Skip not prime number.
                if (i >= rangeFrom && composite[i - rangeFrom])
                {
                    continue;
                }

                // Chunk's scope. Return prime number.
                if (i >= rangeFrom)
                {
                    yield return i;
                }

                // End of chunk. Exit. 
                var pwr2 = i * i;
                if (pwr2 > rangeTo)
                {
                    break;
                }

                // Mark not prime number.
                for (var j = pwr2; j <= rangeTo; j += i)
                {
                    if (j >= rangeFrom)
                    {
                        composite[j - rangeFrom] = true;
                    }
                }
            }

            if (i <= sqrtLimit)
                i++;

            // Return not marked primes.
            for (; i <= rangeTo; i++)
            {
                if (i >= rangeFrom && !composite[i - rangeFrom])
                {
                    yield return i;
                }
            }
        }
    }
}