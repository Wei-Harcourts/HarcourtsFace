namespace Harcourts.Face.WebsiteCommon.ApiServicing
{
    public interface IApiService<TConfiguration>
    {
        bool IsDebugging { get; }

        TConfiguration Configuration { get; set; }
    }
}
