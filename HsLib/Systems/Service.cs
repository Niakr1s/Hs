namespace HsLib.Systems
{
    public abstract class Service
    {
        protected Service(Board board)
        {
            Board = board;
        }

        public Board Board { get; }
    }
}
