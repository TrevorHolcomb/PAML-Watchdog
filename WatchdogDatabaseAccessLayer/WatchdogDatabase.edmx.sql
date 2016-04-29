
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/28/2016 22:42:56
-- Generated from EDMX file: E:\Users\tgb\Documents\Visual Studio 2015\Projects\paml-watchdog\WatchdogDatabaseAccessLayer\WatchdogDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [watchdog2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MessageMessageType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageMessageType];
GO
IF OBJECT_ID(N'[dbo].[FK_EscalationChainEscalationChainLink]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EscalationChains] DROP CONSTRAINT [FK_EscalationChainEscalationChainLink];
GO
IF OBJECT_ID(N'[dbo].[FK_NotifyeeNotifyeeGroup_Notifyee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NotifyeeNotifyeeGroup] DROP CONSTRAINT [FK_NotifyeeNotifyeeGroup_Notifyee];
GO
IF OBJECT_ID(N'[dbo].[FK_NotifyeeNotifyeeGroup_NotifyeeGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NotifyeeNotifyeeGroup] DROP CONSTRAINT [FK_NotifyeeNotifyeeGroup_NotifyeeGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_EscalationChainLinkNotifyeeGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EscalationChainLinks] DROP CONSTRAINT [FK_EscalationChainLinkNotifyeeGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_EscalationChainRule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rules] DROP CONSTRAINT [FK_EscalationChainRule];
GO
IF OBJECT_ID(N'[dbo].[FK_EscalationChainLinkEscalationChainLink]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EscalationChainLinks] DROP CONSTRAINT [FK_EscalationChainLinkEscalationChainLink];
GO
IF OBJECT_ID(N'[dbo].[FK_RuleMessageType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rules] DROP CONSTRAINT [FK_RuleMessageType];
GO
IF OBJECT_ID(N'[dbo].[FK_AlertAlertType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Alerts] DROP CONSTRAINT [FK_AlertAlertType];
GO
IF OBJECT_ID(N'[dbo].[FK_AlertTypeRule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rules] DROP CONSTRAINT [FK_AlertTypeRule];
GO
IF OBJECT_ID(N'[dbo].[FK_AlertRule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Alerts] DROP CONSTRAINT [FK_AlertRule];
GO
IF OBJECT_ID(N'[dbo].[FK_RuleRuleCategory_Rule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RuleRuleCategory] DROP CONSTRAINT [FK_RuleRuleCategory_Rule];
GO
IF OBJECT_ID(N'[dbo].[FK_RuleRuleCategory_RuleCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RuleRuleCategory] DROP CONSTRAINT [FK_RuleRuleCategory_RuleCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[MessageTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageTypes];
GO
IF OBJECT_ID(N'[dbo].[EscalationChains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EscalationChains];
GO
IF OBJECT_ID(N'[dbo].[EscalationChainLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EscalationChainLinks];
GO
IF OBJECT_ID(N'[dbo].[Notifyees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifyees];
GO
IF OBJECT_ID(N'[dbo].[NotifyeeGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NotifyeeGroups];
GO
IF OBJECT_ID(N'[dbo].[Rules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rules];
GO
IF OBJECT_ID(N'[dbo].[AlertTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AlertTypes];
GO
IF OBJECT_ID(N'[dbo].[Alerts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Alerts];
GO
IF OBJECT_ID(N'[dbo].[RuleCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RuleCategories];
GO
IF OBJECT_ID(N'[dbo].[NotifyeeNotifyeeGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NotifyeeNotifyeeGroup];
GO
IF OBJECT_ID(N'[dbo].[RuleRuleCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RuleRuleCategory];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Params] nvarchar(max)  NOT NULL,
    [MessageTypeId] int  NOT NULL,
    [IsProcessed] bit  NOT NULL
);
GO

-- Creating table 'MessageTypes'
CREATE TABLE [dbo].[MessageTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RequiredParameters] nvarchar(max)  NOT NULL,
    [OptionalParameters] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EscalationChains'
CREATE TABLE [dbo].[EscalationChains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [EscalationChainRootLink_Id] int  NOT NULL
);
GO

-- Creating table 'EscalationChainLinks'
CREATE TABLE [dbo].[EscalationChainLinks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NotifyeeGroup_Id] int  NULL,
    [PreviousLink_Id] int  NULL
);
GO

-- Creating table 'Notifyees'
CREATE TABLE [dbo].[Notifyees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [CellPhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NotifyeeGroups'
CREATE TABLE [dbo].[NotifyeeGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Rules'
CREATE TABLE [dbo].[Rules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [RuleTrigger] nvarchar(max)  NOT NULL,
    [EscalationChainId] int  NOT NULL,
    [AlertTypeId] int  NOT NULL,
    [MessageType_Id] int  NOT NULL
);
GO

-- Creating table 'AlertTypes'
CREATE TABLE [dbo].[AlertTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Alerts'
CREATE TABLE [dbo].[Alerts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Payload] nvarchar(max)  NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [AlertTypeId] int  NOT NULL,
    [RuleId] int  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'RuleCategories'
CREATE TABLE [dbo].[RuleCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'NotifyeeNotifyeeGroup'
CREATE TABLE [dbo].[NotifyeeNotifyeeGroup] (
    [Notifyees_Id] int  NOT NULL,
    [NotifyeeGroups_Id] int  NOT NULL
);
GO

-- Creating table 'RuleRuleCategory'
CREATE TABLE [dbo].[RuleRuleCategory] (
    [Rules_Id] int  NOT NULL,
    [RuleCategories_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageTypes'
ALTER TABLE [dbo].[MessageTypes]
ADD CONSTRAINT [PK_MessageTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EscalationChains'
ALTER TABLE [dbo].[EscalationChains]
ADD CONSTRAINT [PK_EscalationChains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EscalationChainLinks'
ALTER TABLE [dbo].[EscalationChainLinks]
ADD CONSTRAINT [PK_EscalationChainLinks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifyees'
ALTER TABLE [dbo].[Notifyees]
ADD CONSTRAINT [PK_Notifyees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NotifyeeGroups'
ALTER TABLE [dbo].[NotifyeeGroups]
ADD CONSTRAINT [PK_NotifyeeGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [PK_Rules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AlertTypes'
ALTER TABLE [dbo].[AlertTypes]
ADD CONSTRAINT [PK_AlertTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Alerts'
ALTER TABLE [dbo].[Alerts]
ADD CONSTRAINT [PK_Alerts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RuleCategories'
ALTER TABLE [dbo].[RuleCategories]
ADD CONSTRAINT [PK_RuleCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Notifyees_Id], [NotifyeeGroups_Id] in table 'NotifyeeNotifyeeGroup'
ALTER TABLE [dbo].[NotifyeeNotifyeeGroup]
ADD CONSTRAINT [PK_NotifyeeNotifyeeGroup]
    PRIMARY KEY CLUSTERED ([Notifyees_Id], [NotifyeeGroups_Id] ASC);
GO

-- Creating primary key on [Rules_Id], [RuleCategories_Id] in table 'RuleRuleCategory'
ALTER TABLE [dbo].[RuleRuleCategory]
ADD CONSTRAINT [PK_RuleRuleCategory]
    PRIMARY KEY CLUSTERED ([Rules_Id], [RuleCategories_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MessageTypeId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageMessageType]
    FOREIGN KEY ([MessageTypeId])
    REFERENCES [dbo].[MessageTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageMessageType'
CREATE INDEX [IX_FK_MessageMessageType]
ON [dbo].[Messages]
    ([MessageTypeId]);
GO

-- Creating foreign key on [EscalationChainRootLink_Id] in table 'EscalationChains'
ALTER TABLE [dbo].[EscalationChains]
ADD CONSTRAINT [FK_EscalationChainEscalationChainLink]
    FOREIGN KEY ([EscalationChainRootLink_Id])
    REFERENCES [dbo].[EscalationChainLinks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EscalationChainEscalationChainLink'
CREATE INDEX [IX_FK_EscalationChainEscalationChainLink]
ON [dbo].[EscalationChains]
    ([EscalationChainRootLink_Id]);
GO

-- Creating foreign key on [Notifyees_Id] in table 'NotifyeeNotifyeeGroup'
ALTER TABLE [dbo].[NotifyeeNotifyeeGroup]
ADD CONSTRAINT [FK_NotifyeeNotifyeeGroup_Notifyee]
    FOREIGN KEY ([Notifyees_Id])
    REFERENCES [dbo].[Notifyees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [NotifyeeGroups_Id] in table 'NotifyeeNotifyeeGroup'
ALTER TABLE [dbo].[NotifyeeNotifyeeGroup]
ADD CONSTRAINT [FK_NotifyeeNotifyeeGroup_NotifyeeGroup]
    FOREIGN KEY ([NotifyeeGroups_Id])
    REFERENCES [dbo].[NotifyeeGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotifyeeNotifyeeGroup_NotifyeeGroup'
CREATE INDEX [IX_FK_NotifyeeNotifyeeGroup_NotifyeeGroup]
ON [dbo].[NotifyeeNotifyeeGroup]
    ([NotifyeeGroups_Id]);
GO

-- Creating foreign key on [NotifyeeGroup_Id] in table 'EscalationChainLinks'
ALTER TABLE [dbo].[EscalationChainLinks]
ADD CONSTRAINT [FK_EscalationChainLinkNotifyeeGroup]
    FOREIGN KEY ([NotifyeeGroup_Id])
    REFERENCES [dbo].[NotifyeeGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EscalationChainLinkNotifyeeGroup'
CREATE INDEX [IX_FK_EscalationChainLinkNotifyeeGroup]
ON [dbo].[EscalationChainLinks]
    ([NotifyeeGroup_Id]);
GO

-- Creating foreign key on [EscalationChainId] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [FK_EscalationChainRule]
    FOREIGN KEY ([EscalationChainId])
    REFERENCES [dbo].[EscalationChains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EscalationChainRule'
CREATE INDEX [IX_FK_EscalationChainRule]
ON [dbo].[Rules]
    ([EscalationChainId]);
GO

-- Creating foreign key on [PreviousLink_Id] in table 'EscalationChainLinks'
ALTER TABLE [dbo].[EscalationChainLinks]
ADD CONSTRAINT [FK_EscalationChainLinkEscalationChainLink]
    FOREIGN KEY ([PreviousLink_Id])
    REFERENCES [dbo].[EscalationChainLinks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EscalationChainLinkEscalationChainLink'
CREATE INDEX [IX_FK_EscalationChainLinkEscalationChainLink]
ON [dbo].[EscalationChainLinks]
    ([PreviousLink_Id]);
GO

-- Creating foreign key on [MessageType_Id] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [FK_RuleMessageType]
    FOREIGN KEY ([MessageType_Id])
    REFERENCES [dbo].[MessageTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleMessageType'
CREATE INDEX [IX_FK_RuleMessageType]
ON [dbo].[Rules]
    ([MessageType_Id]);
GO

-- Creating foreign key on [AlertTypeId] in table 'Alerts'
ALTER TABLE [dbo].[Alerts]
ADD CONSTRAINT [FK_AlertAlertType]
    FOREIGN KEY ([AlertTypeId])
    REFERENCES [dbo].[AlertTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlertAlertType'
CREATE INDEX [IX_FK_AlertAlertType]
ON [dbo].[Alerts]
    ([AlertTypeId]);
GO

-- Creating foreign key on [AlertTypeId] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [FK_AlertTypeRule]
    FOREIGN KEY ([AlertTypeId])
    REFERENCES [dbo].[AlertTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlertTypeRule'
CREATE INDEX [IX_FK_AlertTypeRule]
ON [dbo].[Rules]
    ([AlertTypeId]);
GO

-- Creating foreign key on [RuleId] in table 'Alerts'
ALTER TABLE [dbo].[Alerts]
ADD CONSTRAINT [FK_AlertRule]
    FOREIGN KEY ([RuleId])
    REFERENCES [dbo].[Rules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlertRule'
CREATE INDEX [IX_FK_AlertRule]
ON [dbo].[Alerts]
    ([RuleId]);
GO

-- Creating foreign key on [Rules_Id] in table 'RuleRuleCategory'
ALTER TABLE [dbo].[RuleRuleCategory]
ADD CONSTRAINT [FK_RuleRuleCategory_Rule]
    FOREIGN KEY ([Rules_Id])
    REFERENCES [dbo].[Rules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RuleCategories_Id] in table 'RuleRuleCategory'
ALTER TABLE [dbo].[RuleRuleCategory]
ADD CONSTRAINT [FK_RuleRuleCategory_RuleCategory]
    FOREIGN KEY ([RuleCategories_Id])
    REFERENCES [dbo].[RuleCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleRuleCategory_RuleCategory'
CREATE INDEX [IX_FK_RuleRuleCategory_RuleCategory]
ON [dbo].[RuleRuleCategory]
    ([RuleCategories_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------