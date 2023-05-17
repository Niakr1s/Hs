namespace HsLib.Interfaces
{
    public interface ICardsChooser<TOwner>
    {
        IEnumerable<ICard> ChooseCards(TOwner owner, IEnumerable<ICard> cards);
    }
}
