using HsLib.Cards.Weapons;
using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Containers.Base;
using HsLib.Types.Events;
using HsLib.Types.PlayerAskers;
using HsLib.Types.Stats;

namespace HsLib.Systems
{
    public class BattlefieldPlayer : IWithEvent<ContainerEventArgs>
    {
        public BattlefieldPlayer(Battlefield bf, Pid pid, StartingDeck startingDeck)
        {
            Bf = bf;
            Pid = pid;

            Deck = new(bf, pid, cards: startingDeck.Cards);
            Hand = new(bf, pid);
            Field = new(bf, pid);

            Hero = new HeroContainer(bf, pid, (Hero)startingDeck.HeroId.ToCard());
            Ability = new AbilityContainer(bf, pid, Hero.Card.ProduceAbility());
            Weapon = new WeaponContainer(bf, pid, new NoWeapon());

            Secrets = new Secrets(bf, pid);

            List<IWithEvent<ContainerEventArgs>> containers = new()
            {
                Deck, Hand, Field, Hero, Ability, Weapon, Secrets,
            };
            containers.ForEach(c => c.Event += (s, e) => Event?.Invoke(s, e));
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public event EventHandler<ContainerEventArgs>? Event;

        public IPlayer Player { get; set; } = new DefaultPlayer();

        public Mp Mp { get; } = new Mp(0);

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public HeroContainer Hero { get; }

        public AbilityContainer Ability { get; }

        public WeaponContainer Weapon { get; }

        public Secrets Secrets { get; }

        public ICard GetCard(Loc loc, int index)
        {
            return loc switch
            {
                Loc.Deck => Deck[index],
                Loc.Hand => Hand[index],
                Loc.Field => Field[index],
                Loc.Hero => Hero[index],
                Loc.Weapon => Weapon[index],
                Loc.Ability => Ability[index],
                Loc.Secrets => Secrets[index],
                _ => null,
            } ?? throw new ArgumentException("wrong loc");
        }

        /// <summary>
        /// Gets all cards in all containers.
        /// </summary>
        public IEnumerable<ICard> Cards
        {
            get
            {
                return Deck.Cards
                    .Concat(Hand.Cards)
                    .Concat(Field.Cards)
                    .Concat(Hero.Cards)
                    .Concat(Ability.Cards)
                    .Concat(Weapon.Cards)
                    .Concat(Secrets.Cards);
            }
        }

        /// <summary>
        /// Remove inactive cards from all containers and return them.
        /// </summary>
        /// <returns>Cleaned cards</returns>
        public IEnumerable<RemovedCard> CleanInactiveCards()
        {
            return Deck.RemoveInactiveCards()
                .Concat(Hand.RemoveInactiveCards())
                .Concat(Field.RemoveInactiveCards())
                .Concat(Hero.RemoveInactiveCards())
                .Concat(Ability.RemoveInactiveCards())
                .Concat(Weapon.RemoveInactiveCards())
                .Concat(Secrets.RemoveInactiveCards());
        }
    }
}
