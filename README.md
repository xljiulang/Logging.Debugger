# Logging.Debugger
Microsoft.Extensions.Logging统一日志的System.Diagnostics.Debugger日志提供者，可以使用[DebugViewer](https://github.com/xljiulang/Microsoft.Extensions.Logging.Debugger/blob/master/DebugViewer.exe?raw=true)工具监听日志内容。

### 1 Nuget
PM> `install-package Logging.Debugger`
<br/>支持.net framework 4.5、netstandard2.0
<br/>只支持windows平台 

### 2 如何使用
PM> `install-package Microsoft.Extensions.Logging`<br/>
PM> `install-package Logging.Debugger`

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

### 3 使用场景
System.Diagnostics.Debugger在输出日志时，先判断系统如果有调试器在监听，才将日志内容写入指定的windows的内存映射，并通知调试器有新的数据更新。本项目里面的DebugViewer.exe是调试器，在开启之后System.Diagnostics.Debugger才真正的输出日志，所以Logging.Debugger非常适合用于作Debug内容高度密集输出，你的程序在运行时的性能不会受到任何影响，而且可以随时监听和查看实时日志。DebugViewer项目地址：https://github.com/xljiulang/DebugViewer
