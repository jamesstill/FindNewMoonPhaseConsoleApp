namespace FindNewMoonPhaseConsoleApp
{
    internal static class Utils
    {
        const double LUNAR_CYCLE = 29.53058770576;

        public static IEnumerable<DateTime> EachLunarCycle(DateTime from, DateTime thru)
        {
            for (var d = from; d <= thru; d = d.AddDays(LUNAR_CYCLE))
            {
                yield return d;
            }
        }

        /// <summary>
        /// Meeus (49.1)
        /// Time of mean conjuction or opposition
        /// </summary>
        /// <param name="k"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public static double ToJDE(int k, double T)
        {
            return 2451550.09766 +
                (29.530588861 * k) +
                (0.00015437 * T * T) -
                (0.000000150 * T * T * T) +
                (0.00000000073 * T * T * T * T);
        }

        /// <summary>
        /// Meeus (49.2)
        /// Baseline value k where k = 0 is 6 Jan 2000 (first new moon of J2000.0 epoch).
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int ToK(this DateTime d)
        {
            double decimalYear = (d.ToDecimalYear() - 2000) * 12.3685;
            return (int)Math.Floor(decimalYear);
        }

        /// <summary>
        /// Meeus (49.3)
        /// Time T is the time in Julian centures since the J2000.0 epoch
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double ToT(this int k)
        {
            return k / 1236.85;
        }

        /// <summary>
        /// Meeus (47.6)
        /// Earth's eccentricity in orbit
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static double ToE(this double T)
        {
            return 1 - 0.002516 * T - 0.0000074 * T * T;
        }

        /// <summary>
        /// Meeus (49.4)
        /// Sun's mean anomaly at time JDE
        /// </summary>
        /// <param name="k"></param>
        /// <param name="T"></param>
        /// <returns>M</returns>
        public static double ToM(int k, double T)
        {
            var i = 2.5534 +
                (29.10535670 * k) -
                (0.0000014 * T * T) -
                (0.00000011 * T * T * T);

            return i.ToReducedAngle();
        }

        /// <summary>
        /// Meeus (49.5)
        /// Moon's mean anomaly at time JDE
        /// </summary>
        /// <param name="k"></param>
        /// <param name="T"></param>
        /// <returns>M′</returns>
        public static double ToMPrime(int k, double T)
        {
            var i = 201.5643 +
                (385.81693528 * k) +
                (0.0107582 * T * T) +
                (0.00001238 * T * T * T) -
                (0.000000058 * T * T * T * T);

            return i.ToReducedAngle();
        }

        /// <summary>
        /// Meeus (49.6)
        /// Moon's argument of latitude
        /// </summary>
        /// <param name="k"></param>
        /// <param name="T"></param>
        /// <returns>F</returns>
        public static double ToF(int k, double T)
        {
            var i = 160.7108 +
                (390.67050284 * k) -
                (0.0016118 * (T * T)) -
                (0.00000227 * (T * T * T)) +
                (0.000000011 * (T * T * T * T));

            return i.ToReducedAngle();
        }

        /// <summary>
        /// Meeus (49.7)
        /// Longitude of the ascending node of the lunar orbit
        /// </summary>
        /// <param name="k"></param>
        /// <param name="T"></param>
        /// <returns>Ω</returns>
        public static double ToO(int k, double T)
        {
            var i = 124.7746 -
                (1.56375588 * k) +
                (0.0020672 * (T * T)) +
                (0.00000215 * (T * T * T));

            return i.ToReducedAngle();
        }

        /// <summary>
        /// Planetary coefficients A1 thru A14 in degrees as 
        /// periodic terms for correction to JDE (p. 351).
        /// </summary>
        /// <param name="k"></param>
        /// <param name="T"></param> 
        /// <returns></returns>
        public static double ToA(int k, double T)
        {
            var a1 = 299.77 + 0.107408 * k - 0.009173 * T * T;
            var a2 = 251.88 + 0.016321 * k;
            var a3 = 251.83 + 26.651886 * k;
            var a4 = 349.42 + 36.412478 * k;
            var a5 = 84.66 + 18.206239 * k;
            var a6 = 141.74 + 53.303771 * k;
            var a7 = 207.14 + 2.453732 * k;
            var a8 = 154.84 + 7.306860 * k;
            var a9 = 34.52 + 27.261239 * k;
            var a10 = 207.19 + 0.121824 * k;
            var a11 = 291.34 + 1.844379 * k;
            var a12 = 161.72 + 24.198154 * k;
            var a13 = 239.56 + 25.513099 * k;
            var a14 = 331.55 + 3.592518 * k;

            return
                0.000325 * Math.Sin(a1.ToReducedAngle().ToRadians()) +
                0.000165 * Math.Sin(a2.ToReducedAngle().ToRadians()) +
                0.000164 * Math.Sin(a3.ToReducedAngle().ToRadians()) +
                0.000126 * Math.Sin(a4.ToReducedAngle().ToRadians()) +
                0.000110 * Math.Sin(a5.ToReducedAngle().ToRadians()) +
                0.000062 * Math.Sin(a6.ToReducedAngle().ToRadians()) +
                0.000060 * Math.Sin(a7.ToReducedAngle().ToRadians()) +
                0.000056 * Math.Sin(a8.ToReducedAngle().ToRadians()) +
                0.000047 * Math.Sin(a9.ToReducedAngle().ToRadians()) +
                0.000042 * Math.Sin(a10.ToReducedAngle().ToRadians()) +
                0.000040 * Math.Sin(a11.ToReducedAngle().ToRadians()) +
                0.000037 * Math.Sin(a12.ToReducedAngle().ToRadians()) +
                0.000035 * Math.Sin(a13.ToReducedAngle().ToRadians()) +
                0.000023 * Math.Sin(a14.ToReducedAngle().ToRadians());
        }

        /// <summary>
        /// Additional corrections to JDE in order to obtain 
        /// the true (apparent) phase of the new moon (p. 351).
        /// </summary>
        /// <param name="E">Earth's eccentricity</param>
        /// <param name="Sm">Sun's mean anomaly (radians)</param>
        /// <param name="Mm">Moon's mean anomaly (radians)</param>
        /// <param name="F">Moon's argument of latitude (radians)</param>
        /// <param name="O">Longitude of the ascending node of the lunar orbit</param>
        /// <returns></returns>
        public static double ToNM(double E, double Sm, double Mm, double F, double O)
        {
            Sm = Sm.ToRadians();
            Mm = Mm.ToRadians();
            F = F.ToRadians();
            O = O.ToRadians();

            return
                (-0.40720 * Math.Sin(Mm)) +
                (0.17241 * E * Math.Sin(Sm)) +
                (0.01608 * Math.Sin(Mm * 2)) +
                (0.01039 * Math.Sin(F * 2)) +
                (0.00739 * E * Math.Sin(Mm - Sm)) -
                (0.00514 * E * Math.Sin(Mm + Sm)) +
                (0.00208 * E * E * Math.Sin(Sm * 2)) -
                (0.00111 * Math.Sin(Mm - 2 * F)) -
                (0.00057 * Math.Sin(Mm + 2 * F)) +
                (0.00056 * E * Math.Sin(2 * Mm + Sm)) -
                (0.00042 * Math.Sin(3 * Mm)) +
                (0.00042 * E * Math.Sin(Sm + 2 * F)) +
                (0.00038 * E * Math.Sin(Sm - 2 * F)) -
                (0.00024 * E * Math.Sin(2 * Mm - Sm)) -
                (0.00017 * Math.Sin(O)) -
                (0.00007 * Math.Sin(Mm + 2 * Sm)) +
                (0.00004 * Math.Sin(2 * Mm - 2 * F)) +
                (0.00004 * Math.Sin(3 * Sm)) +
                (0.00003 * Math.Sin(Mm + Sm - 2 * F)) +
                (0.00003 * Math.Sin(2 * Mm + 2 * F)) -
                (0.00003 * Math.Sin(Mm + Sm + 2 * F)) +
                (0.00003 * Math.Sin(Mm - Sm + 2 * F)) -
                (0.00002 * Math.Sin(Mm - Sm - 2 * F)) -
                (0.00002 * Math.Sin(3 * Mm + Sm)) +
                (0.00002 * Math.Sin(4 * Mm));
        }

        /// <summary>
        /// Given the corrected JDE for the new moon event adjust 
        /// for Delta T and return UTC
        /// </summary>
        /// <param name="correctedJDE">Corrected JDE</param>
        /// <returns></returns>
        public static DateTime ToDateTimeUTC(double correctedJDE)
        {
            // convert JDE TD to UTC by subtracting ΔT
            var d = ToDateTime(correctedJDE);
            var deltaT = ToDeltaT(d.Year);
            return d.AddSeconds(-deltaT);
        }

        public static string ToDateTimeUTCDisplay(DateTime d)
        {
            var date = d.ToShortDateString();
            var time = d.ToString("HH:mm:ss");

            return string.Format("{0, 20}", date + " " + time + " UTC");
        }

        /// <summary>
        /// Polynomial expressions from NASA algorithm for approximate value 
        /// of ΔT for a given year between -1999 and +3000
        /// See: https://eclipse.gsfc.nasa.gov/SEhelp/deltatpoly2004.html
        /// </summary>
        /// <param name="y">Year</param>
        /// <returns></returns>
        private static double ToDeltaT(int y)
        {
            var deltaT = y switch
            {
                < -500 =>
                    -20 + 32 * Math.Pow((y - 1820) / 100, 2),

                > -500 and <= 500 =>
                    10583.6 - 1014.41 *
                    (y / 100) + 33.78311 *
                    Math.Pow(y / 100, 2) - 5.952053 *
                    Math.Pow(y / 100, 3) -
                    0.1798452 * Math.Pow(y / 100, 4) +
                    0.022174192 * Math.Pow(y / 100, 5) +
                    0.0090316521 * Math.Pow(y / 100, 6),

                > 500 and <= 1600 =>
                    1574.2 - 556.01 *
                    ((y - 1000) / 100) +
                    71.23472 * Math.Pow(((y - 1000) / 100), 2) +
                    0.319781 * Math.Pow(((y - 1000) / 100), 3) -
                    0.8503463 * Math.Pow(((y - 1000) / 100), 4) -
                    0.005050998 * Math.Pow(((y - 1000) / 100), 5) +
                    0.0083572073 * Math.Pow(((y - 1000) / 100), 6),

                > 1600 and <= 1700 =>
                    120 - 0.9808 * (y - 1600) -
                    0.01532 * Math.Pow(y - 1600, 2) +
                    Math.Pow(y - 1600, 3) / 7129,

                > 1700 and <= 1800 =>
                    8.83 + 0.1603 * (y - 1700) -
                    0.0059285 * Math.Pow(y - 1700, 2) +
                    0.00013336 * Math.Pow(y - 1700, 3) -
                    Math.Pow(y - 1700, 4) / 1174000,

                > 1800 and <= 1860 =>
                    13.72 - 0.332447 * (y - 1800) +
                    0.0068612 * Math.Pow(y - 1800, 2) +
                    0.0041116 * Math.Pow(y - 1800, 3) -
                    0.00037436 * Math.Pow(y - 1800, 4) +
                    0.0000121272 * Math.Pow(y - 1800, 5) -
                    0.0000001699 * Math.Pow(y - 1800, 6) +
                    0.000000000875 * Math.Pow(y - 1800, 7),

                > 1860 and <= 1900 =>
                    7.62 + 0.5737 * (y - 1860) -
                    0.251754 * Math.Pow(y - 1860, 2) +
                    0.01680668 * Math.Pow(y - 1860, 3) -
                    0.0004473624 * Math.Pow(y - 1860, 4) +
                    Math.Pow(y - 1860, 5) / 233174,

                > 1900 and <= 1920 =>
                    -2.79 + 1.494119 * (y - 1900) -
                    0.0598939 * Math.Pow(y - 1900, 2) +
                    0.0061966 * Math.Pow(y - 1900, 3) -
                    0.000197 * Math.Pow(y - 1900, 4),

                > 1920 and <= 1941 =>
                    21.20 + 0.84493 * (y - 1920) -
                    0.076100 * Math.Pow(y - 1920, 2) +
                    0.0020936 * Math.Pow(y - 1920, 3),

                > 1941 and <= 1961 =>
                    29.07 + 0.407 * (y - 1950) -
                    Math.Pow(y - 1950, 2) / 233 +
                    Math.Pow(y - 1950, 3) / 2547,

                > 1961 and <= 1986 =>
                    45.45 + 1.067 * (y - 1975) -
                    Math.Pow(y - 1975, 2) / 260 -
                    Math.Pow(y - 1975, 3) / 718,

                > 1986 and <= 2005 =>
                    63.86 + 0.3345 * (y - 2000) -
                    0.060374 * Math.Pow(y - 2000, 2) +
                    0.0017275 * Math.Pow(y - 2000, 3) +
                    0.000651814 * Math.Pow(y - 2000, 4) +
                    0.00002373599 * Math.Pow(y - 2000, 5),

                > 2005 and <= 2050 =>
                    62.92 + 0.32217 * (y - 2000) +
                    0.005589 * Math.Pow(y - 2000, 2),

                > 2050 and <= 2150 =>
                    -20 + 32 * Math.Pow(((y - 1820) / 100), 2) -
                    0.5628 * (2150 - y),

                > 2150 and <= 3000 =>
                    -20 + 32 * Math.Pow(((y - 1820) / 100), 2),

                _ => 0.0
            };

            return deltaT;
        }

        /// <summary>
        /// Creates a date from a known Julian Day (JD) value.
        /// </summary>
        /// <param name="julianDay"></param>
        private static DateTime ToDateTime(double julianDay)
        {
            if (double.IsNaN(julianDay))
            {
                throw new ArgumentException("julianDay must be a valid value.");
            }

            double A;
            double B;
            int C;
            double D;
            int E;
            double jd = julianDay + 0.5;
            double Z = Math.Truncate(jd);
            double F = jd - Math.Truncate(jd);

            if (Z < 2299161)
            {
                A = Z;
            }
            else
            {
                var a = (int)((Z - 1867216.25) / 36524.25);
                A = Z + 1 + a - (int)(a / 4);
            }

            B = A + 1524;
            C = (int)((B - 122.1) / 365.25);
            D = (int)(365.25 * C);
            E = (int)((B - D) / 30.6001);
            double day = B - D - (int)(30.6001 * E) + F;

            var M = E switch
            {
                14 or 15 => E - 13,
                _ => E - 1,
            };

            var Y = M switch
            {
                1 or 2 => C - 4715,
                _ => C - 4716,
            };

            // leverage the C# TimeSpan struct
            var ts = TimeSpan.FromDays(day);

            return new(Y, M, ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }

        /// <summary>
        /// Converts calendar date to decimal date. For example, the total solar 
        /// eclipse of 21 May 1993 (141st day of year) would be decimal 1993.38.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static double ToDecimalYear(this DateTime dt)
        {
            double daysInYear = (DateTime.IsLeapYear(dt.Year)) ? 366 : 365.2425;
            var fraction = dt.DayOfYear / daysInYear;
            return dt.Year + fraction;
        }

        /// <summary>
        /// Reduce very large angles to between 0 and 360 degrees
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double ToReducedAngle(this double d)
        {
            d %= 360;
            if (d < 0)
            {
                d += 360;
            }

            return d;
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double ToRadians(this double d)
        {
            return (Math.PI / 180) * d;
        }
    }
}
