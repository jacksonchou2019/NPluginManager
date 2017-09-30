namespace NPluginManager.Events
{
    /// <summary NoteObject="class">
    /// [功能描述:创建插件中的类实例后的事件数据]
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
    public class PluginInstanceCreatedArgs<T> : PluginInstanceCreatingArgs where T : class
    {
        /// <summary>
        /// 插件类的实例对象
        /// </summary>
        public T Instance { get; set; }
    }
}
