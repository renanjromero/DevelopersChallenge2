using FluentAssertions;
using Nibo.Domain.Parser;
using System;
using System.Collections.Generic;
using Xunit;

namespace Nibo.Tests.Parser
{
    public class OFXDateTimeParserTests
    {
        [Theory]
        [MemberData(nameof(ParsingAStringParams))]
        public void Parsing_a_string(string dateTimeString, DateTimeOffset expectedResult)
        {
            bool success = OFXDateTimeParser.TryParse(dateTimeString, out DateTimeOffset dateTimeOffset);

            success.Should().BeTrue();
            dateTimeOffset.Should().BeSameDateAs(expectedResult);
        }

        [Fact]
        public void String_can_not_be_parsed()
        {
            var dateTimeString = "20140210100000[-03:UTC]";

            bool success = OFXDateTimeParser.TryParse(dateTimeString, out DateTimeOffset dateTimeOffset);

            success.Should().BeFalse();
        }

        public static IEnumerable<object[]> ParsingAStringParams()
        {
            yield return new object[]
            {
                "20140210100000[-03:EST]",
                new DateTimeOffset(year: 2014, month: 02, day: 10, hour: 00, minute: 00, second: 00, new TimeSpan(-8, 0, 0))
            };

            yield return new object[]
            {
                "20140212100000[-04:EST]",
                new DateTimeOffset(year: 2014, month: 02, day: 12, hour: 00, minute: 00, second: 00, new TimeSpan(-9, 0, 0))
            };

            yield return new object[]
            {
                "20140219100000[-05:EST]",
                new DateTimeOffset(year: 2014, month: 02, day: 19, hour: 00, minute: 00, second: 00, new TimeSpan(-10, 0, 0))
            };
        }

    }
}
