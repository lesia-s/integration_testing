using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class MkDir_Test
    {
        [Fact]
        public void MakeDirectoryEmptyLine()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(""));
        }

        [Fact]
        public void MakeDirectoryReservedNames()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\AUX"));
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\CON"));
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\PRN"));
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\LPT3"));
        }

        [Fact]
        public void MakeDirectoryForbiddenSymbols()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\NewYear?"));
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\New*Year"));
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\New|Year"));
            Assert.Throws<ArgumentException>(() => FileWorker.MkDir(@"C:\FilesTests\New\Year"));
        }

        [Fact]
        public void MakeDirectoryPunctuationMarks()
        {
            Assert.Equal(@"C:\FilesTests\---", FileWorker.MkDir(@"C:\FilesTests\---"));
            Assert.Equal(@"C:\FilesTests\New Year", FileWorker.MkDir(@"C:\FilesTests\New Year"));
            Assert.Equal(@"C:\FilesTests\.jpg", FileWorker.MkDir(@"C:\FilesTests\.jpg"));
            Assert.Equal(@"C:\FilesTests\NewYear.1", FileWorker.MkDir(@"C:\FilesTests\NewYear.1"));
            Assert.Equal(@"C:\FilesTests\New_Year", FileWorker.MkDir(@"C:\FilesTests\New_Year"));
            Assert.Equal(@"C:\FilesTests\New-Year", FileWorker.MkDir(@"C:\FilesTests\New-Year"));
        }

        [Fact]
        public void MakeDirectorySpecificSymbols()
        {
            Assert.Equal(@"C:\FilesTests\🌲🌲🌲", FileWorker.MkDir(@"C:\FilesTests\🌲🌲🌲"));
            Assert.Equal(@"C:\FilesTests\💥", FileWorker.MkDir(@"C:\FilesTests\💥"));
            Assert.Equal(@"C:\FilesTests\ 🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘", FileWorker.MkDir(@"C:\FilesTests\ 🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘"));
            Assert.Equal(@"C:\FilesTests\🦊", FileWorker.MkDir(@"C:\FilesTests\🦊"));
        }

        [Fact]
        public void MakeDirectoryNonLatinLetters()
        {
            Assert.Equal(@"C:\FilesTests\Новий Рік", FileWorker.MkDir(@"C:\FilesTests\Новий Рік"));
            Assert.Equal(@"C:\FilesTests\ЇЇЇ", FileWorker.MkDir(@"C:\FilesTests\ЇЇЇ"));
            Assert.Equal(@"C:\FilesTests\نئون سال", FileWorker.MkDir(@"C:\FilesTests\نئون سال"));
            Assert.Equal(@"C:\FilesTests\नवीन वर्ष", FileWorker.MkDir(@"C:\FilesTests\नवीन वर्ष"));
            Assert.Equal(@"C:\FilesTests\新年", FileWorker.MkDir(@"C:\FilesTests\新年"));
        }

        [Fact]
        public void GetFileNameNonLatinLetters()
        {
            Assert.Equal("Новий Рік.img", FileWorker.GetFileName(@"C:\FilesTests\Новий Рік.img"));
            Assert.Equal("ЇЇЇ", FileWorker.GetFileName(@"C:\FilesTests\ЇЇЇ"));
            Assert.Equal("نئون سال", FileWorker.GetFileName(@"C:\FilesTests\نئون سال"));
            Assert.Equal("नवीन वर्ष", FileWorker.GetFileName(@"C:\FilesTests\नवीन वर्ष"));
            Assert.Equal("新年", FileWorker.GetFileName(@"C:\FilesTests\新年"));
        }
    }
}
