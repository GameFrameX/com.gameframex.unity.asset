using System;
using NUnit.Framework;

namespace GameFrameX.Asset.Tests
{
    internal class UnitTests
    {
        [Test]
        public void TestIsUnixSameDay_SameDay_ReturnsTrue()
        {
            // April 7, 2021 00:00 UTC and 12:00 UTC should be same day
            long timestamp1 = 1617753600L;
            long timestamp2 = 1617796800L;
            Assert.AreEqual(
                DateTimeOffset.FromUnixTimeSeconds(timestamp1).Date,
                DateTimeOffset.FromUnixTimeSeconds(timestamp2).Date);
        }

        [Test]
        public void TestIsUnixSameDay_DifferentDay_ReturnsFalse()
        {
            // April 7, 2021 23:00 UTC and April 8, 2021 01:00 UTC
            long timestamp1 = 1617836400L;
            long timestamp2 = 1617843600L;
            Assert.AreNotEqual(
                DateTimeOffset.FromUnixTimeSeconds(timestamp1).Date,
                DateTimeOffset.FromUnixTimeSeconds(timestamp2).Date);
        }
    }
}