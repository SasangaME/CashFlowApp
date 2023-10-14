CREATE TABLE [User] (
                        [Id] int NOT NULL IDENTITY,
                        [Username] nvarchar(max) NOT NULL,
                        [FirstName] nvarchar(max) NOT NULL,
                        [LastName] nvarchar(max) NOT NULL,
                        [Password] nvarchar(max) NOT NULL,
                        [RoleId] int NOT NULL,
                        [CreatedAt] datetime2 NULL,
                        [CreatedBy] int NULL,
                        [UpdatedAt] datetime2 NULL,
                        [UpdatedBy] int NULL,
                        CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
                        CONSTRAINT [FK_User_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);