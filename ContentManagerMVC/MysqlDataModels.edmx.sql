
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/11/2012 17:11:30
-- Generated from EDMX file: C:\Users\csq\documents\visual studio 2010\Projects\ContentManagerMVC\ContentManagerMVC\MysqlDataModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MediaPlayers'
CREATE TABLE [dbo].[MediaPlayers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] longtext  NOT NULL,
    [IP] longtext  NOT NULL,
    [Status] longtext  NOT NULL,
    [Description] longtext  NOT NULL
);
GO

-- Creating table 'PlayerSchedules'
CREATE TABLE [dbo].[PlayerSchedules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [EndDateTime] datetime  NOT NULL,
    [Continuious] bool  NOT NULL
);
GO

-- Creating table 'ScheduleContents'
CREATE TABLE [dbo].[ScheduleContents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] longtext  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MediaPlayers'
ALTER TABLE [dbo].[MediaPlayers]
ADD CONSTRAINT [PK_MediaPlayers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlayerSchedules'
ALTER TABLE [dbo].[PlayerSchedules]
ADD CONSTRAINT [PK_PlayerSchedules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScheduleContents'
ALTER TABLE [dbo].[ScheduleContents]
ADD CONSTRAINT [PK_ScheduleContents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------