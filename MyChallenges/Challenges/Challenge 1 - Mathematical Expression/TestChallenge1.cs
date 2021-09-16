using NUnit.Framework;
using MyChallenges;

namespace MyTests
{
    [TestFixture]
    public class TestChallenge1
    {
        [Test, Description("should handle parentheses")]
        public void ShouldHandleParentheses()
        {
            Assert.AreEqual(6, Challenge1.Resolve("(1+1) * 3"), 1e-7);
        }

        [Test, Description("should handle addition")]
        public void ShouldHandleAddition()
        {
            Assert.AreEqual(2, Challenge1.Resolve("1+1"), 1e-7);
        }

        [Test, Description("should handle subtraction")]
        public void ShouldHandleSubtraction()
        {
            Assert.AreEqual(0, Challenge1.Resolve("1 - 1"), 1e-7);
        }

        [Test, Description("should handle multiplication")]
        public void ShouldHandleMultiplication()
        {
            Assert.AreEqual(1, Challenge1.Resolve("1* 1"), 1e-7);
            Assert.AreEqual(1476, Challenge1.Resolve("12* 123"), 1e-7);
        }

        [Test, Description("should handle division")]
        public void ShouldHandleDivision()
        {
            Assert.AreEqual(5, Challenge1.Resolve("25 / 5"), 1e-7);
        }
    }
}