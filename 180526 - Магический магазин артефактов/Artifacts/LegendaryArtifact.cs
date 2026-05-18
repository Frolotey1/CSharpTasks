using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180526___Магический_магазин_артефактов.Artifacts
{

    public class LegendaryArtifact : Artifact
    {
        public string CurseDescription { get; set; }
        public bool IsCursed { get; set; }

        public LegendaryArtifact(int id, string name, int powerLevel, Rarity rarity, string curseDescription, bool isCursed)
            : base(id, name, powerLevel, rarity)
        {
            CurseDescription = curseDescription;
            IsCursed = isCursed;
        }

        public override string Serialize()
        {
            return $"LegendaryArtifact: {Name}, Cursed: {IsCursed}, Curse: {CurseDescription}, Power: {PowerLevel}, Rarity: {Rarity}";
        }
    }
}
