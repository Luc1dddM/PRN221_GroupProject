USE [master]
GO
/****** Object:  Database [PRN221_GroupProject]    Script Date: 6/11/2024 18:42:30 ******/
CREATE DATABASE [PRN221_GroupProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN221_GroupProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PRN221_GroupProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN221_GroupProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PRN221_GroupProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PRN221_GroupProject] SET COMPATIBILITY_LEVEL = 150
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/11/2024 18:42:30 ******/
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
	[DateOfBirth] [datetime2](7) NULL,
	[Gender] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[CartDetail]    Script Date: 6/11/2024 18:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CartDetailId] [nvarchar](36) NOT NULL,
	[ProductId] [nvarchar](36) NOT NULL,
	[Count] [int] NOT NULL,
	[CartId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](36) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](36) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK_CartDetaill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartHeader]    Script Date: 6/11/2024 18:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartHeader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](36) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK_CartHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[Coupon]    Script Date: 6/11/2024 18:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CouponId] [nvarchar](36) NOT NULL,
	[CouponCode] [nvarchar](max) NOT NULL,
	[DiscountAmount] [float] NOT NULL,
	[Status] [bit] NOT NULL,
	[MinAmount] [float] NULL,
	[MaxAmount] [float] NULL,
	[CreatedBy] [nvarchar](450) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](450) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Coupon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSend]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 6/11/2024 18:42:30 ******/
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
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 6/11/2024 18:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDetailId] [nvarchar](36) NOT NULL,
	[OrderHeaderId] [nvarchar](36) NOT NULL,
	[ProductId] [nvarchar](36) NOT NULL,
	[Count] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](36) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 6/11/2024 18:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[OrderHeaderId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[District] [nvarchar](50) NOT NULL,
	[Ward] [nvarchar](50) NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[OrderStatus] [nvarchar](50) NOT NULL,
	[TotalPrice] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](36) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](36) NULL,
	[CouponId] [nvarchar](36) NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/11/2024 18:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [nvarchar](36) NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Product_Category]    Script Date: 6/11/2024 18:42:30 ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240611070557_AddGenderandBirth', N'8.0.4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'92cbe301-a2ce-416b-8408-136ce9ca3b18', N'admin', N'admin', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c341ba9f-22e2-4fd8-8530-faf86d9a71aa', N'customer', N'customer', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'92cbe301-a2ce-416b-8408-136ce9ca3b18')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'558a2c26-711a-4395-960d-96679f5d957c', N'c341ba9f-22e2-4fd8-8530-faf86d9a71aa')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'99988b74-b4d3-487b-b52f-04247fbe6a64', N'c341ba9f-22e2-4fd8-8530-faf86d9a71aa')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ec5c44b5-8117-4557-9e47-922cfa455278', N'c341ba9f-22e2-4fd8-8530-faf86d9a71aa')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status], [DateOfBirth], [Gender]) VALUES (N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', N'tambnmce170642@fpt.edu.vn', N'TAMBNMCE170642@FPT.EDU.VN', N'tambnmce170642@fpt.edu.vn', N'TAMBNMCE170642@FPT.EDU.VN', 1, N'AQAAAAIAAYagAAAAEHBbnZXFps544PoGfXQeSYPsTYqHXxpvicZ/+fUJmmml2yFTOTkcc5QohF1ROEOpQQ==', N'B7452UCCCQNMIJJ6GJO27HOQOD5TFZEY', N'29e04c8d-86ae-488d-8456-861a229baa18', N'012345678', 0, 0, NULL, 1, 0, N'Tammm', 1, CAST(N'2003-06-03T00:00:00.0000000' AS DateTime2), N'Female')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status], [DateOfBirth], [Gender]) VALUES (N'558a2c26-711a-4395-960d-96679f5d957c', N'phattqce170538@fpt.edu.vn', N'PHATTQCE170538@FPT.EDU.VN', N'phattqce170538@fpt.edu.vn', N'PHATTQCE170538@FPT.EDU.VN', 1, N'AQAAAAIAAYagAAAAEHjehMtF8d9ca25XI1qszEG1q+kPTTWG7epLwxsEhR6PLkIi+9nXuOcR8hZUrjgfEg==', N'CYZ54CQ4TTPXFYSC2HTF353MC6L3S76I', N'4bb83366-6bd3-4ed5-b1ed-af73582e4d8a', N'1234565412', 0, 0, NULL, 1, 0, N'phat', 1, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status], [DateOfBirth], [Gender]) VALUES (N'99988b74-b4d3-487b-b52f-04247fbe6a64', N'hvuthai123@gmail.com', N'HVUTHAI123@GMAIL.COM', N'hvuthai123@gmail.com', N'HVUTHAI123@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPgOyEltRPpRrJEOcHNrF4WNBEZBrZZyzxltPgz6yIGN6bEC4K9RpxamAd4Mqeyqog==', N'FUAI7WH7HWZ4SKU7LJ2DOGAGIQV5H6QY', N'9bcfb467-a3d5-4cb4-b7ad-c693b5c99ec2', N'1234565412', 0, 0, NULL, 1, 0, N'hvuthai', 1, NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [Status], [DateOfBirth], [Gender]) VALUES (N'ec5c44b5-8117-4557-9e47-922cfa455278', N'buingocminhtam2k3@gmail.com', N'BUINGOCMINHTAM2K3@GMAIL.COM', N'buingocminhtam2k3@gmail.com', N'BUINGOCMINHTAM2K3@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEEhRI10ysVm6DadMeXKhXaD4br+rU6IDLSRjDSFBnIFm1IshbqqA5UKvjkQg8ZtEbQ==', N'WHMAH4X2CQ4DIC6L22YKVSP64J4XBY5W', N'd2b8af91-6b59-4062-a533-d7bdb4b43f57', N'987654321000', 0, 0, NULL, 1, 0, N'MinTam', 0, NULL, N'Female')
GO
SET IDENTITY_INSERT [dbo].[CartHeader] ON 

INSERT [dbo].[CartHeader] ([Id], [CartId], [UserId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'9F1CEED0-A9BB-4B88-A6C9-AEBB770D89F7', N'558a2c26-711a-4395-960d-96679f5d957c', CAST(N'2024-06-09T22:45:04.890' AS DateTime), N'558a2c26-711a-4395-960d-96679f5d957c', NULL, NULL)
SET IDENTITY_INSERT [dbo].[CartHeader] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (1, N'9EA5B651-D194-4AE2-B269-3152F0A60BEE', N'Asus', N'Brand', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:07:46.753' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:07:46.753' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (2, N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'Razer', N'Brand', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:09:16.967' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:09:16.967' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (3, N'5361F644-1D4F-4A53-9D0A-F76F1D9896E7', N'Razer', N'Brand', N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-05T21:59:58.273' AS DateTime), N'unknow', CAST(N'2024-06-05T21:59:57.443' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (4, N'91EC12CE-C675-4D34-889F-54D36BACD616', N'Black', N'Color', N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-05T22:00:58.657' AS DateTime), N'unknow', CAST(N'2024-06-05T22:00:58.657' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (5, N'3E23F2F7-5405-402D-A627-A2A00CFC34C3', N'MSI', N'Brand', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:10:50.503' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:10:50.503' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (6, N'F5F68933-DB01-4346-8FB8-D7AABFDEA777', N'Mouse', N'Device', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:10:58.907' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:10:58.907' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (7, N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'Keyboard', N'Device', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:11:18.333' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:11:18.333' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (8, N'13522664-A26D-4FC0-BD13-BC1E6F66D71D', N'Screen', N'Device', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:11:27.963' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:11:27.963' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (9, N'59820EA2-84C8-42A0-A084-6703557F5A65', N'Black', N'Color', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:12:15.160' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:12:15.160' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (10, N'A9B86F03-8348-4088-A323-C499B231DF0C', N'White', N'Color', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:12:23.710' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:12:23.710' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (11, N'CE955E40-06D8-4DDD-9708-BF7AD305C471', N'Pink', N'Color', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:12:31.727' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:13:17.950' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (12, N'8E44545E-8A1C-4C09-B9A6-21E381E121E8', N'ANGRY MIAO', N'Brand', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:03.780' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:03.780' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (13, N'16DB658F-02B8-4307-B820-7603FD179007', N'Purple', N'Color', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:45:49.677' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:45:49.677' AS DateTime), 1)
INSERT [dbo].[Category] ([Id], [CategoryId], [Name], [Type], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (14, N'C94C122B-A8F9-4516-83AD-D2F79C281978', N'Durgod', N'Brand', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:27.570' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:27.570' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 

INSERT [dbo].[EmailTemplate] ([id], [emailTemplateId], [name], [description], [subject], [body], [active], [category], [createdDate], [createdBy], [updatedDate], [updatedBy]) VALUES (1, N'624FB07E-275F-4FAC-89D7-34D6370EF61C', N'fsfsf', N'fsfsfsdfsdf', N'sfsdfsdf', N'<p>sfsf</p>', 1, N'fssdfsfs', CAST(N'2024-05-26T19:58:37.150' AS DateTime), N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'7CBC766E-1309-46E8-BBB8-6E20171C2A45', 0, N'Razer Pro Click Humanscale Mouse | Wireless', 2290000, N'Razer Pro Click Humanscale Mouse | Wireless', N'Pro-Click-Humanscale.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.437' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.437' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'1969E117-324B-4673-B2B8-F6E90C625906', 1, N'Razer DeathAdder V2 Pro Mouse | Wireless', 1990000, N'Razer DeathAdder V2 Pro Mouse | Wireless', N'DeathAdder-V2-Pro.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:04.970' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:05.133' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'9CB2132D-3792-424D-86B1-670C2F3B0BCE', 2, N'Razer DeathAdder V3-Ultra Lightweight Mouse | Corded - Super light', 1890000, N'Razer DeathAdder V3-Ultra Lightweight Mouse | Corded - Super light', N'DeathAdder-V3-Ultra-Lightweight.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.347' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.347' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'955D1B61-BA2F-4BCC-98CE-C4BAFE4E4498', 3, N'Razer Viper V2 Pro-Wireless Mouse PUBG: Battlegrounds Edition', 3490000, N'Razer Viper V2 Pro-Wireless Mouse PUBG: Battlegrounds Edition', N'PUBG-Battlegrounds-Edition.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.807' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.807' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'B9F290AA-DE51-47D2-875A-9EBA5B58814F', 4, N'Razer Huntsman V3 Pro Tenkeyless mechanical keyboard | Wired - Razer Analog Optical Switch Gen-2', 5290000, N'Razer Huntsman V3 Pro Tenkeyless mechanical keyboard | Wired - Razer Analog Optical Switch Gen-2', N'9303-ban-phim-razer-v3-pro-tenkeyless-1.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.793' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.793' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'AC2C16C1-E140-4362-98CC-C1DC50335A26', 5, N'Razer Huntsman V2 Tenkeyless Pink Keyboard | Quartz Edition | Wired', 4199000, N'Razer Huntsman V2 Tenkeyless Pink Keyboard | Quartz Edition | Wired', N'Huntsman-V2-Tenkeyless-Hồng.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.090' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.090' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'B5855DAD-7BFC-4F5C-9094-08BFCE83CCA2', 6, N'Razer BlackWidow V4 Keyboard 75% | White | Hotswap', 3790000, N'Razer BlackWidow V4 Keyboard 75% | White | Hotswap', N'BlackWidow-V4-75%.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.237' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.237' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'90EF9336-0224-4531-B97F-336A5D8B5F04', 7, N'Razer Huntsman V2-Optical Gaming Keyboard-PUBG: Battlegrounds Edition', 6290000, N'Razer Huntsman V2-Optical Gaming Keyboard-PUBG: Battlegrounds Edition', N'PUBG-Battlegrounds-Edition-1.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:42:48.893' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T15:57:06.107' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'F1BF6A94-4C9A-49C8-A8F2-1CC2B32469D4', 8, N'Dry Studio Black Diamond 75 Forged Carbon Bundle x Angry Miao Keyboard | Advance version', 9490000, N'Dry Studio Black Diamond 75 Forged Carbon Bundle x Angry Miao Keyboard | Advance version', N'Black_Diamond_75_V21.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.207' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.207' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'BF742E37-5C8A-444C-91BC-CE8FEEB4627A', 9, N'Bàn Phím Durgod Cavalry 87 V2 Purple | 3 Mode - Hotswap - RGB', 1550000, N'Bàn Phím Durgod Cavalry 87 V2 Purple | 3 Mode - Hotswap - RGB', N'Durgod-cavalry-Purple-2.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.803' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.803' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'8C849FB9-2833-4726-81A1-6E95C12D6A30', 10, N'MSI Optix MAG274QRX monitor | 27 inches', 10490000, N'MSI Optix MAG274QRX monitor | 27 inches', N'MAG274QRX.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.190' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.190' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'02DD3A81-5314-4241-B649-19F84544607A', 11, N'Msi Optix G27C5 - 165Hz - 27inch', 6590000, N'Msi Optix G27C5 - 165Hz - 27inch', N'Gearshop_MSI_G27C5_1.jpg', N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.950' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T16:26:04.160' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductId], [id], [Name], [Price], [Description], [ImageURL], [CreatedBy], [Created_at], [UpdatedBy], [Updated_at], [Status]) VALUES (N'4B0F5823-D596-4D1F-8D5F-4CAECF13A5B5', 12, N'Anh Lam', 1500000000, N'Khong co', N'Screenshot 2024-04-08 203829.png', N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-09T21:45:18.080' AS DateTime), N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-09T21:46:31.770' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Product_Category] ON 

INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (1, N'AFC67353-7682-4024-B0EF-C1C6DFCAF427', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'7CBC766E-1309-46E8-BBB8-6E20171C2A45', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.637' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.637' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (2, N'E70D23B4-54AE-446F-849F-B300C3DC0047', N'F5F68933-DB01-4346-8FB8-D7AABFDEA777', N'7CBC766E-1309-46E8-BBB8-6E20171C2A45', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.687' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.687' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (3, N'3B9CEBB9-305B-4C29-B1D3-B29D2C82622D', N'A9B86F03-8348-4088-A323-C499B231DF0C', N'7CBC766E-1309-46E8-BBB8-6E20171C2A45', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.690' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T07:52:43.690' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (4, N'BB867809-0501-436F-8511-E16E6E41F988', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'1969E117-324B-4673-B2B8-F6E90C625906', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:13.123' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:13.123' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (5, N'D9738034-E26C-4DC0-BF88-E3A33AF638EB', N'F5F68933-DB01-4346-8FB8-D7AABFDEA777', N'1969E117-324B-4673-B2B8-F6E90C625906', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:13.180' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:13.180' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (6, N'6885EBEB-5104-4D75-AEF6-52044131632F', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'1969E117-324B-4673-B2B8-F6E90C625906', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:13.187' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:33:13.187' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (7, N'B96B4F0A-2E2D-4990-A0AE-133EF7AB665D', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'9CB2132D-3792-424D-86B1-670C2F3B0BCE', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.480' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.480' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (8, N'6953ED17-757E-4441-BBC8-C735BC3BCEC5', N'F5F68933-DB01-4346-8FB8-D7AABFDEA777', N'9CB2132D-3792-424D-86B1-670C2F3B0BCE', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.527' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.527' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (9, N'A0E67738-F54D-44FB-9634-C0A66D8D3322', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'9CB2132D-3792-424D-86B1-670C2F3B0BCE', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.530' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:36:40.530' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (10, N'858B01DB-5381-4206-B0D2-AD8B359C880A', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'955D1B61-BA2F-4BCC-98CE-C4BAFE4E4498', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.817' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.817' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (11, N'19A1FFFF-966F-4268-826E-1B3C58EAD63B', N'F5F68933-DB01-4346-8FB8-D7AABFDEA777', N'955D1B61-BA2F-4BCC-98CE-C4BAFE4E4498', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.820' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.820' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (12, N'36CF519B-52CA-4D8C-BC89-581021126D42', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'955D1B61-BA2F-4BCC-98CE-C4BAFE4E4498', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.823' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:38:11.823' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (13, N'72254CC9-E589-40E4-A792-2F4119677D70', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'B9F290AA-DE51-47D2-875A-9EBA5B58814F', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.800' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.800' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (14, N'736DE0BD-AC76-49C5-8417-155409147D50', N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'B9F290AA-DE51-47D2-875A-9EBA5B58814F', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.807' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.807' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (15, N'5F5BE5EB-DD39-477D-A56D-40D28EDEF44A', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'B9F290AA-DE51-47D2-875A-9EBA5B58814F', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.807' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:39:56.807' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (16, N'AE8D6D44-BBD6-4FDD-BBE9-04CD0DC26376', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'AC2C16C1-E140-4362-98CC-C1DC50335A26', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.097' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.097' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (17, N'C510D4B7-BBBF-4AFE-8575-9E6F75C0E876', N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'AC2C16C1-E140-4362-98CC-C1DC50335A26', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.100' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.100' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (18, N'A5BCE303-1930-4FD4-A79B-AAAFA742685F', N'CE955E40-06D8-4DDD-9708-BF7AD305C471', N'AC2C16C1-E140-4362-98CC-C1DC50335A26', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.103' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:40:54.103' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (19, N'58115750-8154-4058-B698-E5FEE1F4D781', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'B5855DAD-7BFC-4F5C-9094-08BFCE83CCA2', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.247' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.247' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (20, N'4CE0EB5B-561E-4706-B12C-1B9099479CDC', N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'B5855DAD-7BFC-4F5C-9094-08BFCE83CCA2', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.257' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.257' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (21, N'C448AFAC-0CF3-41D1-BD8F-DDEF0DE0D717', N'A9B86F03-8348-4088-A323-C499B231DF0C', N'B5855DAD-7BFC-4F5C-9094-08BFCE83CCA2', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.267' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:41:56.267' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (23, N'30EEBE72-8468-4D4F-9476-A4CF6491A7BC', N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'90EF9336-0224-4531-B97F-336A5D8B5F04', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:42:48.903' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:42:48.903' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (24, N'3AEDFFC5-4AE1-4CFC-8106-4BF1247B3063', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'90EF9336-0224-4531-B97F-336A5D8B5F04', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:42:48.907' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:42:48.907' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (25, N'D02862DA-D042-41A9-B254-25009850D014', N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'F1BF6A94-4C9A-49C8-A8F2-1CC2B32469D4', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.213' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.213' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (26, N'D8480354-B0FD-4B68-90E7-4E925E48D95D', N'8E44545E-8A1C-4C09-B9A6-21E381E121E8', N'F1BF6A94-4C9A-49C8-A8F2-1CC2B32469D4', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.217' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.217' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (27, N'A0D507D1-1DA3-47E6-A54A-76A475867CF5', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'F1BF6A94-4C9A-49C8-A8F2-1CC2B32469D4', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.220' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:44:56.220' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (28, N'71CB9823-C0CF-4A8D-AB99-A764E4F245F8', N'5CC90372-F04B-4243-BF4A-5D197DB949D5', N'BF742E37-5C8A-444C-91BC-CE8FEEB4627A', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.810' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.810' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (29, N'C8CF860B-3115-4D76-9936-B53BD3DE338A', N'C94C122B-A8F9-4516-83AD-D2F79C281978', N'BF742E37-5C8A-444C-91BC-CE8FEEB4627A', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.813' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.813' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (30, N'5947A0A3-E7B3-42BF-ADA0-C2C003C0EA6A', N'16DB658F-02B8-4307-B820-7603FD179007', N'BF742E37-5C8A-444C-91BC-CE8FEEB4627A', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.817' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:46:57.817' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (31, N'BE231CDD-3BAC-4422-A2AD-52A675179CE8', N'3E23F2F7-5405-402D-A627-A2A00CFC34C3', N'8C849FB9-2833-4726-81A1-6E95C12D6A30', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.197' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.197' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (32, N'8A36659E-773A-4F38-BDEE-E2D5D34987E8', N'13522664-A26D-4FC0-BD13-BC1E6F66D71D', N'8C849FB9-2833-4726-81A1-6E95C12D6A30', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.200' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.200' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (33, N'7DBD06C5-82AB-40F6-86AE-8751529A72A3', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'8C849FB9-2833-4726-81A1-6E95C12D6A30', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.200' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:01.200' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (34, N'8400A86C-1C86-4678-A2FE-C9942F840EE3', N'3E23F2F7-5405-402D-A627-A2A00CFC34C3', N'02DD3A81-5314-4241-B649-19F84544607A', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.953' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.953' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (35, N'CFB5294F-740D-406B-B238-26B5BA0B13B4', N'13522664-A26D-4FC0-BD13-BC1E6F66D71D', N'02DD3A81-5314-4241-B649-19F84544607A', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.953' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.953' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (36, N'A3040DA0-06AE-4A30-84FC-612361885BF7', N'59820EA2-84C8-42A0-A084-6703557F5A65', N'02DD3A81-5314-4241-B649-19F84544607A', 50, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.957' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T08:48:48.957' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (37, N'58168594-EEC1-4BEF-A918-D5603D26D2F5', N'F5911F2D-DFF8-4370-A776-9C91DCC2B7C9', N'90EF9336-0224-4531-B97F-336A5D8B5F04', 0, 1, N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T16:02:59.357' AS DateTime), N'99988b74-b4d3-487b-b52f-04247fbe6a64', CAST(N'2024-06-09T16:02:59.357' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (38, N'B7FD9751-ECF7-4538-91D4-A56FFF4907EE', N'5361F644-1D4F-4A53-9D0A-F76F1D9896E7', N'4B0F5823-D596-4D1F-8D5F-4CAECF13A5B5', 0, 1, N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-09T21:45:18.127' AS DateTime), N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-09T21:45:18.127' AS DateTime))
INSERT [dbo].[Product_Category] ([id], [Product_CategoryId], [CategoryId], [ProductId], [Quantity], [Status], [CreatedBy], [CreatedAt], [Updatedby], [UpdatedAt]) VALUES (39, N'54F7620C-66D5-40DA-B10F-DF4317E61799', N'13522664-A26D-4FC0-BD13-BC1E6F66D71D', N'4B0F5823-D596-4D1F-8D5F-4CAECF13A5B5', 0, 1, N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-09T21:45:18.143' AS DateTime), N'38f4313a-01f0-4fc7-ac47-8b2c2e0d6ad3', CAST(N'2024-06-09T21:45:18.143' AS DateTime))
SET IDENTITY_INSERT [dbo].[Product_Category] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CartDetail]    Script Date: 6/11/2024 18:42:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CartDetail] ON [dbo].[CartDetail]
(
	[CartDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CartHeader]    Script Date: 6/11/2024 18:42:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CartHeader] ON [dbo].[CartHeader]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Category]    Script Date: 6/11/2024 18:42:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Category] ON [dbo].[Category]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Coupon]    Script Date: 6/11/2024 18:42:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Coupon] ON [dbo].[Coupon]
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderHeader]    Script Date: 6/11/2024 18:42:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderHeader] ON [dbo].[OrderHeader]
(
	[OrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Product]    Script Date: 6/11/2024 18:42:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Product] ON [dbo].[Product]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Status]
GO
ALTER TABLE [dbo].[CartDetail] ADD  CONSTRAINT [DF__CartDetai__CartD__66603565]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [CartDetailId]
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
ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_OrderDetailId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [OrderDetailId]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_OrderHeaderId]  DEFAULT (CONVERT([nvarchar](36),newid())) FOR [OrderHeaderId]
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
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_CartHeader] FOREIGN KEY([CartId])
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
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_AspNetUsers]
GO
ALTER TABLE [dbo].[Coupon]  WITH CHECK ADD  CONSTRAINT [FK_Coupon_AspNetUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Coupon] CHECK CONSTRAINT [FK_Coupon_AspNetUsers]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplate_AspNetUsers] FOREIGN KEY([createdBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_AspNetUsers]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_OrderHeader] FOREIGN KEY([OrderHeaderId])
REFERENCES [dbo].[OrderHeader] ([OrderHeaderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_OrderHeader]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_AspNetUsers]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_Coupon] FOREIGN KEY([CouponId])
REFERENCES [dbo].[Coupon] ([CouponId])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_Coupon]
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
