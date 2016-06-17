namespace Harcourts.Face.WebsiteService.Lookups
{
    /// <summary>
    /// Represents the provider of JSON data files.
    /// </summary>
    public interface IJsonFileProvider
    {
        /// <summary>
        /// Returns the file path of the specified data file.
        /// </summary>
        /// <param name="dataFileName">The data file name.</param>
        string GetDataFilePath(string dataFileName);
    }
}
