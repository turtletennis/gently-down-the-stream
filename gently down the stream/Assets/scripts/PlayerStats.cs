using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class PlayerStats
{
    public static bool initialised = false;
    public static float startSpeed;
    public static float acceleration;
    public static float totalHealth;
    public static float oarPower;

    public static float steeringAngleChange;
    public static float steeringPower;
    public static float flatDamageResistance;

    public static float percentDamageResistance;
    public static int coins = 100;

    public static int longestSurvivalTime = 0;
}
