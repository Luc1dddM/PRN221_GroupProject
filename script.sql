USE [master]
GO
/****** Object:  Database [PRN221_GroupProject]    Script Date: 5/6/2024 3:34:57 PM ******/
CREATE DATABASE [PRN221_GroupProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN221_GroupProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PRN221_GroupProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN221_GroupProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PRN221_GroupProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PRN221_GroupProject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN221_GroupProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN221_GroupProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN221_GroupProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN221_GroupProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PRN221_GroupProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN221_GroupProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PRN221_GroupProject] SET  MULTI_USER 
GO
ALTER DATABASE [PRN221_GroupProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN221_GroupProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN221_GroupProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN221_GroupProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN221_GroupProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN221_GroupProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PRN221_GroupProject] SET QUERY_STORE = ON
GO
ALTER DATABASE [PRN221_GroupProject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PRN221_GroupProject]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[CartDetail] [nvarchar](36) NOT NULL,
	[id] [nchar](10) NOT NULL,
	[ProductId] [nvarchar](36) NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_CartDetaill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartHeader]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartHeader](
	[CartId] [nvarchar](36) NOT NULL,
	[Id] [nchar](10) NOT NULL,
	[CouponCode] [nvarchar](max) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CartHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[CouponId] [nvarchar](36) NOT NULL,
	[Id] [int] NOT NULL,
	[CouponCode] [nvarchar](max) NOT NULL,
	[DiscountAmount] [float] NOT NULL,
	[MinAmount] [float] NULL,
	[MaxAmount] [float] NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSend]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSend](
	[emailSendId] [nvarchar](36) NOT NULL,
	[id] [int] NOT NULL,
	[templateId] [nvarchar](36) NOT NULL,
	[senderId] [nvarchar](36) NOT NULL,
	[content] [text] NOT NULL,
	[sendate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailSend] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[emailTemplateId] [nvarchar](36) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NOT NULL,
	[subject] [nvarchar](max) NOT NULL,
	[body] [text] NOT NULL,
	[active] [bit] NOT NULL,
	[category] [nvarchar](max) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](36) NOT NULL,
	[updatedDate] [datetime] NULL,
	[updatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/6/2024 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [nvarchar](36) NOT NULL,
	[id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[Description] [text] NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[ImageURL] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240428070018_add-identity', N'8.0.4')
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 

INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'44649CC5-583F-4B31-B216-40854F783DD5', 1, N'Stim', N'Morbi non lectus.', N'Etiam justo. Etiam pretium iaculis justo. In hac habitasse platea dictumst. Etiam faucibus cursus urna.', N'Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat. In congue. Etiam justo. Etiam pretium iaculis justo. In hac habitasse platea dictumst. Etiam faucibus cursus urna.', 1, N'Lauritz', CAST(N'2023-10-06T00:00:00.000' AS DateTime), N'03/10/2024', NULL, NULL)
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'390FEA2D-F5BD-4D0A-9DED-25A9376EC554', 2, N'It', N'Duis bibendum.', N'Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim.', N'Nulla tellus.', 0, N'Michal', CAST(N'2023-09-16T00:00:00.000' AS DateTime), N'06/17/2023', NULL, NULL)
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'A0D8E110-F9FE-494E-88F0-A21E9B024E84', 6, N'Temp', N'Donec dapibus.', N'Fusce consequat. Nulla nisl. Nunc nisl. Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa.', N'Nam dui.', 0, N'Cleve', CAST(N'2023-08-20T00:00:00.000' AS DateTime), N'03/15/2024', NULL, NULL)
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'0AEC320C-2A57-447B-8C4D-D4EDEC963BA1', 8, N'Zontrax', N'Morbi porttitor lorem id ligula.', N'Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem. Integer tincidunt ante vel ipsum.', N'Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi. Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam.', 0, N'Lianne', CAST(N'2023-10-25T00:00:00.000' AS DateTime), N'11/08/2023', NULL, NULL)
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'0EAAC4AF-1A76-4B07-8CB0-E312F1F6003C', 9, N'It', N'In eleifend quam a odio.', N'In quis justo.', N'Phasellus in felis. Donec semper sapien a libero. Nam dui. Proin leo odio, porttitor id, consequat in, consequat ut, nulla. Sed accumsan felis. Ut at dolor quis odio consequat varius. Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.', 0, N'Buck', CAST(N'2023-10-09T00:00:00.000' AS DateTime), N'08/18/2023', NULL, NULL)
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'3630DAB7-2623-4903-B1AA-4CF19BDDB9D3', 15, N'Voyatouch', N'Aliquam erat volutpat.', N'Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis. Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem.', N'Vivamus tortor. Duis mattis egestas metus.', 1, N'Burty', CAST(N'2024-01-30T00:00:00.000' AS DateTime), N'11/16/2023', NULL, NULL)
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'481EFEF1-668E-4D39-A29E-52C2D30B8B77', 16, N'Tin', N'Nullam varius.', N'Pellentesque at nulla. Suspendisse potenti.', N'<p>Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat. Praesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede. Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus.</p>', 1, N'Coconut', CAST(N'2024-02-17T00:00:00.000' AS DateTime), N'08/23/2023', CAST(N'2024-05-06T13:48:12.070' AS DateTime), N'Current User')
INSERT [dbo].[EmailTemplate] ([emailTemplateId], [id], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (N'0B0E74F6-4EA4-4FC7-AB1A-FB286A96E900', 21, N'Test', N'tét', N'Test', N'<ol><li><strong>fsfsfsfsdfsfsfsfsf</strong></li></ol>', 1, N'Chocolate', CAST(N'2024-05-06T15:24:43.207' AS DateTime), N'Current User', CAST(N'2024-05-06T15:24:50.887' AS DateTime), N'Current User')
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF_CartDetaill_CartDetail]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CartDetail]
GO
ALTER TABLE [dbo].[CartHeader] ADD  CONSTRAINT [DF_CartHeader_CartId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CartId]
GO
ALTER TABLE [dbo].[Coupon] ADD  CONSTRAINT [DF_Coupon_CouponId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CouponId]
GO
ALTER TABLE [dbo].[EmailSend] ADD  CONSTRAINT [DF_EmailSend_emailSendId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailSendId]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF_EmailTemplate_emailTemplateId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailTemplateId]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ProductId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [ProductId]
GO
USE [master]
GO
ALTER DATABASE [PRN221_GroupProject] SET  READ_WRITE 
GO
