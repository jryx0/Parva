using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Reflection;

namespace Parva.Infrastructure.Implementations.Logging
{
    public class Log4NetLogger : Parva.Application.Interfaces.Logging.ILogger
    {
        private static readonly log4net.ILog _logger = log4net.LogManager
            .GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

        static Log4NetLogger()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = string.Format(@"App_Data\Logs\log.txt");
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "5MB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.LockingModel = new log4net.Appender.FileAppender.MinimalLock();

            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;

            log4net.Config.BasicConfigurator.Configure(hierarchy);
        }

        public void Log(string message)
        {
            _logger.Info(message);
        }
    }
}