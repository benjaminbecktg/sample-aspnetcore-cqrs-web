namespace SampleAspNetCoreCQRS.Business
{
    public interface IContext
    {
        string OrganizationCode { get; set; }
        string UserId { get; set; }
        string Username { get; set; }

        IDataContext DataContext { get; set; }
    }
}