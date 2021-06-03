using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Nibo.Domain.Parser
{
    public static class OFXDateTimeParser
    {
        // This parser is only working with EST time zone at the moment
        private static Regex _acceptedFormatRegex = new Regex(@"^(?<dateandtime>\d{14})\[(?<offset>-\d{2}):EST\]$");

        public static bool TryParse(string dateTimeString, out DateTimeOffset dateTimeOffset)
        {
            var match = _acceptedFormatRegex.Match(dateTimeString);

            if (match.Success)
            {
                DateTime parsedDateTime = DateTime.ParseExact(match.Groups["dateandtime"].Value, "yyyyMMddhhmmss", CultureInfo.InvariantCulture);
                int parsedOffset = int.Parse(match.Groups["offset"].Value);
                TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                dateTimeOffset = GetDateTimeOffset(parsedDateTime, parsedOffset, estTimeZone);
                return true;
            }

            dateTimeOffset = DateTimeOffset.MinValue;
            return false;
        }

        private static DateTimeOffset GetDateTimeOffset(DateTime parsedDateTime, int parsedOffset, TimeZoneInfo timeZoneInfo)
        {
            TimeSpan parsedOffsetTimeSpan = new TimeSpan(hours: parsedOffset, minutes: 0, seconds: 0);
            TimeSpan finalOffset = parsedOffsetTimeSpan + timeZoneInfo.BaseUtcOffset;
            return new DateTimeOffset(parsedDateTime, finalOffset);
        }
    }
}
