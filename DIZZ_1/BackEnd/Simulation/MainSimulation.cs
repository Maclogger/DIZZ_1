using DIZZ_1.BackEnd.Generators;
using DIZZ_1.BackEnd.Strategies;

namespace DIZZ_1.BackEnd.Simulation;

public class MainSimulation : AsyncSimCore<double, double>
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

    protected override Task BeforeReplication(int replication)
    {
        Warehouse.Reset();
        Cost = 0;
        return base.BeforeReplication(replication);
    }

    public async Task<double> RunCompleteSimulation(
        int replicationCount,
        IProgress<SimulationProgress<double>> progress
    )
    {
        (double cumulative, int doneReplications) = await Run(
            replicationCount,
            (total, currentCost) => total + currentCost,
            0.0,
            progress
        );
        return cumulative / doneReplications;
    }

    protected override Task<double> RunExperiment(int replication)
    {
        List<double>? dailyCosts = (replication <= 1 ? new() : null);
        for (int week = 1; week <= 30; week++)
        {
            if (Config.PrintCompleteReplication) Console.WriteLine($"Week: {week} / 30");
            HandleSuppliers(week); // Monday
            double storageCost = CalculateStorageCost(4);
            if (replication <= 1)
            {
                dailyCosts!.Add(storageCost / 4); // PON
                dailyCosts.Add(storageCost / 4); // UTO
                dailyCosts.Add(storageCost / 4); // STR
                dailyCosts.Add(storageCost / 4); // ŠTV
            }
            Cost += storageCost;
            double fine = HandleOutput(); // Friday
            Cost += fine;
            if (Config.PrintCompleteReplication) Console.WriteLine("After Output:");
            if (Config.PrintCompleteReplication) Console.WriteLine(Warehouse);
            storageCost = CalculateStorageCost(3); // Friday -> Sunday
            if (replication <= 1)
            {
                dailyCosts!.Add(storageCost / 3 + fine); // PIA
                dailyCosts.Add(storageCost / 3); // SOB
                dailyCosts.Add(storageCost / 3); // NED
            }
            Cost += storageCost;
            if (Config.PrintCompleteReplication)
                Console.WriteLine("---------------------------------");
        }

        if (replication <= 1)
        {
            MainApp.Instance.DailyCosts = dailyCosts!;
        }

        return Task.FromResult(Cost);
    }

    private double HandleOutput()
    {
        int wantedAbsorbersCount = MainGenerators.AbsorbersGen.Generate();
        int wantedBrakePadsCount = MainGenerators.BrakePadsGen.Generate();
        int wantedLightsCount = MainGenerators.LightsGen.Generate();

        double fine = GetFineAndHandleOutput(wantedAbsorbersCount, ref Warehouse.AbsorbersCount);
        fine += GetFineAndHandleOutput(wantedBrakePadsCount, ref Warehouse.BrakePadsCount);
        fine += GetFineAndHandleOutput(wantedLightsCount, ref Warehouse.LightsCount);
        return fine;
    }

    private double GetFineAndHandleOutput(int wantedItems, ref int itemsInWarehouse)
    {
        if (itemsInWarehouse - wantedItems <= 0)
        {
            double fine = (wantedItems - itemsInWarehouse) * Config.FinePerUnit;
            itemsInWarehouse = 0;
            if (Config.PrintCompleteReplication) Console.WriteLine($"Paying fine: {fine}");
            return fine;
        }

        itemsInWarehouse -= wantedItems;
        return 0.0;
    }


    private double CalculateStorageCost(int countOfDays)
    {
        double storageCost = Warehouse.AbsorbersCount * Config.AbsorbersDailyStorageCostPerUnit * countOfDays;
        storageCost += Warehouse.BrakePadsCount * Config.BrakePadsDailyStorageCostPerUnit * countOfDays;
        storageCost += Warehouse.LightsCount * Config.LightsDailyStorageCostPerUnit * countOfDays;
        if (Config.PrintCompleteReplication) Console.WriteLine($"storageCost: {storageCost}");
        return storageCost;
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
        int currentSupplier = Strategy!.Weeks[week].Supplier;
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