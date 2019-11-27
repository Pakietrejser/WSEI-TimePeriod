using System;

namespace TimeTimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        // immutable properties without setters
        public byte Hours { get; }

        public byte Minutes { get; }

        public byte Seconds { get; }

        // default values act as "additional" constructors
        // in total, 4 ways to construct Time object
        // refers to a point in TimeTimePeriodtime from 00:00:00 to 23:59:59 (byte cannot be negative)
        public Time(byte hours, byte minutes = 0, byte seconds = 0)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentOutOfRangeException();
            
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time(string time)
        {
            string[] timeParts = time.Split(':');
            if (timeParts.Length != 3)
                throw new ArgumentException("Time needs to be in hh:mm:ss format");
            
            byte hours = Convert.ToByte(timeParts[0]);
            byte minutes = Convert.ToByte(timeParts[1]);
            byte seconds = Convert.ToByte(timeParts[2]);

            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentOutOfRangeException();
            
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        // converts Time to a string hh:mm:ss format
        public override string ToString()
        {
            string h = Convert.ToString(Hours);
            string m = Convert.ToString(Minutes);
            string s = Convert.ToString(Seconds);

            if (Hours < 10)
                h = "0" + h;
            if (Minutes < 10)
                m = "0" + m;
            if (Seconds < 10)
                s = "0" + s;

            return h + ":" + m + ":" + s;
        }

        // methods that compare Time to other objects 
        public bool Equals(Time other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case TimePeriod t:
                    TimePeriod tp = new TimePeriod(t.Seconds % (60*60*24));
                    return this == new Time(tp.ToString());
                default:
                    return false;
            }
        }

        public int CompareTo(Time other)
        {
            return (Hours * 3600 + Minutes * 60 + Seconds).CompareTo(other.Hours * 3600 + other.Minutes * 60 + other.Seconds) % (60*60*24);
        }

        public static bool operator ==(Time t1, Time t2){ return t1.Equals(t2); }

        public static bool operator !=(Time t1, Time t2){ return !t1.Equals(t2); }
        
        public static bool operator <(Time t1, Time t2){ return t1.CompareTo(t2) < 0; }
        
        public static bool operator <=(Time t1, Time t2){ return t1.CompareTo(t2) <= 0; }
        
        public static bool operator >(Time t1, Time t2){ return t1.CompareTo(t2) > 0; }
        
        public static bool operator >=(Time t1, Time t2){ return t1.CompareTo(t2) >= 0; }
        
        // methods that support mathematical time operations
        public Time Plus(TimePeriod timePeriod)
        {
            TimePeriod tp = new TimePeriod(timePeriod.Seconds % (60*60*24));
            
            return this + new Time(tp.ToString());
        }

        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            TimePeriod tp = new TimePeriod(timePeriod.Seconds % (60*60*24));
            
            return time + new Time(tp.ToString());
        }

        public static Time operator +(Time t1, Time t2)
        {
            long seconds = t1.Hours * 3600 + t2.Hours * 3600 + t1.Minutes * 60 + t2.Minutes * 60 + t1.Seconds + t2.Seconds;

            byte ss = Convert.ToByte(seconds % 60);
            byte mm = Convert.ToByte(seconds / 60 % 60);
            byte hh = Convert.ToByte(seconds / 3600 % 24);
            
            return new Time(hh, mm, ss);
        }
        
        public Time Minus(TimePeriod timePeriod)
        {
            TimePeriod tp = new TimePeriod(timePeriod.Seconds % (60*60*24));
            
            return this - new Time(tp.ToString());;
        }

        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            TimePeriod tp = new TimePeriod(timePeriod.Seconds % (60*60*24));
            
            return time - new Time(tp.ToString());
        }

        public static Time operator -(Time t1, Time t2)
        {
            if (t1 < t2)
                throw new ArgumentException("Time cannot be negative! Change the order of subtraction.");
            
            long seconds = (t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds) - (t2.Hours * 3600 + t2.Minutes * 60 + t2.Seconds);

            byte ss = Convert.ToByte(seconds % 60);
            byte mm = Convert.ToByte(seconds / 60 % 60);
            byte hh = Convert.ToByte(seconds / 3600 % 24);
            
            return new Time(hh, mm, ss);
        }
        
        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();
    }
}