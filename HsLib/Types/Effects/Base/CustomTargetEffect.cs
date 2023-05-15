using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    /// <summary>
    /// В этом классе target из метода <see cref="UseEffect(Battlefield, Pid, ICard?)"/> будет игнорироваться,
    /// а вместо него будет использоваться кастомный targetChooser.
    /// </summary>
    public class CustomTargetEffect : TargetEffect
    {
        public CustomTargetEffect(IEffect effect, ICardChooser targetChooser, ICardsChooser? possibleTargetsChooser = null) :
            base(effect, possibleTargetsChooser ?? new Targets())
        {
            _targetChooser = targetChooser;
        }

        private readonly ICardChooser _targetChooser;

        /// <summary>
        /// Вместо параметра target будет вызываться кастомный targetChooser.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="pid"></param>
        /// <param name="target">должен быть null</param>
        public override void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            if (target is not null) { throw new ArgumentException("target should be null"); }

            ICard? targetCard = _targetChooser.ChooseCard(bf, pid);
            if (targetCard is not null) { _effect.UseEffect(bf, targetCard); }
        }
    }
}
