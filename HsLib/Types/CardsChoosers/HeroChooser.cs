using HsLib.Types.Cards;

namespace HsLib.Types.CardsChoosers
{
    public class HeroChooser : CardsChooser<Pid>
    {
        private static readonly CardsChooserFunc<Pid> _f =
            (owner, cards) => cards
            .Where(c => c is Hero && c.PlaceInContainer!.Pid == owner && c.PlaceInContainer!.Loc == Loc.Hero);

        public HeroChooser() : base(_f)
        {
        }
    }
}
