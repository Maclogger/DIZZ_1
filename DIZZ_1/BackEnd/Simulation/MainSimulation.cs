using DIZZ_1.BackEnd.Generators;
using DIZZ_1.BackEnd.Strategies;

namespace DIZZ_1.BackEnd.Simulation;

public class MainSimulation : SimCore
{
    public Strategy? Strategy { get; }
    public MainGenerators MainGenerators { get; set; }
    public Warehouse Warehouse { get; set; }
    public double Cost { get; set; }

    public MainSimulation(Strategy? strategy)
    {
        MainGenerators = new();
        Strategy = strategy;
        Warehouse = new Warehouse();
    }

    protected override void BeforeSimulation()
    {
    }

    protected override void BeforeReplication()
    {
        Warehouse.Reset();
        Cost = 0;
    }

    protected override void AfterReplication(double solution)
    {
    }

    protected override void AfterSimulation(double solution)
    {
    }

    protected override double RunExperiment()
    {
        for (int week = 1; week <= 30; week++)
        {
            if (Config.PrintCompleteReplication) Console.WriteLine($"Week: {week} / 30");
            HandleSuppliers(week); // Monday
            CalculateStorageCost(4); // Monday -> Thursday
            HandleOutput(); // Friday
            if (Config.PrintCompleteReplication) Console.WriteLine("After Output:");
            if (Config.PrintCompleteReplication) Console.WriteLine(Warehouse);
            CalculateStorageCost(3); // Friday -> Sunday
            if (Config.PrintCompleteReplication) Console.WriteLine("---------------------------------");
        }

        return Cost;
    }

    private void HandleOutput()
    {
        int wantedAbsorbersCount = MainGenerators.AbsorbersGen.Generate();
        int wantedBrakePadsCount = MainGenerators.BrakePadsGen.Generate();
        int wantedLightsCount = MainGenerators.LightsGen.Generate();

        HandleFineAndOutput(wantedAbsorbersCount, ref Warehouse.AbsorbersCount);
        HandleFineAndOutput(wantedBrakePadsCount, ref Warehouse.BrakePadsCount);
        HandleFineAndOutput(wantedLightsCount, ref Warehouse.LightsCount);
    }

    private void HandleFineAndOutput(int wantedItems, ref int itemsInWarehouse)
    {
        if (itemsInWarehouse - wantedItems <= 0)
        {
            double fine = (wantedItems - itemsInWarehouse) * Config.FinePerUnit;
            Cost += fine;
            itemsInWarehouse = 0;
            if (Config.PrintCompleteReplication) Console.WriteLine($"Paying fine: {fine}");
            return;
        }

        itemsInWarehouse -= wantedItems;
    }


    private void CalculateStorageCost(int countOfDays)
    {
        Cost += Warehouse.AbsorbersCount * Config.AbsorbersDailyStorageCostPerUnit * countOfDays;
        Cost += Warehouse.BrakePadsCount * Config.BrakePadsDailyStorageCostPerUnit * countOfDays;
        Cost += Warehouse.LightsCount * Config.LightsDailyStorageCostPerUnit * countOfDays;
        if (Config.PrintCompleteReplication) Console.WriteLine($"Cost: {Cost}");
    }

    private void HandleSuppliers(int week)
    {
        Generator<double> generator = GetSupplierGenerator(week);
        if (Config.PrintCompleteReplication) Console.Write("Supplier Generator: ");
        if (Config.PrintCompleteReplication) Console.WriteLine(typeof(Generator<double>).Name);
        double probOfDelivery = generator.Generate();

        double value = MainGenerators.DeliveryGen.Generate();
        if (Config.PrintCompleteReplication) Console.WriteLine($"{value} < {probOfDelivery}");
        bool successfulDelivery = value < probOfDelivery;
        if (!successfulDelivery)
        {
            return;
        }

        Warehouse.AbsorbersCount += Strategy.Weeks[week].Absorbers;
        Warehouse.LightsCount += Strategy.Weeks[week].Lights;
        Warehouse.BrakePadsCount += Strategy.Weeks[week].BrakePads;

        if (Config.PrintCompleteReplication) Console.WriteLine(Warehouse);
    }

    private Generator<double> GetSupplierGenerator(int week)
    {
        int currentSupplier = Strategy.Weeks[week].Supplier;
        if (currentSupplier == 1)
        {
            if (week <= 10)
            {
                return MainGenerators.Supp1First10Gen;
            }

            return MainGenerators.Supp1From11Gen;
        }

        if (currentSupplier == 2)
        {
            if (week <= 15)
            {
                return MainGenerators.Supp2First15Gen;
            }

            return MainGenerators.Supp2From16Gen;
        }

        throw new ArgumentException("Supplier not found");
    }
}