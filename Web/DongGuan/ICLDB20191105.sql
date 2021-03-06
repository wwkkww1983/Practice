USE [master]
GO
/****** Object:  Database [ICLDB]    Script Date: 2019/11/5 16:11:46 ******/
CREATE DATABASE [ICLDB] ON  PRIMARY 
( NAME = N'ICLDB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\ICLDB.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ICLDB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\ICLDB.ldf' , SIZE = 5696KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ICLDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ICLDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ICLDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ICLDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ICLDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ICLDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ICLDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ICLDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ICLDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ICLDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ICLDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ICLDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ICLDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ICLDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ICLDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ICLDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ICLDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ICLDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ICLDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ICLDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ICLDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ICLDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ICLDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ICLDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ICLDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ICLDB] SET  MULTI_USER 
GO
ALTER DATABASE [ICLDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ICLDB] SET DB_CHAINING OFF 
GO
USE [ICLDB]
GO
/****** Object:  Default [Deleted_0]    Script Date: 2019/11/5 16:11:46 ******/
CREATE DEFAULT [dbo].[Deleted_0] 
AS
0
GO
/****** Object:  UserDefinedDataType [dbo].[codestr]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[codestr] FROM [varchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[Count]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[Count] FROM [numeric](10, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[DCode]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[DCode] FROM [varchar](40) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[decimal(20,6)]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[decimal(20,6)] FROM [decimal](20, 6) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[decimal(6,2)]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[decimal(6,2)] FROM [decimal](6, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[DItem]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[DItem] FROM [varchar](20) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[Id]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[Id] FROM [varchar](40) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[Name]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[Name] FROM [nvarchar](30) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[namestr]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[namestr] FROM [nvarchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[path]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[path] FROM [nvarchar](150) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[remark]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[remark] FROM [nvarchar](200) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[status]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[status] FROM [numeric](1, 0) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[string_150]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[string_150] FROM [nvarchar](150) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[string_30]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[string_30] FROM [nvarchar](30) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[string_max]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[string_max] FROM [nvarchar](2000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[YesOrNo]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[YesOrNo] FROM [bit] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[主键GUID]    Script Date: 2019/11/5 16:11:46 ******/
CREATE TYPE [dbo].[主键GUID] FROM [uniqueidentifier] NULL
GO
/****** Object:  Table [dbo].[Base_ParameterIndexTemplet]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Base_ParameterIndexTemplet](
	[Id] [varchar](40) NOT NULL,
	[IndexCode] [nvarchar](50) NULL,
	[IndexName] [nvarchar](50) NULL,
	[Unit] [varchar](40) NULL,
	[Min_Value] [numeric](8, 4) NULL,
	[Max_Value] [numeric](8, 4) NULL,
	[Default_Value] [decimal](8, 4) NULL,
	[Local_Var] [varchar](50) NULL,
	[Out_Var] [varchar](50) NULL,
	[Out_Var_Func] [varchar](60) NULL,
	[Expression] [varchar](100) NULL,
	[Display_Exp] [varchar](200) NULL,
	[Full_Out_Var_Func] [varchar](200) NULL,
	[Full_Out_Var] [varchar](200) NULL,
	[Full_Local_Var] [varchar](200) NULL,
	[Full_Expression] [varchar](200) NULL,
	[Full_Display_Exp] [varchar](200) NULL,
	[Exp_Desc] [varchar](200) NULL,
	[Frequence] [int] NULL,
	[IndexType] [int] NULL,
	[MFlag] [int] NULL,
	[Is_Clear_Zero] [varchar](50) NULL,
	[Is_Write_Back] [bit] NULL,
	[Order_No] [int] NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Basic_Locus]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Basic_Locus](
	[Id] [nvarchar](50) NULL,
	[SectionId] [nvarchar](50) NULL,
	[GisPoint] [nvarchar](50) NULL,
	[LinkPoint] [nvarchar](50) NULL,
	[PointFeature] [nvarchar](50) NULL,
	[Point_X] [decimal](12, 3) NULL,
	[Point_Y] [decimal](12, 3) NULL,
	[Point_Z] [decimal](12, 3) NULL,
	[GroundHeight] [decimal](12, 3) NULL,
	[LineHeight] [decimal](12, 3) NULL,
	[SectionSize] [decimal](12, 3) NULL,
	[Material] [nvarchar](50) NULL,
	[BuryType] [nvarchar](50) NULL,
	[remark] [nvarchar](max) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cable_Info]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cable_Info](
	[Id] [dbo].[Id] NOT NULL,
	[CLNumber] [varchar](50) NULL,
	[TotelLenth] [numeric](8, 2) NULL,
	[CurrentLenth] [numeric](8, 2) NULL,
	[Voltage_Type] [int] NULL,
	[Sheath_Type] [int] NULL,
	[Fsection] [decimal](8, 3) NULL,
	[Max_Traction] [decimal](8, 2) NULL,
	[Max_Lateral_Pressure] [decimal](8, 2) NULL,
	[UserStatus] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL,
	[SectionId] [nvarchar](40) NULL,
	[ProjectID] [nvarchar](40) NULL,
 CONSTRAINT [PK_Cable_Info_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dic_Content]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Content](
	[Id] [nvarchar](50) NULL,
	[DicType_ID] [nvarchar](50) NULL,
	[DicTypeContent] [nvarchar](50) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL,
	[Sort] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dic_Type]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Type](
	[Id] [nvarchar](50) NULL,
	[TypeName] [nvarchar](50) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[Id] [varchar](40) NOT NULL,
	[Name] [varchar](200) NULL,
	[EquipmentTypeId] [varchar](32) NULL,
	[ModelNumber] [varchar](200) NULL,
	[ECode] [varchar](50) NULL,
	[BarCode] [varchar](200) NULL,
	[Weight] [decimal](8, 2) NULL,
	[UseInfo] [text] NULL,
	[State] [int] NULL,
	[Enabled] [int] NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laying_Path]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laying_Path](
	[Id] [dbo].[Id] NOT NULL,
	[SName] [nvarchar](50) NULL,
	[ProjectId] [dbo].[Id] NULL,
	[CableType] [varchar](40) NULL,
	[flength] [numeric](6, 2) NULL,
	[StartAddress] [nvarchar](30) NULL,
	[StartPoint_X] [decimal](8, 4) NULL,
	[StartPoint_Y] [numeric](8, 2) NULL,
	[StartPoint_Z] [numeric](8, 4) NULL,
	[EndAddress] [dbo].[namestr] NULL,
	[EndPoint_X] [numeric](8, 4) NULL,
	[EndPoint_Y] [numeric](8, 4) NULL,
	[fremark] [nvarchar](200) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL,
	[EndPoint_Z] [numeric](8, 4) NULL,
	[Radius] [numeric](8, 4) NULL,
	[PullValue] [numeric](8, 4) NULL,
	[SideValue] [numeric](8, 4) NULL,
	[IsQualified] [nvarchar](50) NULL,
	[Solutions] [nvarchar](max) NULL,
	[SectionId] [nvarchar](40) NULL,
	[Laying_Type] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitoring_Data]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitoring_Data](
	[Id] [dbo].[Id] NOT NULL,
	[ftime] [datetime] NULL,
	[PIndexId] [dbo].[Id] NULL,
	[MonitorPointId] [dbo].[Id] NULL,
	[fdata] [decimal](12, 3) NULL,
	[fremark] [varchar](40) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorPoint]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorPoint](
	[Id] [dbo].[Id] NOT NULL,
	[ProjectId] [dbo].[Id] NULL,
	[SectionId] [dbo].[Id] NULL,
	[EquipmentId] [dbo].[Id] NULL,
	[TerminalId] [dbo].[Id] NULL,
	[ParameterId] [dbo].[Id] NULL,
	[Position] [dbo].[Id] NULL,
	[PositionX] [decimal](16, 4) NULL,
	[PositionY] [decimal](16, 4) NULL,
	[PositionZ] [decimal](16, 4) NULL,
	[PositionSort] [int] NULL,
	[Distance] [decimal](4, 2) NULL,
	[StopOrder] [nvarchar](40) NULL,
	[StartOrder] [nvarchar](40) NULL,
	[IsEncryption] [dbo].[Id] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL,
	[TargetValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_MONITORPOINT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project_Info]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project_Info](
	[Id] [dbo].[Id] NOT NULL,
	[ProjectCode] [varchar](50) NULL,
	[ProjectName] [varchar](40) NULL,
	[Location] [varchar](500) NULL,
	[Distance] [numeric](8, 2) NULL,
	[State] [int] NULL,
	[SectionCount] [int] NULL,
	[CircuitCount] [int] NULL,
	[ClectricityType] [varchar](40) NULL,
	[LayEnvironment] [varchar](40) NULL,
	[Temperature] [numeric](6, 2) NULL,
	[Humidity] [numeric](6, 2) NULL,
	[Voltage] [nvarchar](40) NULL,
	[CutSize] [decimal](8, 2) NULL,
	[BuildUnit] [nvarchar](40) NULL,
	[NowProject] [bit] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](30) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](30) NULL,
	[UpdateTime] [datetime] NULL,
	[Delteted] [bit] NULL,
	[DevUnit] [nvarchar](50) NULL,
	[DevContacts] [nvarchar](50) NULL,
	[DevPhone] [nvarchar](50) NULL,
	[BuildContacts] [nvarchar](50) NULL,
	[BuildPhone] [nvarchar](50) NULL,
	[SuperUnit] [nvarchar](50) NULL,
	[SuperContacts] [nvarchar](50) NULL,
	[SuperPhone] [nvarchar](50) NULL,
	[provinceName] [nvarchar](50) NULL,
	[cityName] [nvarchar](50) NULL,
	[districtName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role_Grant]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Grant](
	[Id] [nvarchar](50) NULL,
	[RoleId] [nvarchar](50) NULL,
	[Grant_Id] [nvarchar](50) NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Section]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
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
	[Delteted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sys_Menu_Grant]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Menu_Grant](
	[Id] [nvarchar](50) NULL,
	[Menu_Id] [nvarchar](50) NULL,
	[grant_query] [bit] NULL,
	[grant_create] [bit] NULL,
	[grant_edit] [bit] NULL,
	[grant_delete] [bit] NULL,
	[grant_print] [bit] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[RoleId] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_Menu]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Menu](
	[Id] [nvarchar](40) NULL,
	[Menu_Name] [nvarchar](50) NULL,
	[Menu_Url] [nvarchar](50) NULL,
	[Menu_Pid] [nvarchar](50) NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Menu_Icon] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_RightObject]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_RightObject](
	[Id] [dbo].[Id] NOT NULL,
	[ParentId] [dbo].[Id] NULL,
	[RightNo] [varchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[RightText] [nvarchar](200) NULL,
	[IsVisible] [bit] NULL,
	[MenumOrPower] [int] NULL,
	[Ico] [image] NULL,
	[SortNumber] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ChangedUser] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[DeleteFlag] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_Role]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Role](
	[Id] [dbo].[Id] NOT NULL,
	[RoleCode] [varchar](50) NULL,
	[RoleName] [dbo].[Id] NULL,
	[Remark] [nvarchar](200) NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ChangedUser] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[DeleteFlag] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_Role_Right]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Role_Right](
	[Id] [dbo].[Id] NOT NULL,
	[RightId] [varchar](40) NULL,
	[ObjectCode] [varchar](40) NULL,
	[RoleId] [dbo].[Id] NULL,
	[RoleName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_UserInfo]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_UserInfo](
	[Id] [dbo].[Id] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[TrueName] [nvarchar](50) NULL,
	[UserType] [int] NULL,
	[Phone] [varchar](16) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Inviter] [dbo].[Id] NULL,
	[InviteCode] [varchar](50) NULL,
	[PictureIco] [image] NULL,
	[CreateId] [dbo].[Id] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ChangedUser] [dbo].[Id] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[DeleteFlag] [bit] NULL,
 CONSTRAINT [PK_SYSTEM_USERINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[System_UserOfRoles]    Script Date: 2019/11/5 16:11:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_UserOfRoles](
	[Id] [dbo].[Id] NOT NULL,
	[UserId] [dbo].[Id] NULL,
	[RoleId] [dbo].[Id] NULL,
 CONSTRAINT [PK_SYSTEM_USEROFROLES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Base_ParameterIndexTemplet] ([Id], [IndexCode], [IndexName], [Unit], [Min_Value], [Max_Value], [Default_Value], [Local_Var], [Out_Var], [Out_Var_Func], [Expression], [Display_Exp], [Full_Out_Var_Func], [Full_Out_Var], [Full_Local_Var], [Full_Expression], [Full_Display_Exp], [Exp_Desc], [Frequence], [IndexType], [MFlag], [Is_Clear_Zero], [Is_Write_Back], [Order_No], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'1', N'01', N'工井', N'隧道', CAST(1.0000 AS Numeric(8, 4)), CAST(3000.0000 AS Numeric(8, 4)), CAST(1.0000 AS Decimal(8, 4)), N'1', N'2', N'33', N'T1 + W * L1', N'T1 + W * L1', N'T1 + W * L1', N'T1 + W * L1', N'T1 + W * L1', N'T1 + W * L1', N'T1 + W * L1', N'成功', 60, 1, 2, N'0', 0, 1, N'李四', N'pass', CAST(N'2015-06-08T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-27T10:47:13.580' AS DateTime), NULL)
INSERT [dbo].[Base_ParameterIndexTemplet] ([Id], [IndexCode], [IndexName], [Unit], [Min_Value], [Max_Value], [Default_Value], [Local_Var], [Out_Var], [Out_Var_Func], [Expression], [Display_Exp], [Full_Out_Var_Func], [Full_Out_Var], [Full_Local_Var], [Full_Expression], [Full_Display_Exp], [Exp_Desc], [Frequence], [IndexType], [MFlag], [Is_Clear_Zero], [Is_Write_Back], [Order_No], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'2', N'02', N'雨水井', N'隧道', CAST(2.0000 AS Numeric(8, 4)), CAST(3.0000 AS Numeric(8, 4)), CAST(4.0000 AS Decimal(8, 4)), N'2', N'1', N'22', N'T1+W+L1', N'T1+W+L1', N'T1+W+L1', N'T1+W+L1', N'T1+W+L1', N'T1+W+L1', N'T1+W+L1', N'合格', 50, 2, 3, N'0', 0, 2, N'张三', N'UU', CAST(N'2015-04-01T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-07-12T16:41:39.797' AS DateTime), NULL)
INSERT [dbo].[Base_ParameterIndexTemplet] ([Id], [IndexCode], [IndexName], [Unit], [Min_Value], [Max_Value], [Default_Value], [Local_Var], [Out_Var], [Out_Var_Func], [Expression], [Display_Exp], [Full_Out_Var_Func], [Full_Out_Var], [Full_Local_Var], [Full_Expression], [Full_Display_Exp], [Exp_Desc], [Frequence], [IndexType], [MFlag], [Is_Clear_Zero], [Is_Write_Back], [Order_No], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'3', N'03', N'检修井', N'隧道', CAST(3.0000 AS Numeric(8, 4)), CAST(3.0000 AS Numeric(8, 4)), CAST(3.0000 AS Decimal(8, 4)), N'3', N'4', N'60', N'T1+W+L2', N'T1+W+L2', N'T1+W+L2', N'T1+W+L2', N'T1+W+L2', N'T1+W+L2', N'T1+W+L2', N'不合格', 60, 2, 3, N'0', 0, 3, N'小明', N'CC', CAST(N'2016-04-01T00:00:00.000' AS DateTime), N'小明一号', N'dd', CAST(N'2016-08-04T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'645cb16b-76e6-4a60-8d5c-da933788c68e', NULL, N'DT149', N'DT147', N'转折', CAST(529095.930 AS Decimal(12, 3)), CAST(370610.480 AS Decimal(12, 3)), CAST(0.400 AS Decimal(12, 3)), CAST(20.580 AS Decimal(12, 3)), CAST(20.180 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.890' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'bc872e48-8c66-4381-8fdb-c8d9cf129af2', NULL, N'DT149', N'DT152', N'转折', CAST(529095.930 AS Decimal(12, 3)), CAST(370610.480 AS Decimal(12, 3)), CAST(0.400 AS Decimal(12, 3)), CAST(20.580 AS Decimal(12, 3)), CAST(20.180 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.893' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'd6d8e8f9-dc87-444b-bd17-00ec541f32f0', NULL, N'DT150', N'DT148', N'转折', CAST(529094.230 AS Decimal(12, 3)), CAST(370603.370 AS Decimal(12, 3)), CAST(0.400 AS Decimal(12, 3)), CAST(20.800 AS Decimal(12, 3)), CAST(20.400 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.893' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'32350e6b-4297-460d-84fb-3401e1810c0f', NULL, N'DT150', N'DT157', N'转折', CAST(529094.230 AS Decimal(12, 3)), CAST(370603.370 AS Decimal(12, 3)), CAST(0.400 AS Decimal(12, 3)), CAST(20.800 AS Decimal(12, 3)), CAST(20.400 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.897' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'1a6e5100-3c27-46c9-84b5-9401c85181ec', NULL, N'DT152', N'DT149', N'转折', CAST(529095.100 AS Decimal(12, 3)), CAST(370603.410 AS Decimal(12, 3)), CAST(0.400 AS Decimal(12, 3)), CAST(20.840 AS Decimal(12, 3)), CAST(20.440 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.900' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'f9b6b29f-5456-48f9-8d2b-a301e9beb502', NULL, N'DT152', N'DT158', N'转折', CAST(529095.100 AS Decimal(12, 3)), CAST(370603.410 AS Decimal(12, 3)), CAST(0.400 AS Decimal(12, 3)), CAST(20.840 AS Decimal(12, 3)), CAST(20.440 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.900' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'f4c6487e-0c42-42c6-87c6-cfeefd43994b', NULL, N'DT157', N'DT150', N'转折', CAST(529099.300 AS Decimal(12, 3)), CAST(370542.000 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.410 AS Decimal(12, 3)), CAST(21.810 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.903' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'e4167872-40d7-4626-a35c-d7839e95490a', NULL, N'DT157', N'DT164', N'转折', CAST(529099.300 AS Decimal(12, 3)), CAST(370542.000 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.410 AS Decimal(12, 3)), CAST(21.810 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.907' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'8a5b89ba-7f4e-4f46-bfd1-4627f5396c95', NULL, N'DT158', N'DT152', N'转折', CAST(529100.300 AS Decimal(12, 3)), CAST(370542.110 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.410 AS Decimal(12, 3)), CAST(21.810 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.907' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'92dcfef0-de74-47f6-95b8-06c5a87318f2', NULL, N'DT158', N'DT163', N'转折', CAST(529100.300 AS Decimal(12, 3)), CAST(370542.110 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.410 AS Decimal(12, 3)), CAST(21.810 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.910' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'5f5bcab5-79ba-45ad-8b8c-e412b8fdcf43', NULL, N'DT161', N'DT163', N'转折', CAST(529102.390 AS Decimal(12, 3)), CAST(370506.980 AS Decimal(12, 3)), CAST(1.200 AS Decimal(12, 3)), CAST(22.920 AS Decimal(12, 3)), CAST(21.720 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.913' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'84d76b25-f946-4c22-8159-1882403cf7c3', NULL, N'DT161', N'DT167', N'转折', CAST(529102.390 AS Decimal(12, 3)), CAST(370506.980 AS Decimal(12, 3)), CAST(1.200 AS Decimal(12, 3)), CAST(22.920 AS Decimal(12, 3)), CAST(21.720 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.913' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'6bbdbced-df24-4b1f-895b-feddab1b72a2', NULL, N'DT162', N'DT164', N'转折', CAST(529102.270 AS Decimal(12, 3)), CAST(370506.980 AS Decimal(12, 3)), CAST(1.200 AS Decimal(12, 3)), CAST(22.920 AS Decimal(12, 3)), CAST(21.720 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.917' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'7a6974de-7a82-4711-a4ce-58817c7caf38', NULL, N'DT162', N'DT166', N'转折', CAST(529102.270 AS Decimal(12, 3)), CAST(370506.980 AS Decimal(12, 3)), CAST(1.200 AS Decimal(12, 3)), CAST(22.920 AS Decimal(12, 3)), CAST(21.720 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.917' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'f134eab7-448a-41c5-a48a-5571de16ef22', NULL, N'DT163', N'DT158', N'转折', CAST(529103.180 AS Decimal(12, 3)), CAST(370509.000 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.910 AS Decimal(12, 3)), CAST(22.310 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.920' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'eb2e79eb-5e95-437c-80c4-59e503abcdf1', NULL, N'DT163', N'DT161', N'转折', CAST(529103.180 AS Decimal(12, 3)), CAST(370509.000 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.910 AS Decimal(12, 3)), CAST(22.310 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.923' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'08a80da9-1091-4683-bcb3-03d2cd17c70e', NULL, N'DT164', N'DT157', N'转折', CAST(529102.190 AS Decimal(12, 3)), CAST(370509.050 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.910 AS Decimal(12, 3)), CAST(22.310 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.927' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'71425f9c-4a02-460a-9869-817ac1b7540c', NULL, N'DT164', N'DT162', N'转折', CAST(529102.190 AS Decimal(12, 3)), CAST(370509.050 AS Decimal(12, 3)), CAST(0.600 AS Decimal(12, 3)), CAST(22.910 AS Decimal(12, 3)), CAST(22.310 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.927' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'cf2b022b-7ef2-4ce0-a634-49804855f9ee', NULL, N'DT166', N'DT162', N'转折', CAST(529073.340 AS Decimal(12, 3)), CAST(370444.630 AS Decimal(12, 3)), CAST(1.500 AS Decimal(12, 3)), CAST(23.630 AS Decimal(12, 3)), CAST(22.130 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.930' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'8dd08360-50ab-4428-bac2-8d67226cd278', NULL, N'DT166', N'DT168', N'转折', CAST(529073.340 AS Decimal(12, 3)), CAST(370444.630 AS Decimal(12, 3)), CAST(1.500 AS Decimal(12, 3)), CAST(23.630 AS Decimal(12, 3)), CAST(22.130 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.933' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'c956e399-cd3b-42da-bf7a-f3738d6fe13c', NULL, N'DT167', N'DT161', N'转折', CAST(529073.340 AS Decimal(12, 3)), CAST(370444.630 AS Decimal(12, 3)), CAST(1.500 AS Decimal(12, 3)), CAST(23.630 AS Decimal(12, 3)), CAST(22.130 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.937' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'3685c5bc-19cc-4ac0-8945-95b871e92e01', NULL, N'DT167', N'DT170', N'转折', CAST(529073.340 AS Decimal(12, 3)), CAST(370444.630 AS Decimal(12, 3)), CAST(1.500 AS Decimal(12, 3)), CAST(23.630 AS Decimal(12, 3)), CAST(22.130 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.937' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'4d3ef8fa-bb18-45a8-92ff-f088ea551e6d', NULL, N'DT168', N'DT166', N'转折', CAST(529071.310 AS Decimal(12, 3)), CAST(370443.240 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(23.140 AS Decimal(12, 3)), CAST(22.840 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.940' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'd20fb16c-b58c-4886-a1c3-e1aecfdb59fc', NULL, N'DT168', N'DT172', N'转折', CAST(529071.310 AS Decimal(12, 3)), CAST(370443.240 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(23.140 AS Decimal(12, 3)), CAST(22.840 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.943' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'066d154a-8a11-4bb9-ad2e-800ebe0cb474', NULL, N'DT170', N'DT167', N'转折', CAST(529072.360 AS Decimal(12, 3)), CAST(370442.490 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(23.130 AS Decimal(12, 3)), CAST(22.830 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.947' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'e1dd4464-bf6b-4e01-9a87-dfdeb21d8dca', NULL, N'DT170', N'DT173', N'转折', CAST(529072.360 AS Decimal(12, 3)), CAST(370442.490 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(23.130 AS Decimal(12, 3)), CAST(22.830 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.947' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'44d94e33-79cd-4374-8768-486736911bab', NULL, N'DT172', N'DT168', N'转折', CAST(529065.510 AS Decimal(12, 3)), CAST(370439.300 AS Decimal(12, 3)), CAST(0.680 AS Decimal(12, 3)), CAST(23.680 AS Decimal(12, 3)), CAST(23.000 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.950' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'4c9c7589-c2be-4092-b7fb-68b967067373', NULL, N'DT172', N'DT176', N'转折', CAST(529065.510 AS Decimal(12, 3)), CAST(370439.300 AS Decimal(12, 3)), CAST(0.680 AS Decimal(12, 3)), CAST(23.680 AS Decimal(12, 3)), CAST(23.000 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.953' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'09b330e7-0444-4fcc-82b8-7da0305377f6', NULL, N'DT173', N'DT170', N'转折', CAST(529066.140 AS Decimal(12, 3)), CAST(370438.320 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.680 AS Decimal(12, 3)), CAST(22.980 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.953' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'29152970-4227-4efe-ad60-5663f5b27649', NULL, N'DT173', N'DT175', N'转折', CAST(529066.140 AS Decimal(12, 3)), CAST(370438.320 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.680 AS Decimal(12, 3)), CAST(22.980 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.957' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'555d7e9b-7d42-4b32-88de-7e1570a70aff', NULL, N'DT175', N'DT173', N'转折', CAST(529053.830 AS Decimal(12, 3)), CAST(370431.220 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.780 AS Decimal(12, 3)), CAST(23.080 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.960' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'a0d49a44-a4e2-4c7a-aab3-b48f4ff08f42', NULL, N'DT175', N'DT180', N'转折', CAST(529053.830 AS Decimal(12, 3)), CAST(370431.220 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.780 AS Decimal(12, 3)), CAST(23.080 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.963' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'dc9bfd96-9275-4f3c-85f0-ef71eda0d45b', NULL, N'DT176', N'DT172', N'转折', CAST(529053.330 AS Decimal(12, 3)), CAST(370432.140 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.770 AS Decimal(12, 3)), CAST(23.070 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.963' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'a3e308f7-1948-4e43-92dc-6c65f6800961', NULL, N'DT176', N'DT179', N'转折', CAST(529053.330 AS Decimal(12, 3)), CAST(370432.140 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.770 AS Decimal(12, 3)), CAST(23.070 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.967' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'840b3bc6-e605-4ff8-9e96-40969c078c30', NULL, N'DT179', N'DT176', N'转折', CAST(529040.800 AS Decimal(12, 3)), CAST(370425.380 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.800 AS Decimal(12, 3)), CAST(23.100 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.970' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'643e0a20-4bc2-4cb6-b47d-208cb5eb8ca5', NULL, N'DT179', N'DT183', N'转折', CAST(529040.800 AS Decimal(12, 3)), CAST(370425.380 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.800 AS Decimal(12, 3)), CAST(23.100 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.973' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'8b64081d-8ebe-4158-92da-3962967bed75', NULL, N'DT180', N'DT175', N'转折', CAST(529041.270 AS Decimal(12, 3)), CAST(370424.510 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.810 AS Decimal(12, 3)), CAST(23.110 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.973' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'5daeaa83-759c-4156-b5fe-64281c8a8892', NULL, N'DT180', N'DT181', N'转折', CAST(529041.270 AS Decimal(12, 3)), CAST(370424.510 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.810 AS Decimal(12, 3)), CAST(23.110 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.977' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'd12ed0e1-ff20-44a6-9b5e-ebc42c54716b', NULL, N'DT181', N'DT180', N'转折', CAST(529028.460 AS Decimal(12, 3)), CAST(370417.980 AS Decimal(12, 3)), CAST(0.670 AS Decimal(12, 3)), CAST(23.770 AS Decimal(12, 3)), CAST(23.100 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.980' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'3cae667d-142d-4ed8-875b-f8a46e3d02a5', NULL, N'DT181', N'DT186', N'转折', CAST(529028.460 AS Decimal(12, 3)), CAST(370417.980 AS Decimal(12, 3)), CAST(0.670 AS Decimal(12, 3)), CAST(23.770 AS Decimal(12, 3)), CAST(23.100 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.983' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'14dd13a7-d592-411e-9ee1-56f1a3e8ee5a', NULL, N'DT183', N'DT179', N'转折', CAST(529027.980 AS Decimal(12, 3)), CAST(370418.950 AS Decimal(12, 3)), CAST(0.640 AS Decimal(12, 3)), CAST(23.770 AS Decimal(12, 3)), CAST(23.130 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.983' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'b68d4ef9-69fc-49ab-a3ce-6c6f37690a67', NULL, N'DT183', N'DT185', N'转折', CAST(529027.980 AS Decimal(12, 3)), CAST(370418.950 AS Decimal(12, 3)), CAST(0.640 AS Decimal(12, 3)), CAST(23.770 AS Decimal(12, 3)), CAST(23.130 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.987' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'3134134b-db67-4e5b-b18b-27ec1b43349c', NULL, N'DT185', N'DT183', N'转折', CAST(529013.670 AS Decimal(12, 3)), CAST(370411.390 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.350 AS Decimal(12, 3)), CAST(22.650 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.990' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'3c58390c-6aa9-459a-afab-941e08d4b091', NULL, N'DT185', N'DT189', N'转折', CAST(529013.670 AS Decimal(12, 3)), CAST(370411.390 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.350 AS Decimal(12, 3)), CAST(22.650 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.993' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'c1dd52d6-e317-4409-8abf-047a250101cb', NULL, N'DT186', N'DT181', N'转折', CAST(529014.190 AS Decimal(12, 3)), CAST(370410.440 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.350 AS Decimal(12, 3)), CAST(22.650 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:56.997' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'c09d9f4f-7799-47d3-b905-5284ca821f8d', NULL, N'DT186', N'DT187', N'转折', CAST(529014.190 AS Decimal(12, 3)), CAST(370410.440 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(23.350 AS Decimal(12, 3)), CAST(22.650 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'4a3aa58c-04b2-44ce-a7dd-6af011b8dacd', NULL, N'DT187', N'DT186', N'转折', CAST(529009.780 AS Decimal(12, 3)), CAST(370407.670 AS Decimal(12, 3)), CAST(0.350 AS Decimal(12, 3)), CAST(22.780 AS Decimal(12, 3)), CAST(22.430 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.003' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'bffa5b39-24f2-4074-ae58-4647e350b99b', NULL, N'DT187', N'DT192', N'转折', CAST(529009.780 AS Decimal(12, 3)), CAST(370407.670 AS Decimal(12, 3)), CAST(0.350 AS Decimal(12, 3)), CAST(22.780 AS Decimal(12, 3)), CAST(22.430 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.007' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'e0dd84db-4eae-492a-a6e5-223e6ac1ad3f', NULL, N'DT189', N'DT185', N'转折', CAST(529009.040 AS Decimal(12, 3)), CAST(370408.420 AS Decimal(12, 3)), CAST(0.370 AS Decimal(12, 3)), CAST(22.790 AS Decimal(12, 3)), CAST(22.420 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.017' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'59a6d640-b813-4e8f-a848-628ed11bac0b', NULL, N'DT189', N'DT190', N'转折', CAST(529009.040 AS Decimal(12, 3)), CAST(370408.420 AS Decimal(12, 3)), CAST(0.370 AS Decimal(12, 3)), CAST(22.790 AS Decimal(12, 3)), CAST(22.420 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.017' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'15386a47-0c9d-4cef-bca0-4c1ef6bc2aaa', NULL, N'DT190', N'DT189', N'转折', CAST(529006.490 AS Decimal(12, 3)), CAST(370405.380 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(22.620 AS Decimal(12, 3)), CAST(22.320 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.020' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'821e6413-faaf-4752-87e1-cea38b8bfa51', NULL, N'DT190', N'DT195', N'转折', CAST(529006.490 AS Decimal(12, 3)), CAST(370405.380 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(22.620 AS Decimal(12, 3)), CAST(22.320 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.023' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'8a277790-fd31-42ae-b306-000b5170f206', NULL, N'DT192', N'DT187', N'转折', CAST(529007.260 AS Decimal(12, 3)), CAST(370404.870 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(22.610 AS Decimal(12, 3)), CAST(22.310 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.023' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'8c53a460-9659-49ba-8789-9c6ea6615d7c', NULL, N'DT192', N'DT194', N'转折', CAST(529007.260 AS Decimal(12, 3)), CAST(370404.870 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(22.610 AS Decimal(12, 3)), CAST(22.310 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.027' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'af91755a-5cf1-45ba-8b13-65cf9ab83985', NULL, N'DT194', N'DT192', N'转折', CAST(529004.610 AS Decimal(12, 3)), CAST(370400.290 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(22.730 AS Decimal(12, 3)), CAST(22.030 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.030' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'872b9fc6-3014-45d7-8c68-14f2c1449b96', NULL, N'DT194', N'DT199', N'转折', CAST(529004.610 AS Decimal(12, 3)), CAST(370400.290 AS Decimal(12, 3)), CAST(0.700 AS Decimal(12, 3)), CAST(22.730 AS Decimal(12, 3)), CAST(22.030 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.030' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'b7de6b44-4902-4caf-ab42-8d38413069b2', NULL, N'DT195', N'DT190', N'转折', CAST(529003.790 AS Decimal(12, 3)), CAST(370400.850 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(22.740 AS Decimal(12, 3)), CAST(22.440 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.033' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'fc37fae4-56cc-4d73-ba59-b1ec17d1f7e7', NULL, N'DT195', N'DT198', N'转折', CAST(529003.790 AS Decimal(12, 3)), CAST(370400.850 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(22.740 AS Decimal(12, 3)), CAST(22.440 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.037' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'a8067bce-7bb1-4385-aacc-e2830e030f5d', NULL, N'DT198', N'DT195', N'转折', CAST(528989.670 AS Decimal(12, 3)), CAST(370371.920 AS Decimal(12, 3)), CAST(0.610 AS Decimal(12, 3)), CAST(21.250 AS Decimal(12, 3)), CAST(20.640 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.037' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'4223c4b4-3234-4dc6-bb9f-38983414ed42', NULL, N'DT198', N'DT201', N'转折', CAST(528989.670 AS Decimal(12, 3)), CAST(370371.920 AS Decimal(12, 3)), CAST(0.610 AS Decimal(12, 3)), CAST(21.250 AS Decimal(12, 3)), CAST(20.640 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.040' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'c96df30c-7493-4492-a1f1-020655cbb46f', NULL, N'DT199', N'DT194', N'转折', CAST(528990.630 AS Decimal(12, 3)), CAST(370371.520 AS Decimal(12, 3)), CAST(0.610 AS Decimal(12, 3)), CAST(21.260 AS Decimal(12, 3)), CAST(20.650 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.043' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'4166b7ae-99e9-49e6-ba52-e25bd5ef667e', NULL, N'DT199', N'DT202', N'转折', CAST(528990.630 AS Decimal(12, 3)), CAST(370371.520 AS Decimal(12, 3)), CAST(0.610 AS Decimal(12, 3)), CAST(21.260 AS Decimal(12, 3)), CAST(20.650 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.043' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'13843951-95af-4960-806b-0e86148ea360', NULL, N'DT201', N'DT198', N'转折', CAST(528985.420 AS Decimal(12, 3)), CAST(370362.320 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(20.560 AS Decimal(12, 3)), CAST(20.260 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.047' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'7c42a3ac-3b42-4efb-bb1f-0658b2374bd3', NULL, N'DT201', N'DT204', N'转折', CAST(528985.420 AS Decimal(12, 3)), CAST(370362.320 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(20.560 AS Decimal(12, 3)), CAST(20.260 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.050' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'cea2fdb5-7659-4ee8-99b1-dd8b4d8d3fd3', NULL, N'DT202', N'DT199', N'转折', CAST(528986.520 AS Decimal(12, 3)), CAST(370362.080 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(20.560 AS Decimal(12, 3)), CAST(20.260 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.050' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'c9fb09b8-433f-4762-9247-6eceb24140ea', NULL, N'DT202', N'DT204', N'转折', CAST(528986.520 AS Decimal(12, 3)), CAST(370362.080 AS Decimal(12, 3)), CAST(0.300 AS Decimal(12, 3)), CAST(20.560 AS Decimal(12, 3)), CAST(20.260 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.053' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'9bd6a5a7-81ba-437b-ade5-4994d08d2197', NULL, N'DT204', N'DT201', N'三分支', CAST(528986.960 AS Decimal(12, 3)), CAST(370358.340 AS Decimal(12, 3)), CAST(1.450 AS Decimal(12, 3)), CAST(20.780 AS Decimal(12, 3)), CAST(19.330 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.057' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Basic_Locus] ([Id], [SectionId], [GisPoint], [LinkPoint], [PointFeature], [Point_X], [Point_Y], [Point_Z], [GroundHeight], [LineHeight], [SectionSize], [Material], [BuryType], [remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime]) VALUES (N'782c8b78-2733-44c1-b997-8b526be595c5', NULL, N'DT204', N'DT202', N'三分支', CAST(528986.960 AS Decimal(12, 3)), CAST(370358.340 AS Decimal(12, 3)), CAST(1.450 AS Decimal(12, 3)), CAST(20.780 AS Decimal(12, 3)), CAST(19.330 AS Decimal(12, 3)), CAST(30.000 AS Decimal(12, 3)), N'光纤', N'套管', NULL, NULL, NULL, CAST(N'2019-09-25T14:52:57.057' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Cable_Info] ([Id], [CLNumber], [TotelLenth], [CurrentLenth], [Voltage_Type], [Sheath_Type], [Fsection], [Max_Traction], [Max_Lateral_Pressure], [UserStatus], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [SectionId], [ProjectID]) VALUES (N'3763bf02-fdff-41a8-b193-c7f2b6fc28ef', N'xx', CAST(1.00 AS Numeric(8, 2)), CAST(2.00 AS Numeric(8, 2)), 2, 2, CAST(2.000 AS Decimal(8, 3)), CAST(2.00 AS Decimal(8, 2)), CAST(2.00 AS Decimal(8, 2)), 0, N'xxxx', N'1', N'admin', CAST(N'2019-07-11T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-27T14:03:47.220' AS DateTime), NULL, N'3c08d3a0-bb9e-4d00-b5fb-961dd06b6233', N'36e22bb1-a29b-4890-bf84-4c8b68fb2b21')
INSERT [dbo].[Cable_Info] ([Id], [CLNumber], [TotelLenth], [CurrentLenth], [Voltage_Type], [Sheath_Type], [Fsection], [Max_Traction], [Max_Lateral_Pressure], [UserStatus], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [SectionId], [ProjectID]) VALUES (N'44c48746-0b4a-4cac-becc-1e5655352fa1', N'22', CAST(3.00 AS Numeric(8, 2)), CAST(4.00 AS Numeric(8, 2)), 3, 4, CAST(5.000 AS Decimal(8, 3)), CAST(4.00 AS Decimal(8, 2)), NULL, 1, N'4', N'1', N'admin', CAST(N'2019-07-11T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-29T19:23:23.157' AS DateTime), NULL, N'2', N'36e22bb1-a29b-4890-bf84-4c8b68fb2b21')
INSERT [dbo].[Dic_Content] ([Id], [DicType_ID], [DicTypeContent], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [Sort]) VALUES (N'6fe5d500-ab3d-42fa-a379-3fdb41dc4207', N'6d7945aa-7948-4672-84f9-04eca59af38e', N'dfgdfg', N'1', N'admin', CAST(N'2019-07-18T09:50:44.687' AS DateTime), N'1', N'admin', CAST(N'2019-07-18T09:50:44.687' AS DateTime), NULL, 0)
INSERT [dbo].[Dic_Content] ([Id], [DicType_ID], [DicTypeContent], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [Sort]) VALUES (N'a48b26a6-75f2-4a6b-9841-ffbdd417d5f5', N'6d7945aa-7948-4672-84f9-04eca59af38e', N'dfsdf', N'1', N'admin', CAST(N'2019-07-17T16:04:14.053' AS DateTime), N'1', N'admin', CAST(N'2019-07-17T16:04:14.053' AS DateTime), NULL, 0)
INSERT [dbo].[Dic_Content] ([Id], [DicType_ID], [DicTypeContent], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [Sort]) VALUES (N'bb271663-4c40-471f-98d4-228517a5e8b5', N'719f5d58-6e06-4351-8d6c-f58d68507447', N'金属护套', N'1', N'admin', CAST(N'2019-07-29T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-09-27T17:15:44.533' AS DateTime), NULL, 0)
INSERT [dbo].[Dic_Type] ([Id], [TypeName], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'6d7945aa-7948-4672-84f9-04eca59af38e', N'设备类型', N'1', N'admin', CAST(N'2019-07-17T14:54:32.443' AS DateTime), N'1', N'admin', CAST(N'2019-07-17T14:54:32.443' AS DateTime), NULL)
INSERT [dbo].[Dic_Type] ([Id], [TypeName], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'39a2e21f-eeaf-4ada-a5ed-594b0e49c154', N'敷设类型', N'1', N'admin', CAST(N'2019-07-17T16:25:19.943' AS DateTime), N'1', N'admin', CAST(N'2019-07-17T16:25:19.943' AS DateTime), NULL)
INSERT [dbo].[Dic_Type] ([Id], [TypeName], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'719f5d58-6e06-4351-8d6c-f58d68507447', N'护套类型', N'1', N'admin', CAST(N'2019-07-29T10:46:15.430' AS DateTime), N'1', N'admin', CAST(N'2019-07-29T10:46:15.430' AS DateTime), NULL)
INSERT [dbo].[Equipment] ([Id], [Name], [EquipmentTypeId], [ModelNumber], [ECode], [BarCode], [Weight], [UseInfo], [State], [Enabled], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'2-001', N'222', N'44', N'33', N'33', N'44', CAST(0.00 AS Decimal(8, 2)), N'11', 0, 0, N'1', N'超级管理员', CAST(N'2019-05-22T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-07-16T18:33:38.807' AS DateTime), 1)
INSERT [dbo].[Laying_Path] ([Id], [SName], [ProjectId], [CableType], [flength], [StartAddress], [StartPoint_X], [StartPoint_Y], [StartPoint_Z], [EndAddress], [EndPoint_X], [EndPoint_Y], [fremark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [EndPoint_Z], [Radius], [PullValue], [SideValue], [IsQualified], [Solutions], [SectionId], [Laying_Type]) VALUES (N'51eb8f45-6878-4ca8-a3e6-2dd4e2260c09', N'虎门1号', N'36e22bb1-a29b-4890-bf84-4c8b68fb2b21', N'1号电缆', CAST(12.00 AS Numeric(6, 2)), N'长沙', CAST(2.0000 AS Decimal(8, 4)), CAST(1.00 AS Numeric(8, 2)), CAST(2.0000 AS Numeric(8, 4)), N'浏阳', CAST(3.0000 AS Numeric(8, 4)), CAST(3.0000 AS Numeric(8, 4)), N'4', N'1', N'admin', CAST(N'2019-07-12T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-27T10:46:36.520' AS DateTime), NULL, CAST(2.0000 AS Numeric(8, 4)), NULL, NULL, NULL, NULL, NULL, N'1', N'2')
INSERT [dbo].[Monitoring_Data] ([Id], [ftime], [PIndexId], [MonitorPointId], [fdata], [fremark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted]) VALUES (N'9a754196-6cf8-4333-98de-e3ceaf722b37', CAST(N'2019-07-17T00:00:00.000' AS DateTime), N'2', N'虎门', CAST(1.020 AS Decimal(12, 3)), N'', N'1', N'admin', CAST(N'2019-07-17T13:51:14.237' AS DateTime), N'1', N'admin', CAST(N'2019-07-17T13:51:14.237' AS DateTime), NULL)
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'36e22bb1-a29b-4890-bf84-4c8b68fb2b21', N'DG1234', N'110千伏富马输变点', N'东莞虎门**街道', CAST(100.00 AS Numeric(8, 2)), 2, 4, 2, N'高压', N'潮湿', CAST(5.00 AS Numeric(6, 2)), CAST(5.00 AS Numeric(6, 2)), N'1', CAST(5.00 AS Decimal(8, 2)), N'中国南网5', NULL, NULL, N'1', N'admin', CAST(N'2019-07-19T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-30T15:17:19.413' AS DateTime), NULL, NULL, N'', N'fdgfd', N'dfgdf', N'', N'dfg', N'gfdg', N'', N'广东省', N'东莞市', N'万江区')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'1663819c-426b-463a-af15-cd77cdb0ff0c', N'qweqweqwe', N'qweqwe', N'', CAST(1111.00 AS Numeric(8, 2)), 1, 3, 2, N'1', N'2', NULL, NULL, N'', NULL, N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-09-04T17:21:04.540' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'46091455-6337-4f93-acd1-d6d65adfd190', N'aasd', N'asdasd', N'', CAST(111.00 AS Numeric(8, 2)), 0, 11, 1, N'2', N'2', NULL, NULL, N'', NULL, N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-09-02T15:17:42.580' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'e2e2c873-d391-42d6-9a33-d96907180e26', N'ewwerwqerwe', N'rwqerewrq', N'', CAST(11.00 AS Numeric(8, 2)), 0, 11, 11, N'', N'', NULL, NULL, N'', CAST(11.00 AS Decimal(8, 2)), N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T16:56:08.917' AS DateTime), N'1', N'admin', CAST(N'2019-08-23T16:56:08.917' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'88c6aeea-aa79-4561-8254-ffeaec72a804', N'XZXZXZX', N'ZXZX', N'11111', CAST(111.00 AS Numeric(8, 2)), 0, 111, 111, N'2', N'1', NULL, NULL, N'', CAST(111.00 AS Decimal(8, 2)), N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-30T09:22:16.337' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'8f063216-f1cf-46de-935c-ab511dab510a', N'asdfsdfsdfsdf', N'sdfdsf', N'111', CAST(111.00 AS Numeric(8, 2)), 0, 1111, 1111, N'', N'', NULL, NULL, N'', NULL, N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T16:58:18.860' AS DateTime), N'1', N'admin', CAST(N'2019-08-23T16:58:18.860' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'8108d979-f8bd-4ece-b745-ecf4b1460234', N'DG000554', N'110父变电站', N'东莞1001', CAST(22.00 AS Numeric(8, 2)), 0, 2, 1, N'', N'', NULL, NULL, N'', CAST(22.00 AS Decimal(8, 2)), N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T16:59:03.457' AS DateTime), N'1', N'admin', CAST(N'2019-08-23T16:59:03.457' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Project_Info] ([Id], [ProjectCode], [ProjectName], [Location], [Distance], [State], [SectionCount], [CircuitCount], [ClectricityType], [LayEnvironment], [Temperature], [Humidity], [Voltage], [CutSize], [BuildUnit], [NowProject], [Remark], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [Delteted], [DevUnit], [DevContacts], [DevPhone], [BuildContacts], [BuildPhone], [SuperUnit], [SuperContacts], [SuperPhone], [provinceName], [cityName], [districtName]) VALUES (N'f071fe97-fe4b-4c72-9873-08a11ea8447c', N'而犬瘟热未确认 ', N'去微软 ', N'2222', CAST(11112.00 AS Numeric(8, 2)), 0, 22, NULL, N'', N'', NULL, NULL, N'', CAST(121212.00 AS Decimal(8, 2)), N'', NULL, NULL, N'1', N'admin', CAST(N'2019-08-23T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-23T17:05:22.540' AS DateTime), NULL, NULL, N'', N'', N'', N'', N'', N'', N'', N'广东省', N'东莞市', N'虎门镇')
INSERT [dbo].[Section] ([Id], [SectionCode], [ProjectId], [Distance], [Location], [State], [DefaultEquipmentDistance], [WarningSMS], [NowSection], [Phones], [SysCompanyId], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [asjcode], [Sazcod], [Asdcod], [Delteted]) VALUES (N'1', N'001', N'36e22bb1-a29b-4890-bf84-4c8b68fb2b21', CAST(100.00 AS Decimal(8, 2)), N'合格', 20, CAST(1.00 AS Decimal(8, 2)), 1, 2, N'1212121212', N'3', N'杨先生', N'cood', CAST(N'2015-06-04T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-28T10:46:59.697' AS DateTime), N'60', N'左', N'30', NULL)
INSERT [dbo].[Section] ([Id], [SectionCode], [ProjectId], [Distance], [Location], [State], [DefaultEquipmentDistance], [WarningSMS], [NowSection], [Phones], [SysCompanyId], [CreateId], [CreateUser], [CreateTime], [UpdateId], [UpdateUser], [UpdateTime], [asjcode], [Sazcod], [Asdcod], [Delteted]) VALUES (N'2', N'002', N'36e22bb1-a29b-4890-bf84-4c8b68fb2b21', CAST(10.00 AS Decimal(8, 2)), N'不合格', 0, CAST(2.00 AS Decimal(8, 2)), 2, 1, N'12313', N'1', N'李四', N'隧道', CAST(N'2016-08-04T00:00:00.000' AS DateTime), N'1', N'admin', CAST(N'2019-08-29T11:59:12.680' AS DateTime), N'80', N'右', N'40', NULL)
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'21408309-2e9b-4223-9206-bf9f5f092117', N'8fe9862e-25f9-483e-a046-3ceda9301911', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-07-25T09:29:43.713' AS DateTime), N'admin', CAST(N'2019-07-30T15:23:20.623' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'208f167b-009b-4e4f-bcbd-030c01289fd1', N'000bb3fd-681a-4c11-8dfe-8ec67b8eb982', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-07-25T16:24:34.440' AS DateTime), N'admin', CAST(N'2019-07-30T15:23:34.020' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'691fa9b6-3679-418c-9aa7-8ed860358c2c', N'a6d67c47-2b5a-4250-b889-9c9b4d360bdb', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-07-25T16:24:45.457' AS DateTime), N'admin', CAST(N'2019-07-30T15:23:48.787' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'1f235c1f-5c91-431c-83b7-02059141f0af', N'6f55f552-cbf6-4d75-af84-9be6f39ed633', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-07-25T16:43:49.983' AS DateTime), N'admin', CAST(N'2019-07-30T15:23:56.550' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'869fdcb9-3806-4cec-a991-bd45822ce1eb', N'e148b20c-116f-40ae-81ec-dc682f88c358', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-07-26T10:30:22.410' AS DateTime), N'admin', CAST(N'2019-08-28T15:11:36.420' AS DateTime), N'0eeed679-c4f1-4566-95aa-4c433444d98b')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'376cdfbc-8e02-49ae-895e-e71033aaea00', N'e148b20c-116f-40ae-81ec-dc682f88c358', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T14:57:59.817' AS DateTime), N'admin', CAST(N'2019-08-01T14:57:59.817' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'761ffb2b-95ca-4ecd-8b36-6fa0075738d0', N'2b78d609-66f9-4049-994b-4b47df3e2c01', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T14:58:18.463' AS DateTime), N'admin', CAST(N'2019-08-01T14:58:18.463' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'508417b5-de00-457a-bd0b-73f13a1d6612', N'591f60f5-14c7-402e-80b7-3cc2c3c9fd8a', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T14:58:40.623' AS DateTime), N'admin', CAST(N'2019-08-01T14:58:40.623' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'fedde975-392b-472c-a34d-a0efda22e7fa', N'11e3a3b7-359e-40a0-8a6e-3074eb96bf19', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T14:59:12.013' AS DateTime), N'admin', CAST(N'2019-08-01T14:59:12.013' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'36f02eb5-e913-46a1-8341-333155194c41', N'c75b615a-07a0-4693-985f-3d8ce528a4d2', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T14:59:36.777' AS DateTime), N'admin', CAST(N'2019-08-01T14:59:36.777' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'2ff17157-31a0-4e0b-b13d-47a1d6c84553', N'6fa03bc9-6f7b-49c4-9657-019f2ffde77a', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-01T14:59:50.547' AS DateTime), N'admin', CAST(N'2019-08-01T14:59:50.547' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'3d6495d1-41dc-4eeb-9917-b13b379969d8', N'84edf342-0bb1-4e1e-b9a5-408a2d7f08fd', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-01T15:00:17.140' AS DateTime), N'admin', CAST(N'2019-08-01T15:00:17.140' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'28a914c3-b06f-41f5-b7c9-ba6c2d462c62', N'00ac1779-c23b-4c85-93b8-6b644a3ccab9', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-01T15:00:37.830' AS DateTime), N'admin', CAST(N'2019-08-01T15:00:37.830' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'a48a5974-5599-4199-9edb-36a9dee5e29c', N'6fb8ffc0-3d5f-494a-8e32-0f8bdbaa2ddb', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-01T15:00:46.960' AS DateTime), N'admin', CAST(N'2019-08-01T15:00:46.960' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'8f6db94f-5a56-4ff1-800c-26ad5e4e12ba', N'20209e8c-dc5e-4e18-8dc0-ea11ec5db7f9', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T15:01:06.493' AS DateTime), N'admin', CAST(N'2019-08-01T15:02:15.623' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'381b1c02-46c6-4b28-8ba5-572a0e66ab79', N'66f580d1-0182-46d5-b700-0059c31ede14', 1, 1, 1, 1, 1, N'admin', CAST(N'2019-08-01T15:01:14.963' AS DateTime), N'admin', CAST(N'2019-08-01T15:02:02.583' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'2e21d404-b4cc-45aa-ae54-b6cb77541cd5', N'1013b268-3319-4dab-b192-d4e83a16f43f', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-09T10:24:38.390' AS DateTime), N'admin', CAST(N'2019-08-09T10:24:38.390' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'52021b5b-6610-4dc6-b704-5cfdb8fa3a69', N'8f0209b3-f68b-475f-aa53-9b533bfc425f', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-09T10:31:10.663' AS DateTime), N'admin', CAST(N'2019-08-09T10:31:10.663' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'8e5f6ada-5bbf-47df-a08d-47f7975e5660', N'8fe9862e-25f9-483e-a046-3ceda9301911', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-28T15:12:41.650' AS DateTime), N'admin', CAST(N'2019-08-28T15:12:41.650' AS DateTime), N'0eeed679-c4f1-4566-95aa-4c433444d98b')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'381b1c02-46c6-4b28-8ba5-572a0e66ab77', N'ab2b51a9-6980-41f3-b987-ce6d43324f26', 1, 1, 1, 1, 1, N'admin', NULL, NULL, NULL, N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'62e63c3d-a07a-4c92-b35f-f3573a74366a', N'073c441f-c5d8-43bd-be6e-35331e37bd49', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-09T10:20:24.423' AS DateTime), N'admin', CAST(N'2019-08-09T10:20:24.423' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[Sys_Menu_Grant] ([Id], [Menu_Id], [grant_query], [grant_create], [grant_edit], [grant_delete], [grant_print], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [RoleId]) VALUES (N'f889e60a-74ac-48da-8aaf-56a827441896', N'b26614da-742b-42a4-85fd-c27f67dc6a27', 0, 0, 0, 0, 0, N'admin', CAST(N'2019-08-09T10:24:09.420' AS DateTime), N'admin', CAST(N'2019-08-09T10:24:09.420' AS DateTime), N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'8fe9862e-25f9-483e-a046-3ceda9301911', N'工程管理', N'', N'0', N'admin', CAST(N'2019-07-24T11:30:33.767' AS DateTime), N'admin', CAST(N'2019-07-25T08:45:48.473' AS DateTime), N'/lib/layui/css/admin/images/gongchengguanli.png')
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'000bb3fd-681a-4c11-8dfe-8ec67b8eb982', N'受力分析', N'', N'0', N'admin', CAST(N'2019-07-25T08:46:06.063' AS DateTime), N'admin', CAST(N'2019-07-25T08:46:25.110' AS DateTime), N'/lib/layui/css/admin/images/fenxi.png')
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'a6d67c47-2b5a-4250-b889-9c9b4d360bdb', N'字典管理', N'', N'0', N'admin', CAST(N'2019-07-25T08:46:15.557' AS DateTime), N'admin', CAST(N'2019-07-25T08:46:29.960' AS DateTime), N'/lib/layui/css/admin/images/zidian.png')
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'e148b20c-116f-40ae-81ec-dc682f88c358', N'工程信息', N'/ProjectInfo/Index', N'8fe9862e-25f9-483e-a046-3ceda9301911', N'admin', CAST(N'2019-07-25T15:15:33.210' AS DateTime), N'admin', CAST(N'2019-08-27T11:55:55.390' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'2b78d609-66f9-4049-994b-4b47df3e2c01', N'工段信息', N'/Section/Index', N'8fe9862e-25f9-483e-a046-3ceda9301911', N'admin', CAST(N'2019-07-25T15:25:31.510' AS DateTime), N'admin', CAST(N'2019-07-25T15:25:31.510' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'11e3a3b7-359e-40a0-8a6e-3074eb96bf19', N'电缆信息', N'/Cable_Info/Index', N'8fe9862e-25f9-483e-a046-3ceda9301911', N'admin', CAST(N'2019-07-25T15:25:59.317' AS DateTime), N'admin', CAST(N'2019-07-25T15:25:59.317' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'591f60f5-14c7-402e-80b7-3cc2c3c9fd8a', N'敷设路径', N'/LayingPath/Index', N'8fe9862e-25f9-483e-a046-3ceda9301911', N'admin', CAST(N'2019-07-25T15:26:27.027' AS DateTime), N'admin', CAST(N'2019-07-25T15:26:27.027' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'c75b615a-07a0-4693-985f-3d8ce528a4d2', N'指标模板', N'/Base_ParameterIndexTemplet/Index', N'a6d67c47-2b5a-4250-b889-9c9b4d360bdb', N'admin', CAST(N'2019-07-25T15:27:03.680' AS DateTime), N'admin', CAST(N'2019-08-26T16:23:12.450' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'6fa03bc9-6f7b-49c4-9657-019f2ffde77a', N'轨迹点信息', N'/Basic_Locus/Index', N'000bb3fd-681a-4c11-8dfe-8ec67b8eb982', N'admin', CAST(N'2019-07-25T15:27:34.713' AS DateTime), N'admin', CAST(N'2019-07-25T15:27:34.713' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'84edf342-0bb1-4e1e-b9a5-408a2d7f08fd', N'设备信息', N'/Equipment/Index', N'a6d67c47-2b5a-4250-b889-9c9b4d360bdb', N'admin', CAST(N'2019-07-25T15:27:57.650' AS DateTime), N'admin', CAST(N'2019-08-26T16:22:39.150' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'00ac1779-c23b-4c85-93b8-6b644a3ccab9', N'监测点信息', N'/MonitorPoint/Index', N'000bb3fd-681a-4c11-8dfe-8ec67b8eb982', N'admin', CAST(N'2019-07-25T15:28:19.013' AS DateTime), N'admin', CAST(N'2019-07-25T15:28:19.013' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'6fb8ffc0-3d5f-494a-8e32-0f8bdbaa2ddb', N'监测数据', N'/Monitoring_Data/Index', N'000bb3fd-681a-4c11-8dfe-8ec67b8eb982', N'admin', CAST(N'2019-07-25T15:28:39.960' AS DateTime), N'admin', CAST(N'2019-07-25T15:28:39.960' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'20209e8c-dc5e-4e18-8dc0-ea11ec5db7f9', N'字典类型', N'/Dic_Type/Index', N'a6d67c47-2b5a-4250-b889-9c9b4d360bdb', N'admin', CAST(N'2019-07-25T15:29:13.577' AS DateTime), N'admin', CAST(N'2019-07-25T15:30:01.200' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'66f580d1-0182-46d5-b700-0059c31ede14', N'字典条目', N'/Dic_Content/Index', N'a6d67c47-2b5a-4250-b889-9c9b4d360bdb', N'admin', CAST(N'2019-07-25T15:29:43.260' AS DateTime), N'admin', CAST(N'2019-07-25T15:29:43.260' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'6f55f552-cbf6-4d75-af84-9be6f39ed633', N'系统管理', N'', N'0', N'admin', CAST(N'2019-07-25T16:25:44.223' AS DateTime), N'admin', CAST(N'2019-07-25T16:25:44.223' AS DateTime), N'/lib/layui/css/admin/images/xitong.png')
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'073c441f-c5d8-43bd-be6e-35331e37bd49', N'用户管理', N'/UserInfo/Index', N'6f55f552-cbf6-4d75-af84-9be6f39ed633', N'admin', CAST(N'2019-07-25T16:26:22.637' AS DateTime), N'admin', CAST(N'2019-07-25T16:26:22.637' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'b26614da-742b-42a4-85fd-c27f67dc6a27', N'角色管理', N'/Role/Index', N'6f55f552-cbf6-4d75-af84-9be6f39ed633', N'admin', CAST(N'2019-07-25T16:27:01.697' AS DateTime), N'admin', CAST(N'2019-07-25T16:27:01.697' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'1013b268-3319-4dab-b192-d4e83a16f43f', N'用户角色管理', N'/UserOfRoles/Index', N'6f55f552-cbf6-4d75-af84-9be6f39ed633', N'admin', CAST(N'2019-07-25T16:27:29.477' AS DateTime), N'admin', CAST(N'2019-07-25T16:27:29.477' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'8f0209b3-f68b-475f-aa53-9b533bfc425f', N'菜单管理', N'/Menu/Index', N'6f55f552-cbf6-4d75-af84-9be6f39ed633', N'admin', CAST(N'2019-07-25T16:27:57.657' AS DateTime), N'admin', CAST(N'2019-07-25T16:27:57.657' AS DateTime), NULL)
INSERT [dbo].[System_Menu] ([Id], [Menu_Name], [Menu_Url], [Menu_Pid], [CreateUser], [CreateTime], [UpdateUser], [UpdateTime], [Menu_Icon]) VALUES (N'ab2b51a9-6980-41f3-b987-ce6d43324f26', N'菜单授权', N'/MenuGrant/Index', N'6f55f552-cbf6-4d75-af84-9be6f39ed633', N'admin', CAST(N'2019-07-25T16:28:24.817' AS DateTime), N'admin', CAST(N'2019-07-25T16:28:24.817' AS DateTime), NULL)
INSERT [dbo].[System_Role] ([Id], [RoleCode], [RoleName], [Remark], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'0eeed679-c4f1-4566-95aa-4c433444d98b', N'ww', N'普通用户', N'', NULL, N'admin', CAST(N'2019-07-19T16:16:15.473' AS DateTime), NULL, N'admin', CAST(N'2019-07-19T16:16:15.473' AS DateTime), NULL)
INSERT [dbo].[System_Role] ([Id], [RoleCode], [RoleName], [Remark], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'25bfc74d-ef0c-43e3-838b-5f1756d883ea', N'admin', N'超级管理员', N'', NULL, N'admin', CAST(N'2019-07-23T16:08:56.883' AS DateTime), NULL, N'admin', CAST(N'2019-07-23T16:08:56.883' AS DateTime), NULL)
INSERT [dbo].[System_UserInfo] ([Id], [UserName], [TrueName], [UserType], [Phone], [Email], [Password], [Inviter], [InviteCode], [PictureIco], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'1', N'admin', N'admin', NULL, N'', N'', N'e10adc3949ba59abbe56e057f20f883e', NULL, NULL, NULL, NULL, N'', NULL, NULL, N'admin', CAST(N'2019-08-28T14:56:27.803' AS DateTime), 0)
INSERT [dbo].[System_UserInfo] ([Id], [UserName], [TrueName], [UserType], [Phone], [Email], [Password], [Inviter], [InviteCode], [PictureIco], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'1054cfac-c1b4-4b94-8849-abf25b53efa3', N'sa', NULL, NULL, N'13345632345', N'330461722@qq.com', N'e10adc3949ba59abbe56e057f20f883e', NULL, NULL, NULL, NULL, N'admin', CAST(N'2019-07-18T16:39:26.943' AS DateTime), NULL, N'admin', CAST(N'2019-07-18T16:39:26.943' AS DateTime), NULL)
INSERT [dbo].[System_UserInfo] ([Id], [UserName], [TrueName], [UserType], [Phone], [Email], [Password], [Inviter], [InviteCode], [PictureIco], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'2', N'mines', N'mines', NULL, N'13237678956', N'330@qq.com', N'e10adc3949ba59abbe56e057f20f883e', NULL, NULL, NULL, NULL, N'', NULL, NULL, N'admin', CAST(N'2019-07-18T16:34:00.337' AS DateTime), 0)
INSERT [dbo].[System_UserInfo] ([Id], [UserName], [TrueName], [UserType], [Phone], [Email], [Password], [Inviter], [InviteCode], [PictureIco], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'3c2b6350-3b94-4929-b88a-cf7700133e0a', N'test', NULL, NULL, N'', N'', N'e10adc3949ba59abbe56e057f20f883e', NULL, NULL, NULL, NULL, N'admin', CAST(N'2019-08-28T15:00:50.650' AS DateTime), NULL, N'admin', CAST(N'2019-08-28T15:00:50.650' AS DateTime), NULL)
INSERT [dbo].[System_UserInfo] ([Id], [UserName], [TrueName], [UserType], [Phone], [Email], [Password], [Inviter], [InviteCode], [PictureIco], [CreateId], [CreateUser], [CreateTime], [ChangedUser], [UpdateUser], [UpdateTime], [DeleteFlag]) VALUES (N'7bd08cb8-6d8c-43d3-a98e-1c3d43e6d011', N'test2', NULL, NULL, N'', N'', N'e10adc3949ba59abbe56e057f20f883e', NULL, NULL, NULL, NULL, N'admin', CAST(N'2019-08-30T15:18:36.340' AS DateTime), NULL, N'admin', CAST(N'2019-08-30T15:18:36.340' AS DateTime), NULL)
INSERT [dbo].[System_UserOfRoles] ([Id], [UserId], [RoleId]) VALUES (N'2d851c6e-307b-4475-8edc-7fabd445fb9a', N'1', N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[System_UserOfRoles] ([Id], [UserId], [RoleId]) VALUES (N'60990970-d082-4ef2-b726-7b5124924f20', N'7bd08cb8-6d8c-43d3-a98e-1c3d43e6d011', N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[System_UserOfRoles] ([Id], [UserId], [RoleId]) VALUES (N'933808eb-a691-449a-b70b-1694afe16316', N'1054cfac-c1b4-4b94-8849-abf25b53efa3', N'25bfc74d-ef0c-43e3-838b-5f1756d883ea')
INSERT [dbo].[System_UserOfRoles] ([Id], [UserId], [RoleId]) VALUES (N'ce7fcd6a-f2b5-463b-9bbb-934aa3e2a333', N'3c2b6350-3b94-4929-b88a-cf7700133e0a', N'0eeed679-c4f1-4566-95aa-4c433444d98b')
INSERT [dbo].[System_UserOfRoles] ([Id], [UserId], [RoleId]) VALUES (N'f2dc2d21-bc03-4cc6-9009-4a2bf0f2ef67', N'2', N'0eeed679-c4f1-4566-95aa-4c433444d98b')
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_BASE_PARAMETERINDEXTEMPLET]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[Base_ParameterIndexTemplet] ADD  CONSTRAINT [PK_BASE_PARAMETERINDEXTEMPLET] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CL_EQUIPMENT]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[Equipment] ADD  CONSTRAINT [PK_CL_EQUIPMENT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_LAYING_PATH]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[Laying_Path] ADD  CONSTRAINT [PK_LAYING_PATH] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_MONITORING_DATA]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[Monitoring_Data] ADD  CONSTRAINT [PK_MONITORING_DATA] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_PROJECT_INFO]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[Project_Info] ADD  CONSTRAINT [PK_PROJECT_INFO] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_CL_SECTION]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[Section] ADD  CONSTRAINT [PK_CL_SECTION] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEM_RIGHTOBJECT]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[System_RightObject] ADD  CONSTRAINT [PK_SYSTEM_RIGHTOBJECT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEM_ROLE]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[System_Role] ADD  CONSTRAINT [PK_SYSTEM_ROLE] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_SYSTEM_ROLE_RIGHT]    Script Date: 2019/11/5 16:11:46 ******/
ALTER TABLE [dbo].[System_Role_Right] ADD  CONSTRAINT [PK_SYSTEM_ROLE_RIGHT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[System_RightObject] ADD  DEFAULT ((0)) FOR [DeleteFlag]
GO
ALTER TABLE [dbo].[System_Role] ADD  DEFAULT ((0)) FOR [DeleteFlag]
GO
ALTER TABLE [dbo].[System_UserInfo] ADD  CONSTRAINT [DF__System_Us__Delet__47A6A41B]  DEFAULT ((0)) FOR [DeleteFlag]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'IndexCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'IndexName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Unit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最小值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Min_Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Max_Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内部变量，计算函数用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Local_Var'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部变量，计算函数用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Out_Var'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部函数变量，计算函数用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Out_Var_Func'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计算公式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Expression'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示公式（包含注释）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Display_Exp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局计算公式外部函数变量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Full_Out_Var_Func'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局计算公式外部变量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Full_Out_Var'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局计算公式内部变量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Full_Local_Var'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局名计算公式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Full_Expression'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局显示公式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Full_Display_Exp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公式描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Exp_Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采集频率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Frequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=实时量；2=计算量；3=手工录入量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'IndexType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标修改标识(0:正常 1:新增 2:修改 3:删除)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'MFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否清零(0:不清零 1:清零)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Is_Clear_Zero'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否回写(0:不回写 1:回写)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Is_Write_Back'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Order_No'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标模版，包括测点指标和计算指标
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Base_ParameterIndexTemplet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'SectionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物探点号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'GisPoint'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连接点号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'LinkPoint'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点特征' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'PointFeature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'坐标X' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'Point_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'坐标Y' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'Point_Y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'埋深' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'Point_Z'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地面高程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'GroundHeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管线高程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'LineHeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管径或断面尺寸' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'SectionSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'材质' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'Material'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'埋设方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'BuryType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Basic_Locus', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电缆编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'CLNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电压等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Voltage_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'护套类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Sheath_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电缆截面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Fsection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大牵引力' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Max_Traction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大侧压力' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Max_Lateral_Pressure'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用状态 0=未使用  1=在使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'UserStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电缆信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cable_Info'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Content', @level2type=N'COLUMN',@level2name=N'DicType_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Content', @level2type=N'COLUMN',@level2name=N'DicTypeContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Content', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Content', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Content', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Type', @level2type=N'COLUMN',@level2name=N'TypeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Type', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Type', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Type', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'EquipmentTypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'型号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'ModelNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'ECode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'BarCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'Weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用途' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'UseInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备使用状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Equipment', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'SName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属工程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'ProjectId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电缆类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'CableType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'敷设长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'flength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起点地名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'StartAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起点经度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'StartPoint_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起点纬度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'StartPoint_Y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起点高程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'StartPoint_Z'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'终点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'EndAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'终点经度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'EndPoint_X'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'终点纬度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'EndPoint_Y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Laying_Path', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'ftime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'指标ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'PIndexId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'MonitorPointId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'fdata'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'fremark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监测数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Monitoring_Data'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'ProjectId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'SectionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'EquipmentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'ParameterId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'Position'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'X坐标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'PositionX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y坐标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'PositionY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Z坐标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'PositionZ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'位置序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'PositionSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'与前测点距离' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'Distance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用停机指令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'StopOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用开机指令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'StartOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否数据加密' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'IsEncryption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监测点表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonitorPoint'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'ProjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'ProjectName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Distance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程状态(0未施工1施工中2完工3停工)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'SectionCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回路数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'CircuitCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电流类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'ClectricityType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'敷设环境' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'LayEnvironment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'温度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Temperature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'湿度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Humidity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电压等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Voltage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截面尺寸' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'CutSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建设单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'BuildUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project_Info'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'SectionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'ProjectId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'Distance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地理位置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'Location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工段状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备间默认距离' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'DefaultEquipmentDistance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用预警短信' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'WarningSMS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收短信手机号列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'Phones'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Section', @level2type=N'COLUMN',@level2name=N'Delteted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'RightNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'RightText'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'IsVisible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=菜单 1=功能' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'MenumOrPower'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标元素' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'Ico'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'SortNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_RightObject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'RoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Right', @level2type=N'COLUMN',@level2name=N'RightId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能码ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Right', @level2type=N'COLUMN',@level2name=N'ObjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Right', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Right', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色功能表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Right'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户类型 1=平台用户 2=业主 3=商家 4=社区管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'UserType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邀请人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'Inviter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邀请码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'InviteCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'PictureIco'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色关联表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_UserOfRoles'
GO
USE [master]
GO
ALTER DATABASE [ICLDB] SET  READ_WRITE 
GO
