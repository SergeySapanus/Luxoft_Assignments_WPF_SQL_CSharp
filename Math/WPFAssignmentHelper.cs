using System;
using System.Collections.Generic;

namespace MathHelpers
{
    public class WPFAssignmentHelper
    {
        private const long COUNT_NUMBERS_PER_CHUNK = 10;

        public static IEnumerable<long> GetPrimesByEratosthenesSieve(long sqrtLimit, long limit)
        {
            var rangeFrom = 0L;

            var countChunks = limit / COUNT_NUMBERS_PER_CHUNK;
            if (limit % COUNT_NUMBERS_PER_CHUNK > 0)
                countChunks++;

            for (var p = 1; p <= countChunks; p++)
            {
                var rangeTo = p * COUNT_NUMBERS_PER_CHUNK;
                if (rangeTo > limit)
                    rangeTo = limit;

                foreach (var prime in GetPrimesByEratosthenesSieve(sqrtLimit, rangeFrom, rangeTo))
                    yield return prime;

                rangeFrom = rangeTo;
            }
        }

        private static IEnumerable<long> GetPrimesByEratosthenesSieve(long sqrtLimit, long rangeFrom, long rangeTo)
        {
            if (sqrtLimit < 2L)
                throw new ArgumentException(nameof(sqrtLimit));

            var countNumbersPerChunk = rangeTo - rangeFrom;
            if (countNumbersPerChunk <= 0)
                throw new ArgumentException($"{nameof(rangeFrom)}, {nameof(rangeTo)}");

            var composite = new bool[countNumbersPerChunk + 1];

            var i = 1L;

            for (; i <= sqrtLimit; i++)
            {
                // Skip number 1.
                if (i == 1L)
                {
                    continue;
                }

                // Chunk's scope. Skip not prime number.
                var indexInChunk = i - rangeFrom;
                if (indexInChunk >= 0 && composite[indexInChunk])
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
                    indexInChunk = j - rangeFrom;
                    if (indexInChunk >= 0)
                        composite[indexInChunk] = true;
                }
            }

            i++;

            // Return not marked primes.
            for (; i <= rangeTo; i++)
            {
                var indexInChunk = i - rangeFrom;
                if (indexInChunk >= 0 && !composite[indexInChunk])
                    yield return i;
            }
        }
    }
}