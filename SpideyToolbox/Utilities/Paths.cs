namespace SpideyToolbox.Utilities
{
    internal class Paths
    {
        // Insomniac Engine paths and environment variables
        //------------------------------------------------------------------------------------------
        public static string igRoot = Environment.GetEnvironmentVariable("IG_ROOT");

        private static string igTool(string Tool)
        {
            return Path.Combine(igRoot, "i30", "devel", "code", "Output", Tool);
        }

        public static string igAssetEditor = igTool("AssetEditor.exe");
        public static string igVault = igTool("Vault.exe");
        public static string igBulkAssetEditor = igTool("BulkAssetEditor.exe");
        //------------------------------------------------------------------------------------------
    }
}
