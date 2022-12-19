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
    public class DiscreteAlarmLimitTests
    {
        [TestMethod()]
        public void FromDataTest()
        {
            byte[] data = new byte[]
            {
                0x07,
                0x00,
                0x03,
                0x01,
                0x01,
                0x01,
                0x02, 0x01,
                0x03, 0x01,
                0x04, 0x03
            };

            var dals = DiscreteAlarmLimit.FromData(data);
            Assert.AreEqual(3, dals.Count());
        }
    }
}