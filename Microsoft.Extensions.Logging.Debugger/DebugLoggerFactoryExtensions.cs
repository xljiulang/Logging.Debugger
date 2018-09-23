using Microsoft.Extensions.Logging.Debugger;
using System;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 提供对LoggerFactory的扩展
    /// </summary>
    public static class DebugLoggerFactoryExtensions
    {
        /// <summary>
        /// 添加Debugger日志提供者到LoggerFactory
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILoggerFactory AddDebugger(this ILoggerFactory factory)
        {
            return factory.AddDebugger(LogLevel.Trace);
        }


        /// <summary>
        /// 添加Debugger日志提供者到LoggerFactory
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="minLevel">日志最小级别</param>
        /// <returns></returns>
        public static ILoggerFactory AddDebugger(this ILoggerFactory factory, LogLevel minLevel)
        {
            return factory.AddDebugger((message, logLevel) => logLevel >= minLevel);
        }

        /// <summary>
        /// 添加Debugger日志提供者到LoggerFactory
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ILoggerFactory AddDebugger(this ILoggerFactory factory, Func<string, LogLevel, bool> filter)
        {
            factory.AddProvider(new DebuggerLoggerProvider(filter));
            return factory;
        }
    }
}