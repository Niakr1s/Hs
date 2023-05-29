using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.Types.Effects
{
    public class DeathrattleEffect
    {
        public DeathrattleEffect(IEffect effect, IChooser<Pid> targetsChooser)
        {
            Effect = effect;
            TargetsChooser = targetsChooser;
        }

        public IEffect Effect { get; }
        public IChooser<Pid> TargetsChooser { get; }

        public Action ActivateDeathrattle(IBoard board, Pid owner)
        {
            List<Action> effectActions = new();

            foreach (ICard target in TargetsChooser.ChooseCards(board, owner))
            {
                // using null! for owner param, coz deathrattle should use only ono-owner effects
                effectActions.Add(Effect.UseEffect(board, null!, target));
            }
            return () => effectActions.ForEach(a => a.Invoke());
        }
    }
}