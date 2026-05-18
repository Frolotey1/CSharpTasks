using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180526___Магический_магазин_артефактов.Artifacts
{

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public abstract class Artifact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PowerLevel { get; set; }
        public Rarity Rarity { get; set; }

        protected Artifact(int id, string name, int powerLevel, Rarity rarity)
        {
            Id = id;
            Name = name;
            PowerLevel = powerLevel;
            Rarity = rarity;
        }

        public abstract string Serialize();
    }
}
