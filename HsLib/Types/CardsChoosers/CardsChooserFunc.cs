using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    public delegate IEnumerable<ICard> CardsChooserFunc(Pid ownerPid, IEnumerable<ICard> cards);

    public static class CardsChooserFuncExtensions
    {
        public static ICardsChooser ToCardsChooser(this CardsChooserFunc f)
        {
            return new CardsChooser(f);
        }
    }
}
