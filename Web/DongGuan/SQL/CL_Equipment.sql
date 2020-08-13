USE [ICLDB]
GO

/****** Object:  Table [dbo].[CL_Equipment]    Script Date: 12/06/2019 10:08:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CL_Equipment](
	[Id] [varchar](40) NOT NULL,
	[Name] [varchar](200) NULL,
	[EquipmentTypeId] [varchar](32) NULL,
	[ModelNumber] [varchar](200) NULL,
	[ECode] [varchar](50) NULL,
	[BarCode] [varchar](200) NULL,
	[Weight] [decimal](8, 2) NULL,
	[Units] [nvarchar](50) NULL,
	[USEINFO] [text] NULL,
	[State] [int] NULL,
	[Enabled] [int] NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL,
 CONSTRAINT [PK_CL_EQUIPMENT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'EquipmentTypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'型号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'ModelNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'ECode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'BarCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'Weight'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用途' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'USEINFO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备使用状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'Enabled'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CL_Equipment', @level2type=N'COLUMN',@level2name=N'Delteted'
GO

