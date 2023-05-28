using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Stats;

namespace HsLib.Types.Auras
{
    public class GiveStatAuraEffect<T> : IAuraEffect
        where T : struct
    {
        public GiveStatAuraEffect(Func<ICard, Stat<T>> statChooser)
        {
            _statChooser = statChooser;
        }

        private readonly Func<ICard, Stat<T>> _statChooser;

        public T Value { get; init; }

        public IEnchantHandler GiveAura(Board board, ICard owner, ICard target)
        {
            return _statChooser(target).AddAura(Value);
        }
    }
}
