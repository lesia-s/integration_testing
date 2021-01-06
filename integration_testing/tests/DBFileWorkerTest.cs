using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;
using IIG.FileWorker;
using IIG.CoSFE.DatabaseUtils;

namespace integration_testing
{
    [TestClass]
    public class DBFileWorkerTest
    {
        private const string Server = @"DESKTOP-A3N0DR3";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"73502";
        private const int ConnectionTimeout = 75;

        StorageDatabaseUtils storageDatabaseUtils = new StorageDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);

        [TestMethod]
        public void DBAddEmptyFile()
        {
            Assert.IsTrue(storageDatabaseUtils.AddFile("nothing here.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\nothing here.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("and here.txt", new byte[0]));
        }
        
        [TestMethod]
        public void DBAddFileEmptyLine()
        {
            Assert.IsFalse(storageDatabaseUtils.AddFile("", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\nothing here.txt"))));
            Assert.IsFalse(storageDatabaseUtils.AddFile("completely nothing.txt", null));
        }

        [TestMethod]
        public void DBAddVariousFiles()
        {
            Assert.IsTrue(storageDatabaseUtils.AddFile("Jingle Bells.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\Jingle Bells.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("Last Christmas.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\Last Christmas.txt"))));
        }
        
        [TestMethod]
        public void DBAddSpecificSymbols()
        {
            Assert.IsTrue(storageDatabaseUtils.AddFile("🌲🌲🌲.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\🌲🌲🌲.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("💥.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\💥.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\🌑 🌒 🌓 🌔 🌕 🌖 🌗 🌘.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("🦊.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\🦊.txt"))));
        }

        [TestMethod]
        public void DBAddNonLatinLetters()
        {
            Assert.IsTrue(storageDatabaseUtils.AddFile("Новий Рік.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\Новий Рік.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("ЇЇЇ.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\ЇЇЇ.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("نئون سال.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\نئون سال.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("नवीन वर्ष.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\नवीन वर्ष.txt"))));
            Assert.IsTrue(storageDatabaseUtils.AddFile("新年.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\新年.txt"))));
        }
        
        [TestMethod]
        public void DBGetFilesNotNull()
        {
            Assert.IsNotNull(storageDatabaseUtils.GetFiles());
            Assert.IsNotNull(storageDatabaseUtils.GetFiles("and here.txt"));
            Assert.IsNotNull(storageDatabaseUtils.GetFiles("Last Christmas.txt"));
        }

        [TestMethod]
        public void DBGetNonExistingFile()
        {
            string name;
            byte[] outputText;

            Assert.IsFalse(storageDatabaseUtils.GetFile(0, out name, out outputText));
            Assert.IsNull(name);
            Assert.IsNull(outputText);

            Assert.IsFalse(storageDatabaseUtils.GetFile(2021, out name, out outputText));
            Assert.IsNull(name);
            Assert.IsNull(outputText);

            Assert.IsFalse(storageDatabaseUtils.GetFile(-2021, out name, out outputText));
            Assert.IsNull(name);
            Assert.IsNull(outputText);
        }
        
        [TestMethod]
        public void DBGetExistingFiles()
        {
            string name;
            byte[] outputText;

            Assert.IsTrue(storageDatabaseUtils.GetFile(5, out name, out outputText));
            Assert.AreEqual("Jingle Bells.txt", name);
            Assert.IsTrue(outputText.SequenceEqual(Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\Jingle Bells.txt"))));
            string jingleBells = "Jingle bells, jingle bells\r\nJingle all the way\r\nOh, what fun it is to ride\r\nIn a one horse open sleigh";
            Assert.IsTrue(outputText.SequenceEqual(Encoding.Unicode.GetBytes(jingleBells)));

            Assert.IsTrue(storageDatabaseUtils.GetFile(14, out name, out outputText));
            Assert.AreEqual("🌲🌲🌲.txt", name);
            Assert.IsTrue(outputText.SequenceEqual(Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\🌲🌲🌲.txt"))));

            Assert.IsTrue(storageDatabaseUtils.GetFile(13, out name, out outputText));
            Assert.AreEqual("新年.txt", name);
            Assert.IsTrue(outputText.SequenceEqual(Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll(@"C:\\integration_testing\\integration_testing\\input\\新年.txt"))));
        }
        
        [TestMethod]
        public void DBDeleteExistingFiles()
        {
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(1));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(3));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(7));
        }

        [TestMethod]
        public void DBDeleteNonExistingFiles()
        {
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(2021));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(-2021));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(1000000000));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(-100000000));
        }
    }
}
