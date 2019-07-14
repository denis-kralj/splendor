using System;
using System.IO;
using System.Collections.Generic;

namespace splendor_lib
{
    public class GameDataLoader
    {
        private const string _headerFirstElementDevelopment = "Level";
        private const string _headerFirstElementNoble = "Prestige";

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

        public List<Noble> LoadNobles()
        {
            var output = new List<Noble>();

            var lines = File.ReadAllLines(_noblesCvsPath);

            foreach(var line in lines)
            {
                var elements = line.Split(new[]{','});
                if(IsHeader(elements[0]))
                {
                    continue;
                }

                var nobleCard = BuildNobleCard(elements);

                output.Add(nobleCard);
            }

            return output;
        }

        private Noble BuildNobleCard(string[] parameters)
        {
            var prestige = int.Parse(parameters[0]);
            var diamondDevelopmentPrice = int.Parse(parameters[1]);
            var rubyDevelopmentPrice = int.Parse(parameters[2]);
            var emeraldDevelopmentPrice = int.Parse(parameters[3]);
            var onyxDevelopmentPrice = int.Parse(parameters[4]);
            var sapphireDevelopmentPrice = int.Parse(parameters[5]);

            return new Noble(prestige, diamondDevelopmentPrice, rubyDevelopmentPrice, emeraldDevelopmentPrice, onyxDevelopmentPrice, sapphireDevelopmentPrice);
        }

        private Development BuildDevelopmentCard(string[] parameters)
        {
            var level = int.Parse(parameters[0]);
            var prestige = int.Parse(parameters[1]);
            Token token = (Token)Enum.Parse(typeof(Token), parameters[2], true);
            var diamondPrice = int.Parse(parameters[3]);
            var sapphirePrice = int.Parse(parameters[4]);
            var emeraldPrice = int.Parse(parameters[5]);
            var rubyPrice = int.Parse(parameters[6]);
            var onyxPrice = int.Parse(parameters[7]);

            return new Development(level,prestige, token,diamondPrice,sapphirePrice,emeraldPrice,rubyPrice,onyxPrice);
        }
        private bool IsHeader(string element) => 
            element == _headerFirstElementDevelopment ||
            element == _headerFirstElementNoble;
    }
}