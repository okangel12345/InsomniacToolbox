namespace SpideyToolbox
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string filePath = args.Length > 0 ? args[0] : null;
            string tocPath = args.Length > 0 ? args[1] : null;

            ApplicationConfiguration.Initialize();
            Application.Run(new MainWindow(filePath, tocPath));
        }
    }
}