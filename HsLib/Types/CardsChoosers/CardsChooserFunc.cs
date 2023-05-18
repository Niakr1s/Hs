using HsLib.Types.Cards;

namespace HsLib.Types.CardsChoosers
{
    public delegate IEnumerable<ICard> CardsChooserFunc<TOwner>(TOwner owner, IEnumerable<ICard> cards);

    public static class CardsChooserFuncExtensions
    {
        public static ICardsChooser<TOwner> ToCardsChooser<TOwner>(this CardsChooserFunc<TOwner> f)
        {
            return new CardsChooser<TOwner>(f);
        }
    }
}
