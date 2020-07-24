USE [ICLDB]
GO

INSERT INTO [dbo].[Basic_Locus]
           ([Id],[SectionId],[Point_X],[Point_Y],[CreateId],[CreateUser],[CreateTime]
     VALUES
           (@Id,@SectionId,@Point_X,@Point_Y,@CreateId,@CreateUser,@CreateTime)
GO

