namespace DIZZ_1.BackEnd.Strategies;

public class StrategiesHandler
{
    public List<Strategy?> Strategies { get; set; }

    public StrategiesHandler()
    {
        Strategies = new();
        List<string?> fileNames = LoadsStrategyNames();
        LoadStrategies(fileNames);
    }

    private void LoadStrategies(List<string?> fileNames)
    {
        foreach (string? fileName in fileNames)
        {
            if (fileName is null || fileName.Length <= 0)
            {
                continue;
            }

            Strategy? strategy = new(fileName);
            Strategies.Add(strategy);
        }
    }

    private static List<string?> LoadsStrategyNames()
    {
        List<string?>? csvFileNames;
        if (Directory.Exists(Config.StrategiesDirectory))
        {
            string[] fileNames = Directory.GetFiles(Config.StrategiesDirectory, "*.csv");
            csvFileNames = fileNames.Select(Path.GetFileName).ToList();
        }
        else
        {
            csvFileNames = new List<string?>();
        }

        return csvFileNames;
    }

    public bool HasStrategies()
    {
        return Strategies.Count > 0;
    }
}