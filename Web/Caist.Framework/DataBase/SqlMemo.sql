
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
