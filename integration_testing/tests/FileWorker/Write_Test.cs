using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class Write_Test
    {
        [Fact]
        public void WriteNonexistentPath()
        {
            Assert.True(FileWorker.Write("last christmas", "lights"));
        }

        [Fact]
        public void TryWriteText()
        {
            Assert.True(FileWorker.Write("Home alone", @"C:\FilesTests\American christmas movies.txt"));
        }

        [Fact]
        public void WriteEmptyLine()
        {
            Assert.True(FileWorker.Write("", @"C:\FilesTests\NEW1.txt"));
        }

        [Fact]
        public void WriteExistingFile()
        {
            Assert.True(FileWorker.Write("ho-ho-ho", @"C:\FilesTests\ho-ho-ho.txt"));
        }

        [Fact]
        public void TryWriteVariousFileExtensions()
        {
            Assert.True(FileWorker.Write("Last Christmas I gave you my heart", @"C:\FilesTests\Merry Christmas.mp4"));
            Assert.True(FileWorker.Write("But the very next day you gave it away", @"C:\FilesTests\Merry Christmas.png"));
            Assert.True(FileWorker.Write("This year, to save me from tears", @"C:\FilesTests\Merry Christmas.zip"));
            Assert.True(FileWorker.Write("I'll give it to someone special", @"C:\FilesTests\Merry Christmas.pdf"));
        }
    }
}
