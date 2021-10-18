CREATE TABLE [dbo].[Roles](
  [Id] INT IDENTITY(1,1) NOT NULL,
  [UniqueId] UNIQUEIDENTIFIER UNIQUE NOT NULL,
  [Name] NVARCHAR(256) NOT NULL,
  [NormalizedName] NVARCHAR(256) NOT NULL,
  [Description] NVARCHAR(max) NOT NULL,
  [IsDeleted] BIT DEFAULT 0,
  [Created] DATETIME DEFAULT GETUTCDATE(),
  [Updated] DATETIME DEFAULT GETUTCDATE(),
  CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED([Id] ASC)
)
GO

CREATE INDEX [IX_Roles_NormalizedName] ON [dbo].[Roles] ([NormalizedName])
GO

CREATE TABLE [dbo].[Users](
  [Id] INT IDENTITY(1,1) NOT NULL,
  [UniqueId] UNIQUEIDENTIFIER UNIQUE NOT NULL,
  [UserName] NVARCHAR(256) NOT NULL,
  [NormalizedUserName] NVARCHAR(256) NOT NULL,
  [FirstName] NVARCHAR(100) NOT NULL,
  [LastName] NVARCHAR(100) NOT NULL,
  [Email] NVARCHAR(256) NOT NULL,
  [NormalizedEmail] NVARCHAR(256) NULL,
  [EmailConfirmed] BIT NOT NULL,
  [PasswordHash] NVARCHAR(MAX) NULL,
  [PhoneNumber] NVARCHAR(50) NULL,
  [PhoneNumberConfirmed] BIT NOT NULL,
  [TwoFactorEnabled] BIT NOT NULL,
  [IsDeleted] BIT DEFAULT 0,
  [Created] DATETIME DEFAULT GETUTCDATE(),
  [Updated] DATETIME DEFAULT GETUTCDATE(),
  [RoleUniqueId] UNIQUEIDENTIFIER NOT NULL,
  CONSTRAINT PK_Users PRIMARY KEY CLUSTERED ( [Id] ASC ),
  CONSTRAINT FK_Roles FOREIGN KEY ([RoleUniqueId]) REFERENCES [dbo].[Roles] ([UniqueId]),
 )
GO


CREATE TABLE [dbo].[UserRole]
(
	[UserUniqueId] UNIQUEIDENTIFIER NOT NULL,
	[RoleUniqueId] UNIQUEIDENTIFIER NOT NULL
  PRIMARY KEY ([UserUniqueId], [RoleUniqueId]),
  CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserUniqueId]) REFERENCES [Users]([UniqueId]),
  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleUniqueId]) REFERENCES [Roles]([UniqueId])
)
GO

CREATE INDEX [IX_Users_NormalizedUserName] ON [dbo].[Users] ([NormalizedUserName])
GO

CREATE INDEX [IX_Users_NormalizedEmail] ON [dbo].[Users] ([NormalizedEmail])
GO

CREATE TABLE [dbo].[Todo](
  [Id] INT IDENTITY(1,1) NOT NULL,
  [UniqueId] UNIQUEIDENTIFIER NOT NULL,
  [Summary] NVARCHAR(100) NOT NULL,
  [Detail] NVARCHAR(max) NOT NULL,
  [IsDone] BIT DEFAULT 0,
  [Created] DATETIME DEFAULT GETUTCDATE(),
  [Updated] DATETIME DEFAULT GETUTCDATE(),
  [AssigneeGuid] UNIQUEIDENTIFIER,
  CONSTRAINT PK_Todo PRIMARY KEY CLUSTERED ( [Id] ASC ),
  CONSTRAINT FK_Users FOREIGN KEY ([AssigneeGuid]) REFERENCES [dbo].[Users] ([UniqueId]),
 )
GO

SET IDENTITY_INSERT [dbo].[Roles] ON
GO
INSERT INTO [dbo].[Roles]([Id], [UniqueId], [Name], [NormalizedName], [Description])
VALUES(1, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4', 'Admin', 'ADMIN', 'A super admin has extra authorisation.')

INSERT INTO [dbo].[Roles]([Id], [UniqueId], [Name], [NormalizedName], [Description])
VALUES(2, '804F7003-5777-4471-B1D4-B793D3FB643C', 'Default', 'DEFAULT', 'A default role is assigned to a user who registers without a role.')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
