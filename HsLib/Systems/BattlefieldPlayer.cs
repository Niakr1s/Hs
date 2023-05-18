using HsLib.Cards.Weapons;
using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Containers.Base;
using HsLib.Types.Stats;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public class BattlefieldPlayer : INotifyCollectionChanged
    {
        public BattlefieldPlayer(Battlefield bf, Pid pid, StartingDeck startingDeck)
        {
            Bf = bf;
            Pid = pid;

            Deck = new(bf, pid, startCards: startingDeck.Cards);
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
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add { _containerList.CollectionChanged += value; }
            remove { _containerList.CollectionChanged -= value; }
        }

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
        /// Gets all cards in all containers in non-chronological order.
        /// </summary>
        public IEnumerable<ICard> Cards => _containerList.Cards;

        public IContainer this[Loc loc] => loc switch
        {
            Loc.Deck => Deck,
            Loc.Hand => Hand,
            Loc.Field => Field,
            Loc.Hero => Hero,
            Loc.Weapon => Weapon,
            Loc.Ability => Ability,
            Loc.Secrets => Secrets,
            _ => throw new ArgumentException($"wrong {loc}"),
        };
    }
}
