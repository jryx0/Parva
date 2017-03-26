using System.Threading;

namespace Parva.Application.Core
{
    public abstract class BaseService : IBaseService
    {
        public virtual bool IsAuthenticated
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.IsAuthenticated;
            }
        }

        public virtual string UserName
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }

        public virtual bool IsInRole(string role)
        {
            return Thread.CurrentPrincipal.IsInRole(role);
        }
    }
}