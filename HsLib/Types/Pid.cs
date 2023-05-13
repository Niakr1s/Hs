namespace HsLib.Types
{
    public enum Pid
    {
        None,
        P1,
        P2,
    }


    public enum PidSide
    {
        None,
        Me,
        He,
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

    public static class PidExtensions
    {
        public static Pid He(this Pid pid) => pid switch
        {
            Pid.P1 => Pid.P2,
            Pid.P2 => Pid.P1,
            _ => throw new Exception("pid is None")
        };

        public static PidSide Side(this Pid pid, Pid other)
        {
            if (pid == Pid.None || other == Pid.None) return PidSide.None;
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
