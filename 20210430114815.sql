/*
MySQL Backup
Database: blog
Backup Time: 2021-04-30 11:48:15
*/

SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS `blog`.`article`;
DROP TABLE IF EXISTS `blog`.`article_category`;
DROP TABLE IF EXISTS `blog`.`sys_button`;
DROP TABLE IF EXISTS `blog`.`sys_config`;
DROP TABLE IF EXISTS `blog`.`sys_dictionary`;
DROP TABLE IF EXISTS `blog`.`sys_login_log`;
DROP TABLE IF EXISTS `blog`.`sys_menu`;
DROP TABLE IF EXISTS `blog`.`sys_role`;
DROP TABLE IF EXISTS `blog`.`sys_role_power`;
DROP TABLE IF EXISTS `blog`.`sys_user`;
DROP TABLE IF EXISTS `blog`.`sys_user_role`;
CREATE TABLE `article` (
  `guid` varchar(36) NOT NULL COMMENT 'guid',
  `category_id` int(11) NOT NULL COMMENT '分类Id',
  `keywords` varchar(255) DEFAULT NULL COMMENT 'keywords',
  `description` varchar(255) DEFAULT NULL COMMENT 'description',
  `article_title` varchar(30) NOT NULL COMMENT '文章标题',
  `subtitle` varchar(255) DEFAULT NULL COMMENT '副标题',
  `title_img` varchar(255) DEFAULT NULL COMMENT '标题图片',
  `content` varchar(4000) DEFAULT NULL COMMENT '文章内容',
  `pulish_status` int(11) NOT NULL DEFAULT '0' COMMENT '发布状态（0＝未发布，1＝已发布）',
  `is_top` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否置顶（0＝否；1＝是）',
  `is_recommend` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否推荐（0＝否；1＝是）',
  `is_original` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否原创（0＝否；1＝是）',
  `pv_count` int(11) NOT NULL DEFAULT '0' COMMENT '浏览量',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态（0＝有效；1＝无效）',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  PRIMARY KEY (`guid`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `article_category` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `name` varchar(30) NOT NULL COMMENT '分类名称',
  `sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `parent_id` int(11) DEFAULT NULL COMMENT '父类Id',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态（0＝有效；1＝无效）',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  `level` int(4) NOT NULL DEFAULT '1' COMMENT '层级',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_button` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `button_name` varchar(20) NOT NULL COMMENT '按钮名称',
  `js_event` varchar(20) NOT NULL COMMENT 'Js事件名称',
  `icon` varchar(20) DEFAULT NULL COMMENT '图标',
  `menu_id` int(11) NOT NULL COMMENT '菜单Id',
  `back_color` varchar(20) DEFAULT NULL COMMENT '背景颜色',
  `size_style` varchar(20) DEFAULT NULL COMMENT '按钮样式',
  `group_id` int(11) NOT NULL DEFAULT '0' COMMENT '分组',
  `sort` int(11) NOT NULL COMMENT '排序',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  `is_toolbar` int(255) DEFAULT NULL COMMENT '(0=自定义按钮;1=工具栏按钮)',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_config` (
  `config_code` varchar(10) NOT NULL COMMENT '配置项编码',
  `config_content` varchar(2000) NOT NULL COMMENT '配置项内容',
  PRIMARY KEY (`config_code`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_dictionary` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `dictionary_name` varchar(10) NOT NULL COMMENT '字典名称',
  `dictionary_code` varchar(10) NOT NULL COMMENT '字典编码',
  `parent_id` int(11) NOT NULL COMMENT '父级Id（-1=顶级）',
  `sort` int(11) NOT NULL DEFAULT '1' COMMENT '排序',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态（0＝有效；1＝无效）',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_login_log` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `action_type` int(11) NOT NULL COMMENT '执行类型（1＝登录；2＝退出）',
  `operator` varchar(50) NOT NULL COMMENT '执行人',
  `exec_ip` varchar(15) DEFAULT NULL COMMENT '执行IP',
  `exec_time` datetime NOT NULL COMMENT '执行时间',
  `exec_content` varchar(1000) NOT NULL COMMENT '执行内容',
  `exec_result` varchar(1000) NOT NULL COMMENT '执行结果',
  `remark` varchar(1000) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4;
CREATE TABLE `sys_menu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `menu_name` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '菜单名称',
  `menu_code` int(11) NOT NULL COMMENT '菜单编码',
  `menu_icon` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '菜单图标',
  `page_title` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '页索引',
  `menu_url` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '菜单Url',
  `parent_id` int(11) NOT NULL COMMENT '父级ID(-1为顶级)',
  `parent_code` int(11) DEFAULT NULL COMMENT '父级编码',
  `enabled` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否启用',
  `level` int(11) NOT NULL COMMENT '层级',
  `sort` int(11) NOT NULL COMMENT '排序',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态（0＝有效；1＝无效）',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_role` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `role_name` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '角色名称',
  `role_code` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '角色编号',
  `role_desc` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '角色描述',
  `sort` int(11) NOT NULL COMMENT '排序',
  `enabled` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否启用',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态（0＝有效；1＝无效）',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_role_power` (
  `power_id` int(11) NOT NULL COMMENT '权限Id',
  `role_id` int(11) NOT NULL COMMENT '角色Id',
  `power_type` int(11) NOT NULL COMMENT '权限类型（1＝菜单；2＝按钮）'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
CREATE TABLE `sys_user` (
  `guid` varchar(36) NOT NULL COMMENT 'guid',
  `user_name` varchar(50) NOT NULL COMMENT '用户名',
  `user_pwd` varchar(50) NOT NULL COMMENT '用户密码',
  `salt_value` varchar(36) NOT NULL COMMENT '盐值',
  `true_name` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '姓名',
  `gender` int(11) NOT NULL DEFAULT '0' COMMENT '性别（0＝不详；1＝男；2＝女）',
  `head_img` varchar(512) DEFAULT NULL COMMENT '头像',
  `status` bit(1) NOT NULL DEFAULT b'0' COMMENT '状态（0＝有效；1＝无效）',
  `create_time` datetime DEFAULT NULL COMMENT '创建时间',
  `creator` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '创建人',
  `create_guid` varchar(36) DEFAULT NULL COMMENT '创建人Guid',
  `modify_time` datetime DEFAULT NULL COMMENT '修改时间',
  `modifier` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '修改人',
  `modify_guid` varchar(36) DEFAULT NULL COMMENT '修改人Guid',
  `enabled` bit(1) NOT NULL DEFAULT b'0' COMMENT '状态（0＝启用；1＝禁用）',
  PRIMARY KEY (`guid`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT COMMENT='系统用户';
CREATE TABLE `sys_user_role` (
  `user_guid` varchar(36) NOT NULL COMMENT '用户Guid',
  `role_id` int(11) NOT NULL COMMENT '角色Id'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;
BEGIN;
LOCK TABLES `blog`.`article` WRITE;
DELETE FROM `blog`.`article`;
INSERT INTO `blog`.`article` (`guid`,`category_id`,`keywords`,`description`,`article_title`,`subtitle`,`title_img`,`content`,`pulish_status`,`is_top`,`is_recommend`,`is_original`,`pv_count`,`status`,`create_time`,`creator`,`create_guid`,`modify_time`,`modifier`,`modify_guid`) VALUES ('20201230-1407-471b-b596-f7a7367de17f', 11, 'css,css3,文字,自动,截取,显示', 'css实现文字溢出自动截取并显示点点点', 'css实现文字溢出自动截取并显示点点点', 'css实现单行文本或多行文本溢出自动截取并显示点点点（省略号）', '/UploadFile/202012/20201230-1642-532d-b0ae-9508b0e5587b.png', '<h4>一、单行文本溢出显示省略号点点点...</h4><pre><span style=\"color: rgb(238, 236, 225);\">overflow:&nbsp;hidden;<br/>text-overflow:ellipsis;<br/>white-space:&nbsp;nowrap;</span><br/></pre><p><br/><br/></p><h4>二、多行文本溢出显示省略号点点点...</h4><pre><span style=\"color: rgb(238, 236, 225);\">display:&nbsp;-webkit-box;<br/>-webkit-box-orient:&nbsp;vertical;<br/>-webkit-line-clamp:&nbsp;2;&nbsp;&nbsp;//这个数字是控制多少行显示省略号<br/>overflow:&nbsp;hidden;</span><br/></pre>', 1, b'0', b'1', b'1', 26, 0, '2020-12-30 14:07:48', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', '2021-01-04 17:42:57', 'administrator', '1'),('20201231-1432-123b-b384-2c40afbd2fc0', 13, 'mysql,忘记,root,密码,登陆 ', 'mysql忘记root密码如何登录,mysql忘记root密码如何找回,mysql忘记root密码如何重置', 'mysql忘记root密码如何登陆 ', '相信很多同学有过忘记mysql root密码的情况，下面我就以linux系统为例，讲下怎么重置root密码。首先以root权限登录mysql数据库所在服务器，然后进行如下操作：', '/UploadFile/202012/20201231-1437-370c-9b9b-925dc151ecf4.jpg', '<p><span>相信很多同学有过忘记mysql root密码的情况，下面我就以linux系统为例，讲下怎么重置root密码。首先以root权限登录mysql数据库所在服务器，然后进行如下操作：</span></p><p><br/></p><p><span>1、第一步关闭 mysql服务&nbsp;</span></p><p><span>&nbsp; &nbsp; &nbsp;命令：systemctl stop mysql&nbsp; 或 sc stop mysql</span></p><p><br/></p><p><span>2、第二步修改mysql配置</span></p><p><span>&nbsp; &nbsp; &nbsp;找到my.cnf 配置文件 通常在 etc 目录下</span></p><p><span>&nbsp; &nbsp; &nbsp;vi /etc/my.cnf&nbsp; &nbsp;打开在[mysqld]段中加上一句: skip-grant-tables 保存并退出</span></p><p><br/></p><p><span>3、重启mysql服务</span></p><p><span>&nbsp; &nbsp; &nbsp;命令：systemctl restart mysql</span></p><p><br/></p><p><span>4、登录mysql</span></p><p><span>&nbsp; &nbsp; &nbsp;mysql -u root -p 回车, （忽略密码）再回车</span></p><p><br/></p><p><span>5、更改root密码</span></p><p><span>&nbsp; &nbsp; &nbsp;UPDATE mysql.user SET Password=PASSWORD(&#39;xxx&#39;) where USER=&#39;root&#39;;</span></p><p><span style=\"color: rgb(255, 0, 0);\">注意：更改密码如出现（ERROR 1054 (42S22): Unknown column &#39;Password&#39; in &#39;field list&#39;）的错误提示，则是mysql的user中没有Password这个字段，新版本的mysql是保存在authentication_string字段中。可通过select * from user 查询</span></p><p><br/></p><p><span>6、把mysql配置文件修改回来</span></p><p><span>&nbsp; &nbsp; &nbsp;vi /etc/my.cnf 打开把刚才添加的&nbsp;skip-grant-tables 删除或注释掉</span></p><p><br/></p><p><span>7、再次重启mysql服务</span></p><p><span>&nbsp; &nbsp; &nbsp;命令：systemctl restart mysql</span></p><p><span><br/></span></p><p><span><br/></span></p>', 1, b'0', b'0', b'1', 29, 0, '2020-12-31 14:32:13', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', '2020-12-31 17:14:34', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1'),('20201231-1748-0614-8f60-a3cbb1fe4e45', 17, '.net core,mvc,开发,网页源码,中文编码', '.net core mvc开发项目网页源码中文编码问题', '.net core mvc开发项目网页源码中文编码问题', '今天查看网页源码发现.net core mvc开发的项目HTML源码中有中文被编码了，经了解发现.net core mvc会默认将网页中文编码。如果想让网页源码正常，所以我们得设置下编码。在Startup.cs文件ConfigureServices方法里增加以下代码就可以解决了。', '/UploadFile/202012/20201231-1747-0315-9f0e-2993f60b76ee.jpg', '<p>今天查看网页源码发现.net core mvc开发的项目HTML源码中有中文被编码了，经了解发现.net core mvc会默认将网页中文编码。如果想让网页源码正常，所以我们得设置下编码。在Startup.cs文件ConfigureServices方法里增加以下代码就可以解决了。</p><p><br/></p><p>//解决ViewBag的中文编码问题 <br/>services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));</p><p><br/></p>', 1, b'0', b'1', b'1', 32, 0, '2020-12-31 17:48:07', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', '2021-01-04 17:42:51', 'administrator', '1'),('20201231-1748-0914-83e5-301c65412d88', 17, NULL, NULL, '.net core mvc开发项目网页源码中文编码问题', NULL, '/UploadFile/202012/20201231-1747-0315-9f0e-2993f60b76ee.jpg', '<p>今天网页源码发现.net core mvc开发的项目HTML源码中有中文被编码了，</p>', 1, b'1', b'1', b'1', 5, 1, '2020-12-31 17:48:10', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', '2020-12-31 17:51:55', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1'),('20210105-1524-2988-9e0a-a8f09e1d5b85', 16, 'C#,历史版本,C#语言', 'C#是一种安全的、稳定的、简单的、优雅的，由C和C++衍生出来的面向对象的编程语言', 'C# 语言历史版本', 'C#语言从2002年发展至今，已经历了9个大版本；C#是一种安全的、稳定的、简单的、优雅的，由C和C++衍生出来的面向对象的编程语言。它在继承C和C++强大功能的同时去掉了一些它们的复杂特性（像没有宏以及不允许多重继承）。C#综合了VB简单的可视化操作和C++的高运行效率，以其强大的操作能力、优雅的语法风格、创新的语言特性和便捷的面向组件编程的支持成为.NET开发的首选语言。', '/UploadFile/202101/20210106-1132-08e4-a997-b98e1b7837d6.png', '<p><br/></p><p style=\"text-align: center;\"><span style=\"font-size: 18px;\"><strong>C# 语言历史版本</strong></span></p><style>.table{color:#ddd;}</style><table class=\"table table-bordered\"><tbody><tr class=\"firstRow\"><td style=\"width:70px\">版本</td><td style=\"width:80px\">发布时间</td><td style=\"width:150px\">.NET Framework要求</td><td style=\"width:150px\">Visual Studio版本</td><td>主要功能</td></tr><tr><td>C# 1.0</td><td>2002.1</td><td>.NET Framework 1.0</td><td>Visual Studio .NET 2002</td><td>类、结构、接口、事件、属性、委托、运算符和表达式、语句、特性</td></tr><tr><td style=\"word-break: break-all;\">C# 1.2</td><td>2003.4<br/></td><td>.NET Framework 1.1</td><td>Visual Studio .NET 2003</td><td style=\"word-break: break-all;\">这个版本没有新增功能，只是对语言做了一些小改进</td></tr><tr><td>C# 2.0</td><td>2005.11</td><td>.NET Framework 2.0</td><td>Visual Studio 2005</td><td>泛型、分部类型、匿名方法、可以为 null 的值类型、迭代器、协变和逆变</td></tr><tr><td>C# 3.0</td><td>2007.11</td><td>.NET Framework 2.0\\3.0\\3.5</td><td>Visual Studio 2008</td><td>自动实现的属性、匿名类型、查询表达式、Lambda 表达式、表达式树、扩展方法、隐式类型本地变量、分部方法、对象和集合初始值设定项</td></tr><tr><td>C# 4.0</td><td>2010.4</td><td>.NET Framework 4.0</td><td>Visual Studio 2010</td><td>动态绑定、命名参数/可选参数、泛型协变和逆变、嵌入的互操作类型</td></tr><tr><td>C# 5.0</td><td>2012.8</td><td>.NET Framework 4.5</td><td>Visual Studio 2012\\2013</td><td>异步成员、调用方信息特性</td></tr><tr><td>C# 6.0</td><td>2015.7</td><td>.NET Framework 4.6</td><td>Visual Studio 2015</td><td>静态导入、异常筛选器、自动属性初始化表达式、Expression bodied 成员、Null 传播器、字符串内插、nameof 运算符</td></tr><tr><td>C# 7.0</td><td>2017.3</td><td>.NET Framework 4.6.2</td><td>Visual Studio 2017</td><td>Out 变量、元组和析构函数、模式匹配、本地函数、已扩展 expression bodied 成员、Ref 局部变量和返回结果、弃元、二进制文本和数字分隔符、引发表达式</td></tr><tr><td>C# 7.1</td><td>2017.6</td><td>.NET Framework 4.7</td><td>Visual Studio 2017 v15.3预览版</td><td style=\"word-break: break-all;\">async Main 方法、default 文本表达式、推断元组元素名称、泛型类型参数的模式匹配</td></tr><tr><td>C# 7.2</td><td>2017.11</td><td>.NET Framework 4.7.1</td><td>Visual Studio 2017 v15.5</td><td style=\"word-break: break-all;\">编写安全高效代码的技巧、非尾随命名参数、数值文字中的前导下划线 、private protected 访问修饰符、条件 ref 表达式&nbsp;</td></tr><tr><td>C# 7.3</td><td>2018.5</td><td style=\"word-break: break-all;\"><p>.NET Framework 4.7.2</p><p>.NET Standard 1.x/2.0</p></td><td style=\"word-break: break-all;\">Visual Studion 2017 v15.7</td><td style=\"word-break: break-all;\">无需固定即可访问固定的字段、可以重新分配 ref 本地变量、可以使用 stackalloc 数组上的初始值设定项、可以对支持模式的任何类型使用 fixed 语句、可以使用其他泛型约束。</td></tr><tr><td>C# 8.0</td><td style=\"word-break: break-all;\">2019.10</td><td>.NET Standard 2.1\n .NET Core 3.x</td><td style=\"word-break: break-all;\">Visual Studion 2019</td><td>Readonly 成员、默认接口方法、模式匹配增强功能：(Switch 表达式、属性模式、元组模式、位置模式)、Using 声明、静态本地函数、可处置的 ref 结构、可为空引用类型、异步流、索引和范围、Null 合并赋值、非托管构造类型、嵌套表达式中的 Stackalloc、内插逐字字符串的增强功能</td></tr><tr><td>C# 9.0</td><td style=\"word-break: break-all;\">2020.6</td><td>.NET 5</td><td style=\"word-break: break-all;\">Visual Studion 2019</td><td style=\"word-break: break-all;\">记录、仅限 Init 的资源库、顶级语句、模式匹配增强功能、本机大小的整数、函数指针、禁止发出 localsinit 标志、目标类型的新表达式、静态匿名函数、目标类型的条件表达式、协变返回类型、扩展 GetEnumerator 支持 foreach 循环、Lambda 弃元参数、本地函数的属性、模块初始值设定项、分部方法的新功能</td></tr></tbody></table><p><br/></p><p>&nbsp; &nbsp; \n	\n	<br/>\n	功能详情说明请查询<a target=\"_blank\" style=\"color:#28a745\" href=\"https://docs.microsoft.com/zh-cn/dotnet/csharp/whats-new/csharp-version-history\">官网</a></p>', 1, b'0', b'0', b'1', 29, 0, '2021-01-05 15:24:29', 'administrator', '1', '2021-01-06 17:13:57', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1'),('20210115-1800-2393-9e96-c837b1051372', 16, 'C#中基本类型占用字节数', 'C#中基本类型占用字节数', 'C#中基本类型占用字节数', 'C#中基本类型占用字节数', '/UploadFile/202101/20210120-1551-088b-a257-6db61aef5269.png', '<div id=\"cnblogs_post_body\" class=\"blogpost-body\"><p>bool -&gt; System.Boolean (布尔型，其值为 true 或者 false)</p><p>byte -&gt; System.Byte (字节型，占 1 字节，表示 8 位正整数，范围 0 ~ 255)</p><p>sbyte -&gt; System.SByte (带符号字节型，占 1 字节，表示 8 位整数，范围 -128 ~ 127)</p><p>char -&gt; System.Char (字符型，占有两个字节，表示 1 个 Unicode 字符)</p><p>short -&gt; System.Int16 (短整型，占 2 字节，表示 16 位整数，范围 -32,768 ~ 32,767)</p><p>ushort -&gt; System.UInt16 (无符号短整型，占 2 字节，表示 16 位正整数，范围 0 ~ 65,535)</p><p>uint -&gt; System.UInt32 (无符号整型，占 4 字节，表示 32 位正整数，范围 0 ~ 4,294,967,295)</p><p>int -&gt; System.Int32 (整型，占 4 字节，表示 32 位整数，范围 -2,147,483,648 到 2,147,483,647)</p><p>float -&gt; System.Single (单精度浮点型，占 4 个字节)</p><p>ulong -&gt; System.UInt64 (无符号长整型，占 8 字节，表示 64 位正整数，范围 0 ~ 大约 10 的 20 次方)</p><p>long -&gt; System.Int64 (长整型，占 8 字节，表示 64 位整数，范围大约 -(10 的 19) 次方 到 10 的 19 次方)</p><p>double -&gt; System.Double (双精度浮点型，占8 个字节)</p></div><p><br/></p>', 1, b'0', b'0', b'0', 14, 0, '2021-01-15 18:00:23', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', '2021-01-20 15:51:53', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1'),('20210119-1550-004e-8d7d-f9c38cba1961', 17, 'asp.net core mvc ', '记一次开发asp.net core mvc项目中碰到代码发布到服务器后报404的问题，看日志发现找不到视图页面', 'asp.net core mvc发布后找不到视图的问题', '记一次开发asp.net core mvc项目中碰到代码发布到服务器后报404的问题，看日志发现找不到视图页面，但看代码视图页面明明存在的，对比地址也没有错误，找了老半天很是头痛。最后考虑是不是发布的问题，因为发布会根据项目文件过滤掉文件，打开项目文件发现果真有问题。', '/UploadFile/202101/20210120-1552-02e6-a04b-dbdc6fdd0d20.jpg', '<p>记一次开发asp.net core mvc项目中碰到代码发布到服务器后报404的问题，看日志提示找不到视图，</p><p>Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware|ERROR|An unhandled exception has occurred while executing the request. The view &#39;Index&#39; was not found. The following locations were searched:&nbsp;<br style=\"white-space: normal;\"/>&nbsp; &nbsp;/Areas/Admin/Views/SysLoginLog/Index.cshtml&nbsp;<br style=\"white-space: normal;\"/>&nbsp; &nbsp;/Areas/Admin/Views/Shared/Index.cshtml&nbsp;<br style=\"white-space: normal;\"/>&nbsp; &nbsp;/Views/Shared/Index.cshtml&nbsp;<br style=\"white-space: normal;\"/>&nbsp; &nbsp;/Pages/Shared/Index.cshtml</p><p><br/></p><p>但看代码视图页面明明存在的，对比地址也没有错误，找了老半天很是头痛。最后考虑是不是发布的问题，因为发布会根据项目文件过滤掉文件，打开项目文件发现果真有问题。</p><p><br/></p><p>解决方案：</p><p>打开项目文件(.csproj)把ItemGroup节点下引用的页面删除重新发布就可以了</p>', 1, b'0', b'0', b'1', 16, 0, '2021-01-19 15:50:00', 'administrator', '1', '2021-01-20 15:58:24', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`article_category` WRITE;
DELETE FROM `blog`.`article_category`;
INSERT INTO `blog`.`article_category` (`Id`,`name`,`sort`,`parent_id`,`status`,`create_time`,`creator`,`create_guid`,`modify_time`,`modifier`,`modify_guid`,`level`) VALUES (7, '前端技术', 1, -1, 0, '2020-12-29 11:36:20', 'administrator', '1', NULL, NULL, NULL, 1),(8, '.Net技术', 2, -1, 0, '2020-12-29 11:36:33', 'administrator', '1', NULL, NULL, NULL, 1),(9, '数据库', 3, -1, 0, '2020-12-29 11:36:44', 'administrator', '1', NULL, NULL, NULL, 1),(10, 'Linux运维', 4, -1, 0, '2020-12-29 11:36:59', 'administrator', '1', NULL, NULL, NULL, 1),(11, 'html+css', 1, 7, 0, '2020-12-29 11:43:21', 'administrator', '1', NULL, NULL, NULL, 2),(12, 'Javascript/JQuery', 2, 7, 0, '2020-12-29 11:43:53', 'administrator', '1', NULL, NULL, NULL, 2),(13, 'Mysql', 1, 9, 0, '2020-12-29 11:44:13', 'administrator', '1', NULL, NULL, NULL, 2),(14, 'SqlServer', 2, 9, 0, '2020-12-29 11:44:47', 'administrator', '1', NULL, NULL, NULL, 2),(15, '.Net Core', 1, -1, 1, '2020-12-29 11:45:12', 'administrator', '1', '2020-12-29 11:45:19', 'administrator', '1', 1),(16, '.Net', 1, 8, 0, '2020-12-29 11:45:38', 'administrator', '1', NULL, NULL, NULL, 2),(17, '.Net Core', 2, 8, 0, '2020-12-29 11:45:51', 'administrator', '1', NULL, NULL, NULL, 2);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_button` WRITE;
DELETE FROM `blog`.`sys_button`;
INSERT INTO `blog`.`sys_button` (`Id`,`button_name`,`js_event`,`icon`,`menu_id`,`back_color`,`size_style`,`group_id`,`sort`,`status`,`create_time`,`creator`,`create_guid`,`modify_time`,`modifier`,`modify_guid`,`is_toolbar`) VALUES (1, '添加', 'add', 'layui-icon-add-1', 2, NULL, 'layui-btn-sm', 1, 1, 0, '2020-11-04 14:06:20', 'administrator', '1', NULL, NULL, NULL, 1),(2, '修改', 'edit', 'layui-icon-edit', 2, NULL, 'layui-btn-sm', 1, 2, 0, '2020-11-04 14:09:39', 'administrator', '1', NULL, NULL, NULL, 1),(3, '删除', 'del', 'layui-icon-close', 2, NULL, 'layui-btn-sm', 1, 3, 0, '2020-11-04 14:10:25', 'administrator', '1', NULL, NULL, NULL, 1),(4, '添加', 'add', 'layui-icon-add-1', 3, NULL, 'layui-btn-sm', 1, 1, 0, '2020-11-04 14:12:29', 'administrator', '1', NULL, NULL, NULL, 1),(5, '添加', 'add', 'layui-icon-add-1', 4, NULL, 'layui-btn-sm', 1, 1, 0, '2020-11-04 14:16:46', 'administrator', '1', NULL, NULL, NULL, 1),(6, '修改', 'edit', 'layui-icon-edit', 4, NULL, 'layui-btn-sm', 1, 2, 1, '2020-11-04 14:17:29', 'administrator', '1', NULL, NULL, NULL, 1),(7, '修改', 'edit', 'layui-icon-edit', 3, NULL, 'layui-btn-sm', 1, 2, 0, '2020-11-04 14:21:47', 'administrator', '1', NULL, NULL, NULL, 1),(8, '保存', 'save', NULL, 3, NULL, 'layui-btn-sm', 2, 4, 0, '2020-11-04 14:23:00', 'administrator', '1', NULL, NULL, NULL, 0),(9, '删除', 'del', 'layui-icon-close', 3, NULL, 'layui-btn-sm', 1, 3, 0, '2020-11-04 15:27:51', 'administrator', '1', NULL, NULL, NULL, 0),(14, '重置密码', 'restPwd', 'layui-icon-set-sm', 2, NULL, 'layui-btn-sm', 1, 4, 0, '2020-11-04 16:15:25', 'administrator', '1', NULL, NULL, NULL, 1),(15, '新增', 'add', 'layui-icon-add-1', 7, NULL, 'layui-btn-sm', 1, 1, 0, '2020-11-11 15:17:26', 'administrator', '1', NULL, NULL, NULL, 1),(16, '修改', 'edit', 'layui-icon-edit', 7, NULL, 'layui-btn-sm', 1, 2, 0, '2020-11-11 15:17:46', 'administrator', '1', NULL, NULL, NULL, 1),(17, '删除', 'del', 'layui-icon-close', 7, NULL, 'layui-btn-sm', 1, 3, 0, '2020-11-12 15:44:28', 'administrator', '1', NULL, NULL, NULL, 1),(18, '新增', 'add', 'layui-icon-add-1', 7, NULL, 'layui-btn-sm', 2, 4, 0, '2020-11-12 15:49:15', 'administrator', '1', NULL, NULL, NULL, 1),(19, '修改', 'edit', 'layui-icon-edit', 7, NULL, 'layui-btn-sm', 2, 5, 0, '2020-11-12 15:49:32', 'administrator', '1', NULL, NULL, NULL, 1),(20, '删除', 'del', 'layui-icon-close', 7, NULL, 'layui-btn-sm', 2, 6, 0, '2020-11-12 15:49:48', 'administrator', '1', NULL, NULL, NULL, 1),(21, '新增', 'add', 'layui-icon-add-1', 12, NULL, 'layui-btn-sm', 1, 1, 0, '2020-11-23 11:36:31', 'administrator', '1', NULL, NULL, NULL, 1),(22, '修改', 'edit', 'layui-icon-edit', 12, NULL, 'layui-btn-xs', 2, 2, 0, NULL, NULL, NULL, '2020-11-24 11:00:53', 'administrator', '1', 1),(23, '删除', 'del', 'layui-icon-close', 12, 'layui-btn-warm', 'layui-btn-xs', 2, 3, 0, NULL, NULL, NULL, '2020-11-24 11:14:44', 'administrator', '1', 1),(26, '添加', 'add', 'layui-icon-add-1', 13, NULL, 'layui-btn-sm', 1, 1, 0, '2020-11-24 09:36:29', 'administrator', '1', NULL, NULL, NULL, 1),(27, '修改', 'edit', 'layui-icon-edit', 13, NULL, 'layui-btn-sm', 1, 2, 0, '2020-11-24 09:36:43', 'administrator', '1', NULL, NULL, NULL, 1),(28, '删除', 'del', 'layui-icon-close', 13, NULL, 'layui-btn-sm', 1, 3, 0, '2020-11-24 09:36:58', 'administrator', '1', NULL, NULL, NULL, 1),(31, '保存', 'formSubmit', 'layui-icon-list', 11, NULL, 'layui-btn-sm', 1, 0, 0, NULL, NULL, NULL, '2020-12-31 17:28:38', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', 1);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_config` WRITE;
DELETE FROM `blog`.`sys_config`;
INSERT INTO `blog`.`sys_config` (`config_code`,`config_content`) VALUES ('baseConfig', '{\"SysName\":\"草堂笔记12\",\"SEOTitle\":\"草堂笔记技术文章分享\",\"Keywords\":\".NET Core,.NET开发,.NET Core开发,WebAPI,SqSugar,MVC,Redis,ORM,Web前端,分布式,微服务\",\"Description\":\".NET开发、.NET Core开发技术、WebAPI、SqSugar、MVC、Redis、ORM、Web前端、微服务、软件设计、数据库技术、操作系统等技术/文章分享。\",\"AppId\":\"草堂笔记\",\"AppSecret\":null,\"WxNotifyUrl\":\"http://loc\",\"MchId\":\"123\",\"PayKey\":\"123\"}');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_dictionary` WRITE;
DELETE FROM `blog`.`sys_dictionary`;
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_login_log` WRITE;
DELETE FROM `blog`.`sys_login_log`;
INSERT INTO `blog`.`sys_login_log` (`Id`,`action_type`,`operator`,`exec_ip`,`exec_time`,`exec_content`,`exec_result`,`remark`) VALUES (1, 1, 'admin', '::1', '2021-01-19 10:42:43', '', '登录成功', NULL),(2, 2, 'admin', '127.0.0.1', '2021-01-19 10:45:40', '退出账号：admin； ', '退出成功', NULL),(3, 1, 'administrator', '::1', '2021-01-19 10:45:48', '', '登录成功', NULL),(4, 1, 'admin', '::1', '2021-01-19 10:46:12', '', '登录成功', NULL),(5, 1, 'admin', '127.0.0.1', '2021-01-19 10:59:17', '', '登录成功', NULL),(6, 1, 'admin', '127.0.0.1', '2021-01-19 11:32:55', '', '登录成功', NULL),(7, 1, 'admin', '::1', '2021-01-19 11:36:48', '', '登录成功', NULL),(8, 1, 'administrator', '::1', '2021-01-19 11:37:23', '', '登录成功', NULL),(9, 1, 'administrator', '::1', '2021-01-19 14:25:27', '', '登录成功', NULL),(10, 1, 'administrator', '::1', '2021-01-19 15:15:20', '', '登录成功', NULL),(11, 1, 'administrator', '127.0.0.1', '2021-01-19 15:19:00', '', '登录成功', NULL),(12, 1, 'administrator', '127.0.0.1', '2021-01-19 15:34:06', '', '登录成功', NULL),(13, 1, 'admin', '::1', '2021-01-20 11:06:27', '', '登录成功', NULL),(14, 1, 'admin', '::1', '2021-01-20 15:44:59', '', '登录成功', NULL),(15, 1, 'admin', '127.0.0.1', '2021-01-21 10:43:28', '', '登录成功', NULL),(16, 1, 'admin', '127.0.0.1', '2021-03-18 11:33:41', '', '登录失败；失败原因：用户名或密码错误', NULL),(17, 1, 'admin', '127.0.0.1', '2021-03-18 11:36:35', '', '登录失败；失败原因：用户名或密码错误', NULL),(18, 1, 'administrator', '127.0.0.1', '2021-03-18 11:37:17', '', '登录成功', NULL),(19, 1, 'admin', '127.0.0.1', '2021-03-24 14:36:10', '', '登录失败；失败原因：用户名或密码错误', NULL),(20, 1, 'admin', '127.0.0.1', '2021-03-24 14:36:17', '', '登录失败；失败原因：用户名或密码错误', NULL),(21, 1, 'admin', '127.0.0.1', '2021-03-24 14:37:31', '', '登录失败；失败原因：用户名或密码错误', NULL),(22, 1, 'administrator', '127.0.0.1', '2021-03-24 14:38:06', '', '登录成功', NULL),(23, 1, 'administrator', '127.0.0.1', '2021-04-20 09:53:03', '', '登录失败；失败原因：用户名或密码错误', NULL),(24, 1, 'admin', '127.0.0.1', '2021-04-20 09:53:20', '', '登录失败；失败原因：输入的验证码错误', NULL),(25, 1, 'admin', '127.0.0.1', '2021-04-20 09:53:32', '', '登录失败；失败原因：用户名或密码错误', NULL),(26, 1, 'admin', '127.0.0.1', '2021-04-20 09:53:43', '', '登录失败；失败原因：输入的验证码错误', NULL),(27, 1, 'admin', '127.0.0.1', '2021-04-20 09:53:52', '', '登录失败；失败原因：用户名或密码错误', NULL),(28, 1, 'administrator', '127.0.0.1', '2021-04-20 09:55:14', '', '登录失败；失败原因：输入的验证码错误', NULL),(29, 1, 'administrator', '127.0.0.1', '2021-04-20 09:55:21', '', '登录成功', NULL),(30, 2, 'administrator', '127.0.0.1', '2021-04-20 09:58:36', '退出账号：administrator； ', '退出成功', NULL),(31, 1, 'administrator', '127.0.0.1', '2021-04-20 09:58:41', '', '登录成功', NULL);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_menu` WRITE;
DELETE FROM `blog`.`sys_menu`;
INSERT INTO `blog`.`sys_menu` (`Id`,`menu_name`,`menu_code`,`menu_icon`,`page_title`,`menu_url`,`parent_id`,`parent_code`,`enabled`,`level`,`sort`,`status`,`create_time`,`creator`,`create_guid`,`modify_time`,`modifier`,`modify_guid`) VALUES (1, '系统管理', 0, 'layui-icon-set', NULL, NULL, -1, 0, b'1', 0, 99, 0, NULL, NULL, NULL, '2020-11-11 15:42:57', 'administrator', '1'),(2, '用户管理', 100101, NULL, NULL, 'SysUser/Index', 1, 1001, b'1', 2, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL),(3, '角色管理', 100102, NULL, NULL, 'SysRole/Index', 1, 1001, b'1', 2, 2, 0, NULL, NULL, NULL, NULL, NULL, NULL),(4, '菜单管理', 100103, NULL, NULL, 'SysMenu/Index', 1, 1001, b'1', 2, 3, 0, NULL, NULL, NULL, NULL, NULL, NULL),(5, '字典管理', 0, NULL, NULL, NULL, 1, 0, b'1', 0, 0, 1, '2020-11-03 13:59:22', 'administrator', '1', '2020-11-04 15:22:52', 'administrator', '1'),(6, '字典管理', 0, NULL, NULL, NULL, 1, 0, b'1', 0, 0, 1, '2020-11-03 13:59:39', 'administrator', '1', '2020-11-04 15:22:55', 'administrator', '1'),(7, '字典管理', 0, NULL, NULL, 'SysDictionary/Index', 1, 0, b'1', 0, 4, 0, NULL, NULL, NULL, '2020-11-11 15:16:44', 'administrator', '1'),(8, '234', 0, 'layui-icon-female', NULL, '124', 1, 0, b'1', 0, 0, 1, '2020-11-03 15:38:58', 'administrator', '1', '2020-11-03 15:41:33', 'administrator', '1'),(9, '324', 0, NULL, NULL, '234', 1, 0, b'1', 0, 0, 1, '2020-11-03 15:39:53', 'administrator', '1', '2020-11-04 14:09:18', 'administrator', '1'),(10, '文章管理', 0, 'layui-icon-read', NULL, NULL, -1, 0, b'1', 0, 2, 0, '2020-11-04 16:17:02', 'administrator', '1', '2020-11-19 15:41:39', 'administrator', '1'),(11, '系统配置', 0, NULL, NULL, 'SysConfig/Index', 1, 0, b'1', 0, 5, 0, '2020-11-20 14:35:42', 'administrator', '1', NULL, NULL, NULL),(12, '分类管理', 0, NULL, NULL, 'ArticleCategory/Index', 10, 0, b'1', 0, 1, 0, '2020-11-20 16:10:10', 'administrator', '1', NULL, NULL, NULL),(13, '文章管理', 0, NULL, NULL, 'Article/Index', 10, 0, b'1', 0, 2, 0, '2020-11-20 16:10:49', 'administrator', '1', NULL, NULL, NULL),(14, '登录日志', 0, NULL, NULL, 'SysLoginLog/Index', 1, 0, b'1', 0, 6, 0, '2021-01-19 10:45:23', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1', '2021-01-19 11:37:14', 'admin', '20201028-1443-0198-b1b4-25c7db05e6c1');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_role` WRITE;
DELETE FROM `blog`.`sys_role`;
INSERT INTO `blog`.`sys_role` (`Id`,`role_name`,`role_code`,`role_desc`,`sort`,`enabled`,`status`,`create_time`,`creator`,`create_guid`,`modify_time`,`modifier`,`modify_guid`) VALUES (1, '管理员', 'admin', NULL, 1, b'1', 0, '2020-09-22 16:01:12', '管理员', '1', NULL, NULL, NULL),(2, '资料录入员', 'test01', NULL, 0, b'1', 0, '2020-09-22 16:01:16', '管理员', '1', NULL, NULL, NULL);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_role_power` WRITE;
DELETE FROM `blog`.`sys_role_power`;
INSERT INTO `blog`.`sys_role_power` (`power_id`,`role_id`,`power_type`) VALUES (10, 1, 1),(12, 1, 1),(13, 1, 1),(1, 1, 1),(2, 1, 1),(3, 1, 1),(4, 1, 1),(7, 1, 1),(11, 1, 1),(14, 1, 1),(21, 1, 2),(22, 1, 2),(23, 1, 2),(26, 1, 2),(27, 1, 2),(28, 1, 2),(1, 1, 2),(2, 1, 2),(3, 1, 2),(14, 1, 2),(4, 1, 2),(7, 1, 2),(9, 1, 2),(5, 1, 2),(15, 1, 2),(16, 1, 2),(17, 1, 2),(18, 1, 2),(19, 1, 2),(20, 1, 2),(31, 1, 2);
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_user` WRITE;
DELETE FROM `blog`.`sys_user`;
INSERT INTO `blog`.`sys_user` (`guid`,`user_name`,`user_pwd`,`salt_value`,`true_name`,`gender`,`head_img`,`status`,`create_time`,`creator`,`create_guid`,`modify_time`,`modifier`,`modify_guid`,`enabled`) VALUES ('1', 'administrator', '0fdd1a4369935bad246a88f30e822e4c33610151', '20201112-0910-086e-b49a-0b1fbf3a850e', '张三', 0, '', b'1', '2020-09-21 17:27:51', NULL, NULL, '2020-11-12 08:58:30', 'administrator', '1', b'0'),('20201028-1443-0198-b1b4-25c7db05e6c1', 'admin', 'd2503a5ae01e2c25e016652e5fa93714d37443c0', '20201229-1153-06da-883c-6521c1b65018', '管理员1', 0, NULL, b'0', '2020-10-28 14:43:01', 'administrator', '1', '2020-11-11 11:14:29', 'administrator', '1', b'0');
UNLOCK TABLES;
COMMIT;
BEGIN;
LOCK TABLES `blog`.`sys_user_role` WRITE;
DELETE FROM `blog`.`sys_user_role`;
INSERT INTO `blog`.`sys_user_role` (`user_guid`,`role_id`) VALUES ('20201028-1443-0198-b1b4-25c7db05e6c1', 1),('20201030-1043-1843-9eba-66489eacb5e4', 1),('20201112-0853-06f9-ac83-16fd430c4380', 1),('20201104-2256-317d-b243-7c292b630682', 2),('20201113-0909-31df-9d63-56c645b3e746', 1),('20201113-0910-33bf-98bc-09182324e374', 1),('20201113-0911-58d4-bf62-b888d532944f', 1),('20201113-0919-2726-8933-522b727c6499', 1),('20201113-0920-04cb-be1c-4c35f223c452', 1),('20201113-0922-3682-a065-e2f624ef139a', 1),('20201113-0923-1616-aeb4-264307ef61b0', 1),('20201113-0924-20ce-9709-f348b381a3eb', 1),('20201113-0930-4930-98b1-3c7b107d923f', 1),('20201113-0931-48ef-9040-e6a82b7b2d38', 1),('20201113-1002-1634-b79e-b66b0dfb9925', 1),('20201113-1019-57d6-8e53-502eaa3f7a37', 1),('20201113-1022-2879-b301-029dc15759ca', 1),('20201113-1026-55ef-b866-0270d65691ff', 1),('20201113-1027-173f-bb85-bab87931e479', 1),('20201113-1043-5846-9cd8-e51d0e946477', 1),('20201113-1050-1295-880e-ffa3b1d54aa9', 1),('20201113-1050-49d1-95ea-f50209141610', 1),('20201113-1100-32f4-8bdc-fb09bc1c1920', 1),('20201113-1102-12cf-8841-8daf4be42c55', 1);
UNLOCK TABLES;
COMMIT;
