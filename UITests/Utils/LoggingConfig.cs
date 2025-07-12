using NLog;

namespace DemoQATests.UITests.Utils
{
    /// <summary>
    /// Centralized logging configuration for the test framework
    /// </summary>
    internal static class LoggingConfig
    {
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes NLog configuration if not already initialized
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized) return;

            lock (_lock)
            {
                if (_isInitialized) return;

                LogManager.Setup().LoadConfigurationFromFile("NLog.config");
                _isInitialized = true;
            }
        }

        /// <summary>
        /// Gets a logger for the specified type
        /// </summary>
        /// <typeparam name="T">The type to get logger for</typeparam>
        /// <returns>Logger instance</returns>
        public static Logger GetLogger<T>()
        {
            Initialize();
            return LogManager.GetLogger(typeof(T).FullName);
        }

        /// <summary>
        /// Gets a logger for the calling class
        /// </summary>
        /// <returns>Logger instance</returns>
        public static Logger GetCurrentClassLogger()
        {
            Initialize();
            return LogManager.GetCurrentClassLogger();
        }
    }
}
