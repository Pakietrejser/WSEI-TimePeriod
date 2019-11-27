﻿using System;

namespace TimeTimePeriod
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        // immutable property without setters
        public long Seconds { get;  }

        // default values act as "additional" constructors
        // in total, 9 ways to construct TimePeriod object
        // refers to a point in time from 00:00:00 to xxx:59:59
        public TimePeriod(long hours, long minutes = 0, long seconds = 0)
        {
            if (hours < 0 || minutes < 0 || seconds < 0)
                throw new ArgumentException("Time cannot be negative!");
            if (minutes >= 60 || seconds >= 60)
                throw new ArgumentException("Incorrect time representation! Use xxx:hh:ss format.");
            
            Seconds = hours * 3600 + minutes * 60 + seconds;
        }
        
        public TimePeriod(long minutes, long seconds = 0)
        {
            if (minutes < 0 || seconds < 0)
                throw new ArgumentException("Time cannot be negative!");
            if (seconds >= 60)
                throw new ArgumentException("Incorrect time representation! Use xxx:ss format.");
            
            Seconds = minutes * 60 + seconds;
        }
        
        public TimePeriod(long seconds)
        {
            if (seconds < 0)
                throw new ArgumentException("Time cannot be negative!");

            Seconds = seconds;
        }

        public TimePeriod(Time t1, Time t2)
        {
            if (t1 < t2)
                throw new ArgumentException("Time cannot be negative! Change the order of subtraction.");
            
            Time time = t1 - t2;
            Seconds = time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
        }

        public TimePeriod(string time)
        {
            string[] timeParts = time.Split(':');
            if (timeParts.Length > 3)
                throw new ArgumentException("TimePeriod needs to be in hh:mm:ss, mm:ss, or ss format");

            long minutes, seconds;

            switch (timeParts.Length)
            {
                case 3:
                    long hours = Convert.ToInt64(timeParts[0]);
                    minutes = Convert.ToInt64(timeParts[1]);
                    seconds = Convert.ToInt64(timeParts[2]);
                    Seconds = hours * 3600 + minutes * 60 + seconds % (60*60*24);
                    break;
                case 2:
                    minutes = Convert.ToInt64(timeParts[0]);
                    seconds = Convert.ToInt64(timeParts[1]);
                    Seconds = minutes * 60 + seconds;
                    break;
                default:
                    seconds = Convert.ToInt64(timeParts[0]);
                    Seconds = seconds;
                    break;
            }
        }
        
        // converts TimeTimePeriod to a string hh:mm:ss format
        public override string ToString()
        {
            long h = Seconds / 3600;
            long m = Seconds / 60 % 60;
            long s = Seconds % 60;
            
            return $"{h:D2}:{m:D2}:{s:D2}";
        }
        
        // methods that compare TimePeriod to other objects 
        public bool Equals(TimePeriod other)
        {
            return Seconds == other.Seconds;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Time t:
                    return this == new TimePeriod(t.ToString());
                default:
                    return false;
            }
        }

        public int CompareTo(TimePeriod other)
        {
            return Seconds.CompareTo(other.Seconds);
        }
        
        public static bool operator ==(TimePeriod t1, TimePeriod t2){ return t1.Equals(t2); }
        
        public static bool operator !=(TimePeriod t1, TimePeriod t2){ return !t1.Equals(t2); }
        
        public static bool operator <(TimePeriod t1, TimePeriod t2){ return t1.CompareTo(t2) < 0; }
        
        public static bool operator <=(TimePeriod t1, TimePeriod t2){ return t1.CompareTo(t2) <= 0; }
        
        public static bool operator >(TimePeriod t1, TimePeriod t2){ return t1.CompareTo(t2) > 0; }
        
        public static bool operator >=(TimePeriod t1, TimePeriod t2){ return t1.CompareTo(t2) >= 0; }

        // methods that support mathematical time operations
        public TimePeriod Plus(TimePeriod timePeriod)
        {
            return this + timePeriod;
        }

        public static TimePeriod Plus(TimePeriod t1, TimePeriod t2)
        {
            return t1 + t2;
        }

        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2)
        {
            return new TimePeriod(t1.Seconds + t2.Seconds);
        }

        public TimePeriod Minus(TimePeriod timePeriod)
        {
            if (this < timePeriod)
                throw new ArgumentException("Time cannot be negative! Change the order of subtraction.");

            return this - timePeriod;
        }

        public static TimePeriod Minus(TimePeriod t1, TimePeriod t2)
        {
            if (t1 < t2)
                throw new ArgumentException("Time cannot be negative! Change the order of subtraction.");

            return t1 - t2;
        }
        
        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2)
        {
            if (t1 < t2)
                throw new ArgumentException("Time cannot be negative! Change the order of subtraction.");
            
            return new TimePeriod(t1.Seconds - t2.Seconds);
        }
        
        public override int GetHashCode() => (Seconds).GetHashCode();
    }
}