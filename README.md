配置剧情文件

`AVGPanel` 为当前的AVG基础界面

首先需要创建一个AvgCommandConfig的配置文件

`HEADER`，负责配置章节标题。

`Background`，负责配置剧情界面最底部的背景图片。

`Delay`，等待延迟时间。

`Dialogue`，剧情对话内容

`CharacterSet`，设置立绘，用于后续的行为动作。

`CharacterAction`，立绘的行为，需要使用`CharacterSet`先将立绘设置到舞台上。目前支持：移动，渐隐和渐显。

`Selection`，用于设置选项按钮，最多支持五个。

`FocusCharacter`，设置当前聚集的角色，其他角色状态置灰。

`Predicate`，拦截`Decision`的选择。一旦玩家选择了某选项，则会使命令队列不断出队，直至检测到出队的元素为对应选项的`Predicate`或是`PredicateEnd`。也正是这个原因，该系统暂时不支持循环式的选择。这一点可能以后改进。

`SceneEffect`，场景的效果控制，目前支持背景画面抖动，淡入，淡出。

`END`，意味着整个剧情的结束。一段剧情可以有多个`END`出口。

------------------------------------------------------------------------------------------------------------------------------------------------------------------



























