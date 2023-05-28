using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public delegate IEnumerable<ICard> ChooserFunc<TOwner>(Board board, TOwner owner);

    public static class CardsChooserFuncExtensions
    {
        public static IChooser<TOwner> ToCardsChooser<TOwner>(this ChooserFunc<TOwner> f)
        {
            return new Chooser<TOwner>(f);
        }
    }
}
