using HsLib.Functions;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;
using HsLib.Types.Turns;

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

        public Action UseEffect(IBoard board, ICard owner, ICard? target)
        {
            if (target is Minion m)
            {
                return () =>
                {
                    Enchant<T> buff = _statChooser(target).AddBuff(Value);
                    if (TillEndOfTurn)
                    {
                        Do.Once<TurnEventArgs>(h => board.Turn.Event += h, h => board.Turn.Event -= h,
                            e => e.Type == TurnEventType.End, () => buff.Deactivate());
                    }
                };
            }
            else
            {
                throw new ValidationException("target is not Minion");
            }
        }
    }
}
