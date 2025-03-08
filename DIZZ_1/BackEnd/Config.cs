namespace DIZZ_1.BackEnd;

public static class Config
{
    public const int MaxPrintValuesInTextArea = 1000;

    private static readonly int? NthPointToDraw = 1000;
    public const string FloatFormat = "F2";
    public const double Tolerance = 0.00001;

    public static int GetNthPointToDraw(int replicationCount)
    {
        if (NthPointToDraw.HasValue) return NthPointToDraw.Value;
        return replicationCount / 1_000;
    }

    public const bool PrintCompleteReplication = false;
    public const string StrategiesDirectory = "Strategies";
    public const double AbsorbersDailyStorageCostPerUnit = 0.2; // eur
    public const double BrakePadsDailyStorageCostPerUnit = 0.3; // eur
    public const double LightsDailyStorageCostPerUnit = 0.25; // eur
    public const double FinePerUnit = 0.3; // eur
}