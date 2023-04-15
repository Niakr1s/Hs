using HsLib.Cards;
using HsLib.Cards.KnownCards.Weapons;
using HsLib.Common.Place;

namespace HsLib.Containers
{
    public class BattlefieldSide
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
        }

        public Pid Pid { get; }

        public Battlefield Bf { get; }

        public Deck Deck { get; }

        public Hand Hand { get; }

        public Field Field { get; }

        public HeroC Hero { get; }

        public AbilityC Ability { get; }

        public WeaponC Weapon { get; }

        public SecretC Secrets { get; }

        public Graveyard Graveyard { get; }
    }
}
