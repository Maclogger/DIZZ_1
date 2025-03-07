namespace DIZZ_1.BackEnd.Strategies;

public class Strategy
{
    public string Name { get; set; }
    public List<Week> Weeks { get; set; }

    public Strategy(string fileName)
    {
        Weeks = new List<Week>();
        Name = fileName;
        LoadFromFile(fileName);
    }

    private void LoadFromFile(string fileName)
    {
        Console.WriteLine(fileName);
        string[] lines = File.ReadAllLines(Path.Combine(Config.StrategiesDirectory, fileName));

        foreach (string line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split(',');

            if (parts.Length < 5)
            {
                Console.WriteLine("Invalid file format");
                continue;
            }
            try
            {
                Week week = new Week
                {
                    WeekNumber = int.Parse(parts[0]),
                    Supplier = int.Parse(parts[1]),
                    Absorbers = int.Parse(parts[2]),
                    BrakePads = int.Parse(parts[3]),
                    Lights = int.Parse(parts[4].Trim())
                };

                Weeks.Add(week);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

public class Week
{
    public int WeekNumber { get; set; }
    public int Supplier { get; set; }
    public int Absorbers { get; set; }
    public int BrakePads { get; set; }
    public int Lights { get; set; }
}