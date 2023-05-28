using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Player;
using HsLib.Types.Stats;
using System.Collections.Specialized;
using System.Diagnostics;

namespace HsLib.Systems
{
    public class BoardSide : INotifyCollectionChanged
    {
        public BoardSide(Board board, Pid pid, StartingDeck startingDeck)
        {
            Board = board;
            Pid = pid;

            Deck = new(board, pid, startCards: startingDeck.Cards);
            Hand = new(board, pid);
            Field = new(board, pid);
            Secrets = new Secrets(board, pid);

            HeroContainer = new(board, pid, (Hero)startingDeck.HeroId.ToCard());
            AbilityContainer = new(board, pid, Hero.ProduceAbility());
            WeaponContainer = new(board, pid);
        }

        public Pid Pid { get; }

        public Board Board { get; }

        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                Deck.CollectionChanged += value;
                Hand.CollectionChanged += value;
                Field.CollectionChanged += value;
                Secrets.CollectionChanged += value;

                HeroContainer.CollectionChanged += value;
                AbilityContainer.CollectionChanged += value;
                WeaponContainer.CollectionChanged += value;
            }
            remove
            {
                Deck.CollectionChanged -= value;
                Hand.CollectionChanged -= value;
                Field.CollectionChanged -= value;
                Secrets.CollectionChanged -= value;

                HeroContainer.CollectionChanged -= value;
                AbilityContainer.CollectionChanged -= value;
                WeaponContainer.CollectionChanged -= value;
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

        public HeroContainer HeroContainer { get; }
        public Hero Hero
        {
            get => HeroContainer.Card;
            set => HeroContainer.Card = value;
        }

        public AbilityContainer AbilityContainer { get; }
        public Ability Ability
        {
            get => AbilityContainer.Card;
            set => AbilityContainer.Card = value;
        }

        public WeaponContainer WeaponContainer { get; }
        public Weapon? Weapon
        {
            get => WeaponContainer.Card;
            set => WeaponContainer.Card = value;
        }

        #endregion



        /// <summary>
        /// Gets all cards in all containers in non-chronological order.
        /// </summary>
        public IEnumerable<ICard> Cards => Deck.AsEnumerable<ICard>().Concat(Hand).Concat(Field).Concat(Secrets)
                    .Concat(HeroContainer).Concat(AbilityContainer).Concat(WeaponContainer);


        public IContainer this[Loc loc] => loc switch
        {
            Loc.Deck => Deck,
            Loc.Hand => Hand,
            Loc.Field => Field,
            Loc.Secrets => Secrets,
            Loc.Hero => HeroContainer,
            Loc.Weapon => WeaponContainer,
            Loc.Ability => AbilityContainer,
            _ => throw new UnreachableException(),
        };


        public bool Remove(ICard card)
        {
            return Deck.Remove(card) || Hand.Remove(card) || Field.Remove(card) || Secrets.Remove(card) ||
                WeaponContainer.Remove(card);
        }

        public IContainer? GetContainer(ICard card)
        {
            if (card.Place.Pid != Pid) { return null; }
            return this[card.Place.Loc];
        }
    }
}
