using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyline.DataMiner.TelesteHelper.AlarmLimits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.DataMiner.TelesteHelper.AlarmLimits.Tests
{
    [TestClass()]
    public class AnalogAlarmLimitTests
    {
        [TestMethod()]
        public void FromResponseTest()
        {
            byte[] input = new byte[]
            {
                0x01, 0x00, 0x00, 0x01, 0x00,
                0x00, 0x0C, 0xE4, 0x1B, 0x58,
                0x17, 0x70, 0x01, 0xF4, 0xFC,
                0x18, 0x00, 0x64, 0xF8, 0x30,
                0x2A, 0xF8, 0x69
            };

            AnalogAlarmLimit output = new AnalogAlarmLimit(input);

            Assert.AreEqual(1, output.ParamId);
            Assert.AreEqual(0, output.ParamIdx);
            Assert.AreEqual(AnalogAlarmState.Nominal, output.State);
            Assert.AreEqual(0, output.StateValue);
            Assert.AreEqual(33d, output.Value);
            Assert.AreEqual(70d, output.HiHi);
            Assert.AreEqual(60d, output.Hi);
            Assert.AreEqual(5d, output.Lo);
            Assert.AreEqual(-10d, output.LoLo);
            Assert.AreEqual(1d, output.Deadband);
            Assert.AreEqual(-20d, output.MinValue);
            Assert.AreEqual(110d, output.MaxValue);
            Assert.AreEqual(Severity.Major, output.HiHiSeverity);
            Assert.AreEqual(Severity.Minor, output.HiSeverity);
            Assert.AreEqual(Severity.Minor, output.LoSeverity);
            Assert.AreEqual(Severity.Major, output.LoLoSeverity);
        }

        [TestMethod()]
        public void FromResponseTestInvalidType()
        {
            byte[] input = new byte[]
            {
                0x01, 0x00, 0x01, 0x01, 0x00,
                0x00, 0x0C, 0xE4, 0x1B, 0x58,
                0x17, 0x70, 0x01, 0xF4, 0xFC,
                0x18, 0x00, 0x64, 0xF8, 0x30,
                0x2A, 0xF8, 0x69
            };

            Assert.ThrowsException<ArgumentException>(() => new AnalogAlarmLimit(input));
        }

        [TestMethod()]
        public void FromResponseTestInvalidLength()
        {
            byte[] input = new byte[]
            {
                0x01, 0x00, 0x00, 0x01, 0x00,
                0x00, 0x0C, 0xE4, 0x1B, 0x58,
                0x17, 0x70, 0x01, 0xF4, 0xFC,
                0x18, 0x00, 0x64, 0xF8, 0x30
            };

            Assert.ThrowsException<ArgumentException>(() => new AnalogAlarmLimit(input));
        }
    }
}