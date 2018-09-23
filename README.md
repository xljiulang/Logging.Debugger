# Microsoft.Extensions.Logging.Debugger
.net 4.5+的System.Diagnostics.Debugger日志输出组件，可以使用[DebugViewer](https://github.com/xljiulang/DebugViewer)工具监听日志内容

### 1 Nuget
PM> `install-package Microsoft.Extensions.Logging.Debugger'
<br/>支持.net framework 4.5

### 2 如何使用
PM> `install-package Microsoft.Extensions.Logging'
PM> `install-package Microsoft.Extensions.Logging.Debugger'

```c#
static void Main(string[] args)
{
    var logging = new LoggerFactory().AddDebugger();
    var logger = logging.CreateLogger(nameof(Demo));

    logger.LogInformation("log info ...");
    logger.LogError(0, new Exception("boom!"), "sorry!");

    Console.ReadLine();
}
```
