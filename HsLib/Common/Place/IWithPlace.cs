namespace Models.Common.Place
{
    public interface IWithPlace
    {
        public Loc Loc { get; }

        public Pid Pid { get; }

        int TurnAdded { get; set; }
    }
}
