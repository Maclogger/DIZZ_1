namespace DIZZ_1.BackEnd
{
    public sealed class MainApp
    {
        private static readonly Lazy<MainApp> _instance = new(() => new MainApp());

        private MainApp()
        {
            Generators = new();
        }

        public static MainApp Instance => _instance.Value;


        public Generators.Generators Generators { get; private set; }


    }
}
