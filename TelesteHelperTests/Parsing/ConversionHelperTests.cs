using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.TelesteHelper.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.DataMiner.TelesteHelper.Parsing.Tests
{
    [TestClass()]
    public class ConversionHelperTests
    {
        [TestMethod()]
        public void GetBitValueTest1()
        {
            byte data = 0xFF; // 1111 1111
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 0));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 1));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 2));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 3));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 4));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 5));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 6));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 7));
        }

        [TestMethod()]
        public void GetBitValueTest2()
        {
            byte data = 0x00; // 0000 0000
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 0));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 1));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 2));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 3));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 4));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 5));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 6));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 7));
        }

        [TestMethod()]
        public void GetBitValueTest3()
        {
            byte data = 0x99; // 1001 1001
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 0));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 1));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 2));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 3));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 4));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 5));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 6));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 7));
        }

        [TestMethod()]
        public void GetBitValueTest4()
        {
            byte[] data = new byte[] { 0xFF, 0x00 }; // 1111 1111 0000 0000
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 0));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 1));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 2));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 3));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 4));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 5));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 6));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 7));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 8));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 9));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 10));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 11));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 12));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 13));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 14));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 15));
        }

        [TestMethod()]
        public void GetBitValueTest5()
        {
            byte[] data = new byte[] { 0x98, 0xF9 }; // 1001 1000 1111 1001
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 0));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 1));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 2));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 3));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 4));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 5));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 6));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 7));

            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 8));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 9));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 10));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 11));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 12));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 13));
            Assert.AreEqual(0, ConversionHelper.GetBitValue(data, 14));
            Assert.AreEqual(1, ConversionHelper.GetBitValue(data, 15));
        }
    }
}