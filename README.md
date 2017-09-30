# NPluginManager
**NPluginManager**基于C#的动态加载插件的实现方式及插件状态跟踪监控。

>**注意**：本库为.NET 3.5，后续会发布其它.NET版本，请关注各自分支：

    .NET 4.0 (计划发布中)
    .NET 4.5 (计划发布中)
    .NET Core 1.0 (计划发布中)
    .NET Core 2.0 (计划发布中)

---

# 项目文件夹说明(src文件夹下)

<table>
  <tr>
    <th bgcolor="#B3D9D9">文件夹</th>
    <th bgcolor="#B3D9D9">说明</th>
  </tr>
  <tr>
    <td>NPluginManager</td>
    <td>核心库</td>
  </tr>
  <tr>
    <td>Test\NPluginManager.Test</td>
    <td>测试主demo</td>
  </tr>
  <tr>
    <td>Test\TestClass1</td>
    <td>测试辅助dll</td>
  </tr>
  <tr>
    <td>Test\TestClass2</td>
    <td>测试辅助dll</td>
  </tr>
</table>

---

# 贡献代码

>如果需要使用或修改此项目的源代码，建议先Fork。也欢迎将您修改的通用版本Pull Request过来。

    1.Fork
    2.创建您的特性分支(git checkout -b my-new-feature)。
    3.提交您的改动(git commit -am 'Added some feature')。
    4.将您的修改记录提交到远程git仓库(git push origin my-new-feature)。
    5.到github网站的该git远程仓库的my-new-feature分支下发起Pull Request(请提交到Developer分支，不要直接提交到master分支)。

---

# 各分支说明

<table>
  <tr>
    <th bgcolor="#B3D9D9">分支</th>
    <th bgcolor="#B3D9D9">说明</th>
  </tr>
  <tr>
    <td>master</td>
    <td>正式发布的主分支，通常这个分支比较稳定，可以用于生产环境。</td>
  </tr>
  <tr>
    <td>Developer</td>
    <td>开发分支，此分支通常为Beta版本，新版本都会先在此分支中进行开发，最后推送稳定版到master分支，如果想对新功能先睹为快，可以使用此分支。</td>
  <tr>
    <td>NET 4.0</td>
    <td>4.0正式发布分支</td>
  </tr>
  <tr>
    <td>NET 4.5</td>
    <td>4.5正式发布分支</td>
  </tr>
  <tr>
    <td>Core 1.0</td>
    <td>Core 1.0正式发布分支</td>
  </tr>
  <tr>
    <td>Core 2.0</td>
    <td>Core 2.0正式发布分支</td>
  </tr>
</table>

---

# 使用Nuget安装到项目中

>后续会上传到Nuget上。

---

# 感谢贡献者

>感谢为此项目做出贡献的开发者，你们不光完善了这个项目，也为中国开源事业出了一份力，感谢你们！名单[点击这里](https://github.com/madfrog1982/NPluginManager/blob/master/contributors.md)查看。

---

# 捐助

>如果这个项目对您有用，我们欢迎各方任何形式的捐助，也包括参与到项目代码更新或意见反馈中来。谢谢！
>
![微信支付](https://github.com/madfrog1982/NPluginManager/blob/master/files/images/wx.JPG)
![支付宝支付](https://github.com/madfrog1982/NPluginManager/blob/master/files/images/zfb.JPG)

---

# License

Apache License Version 2.0

Copyright © 2001-2017 Neusoft Corporation

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Detail：[https://github.com/madfrog1982/NPluginManager/blob/master/license.md](https://github.com/madfrog1982/NPluginManager/blob/master/license.md)