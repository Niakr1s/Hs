using HsLib.Types.Stats;

namespace HsLibTests.Types.Stats
{
    [TestClass()]
    public class MpTests
    {

        [TestMethod()]
        public void IsEnoughTest()
        {
            const int mpInitValue = 5;
            Mp mp = new(mpInitValue);

            for (int i = -10; i < mpInitValue + 10; i++)
            {
                Assert.AreEqual(i <= 5, mp.IsEnough(i));
            }
        }

        [TestMethod()]
        public void DecreaseTest()
        {
            const int mpInitValue = 5;

            for (int i = -10; i < mpInitValue + 10; i++)
            {
                Mp mp = new(mpInitValue);
                void mpDecrease() => mp.Decrease(i);

                if (i < 0 || i > mpInitValue)
                {
                    Assert.ThrowsException<ArgumentException>(mpDecrease);
                }
                else
                {
                    mpDecrease();
                    Assert.AreEqual(mpInitValue - i, mp.Value);
                }
            }
        }
    }
}