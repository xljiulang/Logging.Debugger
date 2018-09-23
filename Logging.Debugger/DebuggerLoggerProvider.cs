using Microsoft.Extensions.Logging;
using System;

namespace Logging.Debugger
{
    /// <summary>
    /// 表示Debugger的日志提供者
    /// </summary> 
    public class DebuggerLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// 日志过滤器
        /// </summary>
        private readonly Func<string, LogLevel, bool> filter;

        /// <summary>
        /// Debugger的日志提供者
        /// </summary>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public DebuggerLoggerProvider()
            : this(null)
        {
        }

        /// <summary>
        /// Debugger的日志提供者
        /// </summary>
        /// <param name="filter">日志过滤器</param>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public DebuggerLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            if (DebuggerLogger.IsPlatformSupported == false)
            {
                throw new PlatformNotSupportedException();
            }

            this.filter = filter;
        }

        /// <summary>
        /// 创建Logger
        /// </summary>
        /// <param name="name">名称</param>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <returns></returns>
        public ILogger CreateLogger(string name)
        {
            return new DebuggerLogger(name, filter);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
        }
    }
}
