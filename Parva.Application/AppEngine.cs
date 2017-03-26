using Parva.Application.Core.IoC;

namespace Parva.Application
{
    public static class AppEngine
    {
        private static IIoCContainer _container;

        public static void Init(IIoCContainer container)
        {
            _container = container;
        }

        public static IIoCContainer Container { get { return _container; } }
    }
}