
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2020 18:50:18
-- Generated from EDMX file: C:\Users\JAntonioC\Documents\Visual Studio 2017\Projects\MenuAPI\MenuAPI\MenuXally.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [XallyMenu];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MenuReceta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Recetas] DROP CONSTRAINT [FK_MenuReceta];
GO
IF OBJECT_ID(N'[dbo].[FK_TipoDePlatilloMenu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menus] DROP CONSTRAINT [FK_TipoDePlatilloMenu];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuDetalleDeOrden]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetallesDeOrden] DROP CONSTRAINT [FK_MenuDetalleDeOrden];
GO
IF OBJECT_ID(N'[dbo].[FK_DetalleDeOrdenOrden]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetallesDeOrden] DROP CONSTRAINT [FK_DetalleDeOrdenOrden];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Recetas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recetas];
GO
IF OBJECT_ID(N'[dbo].[Menus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menus];
GO
IF OBJECT_ID(N'[dbo].[TiposDePlatillo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TiposDePlatillo];
GO
IF OBJECT_ID(N'[dbo].[DetallesDeOrden]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetallesDeOrden];
GO
IF OBJECT_ID(N'[dbo].[Ordenes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ordenes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Recetas'
CREATE TABLE [dbo].[Recetas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DescripcionReceta] nvarchar(max)  NULL,
    [TiempoEstimadoReceta] nvarchar(max)  NULL,
    [EstadoReceta] bit  NOT NULL,
    [IngredientesPrincipales] nvarchar(max)  NOT NULL,
    [MenuId] int  NOT NULL
);
GO

-- Creating table 'Menus'
CREATE TABLE [dbo].[Menus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodigoMenu] nchar(4)  NOT NULL,
    [DescripcionPlatillo] nvarchar(max)  NOT NULL,
    [PrecioPlatillo] float  NOT NULL,
    [EstadoPlatillo] bit  NOT NULL,
    [TipoDePlatilloId] int  NOT NULL
);
GO

-- Creating table 'TiposDePlatillo'
CREATE TABLE [dbo].[TiposDePlatillo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodigoTipoPlatillo] nchar(4)  NOT NULL,
    [DescripcionTipoPlatillo] nvarchar(80)  NOT NULL,
    [EstadoTipoPlatillo] bit  NOT NULL
);
GO

-- Creating table 'DetallesDeOrden'
CREATE TABLE [dbo].[DetallesDeOrden] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CantidadOrden] int  NOT NULL,
    [NotaDetalleOrden] nvarchar(80)  NOT NULL,
    [EstadoDetalleOrden] bit  NOT NULL,
    [MenuId] int  NOT NULL,
    [OrdenId] int  NOT NULL
);
GO

-- Creating table 'Ordenes'
CREATE TABLE [dbo].[Ordenes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodigoOrden] nchar(4)  NOT NULL,
    [FechaOrden] datetime  NOT NULL,
    [TiempoOrden] datetime  NOT NULL,
    [EstadoOrden] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Recetas'
ALTER TABLE [dbo].[Recetas]
ADD CONSTRAINT [PK_Recetas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [PK_Menus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TiposDePlatillo'
ALTER TABLE [dbo].[TiposDePlatillo]
ADD CONSTRAINT [PK_TiposDePlatillo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DetallesDeOrden'
ALTER TABLE [dbo].[DetallesDeOrden]
ADD CONSTRAINT [PK_DetallesDeOrden]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ordenes'
ALTER TABLE [dbo].[Ordenes]
ADD CONSTRAINT [PK_Ordenes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MenuId] in table 'Recetas'
ALTER TABLE [dbo].[Recetas]
ADD CONSTRAINT [FK_MenuReceta]
    FOREIGN KEY ([MenuId])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuReceta'
CREATE INDEX [IX_FK_MenuReceta]
ON [dbo].[Recetas]
    ([MenuId]);
GO

-- Creating foreign key on [TipoDePlatilloId] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [FK_TipoDePlatilloMenu]
    FOREIGN KEY ([TipoDePlatilloId])
    REFERENCES [dbo].[TiposDePlatillo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TipoDePlatilloMenu'
CREATE INDEX [IX_FK_TipoDePlatilloMenu]
ON [dbo].[Menus]
    ([TipoDePlatilloId]);
GO

-- Creating foreign key on [MenuId] in table 'DetallesDeOrden'
ALTER TABLE [dbo].[DetallesDeOrden]
ADD CONSTRAINT [FK_MenuDetalleDeOrden]
    FOREIGN KEY ([MenuId])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuDetalleDeOrden'
CREATE INDEX [IX_FK_MenuDetalleDeOrden]
ON [dbo].[DetallesDeOrden]
    ([MenuId]);
GO

-- Creating foreign key on [OrdenId] in table 'DetallesDeOrden'
ALTER TABLE [dbo].[DetallesDeOrden]
ADD CONSTRAINT [FK_OrdenDetalleDeOrden]
    FOREIGN KEY ([OrdenId])
    REFERENCES [dbo].[Ordenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrdenDetalleDeOrden'
CREATE INDEX [IX_FK_OrdenDetalleDeOrden]
ON [dbo].[DetallesDeOrden]
    ([OrdenId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------