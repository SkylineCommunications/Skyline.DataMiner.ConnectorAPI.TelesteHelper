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
    public class MaskTests
    {
        [TestMethod()]
        public void MaskTest1()
        {
            byte[] receivedMask = new byte[] { 0x00, 0x00, 0x3F, 0xFF };
            Mask mask = new Mask(receivedMask, 0);

            List<int> expectedOutput = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            List<int> output = mask.Parameters;

            Assert.AreEqual(expectedOutput.Count, output.Count);
            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i], output[i]);
            }
        }

        [TestMethod()]
        public void MaskTest2()
        {
            byte[] receivedMask = new byte[] { 0x00, 0x1F, 0x32, 0xFF };
            Mask mask = new Mask(receivedMask, 10);

            List<int> expectedOutput = new List<int> { 10, 11, 12, 13, 14, 15, 16, 17, 19, 22, 23, 26, 27, 28, 29, 30 };
            List<int> output = mask.Parameters;

            Assert.AreEqual(expectedOutput.Count, output.Count);
            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i], output[i]);
            }
        }

        [TestMethod()]
        public void MaskTest3()
        {
            byte[] receivedMask = new byte[] { 0x01, 0x00, 0x1F, 0x32, 0x00 };
            Mask mask = new Mask(receivedMask, 10);

            List<int> expectedOutput = new List<int> { 19, 22, 23, 26, 27, 28, 29, 30, 42 };
            List<int> output = mask.Parameters;

            Assert.AreEqual(expectedOutput.Count, output.Count);
            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i], output[i]);
            }
        }
    }
}