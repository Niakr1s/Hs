using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class AbilityContainer : SingleContainer<Ability>
    {
        public AbilityContainer(Battlefield bf, Pid pid, Ability card) : base(bf, new Place(pid, Loc.Ability), card)
        {
        }

        public Action UseAbility(ICard? target = null)
        {
            BattlefieldPlayer player = Bf[Place!.Pid];
            if (!player.Mp.IsEnough(Card.Mp)) { throw new ValidationException("not enough mp"); }
            Action effectAction = Card.UseAbility(Bf, target);

            return () =>
            {
                player.Mp.Use(Card.Mp);
                effectAction();
            };
        }
    }
}
