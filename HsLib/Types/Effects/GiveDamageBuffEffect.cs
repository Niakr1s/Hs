using HsLib.Functions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Events;
using HsLib.Types.Stats.Base;

namespace HsLib.Types.Effects
{
    public class GiveDamageBuffEffect : Effect
    {
        public GiveDamageBuffEffect(Card owner, EffectType type, Targets targets) : base(owner, type, targets)
        {
        }

        public int DamageBuff { get; set; }

        public bool TillEndOfTurn { get; set; }

        protected override void EffectAction(Battlefield bf, Card? card)
        {
            if (card is Minion m)
            {
                Enchant<int> buff = m.Atk.AddBuff(2);
                if (TillEndOfTurn)
                {
                    Do.Once(bf, e => e.EventArgs is TurnEndEventArgs, () => buff.Active = false);
                }
            }
        }
    }
}
