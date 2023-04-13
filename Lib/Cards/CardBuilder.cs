namespace Models.Cards
{
    public class CardBuilder
    {
        public Card? FromId(CardId id)
        {

            return id switch
            {
                CardId.AbusiveSergeant => null,
                _ => null,
            };
        }
    }
}
