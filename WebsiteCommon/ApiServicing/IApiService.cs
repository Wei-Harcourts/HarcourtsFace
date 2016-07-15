namespace Harcourts.Face.WebsiteCommon.ApiServicing
{
    public interface IApiService<TConfiguration>
        where TConfiguration : class, IApiServiceConfiguration
    {
        bool IsDebugging { get; }
        TConfiguration Configuration { get; set; }
    }
}
