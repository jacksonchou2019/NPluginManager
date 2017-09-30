namespace NPluginManager
{
    /// <summary NoteObject="class">
    /// [功能描述:插件接口]
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
    public abstract class PluginInterface
    {
        /// <summary>
        /// 物理文件名称(带文件后缀)
        /// </summary>
        internal string FileName { get; set; }

        /// <summary>
        /// 加载插件后调用，插件必须实现的方法。
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// 卸载插件前调用，插件必须实现的方法。
        /// </summary>
        public abstract void Unload();

        /// <summary>
        /// 第一个功能方法，可选实现。
        /// </summary>
        /// <returns>null</returns>
        public virtual string Method1()
        {
            return null;
        }

        /// <summary>
        /// 第二个功能方法，可选实现。
        /// </summary>
        /// <param name="param">入参</param>
        /// <returns>入参</returns>
        public virtual string Method2(string param)
        {
            return param;
        }
    }
}
