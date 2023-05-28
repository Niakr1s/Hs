using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Minions
{
    public class FacelessManipulator : Minion
    {
        public FacelessManipulator() : base(5, 3, 3)
        {
            TransformEffect effect = new();
            Targets possibleTargets = new() { Locs = Loc.Field, Sides = Side.Me | Side.He };
            BattlecryEffect = new(this, effect, possibleTargetsChooser: possibleTargets);
        }
    }
}
