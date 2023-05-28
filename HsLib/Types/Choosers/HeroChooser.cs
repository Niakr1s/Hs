using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Choosers
{
    public class HeroChooser : Chooser<ICard>
    {
        private static readonly ChooserFunc<ICard> _f =
            (bf, owner) => bf.Cards
            .Where(c => c is Hero && c.Place.Pid == owner.Place.Pid && c.Place.Loc == Loc.Hero);

        public HeroChooser() : base(_f)
        {
        }
    }
}
