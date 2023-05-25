using Force.DeepCloner;
using HsLib.Systems;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    /// <summary>
    /// Parent for all cards.<br/><br/>
    /// Card reacts on 4 Battlefield events:<br/>
    /// <see cref="Subscribe(Battlefield)"/><br/>
    /// <see cref="Unsubscribe(Battlefield, Place)"/><br/>
    /// <see cref="OnTurnEnd(Battlefield)"/><br/>
    /// <see cref="OnTurnStart(Battlefield)"/>.<br/><br/>
    /// </summary>
    public abstract class Card : ICard, IWithPlaceInContainer
    {
        protected Card(int mp)
        {
            Mp = new Mp(mp);
        }

        public PlaceInContainer? PlaceInContainer { get; set; }

        public Mp Mp { get; protected set; }

        public virtual bool ShouldBeRemovedFromCurrentContainer() { return false; }


        #region reactive

        public virtual void Subscribe(Battlefield bf) { }

        public virtual void Unsubscribe(Battlefield bf, Place previousPlace) { }



        public virtual void OnTurnEnd(Battlefield bf) { }

        public virtual void OnTurnStart(Battlefield bf) { }



        public virtual void OnPreAttack(Battlefield bf, IAttacker attacker, IDamageable defender) { }

        public virtual ICard Clone()
        {
            Card cloned = this.DeepClone();
            cloned.PlaceInContainer = null;
            cloned.Mp = (Mp)Mp.Clone();
            return cloned;
        }

        #endregion
    }
}
