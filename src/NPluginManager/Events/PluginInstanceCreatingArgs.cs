using System;
using System.Reflection;

namespace NPluginManager.Events
{
    /// <summary NoteObject="class">
    /// [功能描述:创建插件中的类实例前的事件数据]
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
    public class PluginInstanceCreatingArgs : PluginEventArgs
    {
        /// <summary>
        /// 程序集对象
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 加载插件类成功后读取到的类的类型
        /// </summary>
        public Type ClassType { get; internal set; }
    }
}
