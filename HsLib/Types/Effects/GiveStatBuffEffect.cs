using HsLib.Functions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Events;
using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public class GiveStatBuffEffect<T> : IEffect
        where T : struct
    {
        public GiveStatBuffEffect(Func<ICard, Stat<T>> statChooser)
        {
            _statChooser = statChooser;
        }

        private readonly Func<ICard, Stat<T>> _statChooser;

        public T Value { get; set; }

        public bool TillEndOfTurn { get; set; }

        public Action UseEffect(Battlefield bf, ICard target)
        {
            Minion m = (Minion)target;

            return () =>
            {
                Enchant<T> buff = _statChooser(target).AddBuff(Value);
                if (TillEndOfTurn)
                {
                    Do.Once<TurnEventArgs>(h => bf.Turn.Event += h, h => bf.Turn.Event -= h,
                        e => e is TurnEndEventArgs, () => buff.Active = false);
                }
            };
        }
    }
}
