﻿namespace HsLib.Types.Places
{
    [Flags]
    public enum Pid
    {
        None = 0,

        P1 = 1,
        P2 = 2,
    }


    [Flags]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1135:Declare enum member with zero value (when enum has FlagsAttribute).",
        Justification = "<Pending>")] // never should be none
    public enum Side
    {
        Me = 1,
        He = 2,
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

        public static Side Side(this Pid pid, Pid other)
        {
            return (pid == other) switch
            {
                true => Places.Side.Me,
                false => Places.Side.He,
            };
        }
    }
}
