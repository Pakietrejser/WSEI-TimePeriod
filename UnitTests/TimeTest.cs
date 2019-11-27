using System;
using NUnit.Framework;
using TimeTimePeriod;

namespace UnitTests
{
    [TestFixture]
    public class TimeTest
    {
        
        [TestCase(22,22,22,"22:22:22")]
        [TestCase(1,0,0,"01:00:00")]
        [TestCase(23,59,59,"23:59:59")]
        [TestCase(12,05,35,"12:05:35")]
        public void CreateTime_IntThreeParameters_CorrectFormat(byte hours, byte minutes, byte seconds, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new Time(hours, minutes, seconds).ToString());
        }
        
        [TestCase(22,22,"22:22:00")]
        [TestCase(1,0,"01:00:00")]
        [TestCase(23,59,"23:59:00")]
        [TestCase(12,05,"12:05:00")]
        public void CreateTime_IntTwoParameters_CorrectFormat(byte hours, byte minutes, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new Time(hours, minutes).ToString());
        }
        
        [TestCase(22,"22:00:00")]
        [TestCase(0,"00:00:00")]
        [TestCase(23,"23:00:00")]
        [TestCase(5,"05:00:00")]
        public void CreateTime_IntOneParameters_CorrectFormat(byte hours, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new Time(hours).ToString());
        }

        [TestCase(25,02,02)]
        [TestCase(69,69,69)]
        [TestCase(1,1,100)]
        [TestCase(100,0,0)]
        public void CreateTime_TooBigParameters_Exception(byte hours, byte minutes, byte seconds)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Time time = new Time(hours, minutes, seconds);
            });
        }

        [TestCase(-25,-02,-02)]
        [TestCase(-69,-69,-69)]
        [TestCase(-1,-1,-1)]
        [TestCase(-22,-0,-0)]
        public void CreateTime_NegativeValues_Exception(int hours, int minutes, int seconds)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Time time = new Time((byte)hours, (byte)minutes, (byte)seconds);
            });
        }

        [TestCase("22")]
        [TestCase("22:1")]
        [TestCase("22:::2")]
        [TestCase("22::12::31::41")]
        public void CreateTime_IncorrectStringFormat_Exception(string str)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Time time = new Time(str);
            });
        }
        
        [TestCase("222:00:09")]
        [TestCase("0:123:0")]
        public void CreateTime_TooBigStringFormat_Exception(string str)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Time time = new Time(str);
            });
        }
        
        [TestCase("00:00:4444")]
        [TestCase("-0:123:-45")]
        public void CreateTime_OverflowStringFormat_Exception(string str)
        {
            Assert.Throws<OverflowException>(() =>
            {
                Time time = new Time(str);
            });
        }

        [TestCase("10:30:30", "10:30:30", true)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:59:59", false)]
        [TestCase("00:00:00", "00:01:00", false)]
        public void Equals_CompareTime(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1.Equals(t2);
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("23:59:59", "23:59:59", false)]
        [TestCase("00:00:00", "00:01:00", false)]
        [TestCase("23:53:59", null, false)]
        [TestCase("23:53:59", 123, false)]
        public void Equals_CompareTimeToObject(string str1, object str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            bool result = t1.Equals(str2);
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("00:00:00", "00:01:00", false)]
        [TestCase("01:00:00", "25:00:00", true)]
        public void Equals_CompareTimeToTimePeriod(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            object t2 = new TimePeriod(str2);
            bool result = t1.Equals(t2);
            
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("10:30:30", "10:30:30", true)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:59:59", false)]
        [TestCase("00:00:00", "00:01:00", false)]
        public void EqualOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1 == t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:30:30", false)]
        [TestCase("00:00:00", "00:00:00", false)]
        [TestCase("23:53:59", "23:59:59", true)]
        [TestCase("00:00:00", "00:01:00", true)]
        public void NotEqualOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1 != t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", true)]
        [TestCase("00:00:00", "00:05:00", true)]
        [TestCase("23:53:59", "23:50:59", false)]
        [TestCase("00:02:00", "00:01:00", false)]
        public void LesserThan_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1 < t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", true)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:50:59", false)]
        [TestCase("00:01:00", "00:01:00", true)]
        public void LesserThanOrEqual_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1 <= t2;
            
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("10:30:30", "10:35:30", false)]
        [TestCase("00:00:00", "00:05:00", false)]
        [TestCase("23:53:59", "23:50:59", true)]
        [TestCase("00:02:00", "00:01:00", true)]
        public void GreaterThan_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1 > t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", false)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:50:59", true)]
        [TestCase("00:01:00", "00:01:00", true)]
        public void GreaterThanOrEqual_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);
            bool result = t1 >= t2;
            
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("10:30:30", "10:35:30", "21:06:00")]
        [TestCase("00:00:00", "00:00:01", "00:00:01")]
        [TestCase("23:59:59", "00:00:01", "00:00:00")]
        [TestCase("12:30:00", "4:30:00", "17:00:00")]
        [TestCase("12:30:00", "23:30:00", "12:00:00")]
        [TestCase("12:30:00", "74:30:00", "15:00:00")]
        public void AddTime_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            TimePeriod t2 = new TimePeriod(str2);

            Time result = t1.Plus(t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("10:30:30", "10:35:30", "21:06:00")]
        [TestCase("00:00:00", "00:00:01", "00:00:01")]
        [TestCase("23:59:59", "00:00:01", "00:00:00")]
        [TestCase("12:30:00", "4:30:00", "17:00:00")]
        [TestCase("12:30:00", "23:30:00", "12:00:00")]
        [TestCase("12:30:00", "74:30:00", "15:00:00")]
        public void AddTimePeriodToTime_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            TimePeriod t2 = new TimePeriod(str2);

            Time result = Time.Plus(t1, t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("10:30:30", "10:35:30", "21:06:00")]
        [TestCase("00:00:00", "00:00:01", "00:00:01")]
        [TestCase("23:59:59", "00:00:01", "00:00:00")]
        [TestCase("12:30:00", "4:30:00", "17:00:00")]
        [TestCase("12:30:00", "23:30:00", "12:00:00")]
        [TestCase("12:30:00", "12:30:00", "01:00:00")]
        public void AddOperator_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Time result = t1 + t2;

            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("21:06:00", "10:35:30", "10:30:30")]
        [TestCase("00:00:01", "00:00:01", "00:00:00")]
        [TestCase("23:59:59", "00:00:01", "23:59:58")]
        [TestCase("12:30:00", "4:30:00", "08:00:00")]
        [TestCase("23:30:00", "12:30:00", "11:00:00")]
        [TestCase("12:30:00", "12:30:00", "00:00:00")]
        public void SubtractTime_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            TimePeriod t2 = new TimePeriod(str2);

            Time result = t1.Minus(t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("21:06:00", "10:35:30", "10:30:30")]
        [TestCase("00:00:01", "00:00:01", "00:00:00")]
        [TestCase("23:59:59", "00:00:01", "23:59:58")]
        [TestCase("12:30:00", "4:30:00", "08:00:00")]
        [TestCase("23:30:00", "12:30:00", "11:00:00")]
        [TestCase("12:30:00", "12:30:00", "00:00:00")]
        public void SubtractTimePeriodToTime_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            TimePeriod t2 = new TimePeriod(str2);

            Time result = Time.Minus(t1, t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("21:06:00", "10:35:30", "10:30:30")]
        [TestCase("00:00:01", "00:00:01", "00:00:00")]
        [TestCase("23:59:59", "00:00:01", "23:59:58")]
        [TestCase("12:30:00", "4:30:00", "08:00:00")]
        [TestCase("23:30:00", "12:30:00", "11:00:00")]
        [TestCase("12:30:00", "12:30:00", "00:00:00")]
        public void SubtractOperator_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Time result = t1 - t2;

            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestCase("09:06:00", "10:35:30")]
        [TestCase("00:00:00", "00:00:01")]
        public void SubtractBiggerFromSmaller_TestMathematicalOperations_Exception(string str1, string str2)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Time t1 = new Time(str1);
                TimePeriod t2 = new TimePeriod(str2);

                Time.Minus(t1, t2);
            });
        }
    }
}