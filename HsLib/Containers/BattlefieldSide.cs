using HsLib.Battle;
using HsLib.Cards;
using HsLib.Cards.KnownCards.Weapons;
using HsLib.Common;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Containers
{
    public class BattlefieldSide : IWithEvent<ContainerEventArgs>
    {
        public BattlefieldSide(Battlefield bf, Pid pid, StartingDeck startingDeck)
        {
            Bf = bf;
            Pid = pid;

            Deck = new(bf, pid, cards: startingDeck.Cards);
            Hand = new(bf, pid);
            Field = new(bf, pid);

            Hero = new HeroC(bf, pid, (Hero)startingDeck.HeroId.ToCard());
            Ability = new AbilityC(bf, pid, Hero.Card.ProduceAbility());
            Weapon = new WeaponC(bf, pid, new NoWeapon());

            Secrets = new SecretC(bf, pid);

            Graveyard = new(bf, pid);

            List<IWithEvent<ContainerEventArgs>> containers = new()
            {
                Deck, Hand, Field, Hero, Ability, Weapon, Secrets, Graveyard,
            };
            containers.ForEach(c => c.Event += (s, e) => Event?.Invoke(s, e));
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public event EventHandler<ContainerEventArgs>? Event;

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public HeroC Hero { get; }

        public AbilityC Ability { get; }

        public WeaponC Weapon { get; }

        public SecretC Secrets { get; }

        public Graveyard Graveyard { get; }

        /// <summary>
        /// Gets all cards in all containers.
        /// </summary>
        public IEnumerable<Card> Cards
        {
            get
            {
                return Deck.Cards
                    .Concat(Hand.Cards)
                    .Concat(Field.Cards)
                    .Concat(Hero.Cards)
                    .Concat(Ability.Cards)
                    .Concat(Weapon.Cards)
                    .Concat(Secrets.Cards)
                    .Concat(Graveyard.Cards);
            }
        }
    }
}
