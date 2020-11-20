using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace Riven.Extensions
{
    public class StringExtensions_Test
    {
        [Fact]
        public void EnsureEndsWith()
        {
            "abc".EnsureEndsWith('x').ShouldBe("abcx");
        }

        [Fact]
        public void EnsureStartsWith()
        {
            "abc".EnsureStartsWith('x').ShouldBe("xabc");
        }

        [Fact]
        public void IsNullOrEmpty()
        {
            "".IsNullOrEmpty().ShouldBeTrue();
            " ".IsNullOrEmpty().ShouldBeFalse();
            "abc".IsNullOrEmpty().ShouldBeFalse();

        }

        [Fact]
        public void IsNullOrWhiteSpace()
        {
            "".IsNullOrWhiteSpace().ShouldBeTrue();
            " ".IsNullOrWhiteSpace().ShouldBeTrue();
            "abc".IsNullOrWhiteSpace().ShouldBeFalse();

        }

        [Fact]
        public void Left()
        {
            var str = default(string);

            Should.Throw<ArgumentNullException>(() =>
            {
                str.Left(5);
            });

            str = "abcedfg";
            Should.Throw<ArgumentException>(() =>
            {
                str.Left(20);
            });

            str.Left(3).ShouldBe("abc");
        }

        [Fact]
        public void Right()
        {
            var str = default(string);

            Should.Throw<ArgumentNullException>(() =>
            {
                str.Right(5);
            });

            str = "abcedfg";
            Should.Throw<ArgumentException>(() =>
            {
                str.Right(20);
            });

            str.Right(3).ShouldBe("dfg");
        }

        [Fact]
        public void NormalizeLineEndings()
        {
            switch (System.Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                    {
                        var str = "a\nb\n";
                        str.NormalizeLineEndings().ShouldContain("\r\n");
                    }
                    break;
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    {
                        var str = "a\r\nb\r\n";
                        str.NormalizeLineEndings().ShouldContain("\n");
                        str.NormalizeLineEndings().ShouldNotContain("\r\n");
                    }
                    break;
            }
        }

        [Fact]
        public void NthIndexOf()
        {
            var str = default(string);

            Should.Throw<ArgumentNullException>(() =>
            {
                str.NthIndexOf('a', 2);
            });

            str = "abcabcabc";

            str.NthIndexOf('a', 2).ShouldBe(3);
            str.NthIndexOf('x', 2).ShouldBe(-1);
        }

        [Fact]
        public void RemovePostFix()
        {
            var str = default(string);
            str.RemovePostFix("x").ShouldBeNullOrEmpty();


            str = "abcxxx";

            str.RemovePostFix("x").ShouldBe("abcxx");

            str.RemovePostFix("c", "xxx").ShouldBe("abc");

            str.RemovePostFix("s").ShouldBe("abcxxx");
        }

        [Fact]
        public void RemovePreFix()
        {
            var str = default(string);
            str.RemovePreFix("a").ShouldBeNullOrEmpty();


            str = "abcxxx";

            str.RemovePreFix("a").ShouldBe("bcxxx");

            str.RemovePreFix("b", "a").ShouldBe("bcxxx");

            str.RemovePreFix("s").ShouldBe("abcxxx");
        }

        [Fact]
        public void ToCamelCase()
        {
            "PascalCase".ToCamelCase().ShouldBe("pascalCase");
        }

        [Fact]
        public void ToSentenceCase()
        {
            "ThisIsSampleSentence".ToSentenceCase().ShouldBe("This is sample sentence");
        }

        [Fact]
        public void ToPascalCase()
        {
            "pascalCase".ToPascalCase().ShouldBe("PascalCase");
        }

        [Fact]
        public void ToEnum()
        {
            var str = default(string);

            Should.Throw<ArgumentNullException>(() =>
            {
                str.ToEnum<StringEnum>();
            });


            str = "Default";
            str.ToEnum<StringEnum>().ShouldBe(StringEnum.Default);


            str = "Abc";
            Should.Throw<ArgumentException>(() =>
            {
                str.ToEnum<StringEnum>();
            });
        }

        public enum StringEnum
        {
            Default = 0,
            Open = 1
        }
    }
}
