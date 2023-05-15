using CardsJsonToMd;
using Newtonsoft.Json;
using System.Text;

if (args.Length == 0)
{
    throw new ArgumentException("Expected 'cards.json' file path");
}

string jsonFilePath = args[0];

List<Card>? cards;
using (FileStream file = File.OpenRead(jsonFilePath))
using (StreamReader sr = new StreamReader(file))
{
    string jsonContents = sr.ReadToEnd();
    cards = JsonConvert.DeserializeObject<List<Card>>(jsonContents)
        ?? throw new Exception("deserialize error: cards is null");
}

string md = MdBuilder.ToMd(cards);

using (FileStream file = File.OpenWrite("cards.md"))
using (StreamWriter sw = new StreamWriter(file))
{
    sw.Write(md);
}
Console.WriteLine("Success!");

namespace CardsJsonToMd
{
    public static class MdBuilder
    {
        public static string ToMd(IEnumerable<Card> cards)
        {
            StringBuilder stringBuilder = new StringBuilder("## Card list:\n\n");
            foreach (Card card in cards)
            {
                stringBuilder.AppendLine(Line(card));
            }
            return stringBuilder.ToString();
        }

        private static string Line(Card card)
        {
            return $"- [ ] {card.name}";
        }
    }

    public class Card
    {
#pragma warning disable CS8618
        public int? id;
        public string name;
        public string description;
        public string image;
        public string @class;
        public string type;
        public string quality;
        public string race;
        public string set;
        public int? mana;
        public int? attack;
        public int? health;
        public bool? collectible;
#pragma warning restore CS8618
    }
}