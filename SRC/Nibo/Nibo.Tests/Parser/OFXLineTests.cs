using FluentAssertions;
using Nibo.Domain.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nibo.Tests.Parser
{
    public class OFXLineTests
    {
        [Theory]
        [InlineData("<STMTTRN>", true)]
        [InlineData("<DTSTART>20140201100000[-03:EST]", true)]
        [InlineData("</STMTTRN>", true)]
        [InlineData("", false)]
        [InlineData("CHECKING", false)]
        public void Deciding_if_the_string_can_be_parsed(string line, bool expectedResult)
        {
            var result = OFXLine.TryParse(line, out OFXLine ofxLine);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Parsing_a_opening_tag()
        {
            string line = "<STMTTRN>";

            var success = OFXLine.TryParse(line, out OFXLine ofxLine);

            success.Should().BeTrue();
            ofxLine.Should().BeEquivalentTo(new OFXLine(tagName: "STMTTRN", value: string.Empty));
        }

        [Fact]
        public void Parsing_a_closing_tag()
        {
            string line = "</STMTTRN>";

            var success = OFXLine.TryParse(line, out OFXLine ofxLine);

            success.Should().BeTrue();
            ofxLine.Should().BeEquivalentTo(new OFXLine(tagName: "/STMTTRN", value: string.Empty));
        }

        [Fact]
        public void Parsing_a_value_tag()
        {
            string line = "<BANKID>0341";

            var success = OFXLine.TryParse(line, out OFXLine ofxLine);

            success.Should().BeTrue();
            ofxLine.Should().BeEquivalentTo(new OFXLine(tagName: "BANKID", value: "0341"));
        }

    }
}
