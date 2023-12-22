using Xunit;

namespace Robot.Tests
{
    public class TerrainTest
    {
        [Theory]
        [MemberData(nameof(TerrainTestData.InBoundData), MemberType = typeof(TerrainTestData))]
        public void Terrain_Should_ReportTrue_If_LocationIsWithin(int rows, int columns, int x, int y)
        {
            var terrain = new Terrain(rows, columns);
            Assert.True(terrain.Contains(new Location(x, y)));
        }

        [Theory]
        [MemberData(nameof(TerrainTestData.OutOfBoundData), MemberType = typeof(TerrainTestData))]
        public void Terrain_Should_ReportFalse_If_LocationIsNotWithin(int rows, int columns, int x, int y)
        {
            var terrain = new Terrain(rows, columns);
            Assert.False(terrain.Contains(new Location(x, y)));
        }
    }
}