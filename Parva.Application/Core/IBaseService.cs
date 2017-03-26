namespace Parva.Application.Core
{
    public interface IBaseService
    {
        bool IsAuthenticated { get; }

        bool IsInRole(string role);

        string UserName { get; }
    }
}