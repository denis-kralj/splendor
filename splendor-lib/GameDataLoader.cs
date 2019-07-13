using System;
using System.IO;
using System.Collections.Generic;

namespace splendor_lib
{
    public class GameDataLoader
    {
        private const string _headerFirstElement = "Level";
        private readonly string _developmentCvsPath;
        private readonly string _noblesCvsPath;

        public GameDataLoader(string developmentCvsPath, string noblesCvsPath)
        {
            _developmentCvsPath = developmentCvsPath;
            _noblesCvsPath = noblesCvsPath;
        }

        public List<Development> LoadDevelopments()
        {
            var output = new List<Development>();

            var lines = File.ReadAllLines(_developmentCvsPath);

            foreach(var line in lines)
            {
                var elements = line.Split(new[]{','});
                if(IsHeader(elements[0]))
                {
                    continue;
                }

                var developmentCard = BuildDevelopmentCard(elements);

                output.Add(developmentCard);
            }

            return output;
        }

        private Development BuildDevelopmentCard(string[] parameters)
        {
            var level = int.Parse(parameters[0]);
            var prestige = int.Parse(parameters[1]);
            Color color = (Color)Enum.Parse(typeof(Color), parameters[2], true);
            var diamondPrice = int.Parse(parameters[3]);
            var sapphirePrice = int.Parse(parameters[4]);
            var emeraldPrice = int.Parse(parameters[5]);
            var rubyPrice = int.Parse(parameters[6]);
            var onyxPrice = int.Parse(parameters[7]);

            return new Development(level,prestige, color,diamondPrice,sapphirePrice,emeraldPrice,rubyPrice,onyxPrice);
        }
        private bool IsHeader(string element) => element == _headerFirstElement;
    }
}