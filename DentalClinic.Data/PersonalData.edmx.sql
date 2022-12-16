
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/30/2019 13:59:24
-- Generated from EDMX file: C:\Users\adamm\source\repos\DentalClinic\Code\DentalClinic.Data\PersonalData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DentalClinic];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TreatmentSubTreatment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubTreatments] DROP CONSTRAINT [FK_TreatmentSubTreatment];
GO
IF OBJECT_ID(N'[dbo].[FK_SubTreatmentSub2Treatment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sub2Treatments] DROP CONSTRAINT [FK_SubTreatmentSub2Treatment];
GO
IF OBJECT_ID(N'[dbo].[FK_PriceListGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_PriceListGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupSubGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubGroups] DROP CONSTRAINT [FK_GroupSubGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_SubGroupSub2Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sub2Groups] DROP CONSTRAINT [FK_SubGroupSub2Group];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientPriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PriceLists] DROP CONSTRAINT [FK_PatientPriceList];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_PersonEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patients] DROP CONSTRAINT [FK_PersonPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_PersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_EmployeeContract];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patients] DROP CONSTRAINT [FK_EmployeePatient];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_EmployeeVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_VisitComment];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PatientComment];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_EmployeeUser];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_EmployeeComment];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_PatientVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_OfficeVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_OfficeVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitTreatment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treatments] DROP CONSTRAINT [FK_VisitTreatment];
GO
IF OBJECT_ID(N'[dbo].[FK_ToothTreatment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treatments] DROP CONSTRAINT [FK_ToothTreatment];
GO
IF OBJECT_ID(N'[dbo].[FK_ToothComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_ToothComment];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitTooth]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teeth] DROP CONSTRAINT [FK_VisitTooth];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Contracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contracts];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[Visits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Visits];
GO
IF OBJECT_ID(N'[dbo].[Teeth]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teeth];
GO
IF OBJECT_ID(N'[dbo].[Treatments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Treatments];
GO
IF OBJECT_ID(N'[dbo].[SubTreatments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubTreatments];
GO
IF OBJECT_ID(N'[dbo].[Sub2Treatments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sub2Treatments];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Offices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices];
GO
IF OBJECT_ID(N'[dbo].[PriceLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PriceLists];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[SubGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubGroups];
GO
IF OBJECT_ID(N'[dbo].[Sub2Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sub2Groups];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [PersonalNumber] nvarchar(max)  NULL,
    [AddDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AddressType] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [Street] nvarchar(max)  NULL,
    [HouseNumber] nvarchar(max)  NULL,
    [FlatNumber] nvarchar(max)  NULL,
    [StateProvince] nvarchar(max)  NULL,
    [CountryRegion] nvarchar(max)  NULL,
    [PostalCode] nvarchar(max)  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [HomePhone] nvarchar(max)  NULL,
    [WorkPhone] nvarchar(max)  NULL,
    [CellPhone] nvarchar(max)  NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PWZNumer] nvarchar(max)  NULL,
    [AddDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [FavoriteColor] nvarchar(max)  NULL,
    [Person_Id] int  NOT NULL
);
GO

-- Creating table 'Contracts'
CREATE TABLE [dbo].[Contracts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [StartDate] nvarchar(max)  NOT NULL,
    [EndDate] nvarchar(max)  NULL,
    [Salary] nvarchar(max)  NOT NULL,
    [EmployeeId] int  NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Insured] bit  NULL,
    [InsuranceNumber] nvarchar(max)  NULL,
    [Disabled] bit  NULL,
    [DisabilityType] nvarchar(max)  NULL,
    [AddDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [EmployeeId] int  NULL,
    [Person_Id] int  NOT NULL
);
GO

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [PatientId] int  NOT NULL,
    [OfficeId] int  NOT NULL
);
GO

-- Creating table 'Teeth'
CREATE TABLE [dbo].[Teeth] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [VisitId] int  NULL
);
GO

-- Creating table 'Treatments'
CREATE TABLE [dbo].[Treatments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [VisitId] int  NULL,
    [ToothId] int  NULL
);
GO

-- Creating table 'SubTreatments'
CREATE TABLE [dbo].[SubTreatments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [TreatmentId] int  NULL
);
GO

-- Creating table 'Sub2Treatments'
CREATE TABLE [dbo].[Sub2Treatments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [SubTreatmentId] int  NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [VisitId] int  NULL,
    [PatientId] int  NULL,
    [EmployeeId] int  NULL,
    [ToothId] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Pass] varbinary(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [Enabled] bit  NOT NULL,
    [Locked] bit  NOT NULL,
    [Employee_Id] int  NULL
);
GO

-- Creating table 'Offices'
CREATE TABLE [dbo].[Offices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NULL,
    [Label] nvarchar(max)  NULL,
    [Number] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PriceLists'
CREATE TABLE [dbo].[PriceLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Discount] decimal(18,0)  NULL,
    [Patient_Id] int  NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [PriceListId] int  NULL,
    [LowerPrice] decimal(18,0)  NULL,
    [UpperPrice] decimal(18,0)  NULL
);
GO

-- Creating table 'SubGroups'
CREATE TABLE [dbo].[SubGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupId] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LowerPrice] decimal(18,0)  NULL,
    [UpperPrice] decimal(18,0)  NULL
);
GO

-- Creating table 'Sub2Groups'
CREATE TABLE [dbo].[Sub2Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubGroupId] int  NULL,
    [LowerPrice] decimal(18,0)  NULL,
    [UpperPrice] decimal(18,0)  NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [PK_Contracts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [PK_Visits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teeth'
ALTER TABLE [dbo].[Teeth]
ADD CONSTRAINT [PK_Teeth]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [PK_Treatments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubTreatments'
ALTER TABLE [dbo].[SubTreatments]
ADD CONSTRAINT [PK_SubTreatments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sub2Treatments'
ALTER TABLE [dbo].[Sub2Treatments]
ADD CONSTRAINT [PK_Sub2Treatments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [PK_Offices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PriceLists'
ALTER TABLE [dbo].[PriceLists]
ADD CONSTRAINT [PK_PriceLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubGroups'
ALTER TABLE [dbo].[SubGroups]
ADD CONSTRAINT [PK_SubGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sub2Groups'
ALTER TABLE [dbo].[Sub2Groups]
ADD CONSTRAINT [PK_Sub2Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TreatmentId] in table 'SubTreatments'
ALTER TABLE [dbo].[SubTreatments]
ADD CONSTRAINT [FK_TreatmentSubTreatment]
    FOREIGN KEY ([TreatmentId])
    REFERENCES [dbo].[Treatments]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TreatmentSubTreatment'
CREATE INDEX [IX_FK_TreatmentSubTreatment]
ON [dbo].[SubTreatments]
    ([TreatmentId]);
GO

-- Creating foreign key on [SubTreatmentId] in table 'Sub2Treatments'
ALTER TABLE [dbo].[Sub2Treatments]
ADD CONSTRAINT [FK_SubTreatmentSub2Treatment]
    FOREIGN KEY ([SubTreatmentId])
    REFERENCES [dbo].[SubTreatments]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubTreatmentSub2Treatment'
CREATE INDEX [IX_FK_SubTreatmentSub2Treatment]
ON [dbo].[Sub2Treatments]
    ([SubTreatmentId]);
GO

-- Creating foreign key on [PriceListId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_PriceListGroup]
    FOREIGN KEY ([PriceListId])
    REFERENCES [dbo].[PriceLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PriceListGroup'
CREATE INDEX [IX_FK_PriceListGroup]
ON [dbo].[Groups]
    ([PriceListId]);
GO

-- Creating foreign key on [GroupId] in table 'SubGroups'
ALTER TABLE [dbo].[SubGroups]
ADD CONSTRAINT [FK_GroupSubGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupSubGroup'
CREATE INDEX [IX_FK_GroupSubGroup]
ON [dbo].[SubGroups]
    ([GroupId]);
GO

-- Creating foreign key on [SubGroupId] in table 'Sub2Groups'
ALTER TABLE [dbo].[Sub2Groups]
ADD CONSTRAINT [FK_SubGroupSub2Group]
    FOREIGN KEY ([SubGroupId])
    REFERENCES [dbo].[SubGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubGroupSub2Group'
CREATE INDEX [IX_FK_SubGroupSub2Group]
ON [dbo].[Sub2Groups]
    ([SubGroupId]);
GO

-- Creating foreign key on [Patient_Id] in table 'PriceLists'
ALTER TABLE [dbo].[PriceLists]
ADD CONSTRAINT [FK_PatientPriceList]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientPriceList'
CREATE INDEX [IX_FK_PatientPriceList]
ON [dbo].[PriceLists]
    ([Patient_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_PersonEmployee]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmployee'
CREATE INDEX [IX_FK_PersonEmployee]
ON [dbo].[Employees]
    ([Person_Id]);
GO

-- Creating foreign key on [Person_Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_PersonPatient]
    FOREIGN KEY ([Person_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPatient'
CREATE INDEX [IX_FK_PersonPatient]
ON [dbo].[Patients]
    ([Person_Id]);
GO

-- Creating foreign key on [PersonId] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_PersonAddress]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAddress'
CREATE INDEX [IX_FK_PersonAddress]
ON [dbo].[Addresses]
    ([PersonId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_EmployeeContract]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeContract'
CREATE INDEX [IX_FK_EmployeeContract]
ON [dbo].[Contracts]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_EmployeePatient]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePatient'
CREATE INDEX [IX_FK_EmployeePatient]
ON [dbo].[Patients]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_EmployeeVisit]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeVisit'
CREATE INDEX [IX_FK_EmployeeVisit]
ON [dbo].[Visits]
    ([EmployeeId]);
GO

-- Creating foreign key on [VisitId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_VisitComment]
    FOREIGN KEY ([VisitId])
    REFERENCES [dbo].[Visits]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitComment'
CREATE INDEX [IX_FK_VisitComment]
ON [dbo].[Comments]
    ([VisitId]);
GO

-- Creating foreign key on [PatientId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PatientComment]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientComment'
CREATE INDEX [IX_FK_PatientComment]
ON [dbo].[Comments]
    ([PatientId]);
GO

-- Creating foreign key on [Employee_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_EmployeeUser]
    FOREIGN KEY ([Employee_Id])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeUser'
CREATE INDEX [IX_FK_EmployeeUser]
ON [dbo].[Users]
    ([Employee_Id]);
GO

-- Creating foreign key on [EmployeeId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_EmployeeComment]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeComment'
CREATE INDEX [IX_FK_EmployeeComment]
ON [dbo].[Comments]
    ([EmployeeId]);
GO

-- Creating foreign key on [PatientId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_PatientVisit]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientVisit'
CREATE INDEX [IX_FK_PatientVisit]
ON [dbo].[Visits]
    ([PatientId]);
GO

-- Creating foreign key on [OfficeId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_OfficeVisit]
    FOREIGN KEY ([OfficeId])
    REFERENCES [dbo].[Offices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OfficeVisit'
CREATE INDEX [IX_FK_OfficeVisit]
ON [dbo].[Visits]
    ([OfficeId]);
GO

-- Creating foreign key on [VisitId] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [FK_VisitTreatment]
    FOREIGN KEY ([VisitId])
    REFERENCES [dbo].[Visits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitTreatment'
CREATE INDEX [IX_FK_VisitTreatment]
ON [dbo].[Treatments]
    ([VisitId]);
GO

-- Creating foreign key on [ToothId] in table 'Treatments'
ALTER TABLE [dbo].[Treatments]
ADD CONSTRAINT [FK_ToothTreatment]
    FOREIGN KEY ([ToothId])
    REFERENCES [dbo].[Teeth]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ToothTreatment'
CREATE INDEX [IX_FK_ToothTreatment]
ON [dbo].[Treatments]
    ([ToothId]);
GO

-- Creating foreign key on [ToothId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_ToothComment]
    FOREIGN KEY ([ToothId])
    REFERENCES [dbo].[Teeth]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ToothComment'
CREATE INDEX [IX_FK_ToothComment]
ON [dbo].[Comments]
    ([ToothId]);
GO

-- Creating foreign key on [VisitId] in table 'Teeth'
ALTER TABLE [dbo].[Teeth]
ADD CONSTRAINT [FK_VisitTooth]
    FOREIGN KEY ([VisitId])
    REFERENCES [dbo].[Visits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitTooth'
CREATE INDEX [IX_FK_VisitTooth]
ON [dbo].[Teeth]
    ([VisitId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------