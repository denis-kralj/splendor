using System;
using System.IO;
using System.Collections.Generic;

namespace splendor_lib;

public class GameDataLoader
{
    private const string _headerFirstElementDevelopment = "Level";
    private const string _headerFirstElementNoble = "Prestige";
    private const string _developmentCsvPath = "csv-data/developments-data.csv";
    private const string _noblesCsvPath = "csv-data/nobles-data.csv";

    public List<Development> LoadDevelopments()
    {
        var output = new List<Development>();

        foreach (var line in File.ReadAllLines(_developmentCsvPath))
        {
            var elements = line.Split(new[] { ',' });
            if (IsHeader(elements[0]))
                continue;

            output.Add(BuildDevelopmentCard(elements));
        }

        return output;
    }
    public List<Noble> LoadNobles()
    {
        var output = new List<Noble>();

        foreach (var line in File.ReadAllLines(_noblesCsvPath))
        {
            var elements = line.Split(new[] { ',' });
            if (IsHeader(elements[0]))
                continue;

            output.Add(BuildNobleCard(elements));
        }

        return output;
    }

    private Noble BuildNobleCard(string[] parameters)
    {
        var prestige = uint.Parse(parameters[0]);
        var diamondDevelopmentPrice = uint.Parse(parameters[1]);
        var rubyDevelopmentPrice = uint.Parse(parameters[2]);
        var emeraldDevelopmentPrice = uint.Parse(parameters[3]);
        var onyxDevelopmentPrice = uint.Parse(parameters[4]);
        var sapphireDevelopmentPrice = uint.Parse(parameters[5]);

        var cost = new NobleRequirements(new TokenCollection(diamondDevelopmentPrice, onyxDevelopmentPrice, sapphireDevelopmentPrice, emeraldDevelopmentPrice, rubyDevelopmentPrice));

        return new Noble(prestige, cost);
    }

    private Development BuildDevelopmentCard(string[] parameters)
    {
        var level = uint.Parse(parameters[0]);
        var prestige = uint.Parse(parameters[1]);
        TokenColor tokenColor = (TokenColor)Enum.Parse(typeof(TokenColor), parameters[2], true);
        var diamondPrice = uint.Parse(parameters[3]);
        var sapphirePrice = uint.Parse(parameters[4]);
        var emeraldPrice = uint.Parse(parameters[5]);
        var rubyPrice = uint.Parse(parameters[6]);
        var onyxPrice = uint.Parse(parameters[7]);

        var price = new TokenCollection(diamondPrice, onyxPrice, sapphirePrice, emeraldPrice, rubyPrice, 0);

        return new Development(level, prestige, tokenColor, price);
    }
    private bool IsHeader(string element) =>
        element == _headerFirstElementDevelopment ||
        element == _headerFirstElementNoble;
}
