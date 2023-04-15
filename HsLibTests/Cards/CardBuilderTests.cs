namespace HsLib.Cards.Tests
{
    [TestClass()]
    public class CardBuilderTests
    {
        [TestMethod()]
        public void FromIdTest()
        {
            foreach (CardId id in Enum.GetValues(typeof(CardId)))
            {
                // checking it doesn't throw exception
                CardBuilder.FromId(id);
            }
        }
    }
}