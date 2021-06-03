using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nibo.Domain.Parser
{
    public struct OFXLine
    {
        public OFXLine(string tagName): this(tagName, string.Empty)
        {
        }

        public OFXLine(string tagName, string value)
        {
            TagName = tagName;
            Value = value;
        }

        public string TagName { get; }

        public string Value { get; } 

        public static bool TryParse(string value, out OFXLine ofxLine)
        {
            var regex = new Regex(@"<(?<tag>/?\w+)>(?<value>.*)");
            var match = regex.Match(value);

            if (regex.IsMatch(value))
            {
                ofxLine = new OFXLine(match.Groups["tag"].Value, match.Groups["value"].Value);
                return true;
            }

            ofxLine = new OFXLine();
            return false;
        }
    }
}
