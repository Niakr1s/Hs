using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Player;
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
            Secrets = new Secrets(bf, pid);

            _heroContainer = new(bf, pid, (Hero)startingDeck.HeroId.ToCard());
            _abilityContainer = new(bf, pid, Hero.ProduceAbility());
            _weaponContainer = new(bf, pid);
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                Deck.CollectionChanged += value;
                Hand.CollectionChanged += value;
                Field.CollectionChanged += value;
                Secrets.CollectionChanged += value;

                _heroContainer.CollectionChanged += value;
                _abilityContainer.CollectionChanged += value;
                _weaponContainer.CollectionChanged += value;
            }
            remove
            {
                Deck.CollectionChanged -= value;
                Hand.CollectionChanged -= value;
                Field.CollectionChanged -= value;
                Secrets.CollectionChanged -= value;

                _heroContainer.CollectionChanged -= value;
                _abilityContainer.CollectionChanged -= value;
                _weaponContainer.CollectionChanged -= value;
            }
        }

        public IPlayer Player { get; set; } = new DefaultPlayer();

        public PlayerMp Mp { get; } = new(0);



        #region multi containers

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public Secrets Secrets { get; }

        #endregion



        #region single containers

        private readonly HeroContainer _heroContainer;
        public Hero Hero
        {
            get => _heroContainer.Card;
            set => _heroContainer.Card = value;
        }

        private readonly AbilityContainer _abilityContainer;
        public Ability Ability
        {
            get => _abilityContainer.Card;
            set => _abilityContainer.Card = value;
        }

        private readonly WeaponContainer _weaponContainer;
        public Weapon? Weapon
        {
            get => _weaponContainer.Card;
            set => _weaponContainer.Card = value;
        }

        #endregion



        /// <summary>
        /// Gets all cards in all containers in non-chronological order.
        /// </summary>
        public IEnumerable<ICard> Cards => Deck.AsEnumerable<ICard>().Concat(Hand).Concat(Field).Concat(Secrets)
                    .Concat(_heroContainer).Concat(_abilityContainer).Concat(_weaponContainer);

        public IEnumerable<ICard> this[Loc loc] =>
            Cards.Where(c => loc.HasFlag(c.Place.Loc));

        public bool Remove(ICard card)
        {
            return Deck.Remove(card) || Hand.Remove(card) || Field.Remove(card) || Secrets.Remove(card) ||
                _weaponContainer.Remove(card);
        }

        public IContainer? GetContainer(ICard card)
        {
            if (card.Place.Pid != Pid) { return null; }

            return card.Place.Loc switch
            {
                Loc.Deck => Deck,
                Loc.Hand => Hand,
                Loc.Field => Field,
                Loc.Secrets => Secrets,
                Loc.Hero => _heroContainer,
                Loc.Weapon => _weaponContainer,
                Loc.Ability => _abilityContainer,
                _ => null,
            };
        }
    }
}
