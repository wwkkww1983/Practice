USE [ICLDB]
GO

/****** Object:  Table [dbo].[T_ShouLiFenXi]    Script Date: 12/06/2019 10:02:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_ShouLiFenXi](
	[ID] [uniqueidentifier] NOT NULL,
	[codes] [int] NULL,
	[Section] [nvarchar](50) NULL,
	[BuryType] [nvarchar](50) NULL,
	[IfQualify] [int] NULL,
	[ProposedProg] [nvarchar](50) NULL,
	[Material] [nvarchar](50) NULL,
	[Lengths] [nvarchar](50) NULL,
	[StartX] [nvarchar](50) NULL,
	[StartY] [nvarchar](50) NULL,
	[StartZ] [nvarchar](50) NULL,
	[EndX] [nvarchar](50) NULL,
	[EndY] [nvarchar](50) NULL,
	[EndZ] [nvarchar](50) NULL,
	[ForceValue1] [nvarchar](50) NULL,
	[ForceValue2] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_T_ShouLiFenXi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_ShouLiFenXi', @level2type=N'COLUMN',@level2name=N'Section'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'敷设方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_ShouLiFenXi', @level2type=N'COLUMN',@level2name=N'BuryType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否合格1合格0不合格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_ShouLiFenXi', @level2type=N'COLUMN',@level2name=N'IfQualify'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建议方案' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_ShouLiFenXi', @level2type=N'COLUMN',@level2name=N'ProposedProg'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'材质' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_ShouLiFenXi', @level2type=N'COLUMN',@level2name=N'Material'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_ShouLiFenXi', @level2type=N'COLUMN',@level2name=N'Lengths'
GO

ALTER TABLE [dbo].[T_ShouLiFenXi] ADD  CONSTRAINT [DF_T_ShouLiFenXi_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[T_ShouLiFenXi] ADD  CONSTRAINT [DF_T_ShouLiFenXi_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO

