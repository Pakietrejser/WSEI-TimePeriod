using System;
using NUnit.Framework;
using TimeTimePeriod;

namespace UnitTests
{
    [TestFixture]
    public class TimePeriodTest
    {
        [TestCase(22,22,22,"22:22:22")]
        [TestCase(1,0,0,"01:00:00")]
        [TestCase(23,null,null,"23:00:00")]
        [TestCase(12,05,null,"12:05:00")]
        public void CreateTimePeriod_CorrectFormat(long hours, long minutes, long seconds, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new TimePeriod(hours, minutes, seconds).ToString());
        }
        
        [TestCase(22,22,"00:22:22")]        [TestCase(1,0,"00:01:00")]
                                            [TestCase(28,59,"00:28:59")]
                                            [TestCase(100,05,"01:40:05")]

        public void CreateTime_TwoParameters_CorrectFormat(long minutes, long seconds, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new TimePeriod(minutes, seconds).ToString());
        }
        
        [TestCase(22,"00:00:22")]
        [TestCase(61,"00:01:01")]
        [TestCase(23,"00:00:23")]
        [TestCase(120,"00:02:00")]
        public void CreateTime_OneParameters_CorrectFormat(long seconds, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new TimePeriod(seconds).ToString());
        }
        
        [TestCase("00:00:22","00:00:22")]
        [TestCase("60","00:01:00")]
        [TestCase("65:59","01:05:59")]
        [TestCase("60:12:12","60:12:12")]
        public void CreateTime_StringParameters_CorrectFormat(string str, string expectedResult)
        {
            Assert.AreEqual(expectedResult, new TimePeriod(str).ToString());
        }
        
        [TestCase(22,70,22)]
        [TestCase(-1,0,99)]
        [TestCase(-23,null,null)]
        [TestCase(12,-05,null)]
        public void CreateTimePeriod_Exception(long hours, long minutes, long seconds)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod test = new TimePeriod(hours, minutes, seconds);
            });
        }
        
        [TestCase(20,72)]
        [TestCase(-1,99)]
        [TestCase(-23,-23)]
        [TestCase(12,-1)]
        public void CreateTimePeriod_TwoParametersException(long minutes, long seconds)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod test = new TimePeriod(minutes, seconds);
            });
        }
        
        [TestCase(-1)]
        public void CreateTimePeriod_OneParameterException(long seconds)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod test = new TimePeriod(seconds);
            });
        }
        
        [TestCase("00:12:02:00")]
        [TestCase("00::02:00")]
        public void CreateTimePeriod_StringTooLongException(string str)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod test = new TimePeriod(str);
            });
        }
        
        [TestCase("00:00:24","00:00:22","00:00:02")]
        [TestCase("00:00:22","00:00:01","00:00:21")]
        public void CreateTimePeriod_FromTwoTimes(string str1, string str2, string expectedResult)
        {
            Time t1 = new Time(str1);
            Time t2 = new Time(str2);

            Assert.AreEqual(expectedResult, new TimePeriod(t1, t2).ToString());
        }
        
        [TestCase("00:00:24","00:00:29")]
        [TestCase("00:00:00","00:01:00")]
        public void CreateTimePeriod_FromTwoTimes_Exception(string str1, string str2)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Time t1 = new Time(str1);
                Time t2 = new Time(str2);
                TimePeriod timePeriod = new TimePeriod(t1, t2);
            });
        }

        [TestCase("10:30:30", "10:30:30", true)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:59:59", false)]
        [TestCase("00:00:00", "00:01:00", false)]
        public void Equals(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            
            bool result = t1.Equals(t2);
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("23:59:59", "23:59:59", false)]
        [TestCase("00:00:00", "00:01:00", false)]
        [TestCase("23:53:59", null, false)]
        [TestCase("23:53:59", 123, false)]
        public void Equals_CompareTimePeriodToObject(string str1, object str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            bool result = t1.Equals(str2);
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("00:12:00", "00:12:00", true)]
        [TestCase("00:00:00", "00:01:00", false)]
        [TestCase("01:00:00", "23:00:00", false)]
        public void Equals_CompareTimePeriodToTime(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            object t2 = new Time(str2);
            bool result = t1.Equals(t2);
            
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("10:30:30", "10:30:30", true)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:59:59", false)]
        [TestCase("00:00:00", "00:01:00", false)]
        public void EqualsOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            bool result = t1 == t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:30:30", false)]
        [TestCase("00:00:00", "00:00:00", false)]
        [TestCase("23:53:59", "23:59:59", true)]
        [TestCase("00:00:00", "00:01:00", true)]
        public void NotEqualsOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            bool result = t1 != t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", true)]
        [TestCase("00:00:00", "00:05:00", true)]
        [TestCase("23:53:59", "23:50:59", false)]
        [TestCase("00:02:00", "00:01:00", false)]
        public void LesserThanOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            bool result = t1 < t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", true)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:50:59", false)]
        [TestCase("00:01:00", "00:01:00", true)]
        public void LesserOrEqualThanOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            bool result = t1 <= t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", false)]
        [TestCase("00:00:00", "00:05:00", false)]
        [TestCase("23:53:59", "23:50:59", true)]
        [TestCase("00:02:00", "00:01:00", true)]
        public void GreaterThanOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            bool result = t1 > t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", false)]
        [TestCase("00:00:00", "00:00:00", true)]
        [TestCase("23:53:59", "23:50:59", true)]
        [TestCase("00:01:00", "00:01:00", true)]
        public void GreaterOrEqualThanOperator_TestMathematicalOperations(string str1, string str2, bool expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);
            bool result = t1 >= t2;
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestCase("10:30:30", "10:35:30", "21:06:00")]
        [TestCase("12:30:00", "74:30:00", "87:00:00")]
        public void AddTimePeriod_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);

            TimePeriod result = t1.Plus(t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("10:30:30", "10:35:30", "21:06:00")]
        [TestCase("12:30:00", "74:30:00", "87:00:00")]
        public void AddTimePeriods_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);

            TimePeriod result = TimePeriod.Plus(t1, t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("10:30:30", "10:35:30", "21:06:00")]
        [TestCase("12:30:00", "74:30:00", "87:00:00")]
        public void AddOperator_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);

            TimePeriod result = t1 + t2;
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("21:06:00", "10:35:30", "10:30:30")]
        [TestCase("87:00:00", "74:30:00", "12:30:00")]
        public void SubtractTimePeriod_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);

            TimePeriod result = t1.Minus(t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("10:35:30", "10:30:30", "00:05:00")]
        [TestCase("74:30:00", "13:30:00", "61:00:00")]
        public void SubtractTimePeriods_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);

            TimePeriod result = TimePeriod.Minus(t1, t2);
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [TestCase("21:06:00", "10:35:30", "10:30:30")]
        [TestCase("87:00:00", "74:30:00", "12:30:00")]
        public void SubtractOperator_TestMathematicalOperations(string str1, string str2, string expectedResult)
        {
            TimePeriod t1 = new TimePeriod(str1);
            TimePeriod t2 = new TimePeriod(str2);

            TimePeriod result = t1 - t2;
            
            Assert.AreEqual(expectedResult, result.ToString());
        }
        
        [Test]
        public void SubtractionOne_TestMathematicalExpetion()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod result = new TimePeriod("00:00:00").Minus(new TimePeriod("00:01:00"));
                
            });
        }
        
        [Test]
        public void SubtractionTwo_TestMathematicalExpetion()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod result = TimePeriod.Minus(new TimePeriod("00:00:00"), new TimePeriod("00:01:00"));
                
            });
        }
        
        [Test]
        public void SubtractionThree_TestMathematicalExpetion()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                TimePeriod result = new TimePeriod("00:00:00") - new TimePeriod("00:01:00");
                
            });
        }
    }
    
}