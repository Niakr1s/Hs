using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public delegate IEnumerable<ICard> ChooserFunc(Battlefield bf, ICard owner);

    public static class CardsChooserFuncExtensions
    {
        public static IChooser ToCardsChooser(this ChooserFunc f)
        {
            return new Chooser(f);
        }
    }
}
