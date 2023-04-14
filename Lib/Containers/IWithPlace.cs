using Models.Common;

namespace Models.Containers
{
    public interface IWithPlace
    {
        public Loc Loc { get; }

        public Pid Pid { get; }
    }
}
