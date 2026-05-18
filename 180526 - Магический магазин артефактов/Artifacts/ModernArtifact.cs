using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180526___Магический_магазин_артефактов.Artifacts
{
    public class ModernArtifact : Artifact
    {
        public double TechLevel { get; set; }
        public string Manufacturer { get; set; }

        public ModernArtifact(int id, string name, int powerLevel, Rarity rarity, double techLevel, string manufacturer)
            : base(id, name, powerLevel, rarity)
        {
            TechLevel = techLevel;
            Manufacturer = manufacturer;
        }

        public override string Serialize()
        {
            return $"ModernArtifact: {Name}, TechLevel: {TechLevel}, Manufacturer: {Manufacturer}, Power: {PowerLevel}, Rarity: {Rarity}";
        }
    }
}
