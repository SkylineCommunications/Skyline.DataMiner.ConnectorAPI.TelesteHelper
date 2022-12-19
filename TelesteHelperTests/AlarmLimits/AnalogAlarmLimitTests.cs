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

            AnalogAlarmLimit expectedOutput = new AnalogAlarmLimit
            {
                ParamId = 1,
                ParamIdx = 0,
                State = AnalogAlarmState.Nominal,
                StateValue = 0,
                Value = 33d,
                HiHi = 70d,
                Hi = 60d,
                Lo = 5d,
                LoLo = -10d,
                Deadband = 1d,
                MinValue = -20d,
                MaxValue = 110d,
                HiHiSeverity = Severity.Major,
                HiSeverity = Severity.Minor,
                LoSeverity = Severity.Minor,
                LoLoSeverity = Severity.Major
            };

            AnalogAlarmLimit output = new AnalogAlarmLimit(input);

            Assert.AreEqual(expectedOutput.ParamId, output.ParamId);
            Assert.AreEqual(expectedOutput.ParamIdx, output.ParamIdx);
            Assert.AreEqual(expectedOutput.State, output.State);
            Assert.AreEqual(expectedOutput.StateValue, output.StateValue);
            Assert.AreEqual(expectedOutput.Value, output.Value);
            Assert.AreEqual(expectedOutput.HiHi, output.HiHi);
            Assert.AreEqual(expectedOutput.Hi, output.Hi);
            Assert.AreEqual(expectedOutput.Lo, output.Lo);
            Assert.AreEqual(expectedOutput.LoLo, output.LoLo);
            Assert.AreEqual(expectedOutput.Deadband, output.Deadband);
            Assert.AreEqual(expectedOutput.MinValue, output.MinValue);
            Assert.AreEqual(expectedOutput.MaxValue, output.MaxValue);
            Assert.AreEqual(expectedOutput.HiHiSeverity, output.HiHiSeverity);
            Assert.AreEqual(expectedOutput.HiSeverity, output.HiSeverity);
            Assert.AreEqual(expectedOutput.LoSeverity, output.LoSeverity);
            Assert.AreEqual(expectedOutput.LoLoSeverity, output.LoLoSeverity);
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