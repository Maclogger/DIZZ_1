namespace DIZZ_1.BackEnd.Simulation;

public class Warehouse
{
    public int AbsorbersCount = 0;
    public int BrakePadsCount = 0;
    public int LightsCount = 0;

    public void Reset()
    {
        AbsorbersCount = 0;
        BrakePadsCount = 0;
        LightsCount = 0;
    }

    public override string ToString()
    {
        return $"Warehouse: {AbsorbersCount}-{LightsCount}-{BrakePadsCount}";
    }
}