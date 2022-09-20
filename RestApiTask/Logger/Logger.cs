using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using ILogger = NLog.ILogger;

namespace RestApiTask.Logger
{
    /// <summary>
    /// This class is using for a creating extended log. It implements a Singleton pattern.
    /// </summary>
    public sealed class Logger
    {
        private static readonly Lazy<Logger> LazyInstance = new Lazy<Logger>(() => new Logger());
        private static readonly ThreadLocal<ILogger> Log = new ThreadLocal<ILogger>(() => (ILogger)LogManager.GetLogger(Thread.CurrentThread.ManagedThreadId.ToString()));

        private Logger()
        {
            try
            {
                LogManager.LoadConfiguration("NLog.config");
            }
            catch (FileNotFoundException ex)
            {
                LogManager.Configuration = GetConfiguration();
            }
        }

        private LoggingConfiguration GetConfiguration()
        {
            string str = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} ${level:uppercase=true} - ${message}";
            LoggingConfiguration configuration = new LoggingConfiguration();
            LogLevel info = LogLevel.Info;
            LogLevel fatal1 = LogLevel.Fatal;
            ConsoleTarget consoleTarget = new ConsoleTarget("logconsole");
            consoleTarget.Layout = (Layout)str;
            configuration.AddRule(info, fatal1, (Target)consoleTarget);
            LogLevel debug = LogLevel.Debug;
            LogLevel fatal2 = LogLevel.Fatal;
            FileTarget fileTarget = new FileTarget("logfile");
            fileTarget.FileName = (Layout)"../../../Log/log.log";
            fileTarget.Layout = (Layout)str;
            fileTarget.KeepFileOpen = false;
            fileTarget.ConcurrentWrites = true;
            configuration.AddRule(debug, fatal2, (Target)fileTarget);
            return configuration;
        }

        /// <summary>Gets Logger instance.</summary>
        public static Logger Instance => LazyInstance.Value;

        /// <summary>Adds configuration (target).</summary>
        /// <param name="target">Target configuration to add.</param>
        /// <returns>Logger instance.</returns>
        public Logger AddTarget(Target target)
        {
            LogManager.Configuration.AddRuleForAllLevels(target, Log.Value.Name);
            LogManager.ReconfigExistingLoggers();
            return Instance;
        }

        /// <summary>Removes configuration (target).</summary>
        /// <param name="target">Target configuratio to remove.</param>
        /// <returns>Logger instance.</returns>
        public Logger RemoveTarget(Target target)
        {
            LogManager.Configuration.RemoveTarget(target.Name);
            LogManager.ReconfigExistingLoggers();
            return Instance;
        }

        /// <summary>Log debug message and optional exception.</summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        public void Debug(string message, Exception exception = null) => Log.Value.Debug(exception, message);

        /// <summary>Log info message.</summary>
        /// <param name="message">Message</param>
        public void Info(string message) => Log.Value.Info(message);

        /// <summary>Log warning message.</summary>
        /// <param name="message">Message</param>
        public void Warn(string message) => Log.Value.Warn(message);

        /// <summary>Log error message.</summary>
        /// <param name="message">Message</param>
        public void Error(string message) => Log.Value.Error(message);

        /// <summary>Log fatal message and exception.</summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        public void Fatal(string message, Exception exception) => Log.Value.Fatal(exception, message);
    }
}
