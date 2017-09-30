namespace NPluginManager.Events
{
    /// <summary>
    /// 插件加载错误事件
    /// </summary>
    /// <typeparam name="T">事件参数的类型</typeparam>
    /// <param name="sender">这个事件来自哪一个插件实例</param>
    /// <param name="e">事件详细信息</param>
    public delegate void PluginErrorEvent<in T>(object sender, T e) where T : PluginErrorEventArgs;
}
