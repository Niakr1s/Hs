using HsLib.Types.Cards;

namespace HsLib.Types.CardsChoosers
{
    public class HeroChooser : CardsChooser
    {
        private static readonly CardsChooserFunc _f = (pid, cards) => cards.Where(c => c is Hero && c.Place!.Pid == pid && c.Place!.Loc == Loc.Hero);

        public HeroChooser() : base(_f)
        {
        }
    }
}
