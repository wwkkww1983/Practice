USE [ICLDB]
GO

/****** Object:  Table [dbo].[T_Point]    Script Date: 12/06/2019 10:02:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_Point](
	[ID] [uniqueidentifier] NULL,
	[Codes] [int] NULL,
	[ProjectID] [uniqueidentifier] NULL,
	[SectionID] [uniqueidentifier] NULL,
	[PointName] [nvarchar](50) NULL,
	[JoinPointName] [nvarchar](50) NULL,
	[PointFeatures] [nvarchar](50) NULL,
	[Appendix] [nvarchar](50) NULL,
	[Xvalues] [nvarchar](50) NULL,
	[Yvaules] [nvarchar](50) NULL,
	[Zvaules] [nvarchar](50) NULL,
	[Materail] [nvarchar](50) NULL
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'ProjectID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区段ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'SectionID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'PointName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连接点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'JoinPointName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点特征' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'PointFeatures'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'附属物' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'Appendix'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'材质' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Point', @level2type=N'COLUMN',@level2name=N'Materail'
GO

ALTER TABLE [dbo].[T_Point] ADD  CONSTRAINT [DF_T_Point_ID]  DEFAULT (newid()) FOR [ID]
GO

