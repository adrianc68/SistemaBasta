
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/13/2021 12:00:51
-- Generated from EDMX file: C:\Users\adrian\Desktop\SistemaBasta-main\Database\Entity\BastaEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Basta];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PlayerAccessAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Players] DROP CONSTRAINT [FK_PlayerAccessAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomRoomConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RomConfigurations] DROP CONSTRAINT [FK_RoomRoomConfiguration];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Hosts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hosts];
GO
IF OBJECT_ID(N'[dbo].[AccessAccounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccessAccounts];
GO
IF OBJECT_ID(N'[dbo].[RomConfigurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RomConfigurations];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [email] nvarchar(120)  NOT NULL,
    [age] smallint  NOT NULL,
    [country] nvarchar(120)  NOT NULL,
    [location] int  NOT NULL,
    [name] nvarchar(120)  NOT NULL,
    [score] float  NOT NULL
);
GO

-- Creating table 'Hosts'
CREATE TABLE [dbo].[Hosts] (
    [mac_address] nvarchar(75)  NOT NULL,
    [attempts] smallint  NOT NULL
);
GO

-- Creating table 'AccessAccounts'
CREATE TABLE [dbo].[AccessAccounts] (
    [email] nvarchar(120)  NOT NULL,
    [username] nvarchar(120)  NOT NULL,
    [password] nvarchar(120)  NOT NULL,
    [recovery_code] nvarchar(8)  NULL,
    [account_state] int  NOT NULL
);
GO

-- Creating table 'RomConfigurations'
CREATE TABLE [dbo].[RomConfigurations] (
    [playerLimit] int  NOT NULL,
    [roomState] int  NOT NULL,
    [code] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [code] nvarchar(10)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [email] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([email] ASC);
GO

-- Creating primary key on [mac_address] in table 'Hosts'
ALTER TABLE [dbo].[Hosts]
ADD CONSTRAINT [PK_Hosts]
    PRIMARY KEY CLUSTERED ([mac_address] ASC);
GO

-- Creating primary key on [email] in table 'AccessAccounts'
ALTER TABLE [dbo].[AccessAccounts]
ADD CONSTRAINT [PK_AccessAccounts]
    PRIMARY KEY CLUSTERED ([email] ASC);
GO

-- Creating primary key on [code] in table 'RomConfigurations'
ALTER TABLE [dbo].[RomConfigurations]
ADD CONSTRAINT [PK_RomConfigurations]
    PRIMARY KEY CLUSTERED ([code] ASC);
GO

-- Creating primary key on [code] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([code] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [email] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_PlayerAccessAccount]
    FOREIGN KEY ([email])
    REFERENCES [dbo].[AccessAccounts]
        ([email])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [code] in table 'RomConfigurations'
ALTER TABLE [dbo].[RomConfigurations]
ADD CONSTRAINT [FK_RoomRoomConfiguration]
    FOREIGN KEY ([code])
    REFERENCES [dbo].[Rooms]
        ([code])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------