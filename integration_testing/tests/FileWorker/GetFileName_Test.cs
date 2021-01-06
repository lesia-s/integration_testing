using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class GetFileName_Test
    {
        [Fact]
        public void GetFileNameNonexistentPath()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.GetFileName(@"C:\itschristmastime\NewYear"));
        }

        [Fact]
        public void GetFileNameEmptyLineName()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.GetFileName(""));
        }

        [Fact]
        public void GetFileNameCommonNameVariousExtensions()
        {
            Assert.Equal("NewYear.avi", FileWorker.GetFileName(@"C:\FilesTests\NewYear.avi"));
            Assert.Equal("NewYear.txt", FileWorker.GetFileName(@"C:\FilesTests\NewYear.txt"));
            Assert.Equal("NewYear.doc", FileWorker.GetFileName(@"C:\FilesTests\NewYear.doc"));
            Assert.Equal("NewYear.zip", FileWorker.GetFileName(@"C:\FilesTests\NewYear.zip"));
            Assert.Equal("NewYear.img", FileWorker.GetFileName(@"C:\FilesTests\NewYear.img"));
            Assert.Equal("NewYear.jpg", FileWorker.GetFileName(@"C:\FilesTests\NewYear.jpg"));
            Assert.Equal("NewYear.xls", FileWorker.GetFileName(@"C:\FilesTests\NewYear.xls"));
            Assert.Equal("NewYear.pdf", FileWorker.GetFileName(@"C:\FilesTests\NewYear.pdf"));
            Assert.Equal("NewYear.html", FileWorker.GetFileName(@"C:\FilesTests\NewYear.html"));
        }

        [Fact]
        public void GetFileNamePunctuationMarks()
        {
            Assert.Equal("---", FileWorker.GetFileName(@"C:\FilesTests\---"));
            Assert.Equal("New Year", FileWorker.GetFileName(@"C:\FilesTests\New Year"));
            Assert.Equal(".jpg", FileWorker.GetFileName(@"C:\FilesTests\.jpg"));
            Assert.Equal("New.Year.xls", FileWorker.GetFileName(@"C:\FilesTests\New.Year.xls"));
            Assert.Equal("New_Year.pdf", FileWorker.GetFileName(@"C:\FilesTests\New_Year.pdf"));
            Assert.Equal("New-Year.html", FileWorker.GetFileName(@"C:\FilesTests\New-Year.html"));
        }

        [Fact]
        public void GetFileNameSpecificSymbols()
        {
            Assert.Equal("🌲🌲🌲", FileWorker.GetFileName(@"C:\FilesTests\🌲🌲🌲"));
            Assert.Equal("💥", FileWorker.GetFileName(@"C:\FilesTests\💥"));
            Assert.Equal(" 🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘", FileWorker.GetFileName(@"C:\FilesTests\ 🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘"));
            Assert.Equal("🦊", FileWorker.GetFileName(@"C:\FilesTests\🦊"));
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
