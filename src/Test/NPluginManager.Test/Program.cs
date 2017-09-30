using System;
using NPluginManager.Enums;

namespace NPluginManager.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginManager<PluginInterface> pluginManager = new PluginManager<PluginInterface>(AppDomain.CurrentDomain);

            // 处理插件管理器发出的事件
            pluginManager.OnAssemblyLoading += PluginManager_OnAssemblyLoading; ;
            pluginManager.OnAssemblyLoaded += PluginManager_OnAssemblyLoaded; ;
            pluginManager.OnError += PluginManager_OnError; ;
            pluginManager.OnInstanceCreating += PluginManager_OnInstanceCreating; ;
            pluginManager.OnInstanceCreated += PluginManager_OnInstanceCreated; ;

            // 加载插件目录下所有插件
            pluginManager.Load();

            // 开始监视目录插件
            pluginManager.StartWatch();

            Console.WriteLine("正在监视插件目录变动，按Enter退出。");
            Console.ReadLine();
        }

        private static void PluginManager_OnInstanceCreated(object sender, Events.PluginInstanceCreatedArgs<PluginInterface> e)
        {
            Console.WriteLine($"从程序集{e.Assembly}加载类型{e.ClassType}成功");
            e.Instance.Load();
            Console.WriteLine("Method1:" + e.Instance.Method1());
            Console.WriteLine("Method2:" + e.Instance.Method2("test"));
            e.Instance.Unload();
        }

        private static void PluginManager_OnInstanceCreating(object sender, Events.PluginInstanceCreatingArgs e)
        {
            Console.WriteLine($"准备从程序集{e.Assembly}加载类型{e.ClassType}");
        }

        private static void PluginManager_OnError(object sender, Events.PluginErrorEventArgs e)
        {
            switch (e.ErrorType)
            {
                case PluginErrorTypes.None:
                    break;
                case PluginErrorTypes.InvalidManagedDllFile:
                    Console.WriteLine($"插件{e.FileName}不是有效的托管dll文件：{e.Exception}");
                    break;
                case PluginErrorTypes.CannotLoadClassTypes:
                    Console.WriteLine($"无法从{e.FileName}程序集中加载类的类型定义，这可能是dll依赖的文件不存在引起的：{e.Exception}");
                    break;
                case PluginErrorTypes.ImplementionClassNotFound:
                    Console.WriteLine($"插件{e.FileName}内未找到实现指定接口的类：{e.Exception}");
                    break;
                case PluginErrorTypes.IllegalClassDefinition:
                    Console.WriteLine($"{e.FileName}是不合法的类型定义，这可能是类型不是class，修饰符包含abstract或未声明为public：{e.Exception}");
                    break;
                case PluginErrorTypes.ZeroParameterConstructorNotFound:
                    Console.WriteLine($"插件{e.FileName}的类不包含无参构造函数：{e.Exception}");
                    break;
                case PluginErrorTypes.InstanceCreateFailed:
                    Console.WriteLine($"在文件{e.FileName}中找到了实现指定接口的类{e.ClassType}，但是创建实例时抛出了异常：{e.Exception}");
                    break;
                case PluginErrorTypes.Unkown:
                    Console.WriteLine("未知错误");
                    break;
                default:
                    break;
            }
        }

        private static void PluginManager_OnAssemblyLoaded(object sender, Events.PluginAssemblyLoadedArgs e)
        {
            Console.WriteLine($"从文件{e.FileName}加载程序集{e.Assembly}成功");
        }

        private static void PluginManager_OnAssemblyLoading(object sender, Events.PluginAssemblyLoadingArgs e)
        {
            Console.WriteLine($"准备从文件{e.FileName}加载程序集");
        }
    }
}
