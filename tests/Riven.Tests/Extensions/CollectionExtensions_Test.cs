using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Riven.Extensions;

namespace Riven.Extensions
{
    public class CollectionExtensions_Test
    {
        [Fact]
        public void IsNullOrEmpty()
        {
            var list = new List<string>();
            list.Add(string.Empty);

            Assert.False(list.IsNullOrEmpty());

            list.Clear();
            Assert.True(list.IsNullOrEmpty());


            list = null;
            Assert.True(list.IsNullOrEmpty());
        }

        [Fact]
        public void AddIfNotContains()
        {
            var list = new List<string>();

            Assert.True(
                list.AddIfNotContains(string.Empty)
             );

            Assert.False(
                list.AddIfNotContains(string.Empty)
            );
        }
    }
}
