namespace bdd
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationState.SetValue("host", "localhost");
            ApplicationState.SetValue("port", "3306");

            ApplicationConfiguration.Initialize();
            Application.Run(new Login());
        }
    }
}