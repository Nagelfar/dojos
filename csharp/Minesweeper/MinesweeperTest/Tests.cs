using System;
using Xunit;
using Minesweeper;
namespace MinesweeperTest
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            Program.Main(new string[0]);
            Assert.True(true);
        }
    }
}