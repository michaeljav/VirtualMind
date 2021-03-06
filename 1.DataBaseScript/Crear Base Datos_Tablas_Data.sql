create database Currency
GO
USE [Currency]
GO
/****** Object:  Table [dbo].[CurrencyBuy]    Script Date: 06/08/2021 9:50:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyBuy](
	[CBuy_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Use_Id] [int] NOT NULL,
	[Use_Name] [nvarchar](100) NOT NULL,
	[CBuy_CurrencyOrigenType] [nvarchar](100) NOT NULL,
	[CBuy_CurrencyOrigenAmount] [decimal](10, 3) NOT NULL,
	[CBuy_CurrencyToBuyType] [nvarchar](100) NOT NULL,
	[CBuy_CurrencyToBuyRate] [decimal](10, 3) NOT NULL,
	[CBuy_CurrencyToBuyAmountCurrencyChanged] [decimal](10, 3) NOT NULL,
	[CBuy_CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CurrencyBuy] PRIMARY KEY CLUSTERED 
(
	[CBuy_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/08/2021 9:50:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Use_Id] [int] IDENTITY(1,1) NOT NULL,
	[Use_UserName] [nvarchar](100) NOT NULL,
	[Use_Password] [nvarchar](100) NOT NULL,
	[Use_Name] [nvarchar](100) NOT NULL,
	[Use_CreateDate] [datetime] NOT NULL,
	[Use_VersionDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Use_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Use_Id], [Use_UserName], [Use_Password], [Use_Name], [Use_CreateDate], [Use_VersionDate]) VALUES (1, N'mjavier', N'e10adc3949ba59abbe56e057f20f883e', N'Michael', CAST(N'2021-08-03T08:54:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[CurrencyBuy]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyBuy_User] FOREIGN KEY([Use_Id])
REFERENCES [dbo].[User] ([Use_Id])
GO
ALTER TABLE [dbo].[CurrencyBuy] CHECK CONSTRAINT [FK_CurrencyBuy_User]
GO
/****** Object:  StoredProcedure [dbo].[GetTotalMonth]    Script Date: 06/08/2021 9:50:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/

CREATE procedure [dbo].[GetTotalMonth] @userId int, @currentToBuyType nvarchar(100)
as
SELECT 
      [Use_Id]    
      ,[CBuy_CurrencyToBuyType]      
      ,sum([CBuy_CurrencyToBuyAmountCurrencyChanged]) as TotalMonth
      ,(convert(nvarchar,year(CBuy_CreateDate))+'-'+convert(nvarchar,MONTH(CBuy_CreateDate))) as DateYearMonth
  FROM [Currency].[dbo].[CurrencyBuy]
    where use_id = @userId  and [CBuy_CurrencyToBuyType] =@currentToBuyType
	  and  year(CBuy_CreateDate) = year(GETDATE()) and  month(CBuy_CreateDate) = month(GETDATE())
  group by Use_Id,[CBuy_CurrencyToBuyType],year(CBuy_CreateDate),MONTH(CBuy_CreateDate)
GO
