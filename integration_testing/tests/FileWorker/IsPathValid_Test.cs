using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class IsPathValid_Test
    {
        [Fact]
        public void PathIsValid()
        {
            string path = @"C:\2021";

            Assert.True(FileWorker.IsPathValid(path));
        }

        [Fact]
        public void PathIsInvalid()
        {
            string path = "\\2020//";

            Assert.False(FileWorker.IsPathValid(path));
        }

        [Fact]
        public void PathEmptyLine()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.IsPathValid(""));
        }

    }
}
