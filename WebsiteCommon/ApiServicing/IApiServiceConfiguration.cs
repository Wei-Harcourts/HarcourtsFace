namespace Harcourts.Face.WebsiteCommon.ApiServicing
{
    public interface IApiServiceConfiguration<TService>
    {
        TService Service { get; }

        void SetService(TService service);

        void Seal();
    }
}
