using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.GameActions;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.GameIntents
{
    public class PlayFromHandIntent : GameIntent
    {
        public PlayFromHandIntent(ICard actor, int? fieldIndex = null, ICard? effectTarget = null)
            : base(actor)
        {
            FieldIndex = fieldIndex;
            EffectTarget = effectTarget;
        }

        public int? FieldIndex { get; }
        public ICard? EffectTarget { get; }

        public override bool CanBeProcessed()
        {
            if (Actor.Place.IsNone()) { return false; }

            if (Actor.Board is null) { return false; }
            IBoard board = Actor.Board;

            PlayerMp mp = board[Actor.Place.Pid].Mp;
            if (!mp.IsEnough(Actor.Mp)) { return false; }

            if (Actor.Place.Loc != Loc.Hand) { return false; }

            if (FieldIndex is not null &&
                !board[Actor.Place.Pid].Field.CanBeInsertedAt(FieldIndex.Value)) { return false; }

            return Actor.CanProcessIntent(this);
        }

        public override IEnumerable<GameAction>? Process()
        {
            if (!CanBeProcessed()) { throw new ValidationException("intenr can't be processed"); }
            return Actor.ProcessIntent(this);
        }
    }
}
