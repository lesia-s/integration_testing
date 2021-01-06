using IIG.Core.FileWorkingUtils;
using System;
using System.IO;
using Xunit;

namespace black_box_testing
{
    public class TryWrite_Test
    {
        [Fact]
        public void TryWriteText()
        {
            Assert.True(FileWorker.TryWrite("Home alone", @"C:\FilesTests\American movies.txt", 1));
        }

        [Fact]
        public void TryCopyEmptyFile()
        {
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Movies.txt", @"C:\FilesTests\Movies1.txt", true, 1));

            Assert.Equal("", FileWorker.ReadAll(@"C:\FilesTests\Movies1.txt"));
        }

        [Fact]
        public void TryWriteEmptyLine()
        {
            Assert.True(FileWorker.TryWrite("", @"C:\FilesTests\NEW.txt", 1));
        }

        [Fact]
        public void TryWriteExistingFile()
        {
            Assert.True(FileWorker.TryWrite("ho-ho-ho", @"C:\FilesTests\ho-ho-ho.txt", 1));
        }

        [Fact]
        public void TryWriteVariousFileExtensions()
        {
            Assert.True(FileWorker.TryWrite("No more champagne", @"C:\FilesTests\Happy new year.mp4", 1));
            Assert.True(FileWorker.TryWrite("And the fireworks are through", @"C:\FilesTests\Happy new year.png", 1));
            Assert.True(FileWorker.TryWrite("Here we are, me and you", @"C:\FilesTests\Happy new year.zip", 1));
            Assert.True(FileWorker.TryWrite("Feeling lost and feeling blue", @"C:\FilesTests\Happy new year.pdf", 1));
        }
    }
}
