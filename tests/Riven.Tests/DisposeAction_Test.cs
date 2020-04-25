using Shouldly;
using Xunit;

namespace Riven.Tests
{
    public class DisposeAction_Test
    {

        [Fact]
        public void Should_Call_Action_When_Disposed()
        {
            var actionIsCalled = false;

            using (new DisposeAction(() => actionIsCalled = true))
            {
                actionIsCalled.ShouldBe(false);
            }


            actionIsCalled.ShouldBe(true);
        }
    }
}

