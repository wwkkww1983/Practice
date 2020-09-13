CREATE TABLE [dbo].[mk_Event_Record](
	[id] [bigint] NOT NULL,
	[instruct_name] [nvarchar](50) NULL,
	[instruct_value] [nvarchar](50) NULL,
	[Operator] [nvarchar](50) NULL,
	[Operator_time] [datetime] NULL,
	[Operation_model] [nvarchar](50) NULL,
	[base_is_delete] [int] NULL,
	[base_create_time] [smalldatetime] NULL,
	[base_modify_time] [smalldatetime] NULL,
	[base_creator_id] [bigint] NULL,
	[base_modifier_id] [bigint] NULL,
	[base_version] [int] NULL,
 CONSTRAINT [PK_mk_Event_Record] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/*2020-08-27
Ìí¼ÓÏµÍ³ID×Ö¶Î
*/
alter table [mk_Event_Record] add System_Id bigint null
