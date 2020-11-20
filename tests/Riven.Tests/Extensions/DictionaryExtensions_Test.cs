using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Riven.Extensions
{
    public class DictionaryExtensions_Test
    {

        [Fact]
        public void GetOrDefault()
        {
            var dict = new Dictionary<string, string>();

            Assert.Null(dict.GetOrDefault("name"));

            dict.Add("name", "staneee");

            Assert.NotNull(dict.GetOrDefault("name"));
        }
    }
}
