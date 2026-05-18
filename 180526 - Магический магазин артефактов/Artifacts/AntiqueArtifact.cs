using _180526___Магический_магазин_артефактов.Artifacts;
using System;
using System.Xml.Linq;

public class AntiqueArtifact : Artifact
{
    public int Age { get; set; }
    public string OriginRealm { get; set; }
    public AntiqueArtifact() : base(0, "", 0, Rarity.Common)
    {
        Age = 0;
        OriginRealm = "";
    }

    public AntiqueArtifact(int id, string name, int powerLevel, Rarity rarity, int age, string originRealm)
        : base(id, name, powerLevel, rarity)
    {
        Age = age;
        OriginRealm = originRealm;
    }

    public override string Serialize()
    {
        return $"AntiqueArtifact: {Name}, Age: {Age}, Origin: {OriginRealm}, Power: {PowerLevel}, Rarity: {Rarity}";
    }
}