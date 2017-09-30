using System;

namespace NPluginManager.Exceptions
{
    /// <summary NoteObject="class">
    /// [功能描述:实例化PluginManager的泛型参数不是抽象类]
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
    [Serializable]
    public class PluginInvalidGeneralTypeException : Exception
    {
        /// <summary>
        /// 实例化PluginManager的泛型参数不是抽象类时会引发此异常
        /// </summary>
        public PluginInvalidGeneralTypeException()
            : base("实例化PluginManager的泛型参数不是抽象类") { }
    }
}
