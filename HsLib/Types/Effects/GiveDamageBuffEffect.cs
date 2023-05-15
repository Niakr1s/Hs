using HsLib.Functions;
using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Events;
using HsLib.Types.Stats.Base;

namespace HsLib.Types.Effects
{
    public class GiveDamageBuffEffect : IEffect
    {
        public int DamageBuff { get; set; }

        public bool TillEndOfTurn { get; set; }

        public void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            if (target is Minion m)
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
