CREATE TABLE dbo.Roles(
  [RoleId] INT IDENTITY(1,1) NOT NULL,
  [RoleKind] nvarchar(150) NOT NULL,
  [Description] nvarchar(max) NOT NULL,
  [Created] datetime2(7) NOT NULL,
  [CONSTRAINT] PK_Roles PRIMARY KEY CLUSTERED([RoleId] ASC)
)
GO
CREATE TABLE dbo.Users(
  [UserId] INT IDENTITY(1,1) NOT NULL,
  [UserUniqueId] UNIQUEIDENTIFIER NOT NULL,
  [Name] nvarchar(100) NOT NULL,
  [Email] nvarchar(150) NOT NULL,
  [Hash] nvarchar(150) NOT NULL,
  [Created] datetime2(7) NOT NULL,
  [RoleId] INT NOT NULL,
  CONSTRAINT PK_Users PRIMARY KEY CLUSTERED ( [UserId] ASC ),
  CONSTRAINT FK_Roles FOREIGN KEY ([RoleId]) REFERENCES dbo.Roles ([RoleId]),
 )
GO

CREATE TABLE dbo.Todo(
  [TodoId] INT IDENTITY(1,1) NOT NULL,
  [TodoUniqueId] UNIQUEIDENTIFIER NOT NULL,
  [Summary] nvarchar(100) NOT NULL,
  [Detail] nvarchar(max) NOT NULL,
  [IsDone] bit NOT NULL,
  [Created] datetime2(7) NOT NULL,
  [Assignee] INT,
  CONSTRAINT PK_Users PRIMARY KEY CLUSTERED ( [UserId] ASC ),
  CONSTRAINT FK_Roles FOREIGN KEY ([RoleId]) REFERENCES dbo.Roles ([RoleId]),
 )
GO

SET IDENTITY_INSERT dbo.Roles ON
GO
INSERT INTO dbo.Roles([RoleId], [RoleKind], [Description], [Created])
VALUES(1, 'Scrum Master', 'A scrum master has more administration rights.', getdate())

INSERT INTO dbo.Roles([RoleId], [RoleKind], [Description], [Created])
VALUES(2, 'Developer', 'A developer can do basic operations.', getdate())
GO
SET IDENTITY_INSERT dbo.Roles OFF
GO

SET IDENTITY_INSERT dbo.Users ON
GO
INSERT INTO dbo.Users([UserId], [UserUniqueId], [Name], [Email], [Hash], [Created], [RoleId])
VALUES(1, '3BD023BD-EB19-4858-92E2-24ABB9EC79C9',
'Jack Black',	'jack.black@todo.io',	'123456', getdate(), 1)

INSERT INTO dbo.Users([UserId], [UserUniqueId], [Name], [Email], [Hash], [Created], [RoleId])
VALUES(2, '1526A845-40B6-4311-B39F-FC04EA638B9A',
 'John Smith', 'john.smith@todo.io',	'12345678', getdate(), 2)
GO

INSERT INTO dbo.Users([UserId], [UserUniqueId], [Name], [Email], [Hash], [Created], [RoleId])
VALUES(3, '2113D227-A8E5-437E-9CDD-8D2C043B66B5',
'Jane Doe',	'jane.doe@todo.io',	'988767', getdate(), 2)
GO

SET IDENTITY_INSERT dbo.Users OFF
GO

SET IDENTITY_INSERT dbo.Todo ON
GO
INSERT INTO dbo.Todo([TodoId], [TodoUniqueId], [Summary], [Detail], [IsDone], [Created], [Assignee])
VALUES(1, '7BE021BD-EBAC-4858-35E2-24ABB9EC79C9',
'Buy Groceries',	'Buy some bananas, mangos, apple',	0, getdate(), 1)

INSERT INTO dbo.Todo([TodoId], [TodoUniqueId], [Summary], [Detail], [IsDone], [Created], [Assignee])
VALUES(2, '35F6A946-DEAD-BEEF-B59E-CF04AA63FBDD',
'Car Service', 'Get car serviced, change oil',	0, getdate(), 2)
GO

SET IDENTITY_INSERT dbo.Todo OFF
GO
