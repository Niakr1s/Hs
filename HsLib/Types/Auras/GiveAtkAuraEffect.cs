using HsLib.Interfaces;
using HsLib.Interfaces.CardTraits;
using HsLib.Systems;
using HsLib.Types.Stats.Base;

namespace HsLib.Types.Auras
{
    public class GiveAtkAuraEffect : IAuraEffect
    {
        public int AtkValue { get; init; } = 0;

        public IEnchantHandler GiveAura(Battlefield bf, ICard target)
        {
            IWithAtk a = (IWithAtk)target;
            return a.Atk.AddAura(AtkValue);
        }
    }
}
