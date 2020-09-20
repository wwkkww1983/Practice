
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
luzhenjie 2020��7��28��
��Ϣ����
*/

--��ɽled�豸����
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
--led��Ϣ�������ݱ�
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


---��������е�ĳ�������Ƿ���ʾ����ҳ
alter table mk_view_manipulate_model add manipulate_model_show_home int not null default(0)

--������Ա��λ����ͼģ��ID
alter table mk_region_pepole_count add View_Function_Id bigint not null default(0);

/*
zhangmeiqing 2020��8��12��
ɾ�����õ�������Ա��λ��
*/
drop table [dbo].[peoplePosition];
drop table [dbo].[��Ա��λ];

/*
zhangmeiqing 2020��8��17��
�޸���Ա��λ����ʷ��
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
zhangmeiqing 2020��8��17��
�޸Ĺ��˲�����ʷ��
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
zhangmeiqing 2020��8��17��
ɾ�����˲��º���Ա��λ֮ǰ����ʷ��
*/
drop table [caist_mk_db].[dbo].[mk_plc_cewen_values];
drop table [caist_mk_db].[dbo].[mk_plc_renyuan_values];

/*
luzhenjie 202��8��17��
����mqtt�ϴ����ñ����
*/
if exists(select * from sysobjects where name='mk_mqtt_code_setting')
	drop table mk_mqtt_code_setting;
go
Create table mk_mqtt_code_setting
(
	id bigint not null primary key,
	--mk_system_setting���id
	system_id bigint not null,
	--������ID
	sensor_id bigint not null,
	--��λ��������  1������ 2������
	code_type int not null,
	--��λ����
	code_name nvarchar(50) not null,
	--plc���
	code_point_setting nvarchar(50) not null,
	--��ϵͳ����
	system_code varchar(20) not null,
	--��ַ����-��������
	address_are_code varchar(10) not null,
	--��ַ����-���ͱ���
	address_type_code varchar(10) not null,
	--��ַ����-�豸��װ��
	address_device_code varchar(10) not null,
	--�豸˳�����
	device_code varchar(10) not null,
	--ֵ��ռ���ַ�����
	value_length int not null,
	--ֵ��С��λ��
	decimal_places int not null,
	--�������͹�����λ���Զ��ŷָ�
	alarm_point varchar(300) null,
	--���������ͱ���
	sensor_type_code varchar(10) not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
)
--��ַ���ͱ����   ����ɸѡmqtt����������ĵ�ַ�����еĵ�ַ���ͱ���ѡ��
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
���ӹ��˲���,��Ա��λʵʱ���Ա�������չ zhangmq 2020-08-19 14:41 
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
����ϵͳ���Ƿ���ʾ��һ���˵���   1��ʾ 0����ʾ
*/
alter table mk_system_setting add  Menu_Show int not null  default(1)
/*
2020-08-29 luzhenjie
���ӱ�����¼��¼��ǰ����ֵ
*/
alter table mk_alarm_record  add alarm_value varchar(50) null
/*���ӿ�������*/
alter table mk_view_paramenter  add control_models varchar(50) null

/*
2020-08-29  zhangmq
plc��ʷ��¼����������������(0:����;1:����;2:�澯;3:����;4:ֹͣ)
*/
ALTER TABLE [dbo].[mk_plc_choucai_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_jushan_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_pidai_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_shuibeng_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_tongfeng_values] ADD Instruct_type INT NULL;
ALTER TABLE [dbo].[mk_plc_yafeng_values] ADD Instruct_type INT NULL;

/*
2020-08-31  luzhenjie
�����������������ñ�
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
������ȫ���ʵʱ��
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




---��ͼģ���Ƿ���ʾ����ҳ  luzhenjie  2020-09-06  Ĭ����ʾ��ҳ1 
alter table mk_view_function add view_function_show_home int not null default(1)

--����mqtt����ϴ�ʱ���¼��һ���ϴ�����ʱ���  luzhejjie  2020-09-06
alter table mk_mq_theme add last_update_time datetime null


/*
2020-08-31  zhangmq
�޸ĵ�����
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
��Ա��λ���ӹ�ʱ,ְ��ְλ��
*/
ALTER TABLE [dbo].[mk_people_position] ADD duty varchar(50) NULL;
ALTER TABLE [dbo].[mk_people_position] ADD post varchar(50) NULL;
ALTER TABLE [dbo].[mk_people_position] ADD in_mine_time datetime NULL;
ALTER TABLE [dbo].[mk_people_position] ADD out_mine_time datetime NULL;


/*
2020-09-07  luzhenjie
������Ա��λ�������洢��
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
���ӹ��˲��±������洢��
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
--���Ӷ������������ֶ�
alter table mk_view_paramenter add  Animation varchar(20) null

--���ϵͳ�豸ָ�� �����ж��Ƿ���Ҫ��ȡ��ǰϵͳ�豸����״̬
alter table mk_system_setting add device_instruction varchar(50) null

--���MQTT�ϴ��õ�ͨ�����
alter table mk_eb_video add channel_number varchar(50) null


/*
2020-09-07  zhangmeiqing
���ӹ����ʵʱ��
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


---�������ʷ�����Ӵ���ʱ��  zhangmeiqing  2020-09-19 
alter table mk_substation add create_Time datetime not null default(getdate());