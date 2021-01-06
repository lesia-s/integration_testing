using IIG.Core.FileWorkingUtils;
using System;
using Xunit;

namespace black_box_testing
{
    public class GetFullPath_Test
    {
        [Fact]
        public void GetFullNonexistentPath()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.GetFullPath(@"C:\itschristmastime\NewYear"));
        }

        [Fact]
        public void GetFullEmptyLinePath()
        {
            Assert.Throws<ArgumentException>(() => FileWorker.GetFullPath(""));
        }

        [Fact]
        public void GetFullCustomPath()
        {
            string path = @"NewYear.txt";

            Assert.Equal(@"C:\Users\lesii\OneDrive\Рабочий стол\тесті\Lab2\black_box_testing\black_box_testing\bin\Debug\netcoreapp3.1\NewYear.txt", FileWorker.GetFullPath(path));
        }

        [Fact]
        public void GetFullPathCommonNameVariousExtensions()
        {
            Assert.Equal(@"C:\FilesTests\NewYear.avi", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.avi"));
            Assert.Equal(@"C:\FilesTests\NewYear.txt", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.txt"));
            Assert.Equal(@"C:\FilesTests\NewYear.doc", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.doc"));
            Assert.Equal(@"C:\FilesTests\NewYear.zip", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.zip"));
            Assert.Equal(@"C:\FilesTests\NewYear.img", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.img"));
            Assert.Equal(@"C:\FilesTests\NewYear.jpg", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.jpg"));
            Assert.Equal(@"C:\FilesTests\NewYear.xls", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.xls"));
            Assert.Equal(@"C:\FilesTests\NewYear.pdf", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.pdf"));
            Assert.Equal(@"C:\FilesTests\NewYear.html", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.html"));
        }

        [Fact]
        public void GetFullPathSpecificNames()
        {
            Assert.Equal(@"C:\FilesTests\Новий Рік.img", FileWorker.GetFullPath(@"C:\FilesTests\Новий Рік.img"));
            Assert.Equal(@"C:\FilesTests\ЇЇЇ", FileWorker.GetFullPath(@"C:\FilesTests\ЇЇЇ"));
            Assert.Equal(@"C:\FilesTests\نئون سال", FileWorker.GetFullPath(@"C:\FilesTests\نئون سال"));
            Assert.Equal(@"C:\FilesTests\नवीन वर्ष", FileWorker.GetFullPath(@"C:\FilesTests\नवीन वर्ष"));
            Assert.Equal(@"C:\FilesTests\新年", FileWorker.GetFullPath(@"C:\FilesTests\新年"));
        }

        [Fact]
        public void GetFullPathSpecificSymbols()
        {
            Assert.Equal(@"C:\FilesTests\🌲🌲🌲", FileWorker.GetFullPath(@"C:\FilesTests\🌲🌲🌲"));
            Assert.Equal(@"C:\FilesTests\💥", FileWorker.GetFullPath(@"C:\FilesTests\💥"));
            Assert.Equal(@"C:\FilesTests\ 🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘", FileWorker.GetFullPath(@"C:\FilesTests\ 🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘"));
            Assert.Equal(@"C:\FilesTests\🦊", FileWorker.GetFullPath(@"C:\FilesTests\🦊"));

        }

        [Fact]
        public void GetFullPathPunctuationMarks()
        {
            Assert.Equal(@"C:\FilesTests\---", FileWorker.GetFullPath(@"C:\FilesTests\---"));
            Assert.Equal(@"C:\FilesTests\New Year", FileWorker.GetFullPath(@"C:\FilesTests\New Year"));
            Assert.Equal(@"C:\FilesTests\.jpg", FileWorker.GetFullPath(@"C:\FilesTests\.jpg"));
            Assert.Equal(@"C:\FilesTests\NewYear.1.xls", FileWorker.GetFullPath(@"C:\FilesTests\NewYear.1.xls"));
            Assert.Equal(@"C:\FilesTests\New_Year.pdf", FileWorker.GetFullPath(@"C:\FilesTests\New_Year.pdf"));
            Assert.Equal(@"C:\FilesTests\New-Year.html", FileWorker.GetFullPath(@"C:\FilesTests\New-Year.html"));
        }
    }
}
