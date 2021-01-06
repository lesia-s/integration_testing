using IIG.Core.FileWorkingUtils;
using System;
using System.IO;
using Xunit;

namespace black_box_testing
{
    public class TryCopy_Test
    {

        [Fact]
        public void TryCopyNonexistentPath()
        {
            Assert.False(FileWorker.TryCopy("holidays", @"C:\FilesTests\copy.txt", false, 1));
            Assert.False(FileWorker.TryCopy("holidays", @"C:\FilesTests\copy.txt", true, 1));
        }

        [Fact]
        public void TryCopyText()
        {
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Songs.txt", @"C:\FilesTests\Songs_1.txt", false, 1));
        }

        [Fact]
        public void TryCopyEmptyFile()
        {
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Movies.txt", @"C:\FilesTests\Movies1.txt", true, 1));

            Assert.Equal("", FileWorker.ReadAll(@"C:\FilesTests\Movies1.txt"));
        }

        [Fact]
        public void TryCopyExistingFile()
        {
            Assert.Throws<IOException>(() => FileWorker.TryCopy(@"C:\FilesTests\Songs.txt", @"C:\FilesTests\Copied songs.txt", false, 1));
        }

        [Fact]
        public void TryCopyVariousFileExtensions()
        {
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Home alone.mp4", @"C:\FilesTests\More Home alone.mp4", true, 1));
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\ChristmasTree.png", @"C:\FilesTests\More ChristmasTree.png", true, 1));
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Presents.zip", @"C:\FilesTests\More presents.zip", true, 1));
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Rudolf.pdf", @"C:\FilesTests\More Rudolf.pdf", true, 1));
        }

        [Fact]
        public void TryCopyRewriteFile()
        {
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Songs.txt", @"C:\FilesTests\Jingle Bells!.txt", true, 1));
            Assert.True(FileWorker.TryCopy(@"C:\FilesTests\Songs.txt", @"C:\FilesTests\Christmas songs.txt", true, 1));
        }
    }
}
