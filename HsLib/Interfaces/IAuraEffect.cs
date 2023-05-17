using HsLib.Systems;
using HsLib.Types.Stats.Base;

namespace HsLib.Interfaces
{
    public interface IAuraEffect
    {
        IEnchantHandler GiveAura(Battlefield bf, ICard owner, ICard target);
    }
}
