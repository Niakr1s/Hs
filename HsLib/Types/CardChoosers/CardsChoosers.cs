using HsLib.Interfaces;
using HsLib.Types.Cards;

namespace HsLib.Types.CardsChoosers
{
    public static class CardsChoosers
    {
        public static ICardsChooser HeroChooser()
        {
            CardsChooserFunc f = (pid, cards) => cards.Where(c => c is Hero && c.Place!.Pid == pid && c.Place!.Loc == Loc.Hero);
            return f.ToCardsChooser();
        }
    }
}
