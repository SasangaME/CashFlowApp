CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatedAt] datetime2 NULL,
    [CreatedBy] int NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] int NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );