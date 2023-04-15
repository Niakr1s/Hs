namespace HsLib.Cards.Tests
{
    [TestClass()]
    public class CardBuilderTests
    {
        public TestContext? TestContext { get; set; }

        [TestMethod()]
        public void FromIdTest()
        {
            foreach (CardId id in Enum.GetValues(typeof(CardId)))
            {
                // checking it doesn't throw exception
                TestContext?.Write($"Test: {id}");
                CardBuilder.FromId(id);
                TestContext?.WriteLine(" => success");
            }
        }
    }
}