using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Riven.Extensions
{
    public class ObjectExtensions_Test
    {
        [Fact]
        public void As()
        {
            object obj = new SampleObject()
            {
                Txt = string.Empty
            };

            var sampleObject = obj.As<SampleObject>();

            Assert.Equal(obj, sampleObject);
        }

        [Fact]
        public void To()
        {
            Assert.Equal(SampleEnum.Default, 0.To<SampleEnum>());
            Assert.Equal(SampleEnum.Open, 1.To<SampleEnum>());
            Assert.Equal(SampleEnum.Closed, 2.To<SampleEnum>());


            Assert.Throws<ArgumentException>(() =>
            {
                20.To<SampleEnum>();
            });
        }
    }

    public class SampleObject
    {
        public string Txt { get; set; }
    }

    public enum SampleEnum
    {
        Default = 0,
        Open = 1,
        Closed = 2
    }

}
