using System;

namespace DailyPlanner;

public class DailyPlanner
{
    public static void Main()
    {
        Authorization.SignUpIn();
        Cycle.Loop();
    }
}