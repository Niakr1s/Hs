using HsLib.Types.BattlefieldSubscribers;
using HsLib.Types.Cards;
using HsLib.Types.Choosers;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Minions
{
    public class Doomsayer : Minion
    {
        public Doomsayer() : base(2, 0, 7)
        {
            DoomsayerEffectSource effectSource = new(this);
            FieldEffectSources.Add(effectSource);
        }
    }

    public class DoomsayerEffectSource : BattlefieldSubscriber<ICard>
    {
        public DoomsayerEffectSource(Minion owner) : base(owner)
        {
        }

        private static readonly Targets _targets = new() { Locs = Loc.Field, Sides = PidSide.Me | PidSide.He };

        protected override void OnTurnStart()
        {
            if (Bf!.Turn.Pid == Owner.PlaceInContainer!.Pid)
            {
                foreach (IMortal card in _targets.ChooseCards(Owner.PlaceInContainer.Pid, Bf.Cards)
                    .Cast<IMortal>())
                {
                    card.Dead = true;
                }
                Bf.DeathService.ProcessDeaths();
            }
        }
    }
}
