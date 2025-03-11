namespace DIZZ_1.BackEnd;

public class Config
{
    public const int MaxPrintValuesInTextArea = 1000;

    private const int PointsInGraph = 1000;
    private const double PercentToCutFromBeggingOfTheChart = 5;
    public const string FloatFormat = "F2";
    public const double Tolerance = 0.00001;

    private int GetNthPointToDraw(int replicationCount)
    {
        return Math.Max(1, replicationCount / PointsInGraph);
    }

    public Task<bool> ShouldBePrintedToGraph(int currentReplication, int replicationCount)
    {
        int startPrintIndex = (int)(replicationCount * (PercentToCutFromBeggingOfTheChart / 100.0));

        int nthPoint = GetNthPointToDraw(replicationCount);
        return Task.FromResult(currentReplication >= startPrintIndex &&
                               currentReplication % nthPoint == 0);
    }


    private int _speed = 1; // 0 = SLOW, 1 = NORMAL, 2 = MAX_SPEED

    public int GetSpeedDelay()
    {
        switch (_speed)
        {
            case 0: return 100;
            case 1: return 10;
            default: return 0;
        }
    }

    public void SetSpeed(int speed)
    {
        if (speed <= 0) _speed = 0;
        if (speed == 1) _speed = 1;
        if (speed >= 2) _speed = 2;
    }

    public const bool PrintCompleteReplication = false;
    public const string StrategiesDirectory = "Strategies";
    public const double AbsorbersDailyStorageCostPerUnit = 0.2; // eur
    public const double BrakePadsDailyStorageCostPerUnit = 0.3; // eur
    public const double LightsDailyStorageCostPerUnit = 0.25; // eur
    public const double FinePerUnit = 0.3; // eur
}