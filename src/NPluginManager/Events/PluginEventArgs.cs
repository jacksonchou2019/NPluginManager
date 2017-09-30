using System;

namespace NPluginManager.Events
{
    /// <summary NoteObject="class">
    /// [功能描述:事件参数的基类]
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
    public abstract class PluginEventArgs : EventArgs
    {
        /// <summary>
        /// 取消继续执行操作，设置为True为阻止本次事件后面的代码继续执行。
        /// </summary>
        public bool Cancel { get; set; }
    }
}
