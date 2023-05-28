using HsLib.Types.Cards;
using HsLib.Types.Choosers;

namespace HsLib.Types.Effects
{
    public class BattlecryEffect : PlayerEffect
    {
        public BattlecryEffect(ICard owner, IEffect effect,
            IChooser<ICard>? possibleTargetsChooser = null, IChooser<ICard>? targetsChooser = null)
            : base(owner, effect, possibleTargetsChooser, targetsChooser)
        {
        }
    }
}