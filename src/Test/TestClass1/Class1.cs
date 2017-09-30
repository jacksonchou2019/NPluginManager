using System;
using NPluginManager;

namespace TestClass1
{
    public class Class1 : PluginInterface
    {
        public override void Load()
        {
            Console.WriteLine("Load " + this.GetType().FullName);
        }

        public override void Unload()
        {
            Console.WriteLine("Unload " + this.GetType().FullName);
        }

        public override string Method1()
        {
            return "这是从第一个插件里面Method1来的数据";
        }

        public override string Method2(string param)
        {
            return "这是从第一个插件里面Method2来的数据：" + param;
        }
    }
}
