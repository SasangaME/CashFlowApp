CREATE TABLE [dbo].[Categories] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (150) NOT NULL,
    [Description] NVARCHAR (250) NULL,
    [CreatedAt]   DATETIME       NULL,
    [CreatedBy]   INT            NULL,
    [UpdatedAt]   DATETIME       NULL,
    [UpdatedBy]   INT            NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

