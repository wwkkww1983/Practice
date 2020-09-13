USE [caist_mk_db]
GO

/****** Object:  Table [dbo].[mk_led_device]    Script Date: 2020/8/20 10:37:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[mk_opc_power](
	[id] [bigint] NOT NULL,
	Device_Name nvarchar(50) not null,
	Device_Host nvarchar(50) not null,
	[Device_Port]nvarchar(50) not null,
	[PLCType]nvarchar(50) not null,
	[Slot_No]nvarchar(50) not null,
	[Local]nvarchar(50) not null,
	[Remote]nvarchar(50) not null,
	[tab_status] int not null,
	[base_is_delete] [int] NOT NULL,
	[base_create_time] [smalldatetime] NOT NULL,
	[base_modify_time] [smalldatetime] NOT NULL,
	[base_creator_id] [bigint] NOT NULL,
	[base_modifier_id] [bigint] NOT NULL,
	[base_version] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
