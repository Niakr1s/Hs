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

            _containerList = new ContainerList(new List<IContainer>()
            {
                Deck, Hand, Field, Hero, Ability, Weapon, Secrets,
            });
            _containerList.ForEach(c => c.Event += (s, e) => Event?.Invoke(s, e));
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

        private readonly ContainerList _containerList;

        public ICard GetCard(Loc loc, int index) => _containerList.GetCard(loc, index);

        /// <summary>
        /// Gets all cards in all containers.
        /// </summary>
        public IEnumerable<ICard> Cards => _containerList.Cards;

        /// <summary>
        /// Remove inactive cards from all containers and return them.
        /// </summary>
        /// <returns>Cleaned cards</returns>
        public IEnumerable<RemovedCard> RemoveInactiveCards() => _containerList.RemoveInactiveCards();
    }
}
