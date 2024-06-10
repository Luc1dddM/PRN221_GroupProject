USE [master]
GO
/****** Object:  Database [PRN221_GroupProject]    Script Date: 5/26/2024 8:04:54 PM ******/
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
ALTER DATABASE [PRN221_GroupProject] SET AUTO_CLOSE ON 
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
ALTER DATABASE [PRN221_GroupProject] SET  ENABLE_BROKER 
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/26/2024 8:04:54 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[id] [nchar](10) NOT NULL,
	[CartDetail] [nvarchar](36) NOT NULL,
	[ProductId] [nvarchar](36) NOT NULL,
	[Count] [int] NOT NULL,
	[CarId] [nvarchar](36) NOT NULL,
 CONSTRAINT [PK_CartDetaill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartHeader]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartHeader](
	[Id] [nchar](10) NOT NULL,
	[CartId] [nvarchar](36) NOT NULL,
	[CouponId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_CartHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
	[Updated_at] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupon]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CouponId] [nvarchar](36) NOT NULL,
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
/****** Object:  Table [dbo].[EmailSend]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSend](
	[id] [int] NOT NULL,
	[emailSendId] [nvarchar](36) NOT NULL,
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
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[emailTemplateId] [nvarchar](36) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NOT NULL,
	[subject] [nvarchar](max) NOT NULL,
	[body] [text] NOT NULL,
	[active] [bit] NOT NULL,
	[category] [nvarchar](max) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](450) NOT NULL,
	[updatedDate] [datetime] NULL,
	[updatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [nvarchar](36) NOT NULL,
	[id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Description] [text] NULL,
	[ImageURL] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
	[Updated_at] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Product_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Category]    Script Date: 5/26/2024 8:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Product_CategoryId] [nvarchar](36) NOT NULL,
	[CategoryId] [nvarchar](36) NOT NULL,
	[ProductId] [nvarchar](36) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Updatedby] [nvarchar](450) NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240507123421_addIdentityTables', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240507142200_ExtendIdentityUser', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240508122251_addInitialCreate', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240508122553_adđtb', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240508154609_addApplicationUser', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240508154936_addAplication', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240508162156_fixname', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240522101322_test', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240526025133_AddColumStatus', N'8.0.4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'1192820c-54c4-4f5c-b108-9342c0938c74')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'92cbe301-a2ce-416b-8408-136ce9ca3b18', N'admin', N'admin', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c341ba9f-22e2-4fd8-8530-faf86d9a71aa', N'customer', N'customer', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'92cbe301-a2ce-416b-8408-136ce9ca3b18')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status]) VALUES (N'345679e7-d142-481e-8113-4cbc3986c8f9', N'tam@gmail.com', N'TAM@GMAIL.COM', N'tam@gmail.com', N'TAM@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEAi573SZWj/nynUauyQbaZxMMb6TDmnj6qJR2cr87mQ+TnVQyo19NKpIEj5MiwwCpA==', N'NQZEEMYFYTJ6KKGDETGYK32M7OAHKSI3', N'1fa96f6c-ad22-4b27-8688-e0516b5f83cc', NULL, 0, 0, NULL, 1, 0, N'tstt', 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status]) VALUES (N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'tambnmce170642@fpt.edu.vn', N'TAMBNMCE170642@FPT.EDU.VN', N'tambnmce170642@fpt.edu.vn', N'TAMBNMCE170642@FPT.EDU.VN', 1, N'AQAAAAIAAYagAAAAEHBbnZXFps544PoGfXQeSYPsTYqHXxpvicZ/+fUJmmml2yFTOTkcc5QohF1ROEOpQQ==', N'B7452UCCCQNMIJJ6GJO27HOQOD5TFZEY', N'f973dc26-de45-4cb0-98b5-21db64673a50', N'012345678', 0, 0, NULL, 1, 0, N'Tammm', 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status]) VALUES (N'6d3db275-e809-498e-88a7-2bd9a7cdc5a5', N'tstt@gmail.com', N'TSTT@GMAIL.COM', N'tstt@gmail.com', N'TSTT@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEB1BlHhNU98xDAyBemRCEkkqKfQv44Yp7YAQZpfEX660m/BbGxnyxAir/+IKx93qNQ==', N'2EGIBAVZGJXX4U5MSIH5DVXGSW5ARB5V', N'b685999d-5392-4bfc-8959-b4f1fd54aab9', NULL, 0, 0, NULL, 1, 0, N'Tammm', 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status]) VALUES (N'be8c8091-c25e-46c0-b9b0-a4086e8eb651', N'test@gmail.com', N'TEST@GMAIL.COM', N'test@gmail.com', N'TEST@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEFtc4Gy+Txr3nvATnSaM8/s+ogrMJ4P/rYhnScoZffMV55oo3thNIa7DIB0i//SV5A==', N'7XOCI2NSDNWY2SC7MXWBLGQD7VUKKDAG', N'61545c55-36cc-4eff-a282-a94a334a9ad0', NULL, 0, 0, NULL, 1, 0, N'test', 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status]) VALUES (N'ec5c44b5-8117-4557-9e47-922cfa455278', N'buingocminhtam2k3@gmail.com', N'BUINGOCMINHTAM2K3@GMAIL.COM', N'buingocminhtam2k3@gmail.com', N'BUINGOCMINHTAM2K3@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEEhRI10ysVm6DadMeXKhXaD4br+rU6IDLSRjDSFBnIFm1IshbqqA5UKvjkQg8ZtEbQ==', N'WHMAH4X2CQ4DIC6L22YKVSP64J4XBY5W', N'6bf475c6-a470-47e9-b8a1-11337c17b530', N'987654321000', 0, 0, NULL, 1, 0, N'MinTam', 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status]) VALUES (N'f0f740f0-f039-43a2-9c77-53d79168132d', N'hey@gmail.com', N'HEY@GMAIL.COM', N'hey@gmail.com', N'HEY@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEBwOQz2vv+JgE2qmG7rW79Vzcpkl3E8ns4fNaEuIN1tsiY5NnX4qNYiHtbN28TSU/A==', N'SVHOUMFDVWYJI6WTGBKGHTVJXRMLIKRP', N'c9fe5beb-a7fd-46ea-8c25-12e9a5c67ac8', NULL, 0, 0, NULL, 1, 0, N'testtt', 0)
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 

INSERT [dbo].[EmailTemplate] ([id], [emailTemplateId], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (1, N'624FB07E-275F-4FAC-89D7-34D6370EF61C', N'fsfsf', N'fsfsfsdfsdf', N'sfsdfsdf', N'<p>sfsf</p>', 1, N'fssdfsfs', CAST(N'2024-05-26T19:58:37.150' AS DateTime), N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CartHeader]    Script Date: 5/26/2024 8:04:54 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CartHeader] ON [dbo].[CartHeader]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Category]    Script Date: 5/26/2024 8:04:54 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Category] ON [dbo].[Category]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Coupon]    Script Date: 5/26/2024 8:04:54 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Coupon] ON [dbo].[Coupon]
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Product]    Script Date: 5/26/2024 8:04:54 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Product] ON [dbo].[Product]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Status]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF__CartDetai__CartD__66603565]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CartDetail]
GO
ALTER TABLE [dbo].[CartHeader] ADD  CONSTRAINT [DF__CartHeade__CartI__6754599E]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CartId]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CategoryId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CategoryId]
GO
ALTER TABLE [dbo].[Coupon] ADD  CONSTRAINT [DF__Coupon__CouponId__693CA210]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CouponId]
GO
ALTER TABLE [dbo].[EmailSend] ADD  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailSendId]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemp__email__6B24EA82]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [emailTemplateId]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ProductId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [ProductId]
GO
ALTER TABLE [dbo].[Product_Category] ADD  CONSTRAINT [DF_Product_Category_Product_CategoryId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [Product_CategoryId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_CartHeader] FOREIGN KEY([CarId])
REFERENCES [dbo].[CartHeader] ([CartId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_CartHeader]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Product]
GO
ALTER TABLE [dbo].[CartHeader]  WITH CHECK ADD  CONSTRAINT [FK_CartHeader_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CartHeader] CHECK CONSTRAINT [FK_CartHeader_AspNetUsers]
GO
ALTER TABLE [dbo].[CartHeader]  WITH CHECK ADD  CONSTRAINT [FK_CartHeader_Coupon] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupon] ([CouponId])
GO
ALTER TABLE [dbo].[CartHeader] CHECK CONSTRAINT [FK_CartHeader_Coupon]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_AspNetUsers]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplate_AspNetUsers] FOREIGN KEY([createdBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_AspNetUsers]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_AspNetUsers]
GO
ALTER TABLE [dbo].[Product_Category]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Product_Category] CHECK CONSTRAINT [FK_Product_Category_AspNetUsers]
GO
ALTER TABLE [dbo].[Product_Category]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product_Category] CHECK CONSTRAINT [FK_Product_Category_Category]
GO
ALTER TABLE [dbo].[Product_Category]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Product_Category] CHECK CONSTRAINT [FK_Product_Category_Product]
GO
USE [master]
GO
ALTER DATABASE [PRN221_GroupProject] SET  READ_WRITE 
GO
