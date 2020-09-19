CREATE TABLE [dbo].[mk_setting_value](
	[id] [bigint] NOT NULL,
	[parameter_name] [nvarchar](50) NULL,
	[parameter_Ip] [nvarchar](50) NULL,
	[parameter_Port] [nvarchar](50) NULL,
	[parameter_plc_type] [nvarchar](50) NULL,
	[parameter_data_type] [nvarchar](50) NULL,
	[parameter_instructions] [nvarchar](50) NULL,
	[parameter_value] [nvarchar](50) NULL,
	[parameter_min_instructions] [nvarchar](50) NULL,
	[parameter_min_value] [nvarchar](50) NULL,
	[parameter_max_instructions] [nvarchar](50) NULL,
	[parameter_max_value] [nvarchar](50) NULL,
	[parameter_unit] [nvarchar](50) NULL,
	[parameter_setting_type] [nvarchar](50) NULL,
	[parameter_sort] [int] NULL,
	[base_is_delete] [int] NULL,
	[base_create_time] [smalldatetime] NULL,
	[base_modify_time] [smalldatetime] NULL,
	[base_creator_id] [bigint] NULL,
	[base_modifier_id] [bigint] NULL,
	[base_version] [int] NULL,
	[parameter_models] [nvarchar](50) NULL,
	[parameter_controls] [nvarchar](50) NULL,
	[parameter_type] [nvarchar](50) NULL,
 CONSTRAINT [PK_mk_setting_value] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]