using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NPluginManager.Enums;
using NPluginManager.Events;
using NPluginManager.Exceptions;

namespace NPluginManager
{
    /// <summary NoteObject="class">
    /// [功能描述:插件管理器]
    /// [设计创建者:madfrog]
    /// [设计创建时间:2017-09-13]
    /// [编码创建者:madfrog]
    /// [编码创建时间:2017-09-13]
    /// <说明>
    ///     [内容:]
    /// </说明>
    /// <修改记录>
    ///     [修改者:]
    ///     [修改时间:]
    ///     [修改内容:]
    /// </修改记录>
    /// </summary>
    public sealed class PluginManager<TPluginInterface> : IDisposable where TPluginInterface : PluginInterface
    {
        #region Fields
        /// <summary>
        /// 存储插件的目录
        /// </summary>
        private string _pluginFolder;

        /// <summary>
        /// 抽象类类型
        /// </summary>
        private Type _abstractType;

        private FileSystemWatcher _watcher;

        /// <summary>
        /// 程序集加载前的事件
        /// </summary>
        public event PluginEvent<PluginAssemblyLoadingArgs> OnAssemblyLoading;

        /// <summary>
        /// 程序集加载后的事件
        /// </summary>
        public event PluginEvent<PluginAssemblyLoadedArgs> OnAssemblyLoaded;

        /// <summary>
        /// 插件实例创建前的事件
        /// </summary>
        public event PluginEvent<PluginInstanceCreatingArgs> OnInstanceCreating;

        /// <summary>
        /// 插件实例创建后的事件
        /// </summary>
        public event PluginEvent<PluginInstanceCreatedArgs<TPluginInterface>> OnInstanceCreated;

        /// <summary>
        /// 加载插件失败的事件
        /// 错误类型参见<see cref="PluginErrorTypes"/>
        /// </summary>
        public event PluginErrorEvent<PluginErrorEventArgs> OnError;

        /// <summary>
        /// 所有插件加完成后的事件
        /// </summary>
        public event PluginEvent<PluginReadyArgs> OnReady;
        #endregion

        #region Properties
        public List<TPluginInterface> PluginList { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 初始化插件管理器
        /// </summary>
        /// <param name="appDomain">使用插件的应用程序域</param>
        /// <param name="pluginFolder">插件所在目录，绝对路径或相对路径。</param>
        public PluginManager(AppDomain appDomain, string pluginFolder = "plugin")
        {
            _abstractType = typeof(TPluginInterface);

            // 检查泛型参数类型
            if (!_abstractType.IsAbstract)
            {
                throw new PluginInvalidGeneralTypeException();
            }

            _pluginFolder = Path.IsPathRooted(pluginFolder)
                ? pluginFolder : Path.Combine(appDomain.BaseDirectory, pluginFolder);

            // 如果插件目录不存在，就创建。
            if (!Directory.Exists(_pluginFolder))
            {
                Directory.CreateDirectory(_pluginFolder);
            }

            PluginList = new List<TPluginInterface>();
        }
        #endregion

        #region Events
        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(e.FullPath);
            if (!fileInfo.Exists)
            {
                return;
            }

            while (true)
            {
                try
                {
                    fileInfo.OpenRead().Close();
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
                break;
            }
            LoadPlugin(e.FullPath);
        }

        private void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(e.FullPath);
            if (!fileInfo.Exists)
            {
                return;
            }

            while (true)
            {
                try
                {
                    fileInfo.OpenRead().Close();
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
                break;
            }
            RemovePlugin(e.OldName);
            LoadPlugin(e.FullPath);
        }

        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            RemovePlugin(Path.GetFileName(e.FullPath));
        }
        #endregion

        #region Methods(public)
        /// <summary>
        /// 加载插件目录下所有插件
        /// </summary>
        /// <param name="filter">插件过滤器，返回False表示不加载这个插件。</param>
        /// <param name="emitEvent">是否Ready触发事件，默认为True。</param>
        public void Load(Func<string, bool> filter = null, bool emitEvent = true)
        {
            // 遍历插件目录下的dll，筛选dll文件，如果调用Load时有过滤器，则调用过滤器。
            foreach (var filename in Directory.GetFiles(_pluginFolder, "*.dll")
                .Where(filename => filter == null || filter(filename)))
            {
                LoadPlugin(filename);
            }

            if (emitEvent)
            {
                EmitPluginEvent(OnReady, new PluginReadyArgs
                {
                    Count = PluginList.Count
                });
            }
        }

        public void StartWatch()
        {
            if (_watcher == null)
            {
                _watcher = new FileSystemWatcher(_pluginFolder, "*.dll");
                _watcher.Created += watcher_Created; ;
                _watcher.Renamed += watcher_Renamed; ;
                _watcher.Deleted += watcher_Deleted;
            }

            if (_watcher.EnableRaisingEvents)
            {
                return;
            }

            _watcher.EnableRaisingEvents = true;
        }

        public void StopWatch()
        {
            if (_watcher.EnableRaisingEvents)
            {
                _watcher.EnableRaisingEvents = false;
            }
        }

        public void Dispose()
        {
            StopWatch();
            _watcher.Dispose();
            _pluginFolder = null;
            _abstractType = null;
            OnAssemblyLoading = null;
            OnAssemblyLoaded = null;
            OnInstanceCreating = null;
            OnInstanceCreated = null;
            OnError = null;
            OnReady = null;
        }
        #endregion

        #region Methods(private)
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="filename">文件名称</param>
        private void LoadPlugin(string filename)
        {
            Assembly assembly = LoadAssembly(filename);

            if (assembly == null)
            {
                return;
            }

            LoadPluginType(filename, assembly);
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns>程序集</returns>
        private Assembly LoadAssembly(string filename)
        {
            try
            {
                // 加载程序集前发出一个事件，确认是否想用这个文件。
                if (EmitPluginEvent(OnAssemblyLoading, new PluginAssemblyLoadingArgs
                {
                    FileName = filename
                }))
                {
                    return null;
                }

                // 加载
                Assembly assembly = Assembly.LoadFrom(filename);

                // 检查插件依赖
                AssemblyName[] dependencies = assembly.GetReferencedAssemblies();

                // 加载后再发出一个事件，如果程序现在才知道不想用这个文件呢。
                if (EmitPluginEvent(OnAssemblyLoaded, new PluginAssemblyLoadedArgs
                {
                    FileName = filename,
                    Assembly = assembly
                }))
                {
                    return null;
                }

                return assembly;
            }
            catch (Exception e)
            {
                EmitPluginErrorEvent(new PluginErrorEventArgs
                {
                    FileName = filename,
                    Exception = e,
                    ErrorType = PluginErrorTypes.InvalidManagedDllFile
                });
                return null;
            }
        }

        /// <summary>
        /// 发出插件事件的公共函数
        /// </summary>
        /// <returns>返回True阻止插件继续加载</returns>
        private bool EmitPluginEvent<TArg>(PluginEvent<TArg> eventName, TArg arg)
            where TArg : PluginEventArgs
        {
            if (eventName == null)
            {
                return false;
            }

            eventName.Invoke(this, arg);

            return arg.Cancel;
        }

        /// <summary>
        /// 发出插件错误事件的公共函数
        /// </summary>
        private void EmitPluginErrorEvent(PluginErrorEventArgs arg)
        {
            OnError?.Invoke(this, arg);
        }

        /// <summary>
        /// 从程序集加载插件的实现类(仅加载第一个)
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <param name="assembly">程序集对象</param>
        private void LoadPluginType(string filename, Assembly assembly)
        {
            Type[] types;
            try
            {
                // 加载程序集内的所有类型
                types = assembly.GetTypes();
            }
            catch (Exception e)
            {
                EmitPluginErrorEvent(new PluginErrorEventArgs
                {
                    FileName = filename,
                    Assembly = assembly,
                    Exception = e,
                    ErrorType = PluginErrorTypes.CannotLoadClassTypes
                });
                return;
            }

            // 查找继承自指定类的派生类
            Type type = types.FirstOrDefault(t => t.IsSubclassOf(_abstractType));

            // 插件内没有找到继承抽象类的类，可能不是插件。
            if (type == null)
            {
                EmitPluginErrorEvent(new PluginErrorEventArgs
                {
                    FileName = filename,
                    Assembly = assembly,
                    ErrorType = PluginErrorTypes.ImplementionClassNotFound
                });
                return;
            }

            // 检查类型修饰符
            // 这个类型不是类class或不是abstract或不是public。
            if (!type.IsClass || type.IsAbstract || !type.IsPublic)
            {
                EmitPluginErrorEvent(new PluginErrorEventArgs
                {
                    FileName = filename,
                    Assembly = assembly,
                    ClassType = type,
                    ErrorType = PluginErrorTypes.IllegalClassDefinition
                });
                return;
            }

            // 检查是否存在无参的构造
            if (type.GetConstructors().All(con => con.GetParameters().Length > 0))
            {
                EmitPluginErrorEvent(new PluginErrorEventArgs
                {
                    FileName = filename,
                    Assembly = assembly,
                    ClassType = type,
                    ErrorType = PluginErrorTypes.ZeroParameterConstructorNotFound
                });
                return;
            }

            try
            {
                // 创建实例前，发出事件，可以阻止一下。
                if (EmitPluginEvent(OnInstanceCreating, new PluginInstanceCreatingArgs
                {
                    Assembly = assembly,
                    ClassType = type
                }))
                {
                    return;
                }

                // 创建实例
                if (type.FullName != null)
                {
                    TPluginInterface instance = assembly.CreateInstance(type.FullName) as TPluginInterface;
                    if (instance == null)
                    {
                        // 创建实例没有报错，但实例为null，发出事件。
                        EmitPluginErrorEvent(new PluginErrorEventArgs
                        {
                            FileName = filename,
                            Assembly = assembly,
                            ClassType = type,
                            ErrorType = PluginErrorTypes.ImplementionClassNotFound
                        });
                        return;
                    }

                    // 创建实例成功后，发出事件。
                    if (EmitPluginEvent(OnInstanceCreated, new PluginInstanceCreatedArgs<TPluginInterface>
                    {
                        Assembly = assembly,
                        ClassType = type,
                        Instance = instance
                    }))
                    {
                        return;
                    }

                    instance.FileName = Path.GetFileName(filename);

                    PluginList.Add(instance);
                }
            }
            catch (Exception e)
            {
                // 创建实例时发生了异常，发出事件。
                EmitPluginErrorEvent(new PluginErrorEventArgs
                {
                    FileName = filename,
                    Assembly = assembly,
                    ClassType = type,
                    ErrorType = PluginErrorTypes.InstanceCreateFailed,
                    Exception = e
                });
            }
        }

        private void RemovePlugin(string filename)
        {
            for (int i = PluginList.Count - 1; i >= 0; i--)
            {
                if (PluginList[i].FileName == filename)
                {
                    PluginList.Remove(PluginList[i]);
                }
            }
        }
        #endregion
    }
}
