using System.Collections.Generic;
using System.Linq;

namespace Robot.Tests
{
    public class TerrainTestData
    {
        public static IEnumerable<object[]> InBoundData
        {
            get
            {
                int rows = 5;
                int columns = 5;

                foreach (var x in Enumerable.Range(0, columns))
                {
                    foreach (var y in Enumerable.Range(0, rows))
                    {
                        yield return new object[] { rows, columns, x, y };
                    }
                }
            }
        }

        public static IEnumerable<object[]> OutOfBoundData
        {
            get
            {
                int rows = 5;
                int columns = 5;

                foreach (var x in Enumerable.Range(0, columns))
                {
                    foreach (var y in Enumerable.Range(0, rows))
                    {
                        yield return new object[] { rows, columns, x - columns, y - rows };
                        yield return new object[] { rows, columns, x + columns, y + rows };
                    }
                }
            }
        }
    }
}