doIF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    CREATE TABLE [Candidates] (
        [Id] int NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Candidates] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO `Candidates` (FirstName,LastName) VALUES ('Jon','Snow');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO `Candidates` (FirstName,LastName) VALUES ('Daenerys','Tangaryen');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO `Candidates` (FirstName,LastName) VALUES ('Cersei','Lannister');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO `Candidates` (FirstName,LastName) VALUES ('Sansa','Stark');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO `Candidates` (FirstName,LastName) VALUES ('Arya','Stark');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO `Candidates` (FirstName,LastName) VALUES ('Tyrion','Lannister');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713124619_InitialCandidates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180713124619_InitialCandidates', N'2.1.1-rtm-30846');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    CREATE TABLE [Skills] (
        [Id] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Skills] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    INSERT INTO `Skills` (Name) VALUES ('SQL');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    INSERT INTO `Skills` (Name) VALUES ('JavaScript');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    INSERT INTO `Skills` (Name) VALUES ('C#');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    INSERT INTO `Skills` (Name) VALUES ('Java');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    INSERT INTO `Skills` (Name) VALUES ('Python');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180713125524_InitialSkills')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180713125524_InitialSkills', N'2.1.1-rtm-30846');
END;

GO

