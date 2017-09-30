using System;
using System.Reflection;
using NPluginManager.Enums;

namespace NPluginManager.Events
{
    /// <summary NoteObject="class">
    /// [功能描述:插件加载失败的事件数据]
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
    public class PluginErrorEventArgs : EventArgs
    {
        /// <summary>
        /// DLL文件的完整路径
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// 程序集对象
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// 类加载失败的异常信息
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        public PluginErrorTypes ErrorType { get; internal set; }

        /// <summary>
        /// 加载插件类成功后读取到的类的类型
        /// </summary>
        public Type ClassType { get; internal set; }
    }
}
