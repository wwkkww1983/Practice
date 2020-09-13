USE [ICLDB]
GO

/****** Object:  Table [dbo].[CL_Section]    Script Date: 12/06/2019 10:18:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CL_Section](
	[Id] [dbo].[Id] NOT NULL,
	[SectionCode] [varchar](32) NULL,
	[ProjectId] [dbo].[Id] NULL,
	[Distance] [decimal](8, 2) NULL,
	[Location] [varchar](500) NULL,
	[State] [int] NULL,
	[DefaultEquipmentDistance] [decimal](8, 2) NULL,
	[WarningSMS] [int] NULL,
	[NowSection] [int] NULL,
	[Phones] [varchar](200) NULL,
	[SysCompanyId] [dbo].[Id] NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[asjcode] [varchar](50) NULL,
	[Sazcod] [varchar](50) NULL,
	[Asdcod] [varchar](50) NULL,
	[Delteted] [bit] NULL,
 CONSTRAINT [PK_CL_SECTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'SectionCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'ProjectId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'Distance'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地理位置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'Location'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备间默认距离' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'DefaultEquipmentDistance'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用预警短信' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'WarningSMS'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收短信手机号列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'Phones'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Section', @level2type=N'COLUMN',@level2name=N'Delteted'
GO

