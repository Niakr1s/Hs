namespace Models.Common.Place
{
    public enum Pid
    {
        None,
        P1,
        P2,
    }

    public static class Pids
    {
        public static Pid[] All(bool withNone = false)
        {
            if (withNone)
            {
                return new Pid[] { Pid.None, Pid.P1, Pid.P2 };
            }
            else
            {
                return new Pid[] { Pid.P1, Pid.P2 };
            }
        }
    }
}
