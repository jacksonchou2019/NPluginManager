namespace NPluginManager.Enums
{
    /// <summary NoteObject="enum">
    /// [功能描述:加载插件时发生错误的错误类型]
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
    public enum PluginErrorTypes
    {
        /// <summary>
        /// 没有错误
        /// </summary>
        None,

        /// <summary>
        /// 插件不是有效的托管dll文件
        /// </summary>
        InvalidManagedDllFile,

        /// <summary>
        /// 无法从程序集中加载类的类型定义，这可能是dll依赖的文件不存在引起的。
        /// </summary>
        CannotLoadClassTypes,

        /// <summary>
        /// 插件内未找到实现指定抽象类的类
        /// </summary>
        ImplementionClassNotFound,

        /// <summary>
        /// 不合法的类型定义，这可能是类型不是class，修饰符包含abstract或未声明为public。
        /// </summary>
        IllegalClassDefinition,

        /// <summary>
        /// 插件的类不包含无参构造函数
        /// </summary>
        ZeroParameterConstructorNotFound,

        /// <summary>
        /// 实例创建失败
        /// </summary>
        InstanceCreateFailed,

        /// <summary>
        /// 未知错误
        /// </summary>
        Unkown
    }
}
