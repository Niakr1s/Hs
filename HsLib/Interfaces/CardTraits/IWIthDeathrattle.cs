using HsLib.Types;

namespace HsLib.Interfaces.CardTraits
{
    public interface IWithDeathrattle
    {
        IActiveEffect<Pid>? Deathrattle { get; }
    }
}
