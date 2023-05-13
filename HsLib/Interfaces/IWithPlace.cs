using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface IWithPlace
    {
        public Loc Loc { get; }

        public Pid Pid { get; }

        public int Index { get; }
    }
}
