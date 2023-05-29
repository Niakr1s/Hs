using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.GameIntents
{
    internal class PlayFromHandIntent : GameIntent
    {
        public PlayFromHandIntent(IBoard board, ICard card, int? fieldIndex = null, ICard? effectTarget = null)
            : base(board, card)
        {
            FieldIndex = fieldIndex;
            EffectTarget = effectTarget;
        }

        public int? FieldIndex { get; }
        public ICard? EffectTarget { get; }
    }
}
