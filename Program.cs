using System;
using System.Collections.Generic;
using System.Linq;

namespace FindExpiredFood
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            supermarket.Work();
        }
    }

    class Supermarket
    {
        private List<Stew> _stews;

        public Supermarket()
        {
            int stewCount = 5;

            StewCreator stewCreator = new StewCreator();
            _stews = stewCreator.CreateStews(stewCount);
        }

        public void Work()
        {
            ConsoleOutputMethods.WriteRedText("Список всей тушенки");
            ShowStewList(_stews);

            ConsoleOutputMethods.WriteRedText("Список просроченной тушенки");
            ShowExpiredStew(_stews);
        }

        private void ShowExpiredStew(List<Stew> stews)
        {
            var expiredStew = stews
                .Where(stew => (DateTime.Now.Year - stew.ProductionDate) >= stew.ShelfLife)
                .ToList();

            ShowStewList(expiredStew);
        }

        private void ShowStewList(List<Stew> stews)
        {
            foreach (Stew stew in stews)
            {
                Console.WriteLine($"{stew.Lable}\n" +
                    $"Год производства - {stew.ProductionDate}\n" +
                    $"Срок годности - {stew.ShelfLife}\n");
            }
        }
    }

    class Stew
    {
        public Stew(string label, int productionDate, int shelfLife)
        {
            Lable = label;
            ProductionDate = productionDate;
            ShelfLife = shelfLife;
        }

        public string Lable { get; private set; }
        public int ProductionDate { get; private set; }
        public int ShelfLife { get; private set; }
    }

    class StewCreator
    {
        private List<string> _labels;
        private Random _random;

        public StewCreator()
        {
            _random = new Random();

            _labels = new List<string>
            {
                "Гунар\nКусковая говядина",
                "Мясной-Союз\nТушенка кусковая говяжья",
                "Консервы мясорастительные\nТушенка кусковая говяжья",
                "Семейный бюджет\nТушенка свинная",
                "Халял\nТушенка ароматная",
                "Русское мясо\nТушенка любительская"
            };
        }

        public List<Stew> CreateStews(int count)
        {
            List<Stew> stews = new List<Stew>();

            int earlyProductionYear = 2020;
            int lateProductionYear = 2023;

            int minimumShelfLife = 1;
            int maximumShelfLife = 5;

            for (int i = 0; i < count; i++)
            {
                stews.Add(new Stew(_labels[_random.Next(0, _labels.Count)],
                    _random.Next(earlyProductionYear, lateProductionYear + 1),
                    _random.Next(minimumShelfLife, maximumShelfLife + 1)));
            }

            return stews;
        }
    }

    static class ConsoleOutputMethods
    {
        public static void WriteRedText(string text)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = tempColor;
        }
    }
}
