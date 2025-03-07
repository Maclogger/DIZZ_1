namespace DIZZ_1.BackEnd;

public static class Config
{
    public const int MaxPrintValuesInTextArea = 1000;

    private static readonly int? NthPointToDraw = 1000;
    public static int GetNthPointToDraw(int replicationCount)
    {
        if (NthPointToDraw.HasValue) return NthPointToDraw.Value;
        return replicationCount / 1_000;
    }
}