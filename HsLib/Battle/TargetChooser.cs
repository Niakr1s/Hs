using HsLib.Cards;
using HsLib.Common.Place;

namespace HsLib.Battle
{
    public class TargetChooser
    {
        public TargetChooser(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        public IEnumerable<Card> Choose(IWithPlace owner, Target target)
        {
            return Bf.Cards.Where(c => target.IsValidTarget(owner, c));
        }
    }
}
