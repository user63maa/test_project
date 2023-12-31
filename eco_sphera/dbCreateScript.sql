USE [Ecosystem]
GO
/****** Object:  User [app_ecosystem_rw]    Script Date: 04.10.2023 21:30:55 ******/
CREATE USER [app_ecosystem_rw] FOR LOGIN [app_ecosystem_rw] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [GIPROVOSTOKNEFT\sql_ecosystem_o]    Script Date: 04.10.2023 21:30:55 ******/
CREATE USER [GIPROVOSTOKNEFT\sql_ecosystem_o] FOR LOGIN [GIPROVOSTOKNEFT\sql_ecosystem_o]
GO
ALTER ROLE [db_datareader] ADD MEMBER [app_ecosystem_rw]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [app_ecosystem_rw]
GO
ALTER ROLE [db_owner] ADD MEMBER [GIPROVOSTOKNEFT\sql_ecosystem_o]
GO
/****** Object:  Table [dbo].[Access]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Access](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PersonnelLogin] [varchar](50) NULL,
	[Comment] [varchar](100) NULL,
	[Date] [datetime] NULL,
	[AccessRoles_id] [int] NULL,
 CONSTRAINT [PK_Access] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessRoles]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessRoles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](500) NULL,
 CONSTRAINT [PK_AccessRoles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalculationCompositionForm]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalculationCompositionForm](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ControlName] [varchar](100) NULL,
	[ControlValue] [varchar](50) NULL,
	[CalculationResult_id] [int] NULL,
 CONSTRAINT [PK_CalculationCompositionForm] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalculationCompositionForReport]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalculationCompositionForReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CellValue] [varchar](500) NULL,
	[CalculationResult_id] [int] NULL,
	[ColNumber] [int] NULL,
	[RowNumber] [int] NULL,
 CONSTRAINT [PK_CalculationComposition] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalculationResult]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalculationResult](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SourceOfEmissionFuel_id] [int] NULL,
	[ResultSum] [float] NULL,
	[SaveDate] [datetime] NULL,
	[PersonnelLogin] [varchar](50) NULL,
 CONSTRAINT [PK_CalculationResult] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryOfFuel]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryOfFuel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](500) NULL,
	[FuelTableName] [varchar](100) NULL,
	[SumCategoryName] [varchar](500) NULL,
 CONSTRAINT [PK_CategoryOfFuel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryOfFuelColumnsDictionary]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryOfFuelColumnsDictionary](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Characteristic] [varchar](500) NULL,
	[isHasDefValue] [bit] NULL,
 CONSTRAINT [PK_CategoryOfFuelColumnsDictionary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyDO]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyDO](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[ShortName] [varchar](100) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_CompanyDO] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnergySystem]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnergySystem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Coef1] [int] NULL,
	[Coef2] [int] NULL,
 CONSTRAINT [PK_EnergySystem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlareCondition]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlareCondition](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ConditionName] [varchar](500) NULL,
	[CoefficientUnderburning] [float] NULL,
 CONSTRAINT [PK_FlareCondition] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlareMeasurementCondition]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlareMeasurementCondition](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Condition] [nvarchar](500) NULL,
	[DencityCO2] [float] NULL,
	[DencityCH4] [float] NULL,
 CONSTRAINT [PK_FlareMeasurementCondition] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupOfFuel]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupOfFuel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](500) NULL,
 CONSTRAINT [PK_GroupOfFuel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionSite]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionSite](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyDO_id] [int] NOT NULL,
	[Name] [varchar](1000) NULL,
	[DateAdd] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[AdministrativeArea] [varchar](500) NULL,
	[Region] [varchar](500) NULL,
 CONSTRAINT [PK_ProductionSite] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceOfEmission]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceOfEmission](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProductionSite_id] [int] NOT NULL,
	[Name] [varchar](500) NULL,
	[DateAdd] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_SourceOfEmission] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SourceOfEmissionFuel]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceOfEmissionFuel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SourceOfEmission_id] [int] NOT NULL,
	[CategoryOfFuel_id] [int] NULL,
	[TypeOfFuelTable_id] [int] NULL,
	[TypeOfFuelName] [varchar](500) NULL,
	[isDeleted] [bit] NULL,
 CONSTRAINT [PK_SourceOfEmissionFuel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfFuel]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfFuel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Dimension] [varchar](50) NULL,
	[ConversionFactor1] [float] NULL,
	[ConversionFactor2] [float] NULL,
	[EmissionFactor1] [float] NULL,
	[EmissionFactor2] [float] NULL,
	[CarbonContent1] [float] NULL,
	[CarbonContent2] [float] NULL,
	[GroupOfFuel_id] [int] NULL,
 CONSTRAINT [PK_TypeOfFuelWithCoef] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfFuelForFlareCombustion]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfFuelForFlareCombustion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[EmissionFactorCO2] [float] NULL,
	[EmissionFactorCH4] [float] NULL,
 CONSTRAINT [PK_TypeOfFuelForFlareCombustion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfFuelForFugitivEmission]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfFuelForFugitivEmission](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[CO2Content] [float] NULL,
	[CH4Content] [float] NULL,
 CONSTRAINT [PK_TypeOfFuelForFugitivEmission] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfFuelForTransport]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfFuelForTransport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmissionFactor] [float] NULL,
	[ConversionFactor1] [float] NULL,
	[ConversionFactor2] [float] NULL,
	[Density] [float] NULL,
	[Name] [varchar](500) NULL,
 CONSTRAINT [PK_TypeOfFuelForTransport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypesForCategoryFuel]    Script Date: 04.10.2023 21:30:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypesForCategoryFuel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryOfFuel_id] [int] NOT NULL,
	[TypeOfFuel_id] [int] NOT NULL,
 CONSTRAINT [PK_TypesForCategoryFuel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CalculationResult] ADD  CONSTRAINT [DF_CalculationResult_SaveDate]  DEFAULT (getdate()) FOR [SaveDate]
GO
ALTER TABLE [dbo].[CategoryOfFuelColumnsDictionary] ADD  CONSTRAINT [DF_CategoryOfFuelColumnsDictionary_isHasDefValue]  DEFAULT ((0)) FOR [isHasDefValue]
GO
ALTER TABLE [dbo].[CompanyDO] ADD  CONSTRAINT [DF_CompanyDO_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProductionSite] ADD  CONSTRAINT [DF_ProductionSite_DateAdd]  DEFAULT (getdate()) FOR [DateAdd]
GO
ALTER TABLE [dbo].[ProductionSite] ADD  CONSTRAINT [DF_ProductionSite_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SourceOfEmission] ADD  CONSTRAINT [DF_SourceOfEmission_DateAdd]  DEFAULT (getdate()) FOR [DateAdd]
GO
ALTER TABLE [dbo].[SourceOfEmission] ADD  CONSTRAINT [DF_SourceOfEmission_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[SourceOfEmissionFuel] ADD  CONSTRAINT [DF_SourceOfEmissionFuel_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Access]  WITH CHECK ADD  CONSTRAINT [FK_Access_AccessRoles] FOREIGN KEY([AccessRoles_id])
REFERENCES [dbo].[AccessRoles] ([id])
GO
ALTER TABLE [dbo].[Access] CHECK CONSTRAINT [FK_Access_AccessRoles]
GO
ALTER TABLE [dbo].[CalculationCompositionForm]  WITH CHECK ADD  CONSTRAINT [FK_CalculationCompositionForm_CalculationResult] FOREIGN KEY([CalculationResult_id])
REFERENCES [dbo].[CalculationResult] ([id])
GO
ALTER TABLE [dbo].[CalculationCompositionForm] CHECK CONSTRAINT [FK_CalculationCompositionForm_CalculationResult]
GO
ALTER TABLE [dbo].[CalculationCompositionForReport]  WITH CHECK ADD  CONSTRAINT [FK_CalculationComposition_CalculationResult] FOREIGN KEY([CalculationResult_id])
REFERENCES [dbo].[CalculationResult] ([id])
GO
ALTER TABLE [dbo].[CalculationCompositionForReport] CHECK CONSTRAINT [FK_CalculationComposition_CalculationResult]
GO
ALTER TABLE [dbo].[CalculationResult]  WITH CHECK ADD  CONSTRAINT [FK_CalculationResult_SourceOfEmissionFuel] FOREIGN KEY([SourceOfEmissionFuel_id])
REFERENCES [dbo].[SourceOfEmissionFuel] ([id])
GO
ALTER TABLE [dbo].[CalculationResult] CHECK CONSTRAINT [FK_CalculationResult_SourceOfEmissionFuel]
GO
ALTER TABLE [dbo].[ProductionSite]  WITH CHECK ADD  CONSTRAINT [FK_ProductionSite_CompanyDO] FOREIGN KEY([CompanyDO_id])
REFERENCES [dbo].[CompanyDO] ([id])
GO
ALTER TABLE [dbo].[ProductionSite] CHECK CONSTRAINT [FK_ProductionSite_CompanyDO]
GO
ALTER TABLE [dbo].[SourceOfEmission]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfEmission_ProductionSite] FOREIGN KEY([ProductionSite_id])
REFERENCES [dbo].[ProductionSite] ([id])
GO
ALTER TABLE [dbo].[SourceOfEmission] CHECK CONSTRAINT [FK_SourceOfEmission_ProductionSite]
GO
ALTER TABLE [dbo].[SourceOfEmissionFuel]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfEmissionFuel_CategoryOfFuel] FOREIGN KEY([CategoryOfFuel_id])
REFERENCES [dbo].[CategoryOfFuel] ([id])
GO
ALTER TABLE [dbo].[SourceOfEmissionFuel] CHECK CONSTRAINT [FK_SourceOfEmissionFuel_CategoryOfFuel]
GO
ALTER TABLE [dbo].[SourceOfEmissionFuel]  WITH CHECK ADD  CONSTRAINT [FK_SourceOfEmissionFuel_SourceOfEmission] FOREIGN KEY([SourceOfEmission_id])
REFERENCES [dbo].[SourceOfEmission] ([id])
GO
ALTER TABLE [dbo].[SourceOfEmissionFuel] CHECK CONSTRAINT [FK_SourceOfEmissionFuel_SourceOfEmission]
GO
ALTER TABLE [dbo].[TypesForCategoryFuel]  WITH CHECK ADD  CONSTRAINT [FK_TypesForCategoryFuel_CategoryOfFuel] FOREIGN KEY([CategoryOfFuel_id])
REFERENCES [dbo].[CategoryOfFuel] ([id])
GO
ALTER TABLE [dbo].[TypesForCategoryFuel] CHECK CONSTRAINT [FK_TypesForCategoryFuel_CategoryOfFuel]
GO
ALTER TABLE [dbo].[TypesForCategoryFuel]  WITH CHECK ADD  CONSTRAINT [FK_TypesForCategoryFuel_TypeOfFuel] FOREIGN KEY([TypeOfFuel_id])
REFERENCES [dbo].[TypeOfFuel] ([id])
GO
ALTER TABLE [dbo].[TypesForCategoryFuel] CHECK CONSTRAINT [FK_TypesForCategoryFuel_TypeOfFuel]
GO
