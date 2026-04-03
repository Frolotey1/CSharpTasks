using System;

namespace SevenWonders {
    public abstract class Wonder {
        public string Name { get; set; }
        public string Location { get; set; }
        public int YearBuilt { get; set; }
        
        public virtual string GetInfo()
        {
            return $"{Name} - {Location}, построено в {YearBuilt} году до н.э.";
        }
    }
    
    public class PyramidOfCheops : Wonder {
        public double Height { get; set; }
        
        public PyramidOfCheops() {
            Name = "Пирамида Хеопса";
            Location = "Гиза, Египет";
            YearBuilt = 2560;
            Height = 146.6;
        }
        
        public override string GetInfo()  {
            return base.GetInfo() + $" Высота: {Height} м.";
        }
    }
    
    public class HangingGardens : Wonder {
        public string Legend { get; set; }
        
        public HangingGardens()  {
            Name = "Висячие сады Семирамиды";
            Location = "Вавилон, Ирак";
            YearBuilt = 600;
            Legend = "Созданы для царицы Амитис";
        }
        
        public override string GetInfo()  {
            return base.GetInfo() + $" Легенда: {Legend}";
        }
    }
    
    public class StatueOfZeus : Wonder {
        public string Material { get; set; }
        
        public StatueOfZeus() {
            Name = "Статуя Зевса";
            Location = "Олимпия, Греция";
            YearBuilt = 435;
            Material = "Золото и слоновая кость";
        }
        
        public override string GetInfo() {
            return base.GetInfo() + $" Материал: {Material}";
        }
    }
    
    public class TempleOfArtemis : Wonder {
        public int ColumnsCount { get; set; }
        
        public TempleOfArtemis() {
            Name = "Храм Артемиды";
            Location = "Эфес, Турция";
            YearBuilt = 550;
            ColumnsCount = 127;
        }
        
        public override string GetInfo() {
            return base.GetInfo() + $" Количество колонн: {ColumnsCount}";
        }
    }
    
    public class Mausoleum : Wonder {
        public string Ruler { get; set; }
        
        public Mausoleum() {
            Name = "Мавзолей в Галикарнасе";
            Location = "Галикарнас, Турция";
            YearBuilt = 350;
            Ruler = "Царь Мавсол";
        }
        
        public override string GetInfo() {
            return base.GetInfo() + $" Правитель: {Ruler}";
        }
    }
    
    public class Colossus : Wonder {
        public int Height { get; set; }
        
        public Colossus()  {
            Name = "Колосс Родосский";
            Location = "Родос, Греция";
            YearBuilt = 280;
            Height = 33;
        }
        
        public override string GetInfo()  {
            return base.GetInfo() + $" Высота: {Height} м.";
        }
    }
    
    public class Lighthouse : Wonder {
        public int Height { get; set; }
        
        public Lighthouse() {
            Name = "Александрийский маяк";
            Location = "Александрия, Египет";
            YearBuilt = 280;
            Height = 135;
        }
        
        public override string GetInfo() {
            return base.GetInfo() + $" Высота: {Height} м.";
        }
    }
    
    public class WondersDemo  {
        private Wonder[] wonders;
        
        public WondersDemo() {
            wonders = new Wonder[]  {
                new PyramidOfCheops(),
                new HangingGardens(),
                new StatueOfZeus(),
                new TempleOfArtemis(),
                new Mausoleum(),
                new Colossus(),
                new Lighthouse()
            };
        }
        
        public void Run() {
            Console.WriteLine("7 ЧУДЕС СВЕТА");
            
            for (int i = 0; i < wonders.Length; i++) {
                Console.WriteLine($"{i + 1}. {wonders[i].GetInfo()}");
                Console.WriteLine();
            }
            
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
