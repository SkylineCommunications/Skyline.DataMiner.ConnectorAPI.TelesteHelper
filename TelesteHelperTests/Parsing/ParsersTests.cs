using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.TelesteHelper.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.DataMiner.TelesteHelper.Parsing.Tests
{
    [TestClass()]
    public class ParsersTests
    {
        [TestMethod()]
        public void ParseMacAddressTest()
        {
            byte[] input = new byte[] { 0, 144, 80, 7, 203, 99 };
            string expectedOutput = "00905007CB63";
            string output = Parsers.ParseMacAddress(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseSoftwareVersionTest1()
        {
            byte[] input = new byte[] { 72, 13 };
            string expectedOutput = "4.8.13";
            string output = Parsers.ParseSoftwareVersion(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseSoftwareVersionTest2()
        {
            byte[] input = new byte[] { 9, 13 };
            string expectedOutput = "9.13";
            string output = Parsers.ParseSoftwareVersion(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseSoftwareVersionTest3()
        {
            byte[] input = new byte[] { 16, 13 };
            string expectedOutput = "1.0.13";
            string output = Parsers.ParseSoftwareVersion(input);

            Assert.AreEqual(expectedOutput, output);
        }
        
        [TestMethod()]
        public void ParseSoftwareVersionTest4()
        {
            byte[] input = new byte[] { 0, 175 };
            string expectedOutput = "0.175";
            string output = Parsers.ParseSoftwareVersion(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseRepliesTest()
        {
            byte[] input = new byte[] { 5, 24, 217, 163 };
            uint expectedOutput = 85514659;
            uint output = Parsers.ParseReplies(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseFailuresTest()
        {
            byte[] input = new byte[] { 0, 152 };
            uint expectedOutput = 152;
            uint output = Parsers.ParseFailures(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseTxLevelToDecibelMicroVoltTest1()
        {
            byte input = 162;
            int expectedOutput = 98;
            int output = Parsers.ParseTxLevelToDecibelMicroVolt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseTxLevelToDecibelMilliVoltTest1()
        {
            byte input = 162;
            int expectedOutput = 38;
            int output = Parsers.ParseTxLevelToDecibelMilliVolt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseTxLevelToDecibelMicroVoltTest2()
        {
            byte input = 21;
            int expectedOutput = 85;
            int output = Parsers.ParseTxLevelToDecibelMicroVolt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseTxLevelToDecibelMilliVoltTest2()
        {
            byte input = 21;
            int expectedOutput = 25;
            int output = Parsers.ParseTxLevelToDecibelMilliVolt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseRxLevelToDecibelMicroVoltTest()
        {
            byte input = 69;
            int expectedOutput = 69;
            int output = Parsers.ParseRxLevelToDecibelMicroVolt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseRxLevelToDecibelMilliVoltTest()
        {
            byte input = 69;
            int expectedOutput = 9;
            int output = Parsers.ParseRxLevelToDecibelMilliVolt(input);

            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod()]
        public void ParseSubnetMaskTest()
        {
            byte input = 33;
            IPAddress expectedOutput = new IPAddress(new byte[] { 255, 255, 240, 0 });
            IPAddress output = Parsers.ParseSubnetMask(input);

            Assert.AreEqual(expectedOutput, output);
        }
    }
}