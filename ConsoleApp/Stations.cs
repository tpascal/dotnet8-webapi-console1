using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class Stations
{
    public static Dictionary<string, int> GetStations()
    {
        return new Dictionary<string, int>()
        {
            { "Roseworthy", 95671},
            { "Nuriootpa", 94681},
            { "Turretfield", 95674},
            { "Edinburgh", 95676},
            { "Mount Crawford", 94678},
            { "Outer Harbor", 95675},
            { "Parafield", 95677},
            { "Adelaide", 94648},
            { "Adelaide Airport", 94672},
            { "Mount Lofty", 95678},
            { "Mount Barker", 94806},
            { "Pallamana", 95818},
            { "Noarlunga", 94808},
            { "Kuitpo", 94683},
            { "Murray Bridge", 95812},
            { "Strathalbyn", 94814},
            { "Sellicks Hill", 95679},
            { "Hindmarsh Island", 94677},
            { "Parawa West", 94811},
            { "Victor Harbor", 95811}
        };
    }
}
