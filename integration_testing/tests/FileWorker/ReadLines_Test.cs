using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class ReadLines_Test
    {
        [Fact]
        public void ReadLinesNonexistentPath()
        {
            Assert.Null(FileWorker.ReadLines("holidays"));
        }

        [Fact]
        public void ReadLinesEmptyName()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.ReadLines(""));
        }

        [Fact]
        public void ReadLinesEmptyFile()
        {
            string[] exp = { };

            Assert.Equal(exp, FileWorker.ReadLines(@"C:\FilesTests\Movies.txt"));
        }

        [Fact]
        public void ReadLinesVariousFileExtensions()
        {
            Assert.NotNull(FileWorker.ReadLines(@"C:\FilesTests\Home alone.mp4"));
            Assert.NotNull(FileWorker.ReadLines(@"C:\FilesTests\ChristmasTree.png"));
            Assert.NotNull(FileWorker.ReadLines(@"C:\FilesTests\Presents.zip"));
            Assert.NotNull(FileWorker.ReadLines(@"C:\FilesTests\Rudolf.pdf"));
        }

        [Fact]
        public void ReadLinesText()
        {
            string[] expected = { "Jingle bells, jingle bells", "Jingle all the way", "Oh, what fun it is to ride", "In a one horse open sleigh" };

            Assert.Equal(expected, FileWorker.ReadLines(@"C:\FilesTests\Songs.txt"));
        }
    }
}
