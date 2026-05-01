IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [FullName] nvarchar(100) NOT NULL,
    [DisplayName] nvarchar(max) NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [BirthDate] datetime2 NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [UserImage] nvarchar(max) NULL,
    [IsEmergencyMode] bit NOT NULL,
    [LastLatitude] float NULL,
    [LastLongitude] float NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsDeviceActive] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Devices] (
    [Id] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    [DeviceName] nvarchar(max) NOT NULL,
    [SerialNumber] nvarchar(max) NOT NULL,
    [BatteryLevel] float NOT NULL,
    [LastSync] datetime2 NOT NULL,
    [FrimWareVersion] nvarchar(max) NOT NULL,
    [OwnerUserId] uniqueidentifier NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    [bodyLocation] int NULL,
    [FirmwareVersion] nvarchar(max) NULL,
    [CameraResolution] nvarchar(max) NULL,
    [SensorRang] nvarchar(max) NULL,
    CONSTRAINT [PK_Devices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Devices_Users_OwnerUserId] FOREIGN KEY ([OwnerUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [EmergencyContect] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Relation] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_EmergencyContect] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmergencyContect_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FacesProfile] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [TypeRelationShip] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    [image] nvarchar(max) NOT NULL,
    [EmbeddingHash] nvarchar(max) NOT NULL,
    [AddedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_FacesProfile] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FacesProfile_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [MedicalProfile] (
    [Id] uniqueidentifier NOT NULL,
    [BoodType] nvarchar(max) NOT NULL,
    [Allergies] nvarchar(max) NOT NULL,
    [Medications] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NOT NULL,
    [DrPhone] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_MedicalProfile] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MedicalProfile_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Alerts] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DeviceId] uniqueidentifier NOT NULL,
    [AlertType] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [IsResolved] bit NOT NULL,
    [Notification] bit NOT NULL,
    [Location_Latitude] float NOT NULL,
    [Location_Longitude] float NOT NULL,
    [Location_ReadableAddress] nvarchar(max) NULL,
    [Location_CapturedAt] datetime2 NOT NULL,
    [Location_Accuracy] float NULL,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    CONSTRAINT [PK_Alerts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Alerts_Devices_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Devices] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Alerts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Alerts_DeviceId] ON [Alerts] ([DeviceId]);
GO

CREATE INDEX [IX_Alerts_UserId] ON [Alerts] ([UserId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [IX_Devices_OwnerUserId] ON [Devices] ([OwnerUserId]);
GO

CREATE INDEX [IX_EmergencyContect_UserId] ON [EmergencyContect] ([UserId]);
GO

CREATE INDEX [IX_FacesProfile_UserId] ON [FacesProfile] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_MedicalProfile_UserId] ON [MedicalProfile] ([UserId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304132706_RealFinalInitial', N'8.0.20');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Alerts] DROP CONSTRAINT [FK_Alerts_Devices_DeviceId];
GO

ALTER TABLE [Alerts] DROP CONSTRAINT [FK_Alerts_Users_UserId];
GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_Roles_RoleId];
GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_Users_UserId];
GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_Users_UserId];
GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_Roles_RoleId];
GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_Users_UserId];
GO

ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_Users_UserId];
GO

ALTER TABLE [Devices] DROP CONSTRAINT [FK_Devices_Users_OwnerUserId];
GO

ALTER TABLE [EmergencyContect] DROP CONSTRAINT [FK_EmergencyContect_Users_UserId];
GO

ALTER TABLE [FacesProfile] DROP CONSTRAINT [FK_FacesProfile_Users_UserId];
GO

ALTER TABLE [MedicalProfile] DROP CONSTRAINT [FK_MedicalProfile_Users_UserId];
GO

ALTER TABLE [Alerts] ADD CONSTRAINT [FK_Alerts_Devices_DeviceId] FOREIGN KEY ([DeviceId]) REFERENCES [Devices] ([Id]);
GO

ALTER TABLE [Alerts] ADD CONSTRAINT [FK_Alerts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_AspNetRoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]);
GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]);
GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [Devices] ADD CONSTRAINT [FK_Devices_Users_OwnerUserId] FOREIGN KEY ([OwnerUserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [EmergencyContect] ADD CONSTRAINT [FK_EmergencyContect_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [FacesProfile] ADD CONSTRAINT [FK_FacesProfile_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

ALTER TABLE [MedicalProfile] ADD CONSTRAINT [FK_MedicalProfile_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304133007_FinalInitialMigration', N'8.0.20');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MedicalProfile]') AND [c].[name] = N'Medications');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [MedicalProfile] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [MedicalProfile] DROP COLUMN [Medications];
GO

CREATE TABLE [Medications] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Dosage] nvarchar(max) NOT NULL,
    [Schedule] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [MedicalProfileId] uniqueidentifier NULL,
    CONSTRAINT [PK_Medications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Medications_MedicalProfile_MedicalProfileId] FOREIGN KEY ([MedicalProfileId]) REFERENCES [MedicalProfile] ([Id]),
    CONSTRAINT [FK_Medications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE INDEX [IX_Medications_MedicalProfileId] ON [Medications] ([MedicalProfileId]);
GO

CREATE INDEX [IX_Medications_UserId] ON [Medications] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260309043725_addMedicationTable', N'8.0.20');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Gender');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [Gender] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260309045302_MakeGenderNullable', N'8.0.20');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MedicalProfile]') AND [c].[name] = N'Allergies');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [MedicalProfile] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [MedicalProfile] DROP COLUMN [Allergies];
GO

EXEC sp_rename N'[MedicalProfile].[BoodType]', N'BloodType', N'COLUMN';
GO

ALTER TABLE [MedicalProfile] ADD [Height] real NULL;
GO

ALTER TABLE [MedicalProfile] ADD [Weight] real NULL;
GO

CREATE TABLE [Allergies] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Severity] nvarchar(max) NULL,
    [Reaction] nvarchar(max) NULL,
    [MedicalProfileId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Allergies] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Allergies_MedicalProfile_MedicalProfileId] FOREIGN KEY ([MedicalProfileId]) REFERENCES [MedicalProfile] ([Id])
);
GO

CREATE TABLE [ChronicDiseases] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [DiagnosedDate] datetime2 NULL,
    [MedicalProfileId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ChronicDiseases] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChronicDiseases_MedicalProfile_MedicalProfileId] FOREIGN KEY ([MedicalProfileId]) REFERENCES [MedicalProfile] ([Id])
);
GO

CREATE TABLE [MedicalHistoryEntries] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [EventDate] datetime2 NOT NULL,
    [DoctorName] nvarchar(max) NULL,
    [MedicalProfileId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_MedicalHistoryEntries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MedicalHistoryEntries_MedicalProfile_MedicalProfileId] FOREIGN KEY ([MedicalProfileId]) REFERENCES [MedicalProfile] ([Id])
);
GO

CREATE INDEX [IX_Allergies_MedicalProfileId] ON [Allergies] ([MedicalProfileId]);
GO

CREATE INDEX [IX_ChronicDiseases_MedicalProfileId] ON [ChronicDiseases] ([MedicalProfileId]);
GO

CREATE INDEX [IX_MedicalHistoryEntries_MedicalProfileId] ON [MedicalHistoryEntries] ([MedicalProfileId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260407225941_MedicalUpdate', N'8.0.20');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260409004256_Medical-Entity', N'8.0.20');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Medications] DROP CONSTRAINT [FK_Medications_Users_UserId];
GO

DROP INDEX [IX_Medications_UserId] ON [Medications];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medications]') AND [c].[name] = N'UserId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Medications] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Medications] DROP COLUMN [UserId];
GO

DROP INDEX [IX_Medications_MedicalProfileId] ON [Medications];
DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Medications]') AND [c].[name] = N'MedicalProfileId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Medications] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [Medications] SET [MedicalProfileId] = '00000000-0000-0000-0000-000000000000' WHERE [MedicalProfileId] IS NULL;
ALTER TABLE [Medications] ALTER COLUMN [MedicalProfileId] uniqueidentifier NOT NULL;
ALTER TABLE [Medications] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [MedicalProfileId];
CREATE INDEX [IX_Medications_MedicalProfileId] ON [Medications] ([MedicalProfileId]);
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MedicalProfile]') AND [c].[name] = N'BloodType');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [MedicalProfile] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [MedicalProfile] ALTER COLUMN [BloodType] int NOT NULL;
GO

ALTER TABLE [MedicalProfile] ADD [Gender] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260426205955_TestConnection', N'8.0.20');
GO

COMMIT;
GO

