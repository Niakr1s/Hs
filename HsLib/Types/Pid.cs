namespace HsLib.Types
{
    public enum Pid
    {
        P1,
        P2,
    }


    public enum PidSide
    {
        Me,
        He,
    }

    public static class Pids
    {
        public static Pid[] All()
        {
            return new Pid[] { Pid.P1, Pid.P2 };
        }
    }

    public static class PidExtensions
    {
        public static Pid He(this Pid pid) => pid switch
        {
            Pid.P1 => Pid.P2,
            Pid.P2 => Pid.P1,
            _ => throw new InvalidOperationException()
        };

        public static PidSide Side(this Pid pid, Pid other)
        {
            return (pid == other) switch
            {
                true => PidSide.Me,
                false => PidSide.He,
            };
        }
    }

    public class PidException : ArgumentException
    {
        public PidException() : base()
        {
        }

        public PidException(string? message) : base(message)
        {
        }

        public PidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
