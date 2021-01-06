using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;

namespace integration_testing
{
    [TestClass]
    public class DBBinaryFlagTest
    {
        private const string Server = @"DESKTOP-A3N0DR3";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"73502";
        private const int ConnectionTimeout = 75;

        FlagpoleDatabaseUtils flagpoleDatabaseUtils = new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeout);

        MultipleBinaryFlag flag_1;
        MultipleBinaryFlag flag_2;
        MultipleBinaryFlag flag_3;

        [TestInitialize]
        public void InitializeFlags()
        {
            flag_1 = new MultipleBinaryFlag(7);
            flag_2 = new MultipleBinaryFlag(7, true);
            flag_3 = new MultipleBinaryFlag(7, false);
        }

        [TestMethod]
        public void DBAdIncorrectEmptyFlags()
        {
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("", false));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag(null, true));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag(null, false));
        }

        [TestMethod]
        public void DBAdIncorrectNameFlags()
        {
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("HOHOHO", true));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("HOHOHO", false));
        }

        [TestMethod]
        public void DBAdIncorrectValueFlags()
        {
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag(flag_1.ToString(), false));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag(flag_2.ToString(), false));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), true));

            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("T", false));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("F", true));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("t", false));
            Assert.IsFalse(flagpoleDatabaseUtils.AddFlag("f", true));
        }

        [TestMethod]
        public void DBAddCorrectExistingFlags()
        {
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_1.ToString(), flag_1.GetFlag()));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_2.ToString(), flag_2.GetFlag()));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), flag_3.GetFlag()));
        }
        
        [TestMethod]
        public void DBAddCorrectPartitialNewFlags()
        {
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_1.ToString(), true));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_2.ToString(), true));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), false));

            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_1.ToString(), true));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_2.ToString(), true));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), false));
        }

        [TestMethod]
        public void DBAddCorrectNewFlags()
        {
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("TTTTTTT", true));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("FFFFFFF", false));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("ffff", false));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("tttt", true));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("FfFfF", false));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("TtTtT", true));

            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("FTFTFTFT", false));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("TTTTTTF", false));
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag("TTTttF", false));

        }
        
        [TestMethod]
        public void DBAddSettedFlags()
        {
            flag_1.SetFlag(1);
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_1.ToString(), true));

            flag_2.SetFlag(1);
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_2.ToString(), true));

            flag_3.SetFlag(1);
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), false));

            for (uint i = 0; i < 7; i++)
            {
                flag_3.SetFlag(i);
            }
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), true));
        }
        
        [TestMethod]
        public void DBAddResetedFlags()
        {
            flag_1.ResetFlag(1);
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_1.ToString(), false));

            flag_2.ResetFlag(1);
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_2.ToString(), false));

            flag_3.ResetFlag(1);
            Assert.IsTrue(flagpoleDatabaseUtils.AddFlag(flag_3.ToString(), false));
        }

        [TestMethod]
        public void DBGetNonexistentFlags()
        {
            Assert.IsFalse(flagpoleDatabaseUtils.GetFlag(2021, out _, out _));
            Assert.IsFalse(flagpoleDatabaseUtils.GetFlag(0, out _, out _));
            Assert.IsFalse(flagpoleDatabaseUtils.GetFlag(-2021, out _, out _));
        }

        [TestMethod]
        public void DBGetExistentFlags()
        {
            string name;
            bool? value;

            flagpoleDatabaseUtils.GetFlag(1, out name, out value);
            Assert.AreEqual("TTTTTTT", name);
            Assert.AreEqual(true, value);

            flagpoleDatabaseUtils.GetFlag(20, out name, out value);
            Assert.AreEqual("FfFfF", name);
            Assert.AreEqual(false, value);

            flagpoleDatabaseUtils.GetFlag(41, out name, out value);
            Assert.AreEqual("TTTTTTF", name);
            Assert.AreEqual(false, value);
        }
    }
}
