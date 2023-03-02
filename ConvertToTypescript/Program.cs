using System.Text;
using System.Text.RegularExpressions;

class Program
{
    public class Flight
    {
        public string Carrier { get; set; }
        public List<Route> routes { get; set; }
        public int FlightNumber { get; set; }
        public long? AgentId { get; set; }
        public class Route
        {
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string FlightDuration { get; set; }
        }
    }
    static void Main(String[] args)
    {
        List<StringBuilder> list = new List<StringBuilder>();
        Console.Write("Enter class string: ");
        StringBuilder lines = new StringBuilder();
        string line = "";
        while ((line = Console.ReadLine()) != "")
        {
            lines.AppendLine(line);
        }
        string? inputString = lines.ToString();
        string input = "";
        if (inputString != null)
        {
            int counter = -1;
            foreach (string word in inputString.Split(" "))
            {
                if (input == "")
                {
                    if (word == "class")
                    {
                        list.Add(new StringBuilder());
                        counter += 1;
                        input = "class";
                    }
                    else if (word.Contains("string"))
                    {
                        input = word;
                    }
                    else if (word.Contains("int"))
                    {
                        input = word;
                    }
                    else if (word.Contains("long"))
                    {
                        input = word;
                    }
                    else if (word.Contains("List"))
                    {
                        input = word;
                    }
                }
                else
                {
                    StringBuilder converted = list.ElementAt(counter);
                    if (input == "class")
                    {
                        string tmp = word.Trim().Replace("\n", "");
                        converted.AppendLine("export interface " + tmp + "{");
                        input = "";
                    }
                    else if (input.Contains("List"))
                    {
                        Regex regex = new Regex("<(.*?)>");
                        string listName = regex.Match(input).Groups[1].ToString();
                        converted.AppendLine("\t" + word.ToLower() + ": " + listName + "[];");
                        input = "";
                    }
                    else
                    {
                        string isNull = "";
                        if(input.Contains("?"))
                        {
                            isNull = "?";
                        }
                        converted.AppendLine("\t" + word[0].ToString().ToLower() + word.Substring(1) + isNull + ": " + input.ToLower().Replace("?", "") + ";");
                        input = "";
                    }
                }
            }
            StringBuilder final = new StringBuilder();
            foreach (StringBuilder item in list) {
                item.AppendLine("}");
                final.Append(item);
            }
            Console.WriteLine("The converted string is: \n");
            Console.WriteLine(final.ToString());
        }
    }
}
