using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class ReadAll_Test
    {
        [Fact]
        public void ReadAllNonexistentPath()
        {
            Assert.Null(FileWorker.ReadAll("holidays"));
        }

        [Fact]
        public void ReadAllEmptyLine()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.ReadAll(""));
        }

        [Fact]
        public void ReadAllText()
        {
            Assert.Equal("Jingle bells, jingle bells\r\nJingle all the way\r\nOh, what fun it is to ride\r\nIn a one horse open sleigh", FileWorker.ReadAll(@"C:\FilesTests\Songs.txt"));
        }

        [Fact]
        public void ReadAllEmptyFile()
        {
            Assert.Equal("", FileWorker.ReadAll(@"C:\FilesTests\Movies.txt"));
        }

        [Fact]
        public void ReadAllVariousFileExtensions()
        {
            Assert.NotNull(FileWorker.ReadAll(@"C:\FilesTests\Home alone.mp4"));
            Assert.NotNull(FileWorker.ReadAll(@"C:\FilesTests\ChristmasTree.png"));
            Assert.NotNull(FileWorker.ReadAll(@"C:\FilesTests\Presents.zip"));
            Assert.NotNull(FileWorker.ReadAll(@"C:\FilesTests\Rudolf.pdf"));
        }
    }
}
