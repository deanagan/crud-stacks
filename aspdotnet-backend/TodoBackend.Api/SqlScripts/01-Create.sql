CREATE TABLE [dbo].[Roles](
  [Id] INT IDENTITY(1,1) NOT NULL,
  [UniqueId] UNIQUEIDENTIFIER NOT NULL,
  [Kind] NVARCHAR(150) NOT NULL,
  [Description] NVARCHAR(max) NOT NULL,
  [Created] DATETIME DEFAULT GETUTCDATE(),
  [Updated] DATETIME DEFAULT GETUTCDATE(),
  CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED([Id] ASC)
)
GO

CREATE TABLE [dbo].[Users](
  [Id] INT IDENTITY(1,1) NOT NULL,
  [UniqueId] UNIQUEIDENTIFIER NOT NULL,
  [FirstName] NVARCHAR(100) NOT NULL,
  [LastName] NVARCHAR(100) NOT NULL,
  [Email] NVARCHAR(150) NOT NULL,
  [Hash] NVARCHAR(150) NOT NULL,
  [Created] DATETIME DEFAULT GETUTCDATE(),
  [Updated] DATETIME DEFAULT GETUTCDATE(),
  [RoleId] INT NOT NULL,
  CONSTRAINT PK_Users PRIMARY KEY CLUSTERED ( [Id] ASC ),
  CONSTRAINT FK_Roles FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
 )
GO

CREATE TABLE [dbo].[Todo](
  [Id] INT IDENTITY(1,1) NOT NULL,
  [UniqueId] UNIQUEIDENTIFIER NOT NULL,
  [Summary] NVARCHAR(100) NOT NULL,
  [Detail] NVARCHAR(max) NOT NULL,
  [IsDone] BIT DEFAULT 0,
  [Created] DATETIME DEFAULT GETUTCDATE(),
  [Updated] DATETIME DEFAULT GETUTCDATE(),
  [AssigneeId] INT,
  CONSTRAINT PK_Todo PRIMARY KEY CLUSTERED ( [Id] ASC ),
  CONSTRAINT FK_Users FOREIGN KEY ([AssigneeId]) REFERENCES [dbo].[Users] ([Id]),
 )
GO

SET IDENTITY_INSERT [dbo].[Roles] ON
GO
INSERT INTO [dbo].[Roles]([Id], [UniqueId], [Kind], [Description])
VALUES(1, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4', 'Scrum Master', 'A scrum master has more administration rights.')

INSERT INTO [dbo].[Roles]([Id], [UniqueId], [Kind], [Description])
VALUES(2, '804F7003-5777-4471-B1D4-B793D3FB643C', 'Developer', 'A developer can do basic operations.')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO

SET IDENTITY_INSERT [dbo].[Users] ON
GO
INSERT INTO [dbo].[Users]([Id], [UniqueId], [FirstName], [LastName], [Email], [Hash], [RoleId])
VALUES(1, '3BD023BD-EB19-4858-92E2-24ABB9EC79C9',
'Jack', 'Black',	'jack.black@todo.io',	'123456', 1)

INSERT INTO [dbo].[Users]([Id], [UniqueId], [FirstName], [LastName], [Email], [Hash], [RoleId])
VALUES(2, '1526A845-40B6-4311-B39F-FC04EA638B9A',
 'John', 'Smith', 'john.smith@todo.io',	'12345678', 2)
GO

INSERT INTO [dbo].[Users]([Id], [UniqueId], [FirstName], [LastName], [Email], [Hash], [RoleId])
VALUES(3, '2113D227-A8E5-437E-9CDD-8D2C043B66B5',
'Jane', 'Doe', 'jane.doe@todo.io',	'988767', 2)
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO

SET IDENTITY_INSERT [dbo].Todo ON
GO
INSERT INTO [dbo].Todo([Id], [UniqueId], [Summary], [Detail], [IsDone], [UserId])
VALUES(1, '7BE021BD-EBAC-4858-35E2-24ABB9EC79C9',
'Buy Groceries',	'Buy some bananas, mangos, apple',	0, 1)

INSERT INTO [dbo].Todo([Id], [UniqueId], [Summary], [Detail], [IsDone], [UserId])
VALUES(2, '35F6A946-DEAD-BEEF-B59E-CF04AA63FBDD',
'Car Service', 'Get car serviced, change oil',	0, 2)
GO

SET IDENTITY_INSERT [dbo].Todo OFF
GO
