using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace splendor_lib;

public class GameDataLoader
{
    private const string _headerFirstElementDevelopment = "Level";
    private const string _headerFirstElementNoble = "Prestige";
    private const string _developmentCsvPath = "splendor_lib.csv_data.developments-data.csv";
    private const string _noblesCsvPath = "splendor_lib.csv_data.nobles-data.csv";

    private readonly string _developmentCsvContent = string.Empty;
    private readonly string _noblesCsvContent = string.Empty;

    public GameDataLoader()
    {
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_developmentCsvPath))
        using (StreamReader reader = new StreamReader(stream))
        {
            _developmentCsvContent = reader.ReadToEnd();
        }
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_noblesCsvPath))
        using (StreamReader reader = new StreamReader(stream))
        {
            _noblesCsvContent = reader.ReadToEnd();
        }
    }

    public List<Development> LoadDevelopments()
    {
        var output = new List<Development>();

        foreach (var line in _developmentCsvContent.Split(Environment.NewLine))
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

        foreach (var line in _noblesCsvContent.Split(Environment.NewLine))
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
        Token type = (Token)Enum.Parse(typeof(Token), parameters[2], true);
        var diamondPrice = uint.Parse(parameters[3]);
        var sapphirePrice = uint.Parse(parameters[4]);
        var emeraldPrice = uint.Parse(parameters[5]);
        var rubyPrice = uint.Parse(parameters[6]);
        var onyxPrice = uint.Parse(parameters[7]);

        var price = new TokenCollection(diamondPrice, onyxPrice, sapphirePrice, emeraldPrice, rubyPrice, 0);

        return new Development(level, prestige, type, price);
    }
    private bool IsHeader(string element) =>
        element == _headerFirstElementDevelopment ||
        element == _headerFirstElementNoble;
}
