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

            _hero = (Hero)startingDeck.HeroId.ToCard();
            _ability = Hero.ProduceAbility();
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        private NotifyCollectionChangedEventHandler? _collectionChanged;


        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                _collectionChanged += value;
                Deck.CollectionChanged += value;
                Hand.CollectionChanged += value;
                Field.CollectionChanged += value;
                Secrets.CollectionChanged += value;
            }
            remove
            {
                _collectionChanged -= value;
                Deck.CollectionChanged -= value;
                Hand.CollectionChanged -= value;
                Field.CollectionChanged -= value;
                Secrets.CollectionChanged -= value;
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



        #region single cards

        private Hero _hero;
        public Hero Hero
        {
            get { return _hero; }
            set
            {
                if (_hero == value)
                {
                    return;
                }

                Hero old = _hero;
                old.PlaceInContainer = null;
                value.PlaceInContainer = new(Pid, Loc.Hero, Bf.Turn.No, 0);
                _hero = value;
                _collectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, old));
            }
        }

        private Ability _ability;
        public Ability Ability
        {
            get { return _ability; }
            set
            {
                if (_ability == value)
                {
                    return;
                }

                Ability old = _ability;
                old.PlaceInContainer = null;
                value.PlaceInContainer = new(Pid, Loc.Ability, Bf.Turn.No, 0);
                _ability = value;
                _collectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, old));
            }
        }

        private Weapon? _weapon;
        public Weapon? Weapon
        {
            get { return _weapon; }
            set
            {
                if (_weapon == value)
                {
                    return;
                }

                Weapon? old = _weapon;
                if (old is not null) { old.PlaceInContainer = null; }
                if (value is not null) { value.PlaceInContainer = new(Pid, Loc.Ability, Bf.Turn.No, 0); }
                _weapon = value;
                _collectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, old));
            }
        }

        #endregion



        /// <summary>
        /// Gets all cards in all containers in non-chronological order.
        /// </summary>
        public IEnumerable<ICard> Cards
        {
            get
            {
                yield return Hero;
                yield return Ability;
                if (Weapon is not null) { yield return Weapon; }

                foreach (ICard card in Deck.AsEnumerable<ICard>().Concat(Hand).Concat(Field).Concat(Secrets))
                {
                    yield return card;
                }
            }
        }

        public IEnumerable<ICard> this[Loc loc] =>
            Cards.Where(c => c.PlaceInContainer is not null && loc.HasFlag(c.PlaceInContainer.Loc));

        public bool Remove(ICard card)
        {
            if (Weapon == card) { Weapon = null; return true; }
            return Deck.Remove(card) || Hand.Remove(card) || Field.Remove(card) || Secrets.Remove(card);
        }
    }
}
