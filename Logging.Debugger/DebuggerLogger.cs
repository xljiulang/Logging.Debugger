using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Logging.Debugger
{
    /// <summary>
    /// 表示包装System.Diagnostics.Debugger.Log的日志
    /// </summary>
    public class DebuggerLogger : ILogger
    {
        /// <summary>
        /// 名称
        /// </summary>
        private readonly string name;

        /// <summary>
        /// 过滤器
        /// </summary>
        private readonly Func<string, LogLevel, bool> filter;


        /// <summary>
        /// 包装System.Diagnostics.Debugger.Log的日志
        /// </summary>
        /// <param name="name">名称</param>
        public DebuggerLogger(string name)
            : this(name, null)
        {
        }

        /// <summary>
        /// 包装System.Diagnostics.Debugger.Log的日志
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="filter">内容过滤器</param>
        public DebuggerLogger(string name, Func<string, LogLevel, bool> filter)
        {
            this.name = string.IsNullOrEmpty(name) ? nameof(DebuggerLogger) : name;
            this.filter = filter;
        }


        /// <summary>
        /// 开启一个日志范围
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state">内容</param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        /// <summary>
        /// 返回指定日志级别是否可用
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && (this.filter == null || this.filter(this.name, logLevel));
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">日志级别</param>
        /// <param name="eventId">事件id</param>
        /// <param name="state">内容</param>
        /// <param name="exception">异常</param>
        /// <param name="formatter">格式化委托</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (this.IsEnabled(logLevel) == false)
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var builder = new StringBuilder()
                .Append($"{this.name}[{eventId.Id}] [{logLevel}]");

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) == false)
            {
                builder.AppendLine().Append(message);
            }

            if (exception != null)
            {
                builder.AppendLine().AppendLine().Append(exception.ToString());
            }

            System.Diagnostics.Debugger.Log((int)logLevel, null, builder.ToString());
        }

        private class NoopDisposable : IDisposable
        {
            public readonly static NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            {
            }
        }
    }
}
