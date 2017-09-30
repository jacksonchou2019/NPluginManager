namespace NPluginManager.Events
{
    /// <summary NoteObject="class">
    /// [功能描述:Assembly加载前事件的参数]
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
    public class PluginAssemblyLoadingArgs : PluginEventArgs
    {
        /// <summary>
        /// DLL文件的完整路径
        /// </summary>
        public string FileName { get; internal set; }
    }
}
