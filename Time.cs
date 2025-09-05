namespace Taller01
{
    public class Time
    {
        // CAMPOS
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;

        // PROPIEDADES
        public int Hour => _hour;
        public int Minute => _minute;
        public int Second => _second;
        public int Millisecond => _millisecond;

        // CONSTRUCTORES
        public Time() : this(0, 0, 0, 0) { }

        public Time(int hour) : this(hour, 0, 0, 0) { }

        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }

        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

        public Time(int hour, int minute, int second, int millisecond)
        {
            if (!ValidHour(hour))
                throw new ArgumentException($"The hour: {hour}, is not valid.");
            if (!ValidMinute(minute))
                throw new ArgumentException($"The minute: {minute}, is not valid.");
            if (!ValidSecond(second))
                throw new ArgumentException($"The second: {second}, is not valid.");
            if (!ValidMillisecond(millisecond))
                throw new ArgumentException($"The millisecond: {millisecond}, is not valid.");

            _hour = hour;
            _minute = minute;
            _second = second;
            _millisecond = millisecond;
        }

        // VALIDACIONES
        public static bool ValidHour(int hour) => hour >= 0 && hour <= 23;
        public static bool ValidMinute(int minute) => minute >= 0 && minute <= 59;
        public static bool ValidSecond(int second) => second >= 0 && second <= 59;
        public static bool ValidMillisecond(int millisecond) => millisecond >= 0 && millisecond <= 999;

        // CONVERSION A STRING
        public override string ToString()
        {
            bool valid = ValidHour(_hour) && ValidMinute(_minute) &&
                         ValidSecond(_second) && ValidMillisecond(_millisecond);
            if (!valid)
                throw new InvalidOperationException("Time is not valid.");

            int displayHour = _hour % 12;
            if (displayHour == 0) displayHour = 12;
            string ampm = (_hour < 12) ? "AM" : "PM";
            return $"{displayHour:00}:{_minute:00}:{_second:00}.{_millisecond:000} {ampm}";
        }

        // CONVERSORES
        public long ToMilliseconds()
        {
            bool valid = ValidHour(_hour) && ValidMinute(_minute) &&
                         ValidSecond(_second) && ValidMillisecond(_millisecond);
            if (!valid) return 0;

            return (long)_hour * 3_600_000L
                 + (long)_minute * 60_000L
                 + (long)_second * 1_000L
                 + (long)_millisecond;
        }

        public long ToSeconds()
        {
            bool valid = ValidHour(_hour) && ValidMinute(_minute) &&
                         ValidSecond(_second) && ValidMillisecond(_millisecond);
            if (!valid) return 0;

            return (long)_hour * 3_600L + (long)_minute * 60L + _second;
        }

        public long ToMinutes()
        {
            bool valid = ValidHour(_hour) && ValidMinute(_minute) &&
                         ValidSecond(_second) && ValidMillisecond(_millisecond);
            if (!valid) return 0;

            return (long)_hour * 60L + _minute;
        }

        // SUMA Y ITSOTHERDAY
        public bool IsOtherDay(Time other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            const long DayMs = 24L * 3_600_000L;
            long sum = this.ToMilliseconds() + other.ToMilliseconds();
            return sum >= DayMs;
        }

        public Time Add(Time other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            const long DayMs = 24L * 3_600_000L;

            long sum = this.ToMilliseconds() + other.ToMilliseconds();
            sum %= DayMs;

            int hour = (int)(sum / 3_600_000L);
            sum %= 3_600_000L;

            int minute = (int)(sum / 60_000L);
            sum %= 60_000L;

            int second = (int)(sum / 1_000L);
            int millisecond = (int)(sum % 1_000L);

            return new Time(hour, minute, second, millisecond);
        }
    }
}
