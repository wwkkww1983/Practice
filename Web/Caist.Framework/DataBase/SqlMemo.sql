
/*
* zhangmeiqing 2020-7-28 9:16
*/
USE [caist_mk_db]
GO

/****** Object:  Table [dbo].[mk_plc_cewen_values]    Script Date: 2020-07-28 9:13:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_plc_cewen_values](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[area_name] [nvarchar](50) NULL,
	[max_value] [nvarchar](50) NULL,
	[min_value] [nvarchar](50) NULL,
	[average_value] [nvarchar](50) NULL,
	[create_Time] [datetime] NOT NULL,
 CONSTRAINT [PK_mk_plc_cewen_values] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[mk_plc_cewen_values] ADD  CONSTRAINT [DF_mk_plc_cewen_values_create_Time]  DEFAULT (getdate()) FOR [create_Time]
GO



/*
luzhenjie 2020年7月28日
信息发布
*/

--矿山led设备管理
if exists(select * from sysobjects where name='mk_led_device')
	drop table mk_led_device;
go
create table mk_led_device(
	id bigint not null primary key,
	Device_Uid varchar(50) not null,
	Device_Name nvarchar(50) not null,
	Ip_Address nvarchar(20) not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)
--led信息发布数据表
if exists(select * from sysobjects where name='mk_information_publish')
	drop table mk_information_publish;
go
create table mk_information_publish(
	id bigint not null primary key,
	Device_Uid varchar(50) not null,
	Link_Content nvarchar(500),
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)


---控制面板中的某个属性是否显示在首页
alter table mk_view_manipulate_model add manipulate_model_show_home int not null default(0)

--增加人员定位绑定视图模块ID
alter table mk_region_pepole_count add View_Function_Id bigint not null default(0);

/*
zhangmeiqing 2020年8月12日
删掉无用的两张人员定位表
*/
drop table [dbo].[peoplePosition];
drop table [dbo].[人员定位];

/*
zhangmeiqing 2020年8月17日
修改人员定位表历史表
*/
USE [caist_mk_db]
GO
if exists(select * from sysobjects where name='mk_people_position')
	drop table mk_people_position;
go
/****** Object:  Table [dbo].[mk_people_position]    Script Date: 2020-08-17 10:28:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_people_position](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pepole_number] [int] NULL,
	[people_name] [nvarchar](50) NULL,
	[type_of_work_name] [nvarchar](50) NULL,
	[current_station] [nvarchar](50) NULL,
	[station_address] [nvarchar](100) NULL,
	[temp_field] [nvarchar](50) NULL,
	[report_time] [datetime] NULL,
 CONSTRAINT [PK_mk_people_position] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/*
zhangmeiqing 2020年8月17日
修改光纤测温历史表
*/
USE [caist_mk_db]
GO

if exists(select * from sysobjects where name ='mk_cable_thermometry')
	drop table mk_cable_thermometry;
go

/****** Object:  Table [dbo].[mk_cable_thermometry]    Script Date: 2020-08-17 8:07:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_cable_thermometry](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[area_name] [nvarchar](50) NULL,
	[max_value] [nvarchar](50) NULL,
	[min_value] [nvarchar](50) NULL,
	[average_value] [nvarchar](50) NULL,
	[create_time] [datetime] NULL,
 CONSTRAINT [PK_mk_cable_thermometry] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/*
zhangmeiqing 2020年8月17日
删除光纤测温和人员定位之前的历史表
*/
drop table [caist_mk_db].[dbo].[mk_plc_cewen_values];
drop table [caist_mk_db].[dbo].[mk_plc_renyuan_values];

/*
luzhenjie 202年8月17日
增加mqtt上传配置编码表
*/
if exists(select * from sysobjects where name='mk_mqtt_code_setting')
	drop table mk_mqtt_code_setting;
go
Create table mk_mqtt_code_setting
(
	id bigint not null primary key,
	--mk_system_setting表的id
	system_id bigint not null,
	--传感器ID
	sensor_id bigint not null,
	--点位数据类型  1：数据 2：报警
	code_type int not null,
	--点位名称
	code_name nvarchar(50) not null,
	--plc点表
	code_point_setting nvarchar(50) not null,
	--子系统编码
	system_code varchar(20) not null,
	--地址编码-采区编码
	address_are_code varchar(10) not null,
	--地址编码-类型编码
	address_type_code varchar(10) not null,
	--地址编码-设备安装点
	address_device_code varchar(10) not null,
	--设备顺序编码
	device_code varchar(10) not null,
	--值总占用字符长度
	value_length int not null,
	--值得小数位数
	decimal_places int not null,
	--报警类型关联点位，以逗号分隔
	alarm_point varchar(300) null,
	--传感器类型编码
	sensor_type_code varchar(10) not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)
--地址类型编码表   用于筛选mqtt传感器编码的地址编码中的地址类型编码选择
if exists(select * from sysobjects where name='mk_mqtt_address_type')
	drop table mk_mqtt_address_type;
go
create table mk_mqtt_address_type
(
	id int IDENTITY(1,1) not null primary key,
	system_code varchar(20) not null,
	name nvarchar(50) not null,
	code varchar(20) not null,
)


/*
增加光纤测温,人员定位实时表，以备后续扩展 zhangmq 2020-08-19 14:41 
*/
USE [caist_mk_db]
GO
if exists(select * from sysobjects where name='mk_cable_thermometry_realTime')
	drop table mk_cable_thermometry_realTime;
go

/****** Object:  Table [dbo].[mk_cable_thermometry_realTime]    Script Date: 2020-08-19 2:35:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_cable_thermometry_realTime](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[area_name] [nvarchar](50) NULL,
	[max_value] [nvarchar](50) NULL,
	[min_value] [nvarchar](50) NULL,
	[average_value] [nvarchar](50) NULL
 CONSTRAINT [PK_mk_cable_thermometry_realTime] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [caist_mk_db]
GO
if exists(select * from sysobjects where name='mk_people_position_realTime')
	drop table mk_people_position_realTime;
go

/****** Object:  Table [dbo].[mk_people_position_realTime]    Script Date: 2020-08-19 2:38:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_people_position_realTime](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pepole_number] [int] NULL,
	[people_name] [nvarchar](50) NULL,
	[type_of_work_name] [nvarchar](50) NULL,
	[current_station] [nvarchar](50) NULL,
	[station_address] [nvarchar](100) NULL,
	[temp_field] [nvarchar](50) NULL
 CONSTRAINT [PK_mk_people_position_realTime] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



/*
2020-08-28  luzhenjie
增加系统表是否显示在一级菜单的   1显示 0不显示
*/
alter table mk_system_setting add  Menu_Show int not null  default(1)
/*
2020-08-29 luzhenjie
增加报警记录记录当前报警值
*/
alter table mk_alarm_record  add alarm_value varchar(50) null
/*增加控制类型*/
alter table mk_view_paramenter  add control_models varchar(50) null

/*
2020-08-29  zhangmq
plc历史记录表增加命令控制类别(0:数据;1:控制;2:告警;3:启动;4:停止)
*/
ALTER TABLE [dbo].[mk_plc_choucai_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_jushan_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_pidai_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_shuibeng_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_tongfeng_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_yafeng_values] ADD Instruct_type INT NULL;

/*
2020-08-31  luzhenjie
创建传感器编码配置表
*/
if exists(select * from sysobjects where name='mk_mqtt_sensor_setting')
	drop table mk_mqtt_sensor_setting;
go
create table mk_mqtt_sensor_setting
(
	id bigint not null primary key,
	system_code varchar(20) not null,
	code_type int not null,
	name nvarchar(50) not null,
	code varchar(20) not null,
	value_length int not null,
	decimal_places int not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)

/*
2020-08-31  zhangmq
创建安全监控实时表
*/
USE [caist_mk_db]
GO
if exists(select * from sysobjects where name='security_monitor_ingreal_time_data_realTime')
	drop table security_monitor_ingreal_time_data_realTime;
go
/****** Object:  Table [dbo].[security_monitor_ingreal_time_data_realTime]    Script Date: 2020/8/31 19:03:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[security_monitor_ingreal_time_data_realTime](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[point] [varchar](64) NOT NULL,
	[Address] [varchar](64) NULL,
	[lc] [varchar](255) NULL,
	[lx] [varchar](64) NULL,
	[dw] [varchar](64) NULL,
	[ssz] [varchar](64) NULL,
	[Stat_Name] [varchar](64) NULL,
 CONSTRAINT [PK_security_monitor_ingreal_time_data_realTime] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




---视图模块是否显示在首页  luzhenjie  2020-09-06  默认显示首页1 
alter table mk_view_function add view_function_show_home int not null default(1)

--增加mqtt最后上传时间记录上一次上传数据时间点  luzhejjie  2020-09-06
alter table mk_mq_theme add last_update_time datetime null


/*
2020-08-31  zhangmq
修改电力表
*/
USE [caist_mk_db]
GO
if	exists(select * from sysobjects where name='mk_opc_power')
	drop table mk_opc_power;
go

/****** Object:  Table [dbo].[mk_opc_power]    Script Date: 2020/9/6 20:36:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_opc_power](
	[id] [bigint] NOT NULL,
	[Num] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Area] [nvarchar](50) NOT NULL,
	[Data_Type] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[tab_status] [int] NOT NULL,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
 CONSTRAINT [PK__mk_opc_p__3213E83F69FBBC1F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*
2020-9-10  zhangmq
人员定位增加工时,职务，职位列
*/
ALTER TABLE [dbo].[mk_people_position] ADD duty varchar(50) NULL;
ALTER TABLE [dbo].[mk_people_position] ADD post varchar(50) NULL;
ALTER TABLE [dbo].[mk_people_position] ADD in_mine_time datetime NULL;
ALTER TABLE [dbo].[mk_people_position] ADD out_mine_time datetime NULL;


/*
2020-09-07  luzhenjie
增加人员定位标记坐标存储表
*/
if exists(select * from sysobjects where name='mk_people_position_mark')
	drop table mk_people_position_mark;
go
create table mk_people_position_mark
(
	id bigint not null primary key,
	i varchar(20) not null,
	x varchar(10) not null,
	y varchar(10) not null,
	w varchar(10) not null,
	h varchar(10) not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)
/*
增加光纤测温标记坐标存储表
*/
if exists(select * from sysobjects where name='mk_fiber_mark')
	drop table mk_fiber_mark;
go
create table mk_fiber_mark
(
	id bigint not null primary key,
	i varchar(20) not null,
	x varchar(10) not null,
	y varchar(10) not null,
	w varchar(10) not null,
	h varchar(10) not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)
--增加动画参数控制字段
alter table mk_view_paramenter add  Animation varchar(20) null

--添加系统设备指令 用于判断是否需要获取当前系统设备链接状态
alter table mk_system_setting add device_instruction varchar(50) null

--添加MQTT上传用得通道编号
alter table mk_eb_video add channel_number varchar(50) null


/*
2020-09-07  zhangmeiqing
增加供配电实时表
*/
USE [caist_mk_db]
GO
if exists(select * from sysobjects where name='mk_substation_realTime')
	drop table mk_substation_realTime;
go
/****** Object:  Table [dbo].[mk_substation_realTime]    Script Date: 2020/9/19 18:23:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[mk_substation_realTime](
	[Id] [int] NOT NULL,
	[Sys_Id] [int] NOT NULL,
	[Dian_Wei] [varchar](32) NULL,
	[F] [varchar](16) NULL,
	[IA] [varchar](16) NULL,
	[P] [varchar](16) NULL,
	[Q] [varchar](16) NULL,
	[COS] [varchar](16) NULL
) ON [PRIMARY]
GO


---供配电历史表增加创建时间  zhangmeiqing  2020-09-19 
alter table mk_substation add create_Time datetime not null default(getdate());