namespace DIZZ_1.BackEnd
{
    public sealed class MainApp
    {
        // Singleton staff
        private static readonly Lazy<MainApp> _instance = new(() => new MainApp());
        public static MainApp Instance => _instance.Value;
        // Singleton staff

        private MainApp()
        {
            Generators = new();
            MasterSeeder = new Random(0);
            Generators.InitializeAll();


        }

        public Random MasterSeeder { get; set; }
        public Generators.Generators Generators { get; private set; }
    }
}
