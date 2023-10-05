CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CreatedAt] datetime2 NULL,
    [CreatedBy] int NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] int NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
