CREATE TABLE [dbo].[Roles]
(
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

CREATE TABLE [dbo].[Users]
(
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

CREATE TABLE [dbo].[Todo]
(
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
INSERT INTO [dbo].[Roles]
  ([Id], [UniqueId], [Name], [NormalizedName], [Description])
VALUES(1, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4', 'Admin', 'ADMIN', 'A super admin has extra authorisation.')

INSERT INTO [dbo].[Roles]
  ([Id], [UniqueId], [Name], [NormalizedName], [Description])
VALUES(2, '804F7003-5777-4471-B1D4-B793D3FB643C', 'Default', 'DEFAULT', 'A default role is assigned to a user who registers without a role.')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO

SET IDENTITY_INSERT [dbo].[Users] ON
GO
INSERT INTO [dbo].[Users]
  (
  [Id],
  [UniqueId],
  [UserName],
  [NormalizedUserName],
  [Email],
  [FirstName],
  [LastName],
  [NormalizedEmail],
  [EmailConfirmed],
  [PasswordHash],
  [PhoneNumber],
  [PhoneNumberConfirmed],
  [TwoFactorEnabled],
  [IsDeleted],
  [RoleUniqueId]
  )
VALUES
  (1, 'b8af53eb-719d-4d47-9761-359dd2b8c003', 'Matilda.Brandon', 'MATILDA.BRANDON', 'Matilda.Brandon@todo.io', 'Matilda', 'Brandon', 'MATILDA.BRANDON@TODO.IO', 0, 'WPhBuod5', 'HLwI6YSg', 1, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (2, '9394386e-f0ee-4717-a4a4-6367b5d857cf', 'Millie.Breeden', 'MILLIE.BREEDEN', 'Millie.Breeden@todo.io', 'Millie', 'Breeden', 'MILLIE.BREEDEN@TODO.IO', 0, 'puug9K1S', '1fZwGA7h', 0, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (3, '0ce1698e-962c-4c32-870a-ba13854ab425', 'Olivia.Morgan', 'OLIVIA.MORGAN', 'Olivia.Morgan@todo.io', 'Olivia', 'Morgan', 'OLIVIA.MORGAN@TODO.IO', 0, 'lGSVixPw', 'bihbQIo4', 0, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (4, '7601aaf0-b49a-4016-9d18-b1ad7de0ecee', 'Emma.Marley', 'EMMA.MARLEY', 'Emma.Marley@todo.io', 'Emma', 'Marley', 'EMMA.MARLEY@TODO.IO', 1, 'UBarOB0q', '7cfoWhUe', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (5, '7216ad3d-6e3d-407c-b444-ccdc7aaf1c33', 'Faith.Jackson', 'FAITH.JACKSON', 'Faith.Jackson@todo.io', 'Faith', 'Jackson', 'FAITH.JACKSON@TODO.IO', 1, 'lTsdBv5u', 'Q1dSPQxc', 1, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (6, '3b2200d9-bc91-466f-9d2d-5e19be73e145', 'Maddie.Carlyle', 'MADDIE.CARLYLE', 'Maddie.Carlyle@todo.io', 'Maddie', 'Carlyle', 'MADDIE.CARLYLE@TODO.IO', 0, 'E0nDzcxd', 'lZtLphTW', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (7, 'f40bcf35-eeac-474c-9d84-23f7dfd5f266', 'Samantha.Harrington', 'SAMANTHA.HARRINGTON', 'Samantha.Harrington@todo.io', 'Samantha', 'Harrington', 'SAMANTHA.HARRINGTON@TODO.IO', 1, 'VPxceCEN', 'JfrWbTfE', 1, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (8, '928a827a-b46f-4a59-a94b-0885abe79830', 'Megan.Eastoft', 'MEGAN.EASTOFT', 'Megan.Eastoft@todo.io', 'Megan', 'Eastoft', 'MEGAN.EASTOFT@TODO.IO', 0, 'QQ4SgUP8', 'GS9VnAQg', 1, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (9, '64595ee9-fbcc-4049-9044-94dc4dec9ef3', 'Samantha.Evans', 'SAMANTHA.EVANS', 'Samantha.Evans@todo.io', 'Samantha', 'Evans', 'SAMANTHA.EVANS@TODO.IO', 0, 'RIbyZcns', 'OLyauc7V', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (10, '98d0b69d-4958-42e9-9e2d-153e5eb5c0fb', 'Megan.Ainsley', 'MEGAN.AINSLEY', 'Megan.Ainsley@todo.io', 'Megan', 'Ainsley', 'MEGAN.AINSLEY@TODO.IO', 1, 'tVDiOPgI', 'AFMc2g5D', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (11, '021b02a1-1e40-4083-bc1f-a3ef761cc207', 'Jessica.Hales', 'JESSICA.HALES', 'Jessica.Hales@todo.io', 'Jessica', 'Hales', 'JESSICA.HALES@TODO.IO', 0, 'pJB3LQUN', 'JbMPus9h', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (12, '1510bdad-1992-4103-87c7-ff4d859224a6', 'Violet.Nash', 'VIOLET.NASH', 'Violet.Nash@todo.io', 'Violet', 'Nash', 'VIOLET.NASH@TODO.IO', 1, 'Jo77GX4Q', '9a98A0Wv', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (13, 'bc37f0d6-a73d-4712-9269-10c464e3045d', 'Lacey.Foster', 'LACEY.FOSTER', 'Lacey.Foster@todo.io', 'Lacey', 'Foster', 'LACEY.FOSTER@TODO.IO', 0, 'rxOF8MJo', 'XMp1yNpa', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (14, '83496360-467c-4878-a76c-c23741b2e180', 'Victoria.Addington', 'VICTORIA.ADDINGTON', 'Victoria.Addington@todo.io', 'Victoria', 'Addington', 'VICTORIA.ADDINGTON@TODO.IO', 0, 'eGifod1Y', 'v2eQydVV', 1, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (15, 'b50d2561-fe90-4ad3-98c5-c4d1f2045424', 'Rebecca.Bullock', 'REBECCA.BULLOCK', 'Rebecca.Bullock@todo.io', 'Rebecca', 'Bullock', 'REBECCA.BULLOCK@TODO.IO', 1, 'aTSaT0c6', 'PT7vWwMu', 0, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (16, 'af0eb969-5b13-455e-a906-a0703e35ba8c', 'Clara.Presley', 'CLARA.PRESLEY', 'Clara.Presley@todo.io', 'Clara', 'Presley', 'CLARA.PRESLEY@TODO.IO', 0, '2bMB84pm', 'UQEju2de', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (17, 'c11caf21-956e-46c2-88fe-9a603e471dd4', 'Elsie.Blyth', 'ELSIE.BLYTH', 'Elsie.Blyth@todo.io', 'Elsie', 'Blyth', 'ELSIE.BLYTH@TODO.IO', 1, 'suONRTxn', '0cyBpIVi', 0, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (18, '5c8562d5-b32a-40ad-a6f9-d66174f5c39b', 'Ashleigh.Burton', 'ASHLEIGH.BURTON', 'Ashleigh.Burton@todo.io', 'Ashleigh', 'Burton', 'ASHLEIGH.BURTON@TODO.IO', 1, 'TcO8TufZ', 'Qnm62IZj', 1, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (19, 'fa9f1d03-d677-4fab-8b0d-b114e16152f6', 'Summer.Harrison', 'SUMMER.HARRISON', 'Summer.Harrison@todo.io', 'Summer', 'Harrison', 'SUMMER.HARRISON@TODO.IO', 1, 'ipp1Mnps', 'V2Ula78c', 1, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (20, '93cf9ec2-1835-49c0-b671-38fdc9d26546', 'Elsie.Hayes', 'ELSIE.HAYES', 'Elsie.Hayes@todo.io', 'Elsie', 'Hayes', 'ELSIE.HAYES@TODO.IO', 0, 'P8JaemGt', 'IYAbczRb', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (21, '214c4226-8046-4a40-8f36-8f56912f0b83', 'Joanne.Pickering', 'JOANNE.PICKERING', 'Joanne.Pickering@todo.io', 'Joanne', 'Pickering', 'JOANNE.PICKERING@TODO.IO', 0, 'ThkoiEpM', 'ScrALHC6', 0, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (22, '57d70c90-34b1-41c1-b404-cb86f10e9924', 'Heidi.Curtis', 'HEIDI.CURTIS', 'Heidi.Curtis@todo.io', 'Heidi', 'Curtis', 'HEIDI.CURTIS@TODO.IO', 1, '143u5HOu', 'ngjKlPAF', 0, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (23, '084d562d-e290-49b6-8516-f3b61b07d7c0', 'Faith.Edwards', 'FAITH.EDWARDS', 'Faith.Edwards@todo.io', 'Faith', 'Edwards', 'FAITH.EDWARDS@TODO.IO', 0, 'fNeGU03Z', 'e2MaJhEY', 1, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (24, 'a0161595-99f4-4951-a22d-7eff8d393adc', 'Helen.Trollope', 'HELEN.TROLLOPE', 'Helen.Trollope@todo.io', 'Helen', 'Trollope', 'HELEN.TROLLOPE@TODO.IO', 0, 'ATwx7BNq', 'YtCyNXnf', 1, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (25, '6be9c2f1-5d0a-433a-a3fe-f477ac768e23', 'Bonnie.Brown', 'BONNIE.BROWN', 'Bonnie.Brown@todo.io', 'Bonnie', 'Brown', 'BONNIE.BROWN@TODO.IO', 0, 'EV9atcNN', 'XGBiotgh', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (26, 'dcfc08dd-6033-4409-9af0-bd42b1c3cfab', 'Rose.Butler', 'ROSE.BUTLER', 'Rose.Butler@todo.io', 'Rose', 'Butler', 'ROSE.BUTLER@TODO.IO', 1, 'R6FQjaqP', 'hmS8Ehsn', 0, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (27, '8d7e6c9d-8452-4afb-a483-d804b3618b0d', 'Martha.Tenley', 'MARTHA.TENLEY', 'Martha.Tenley@todo.io', 'Martha', 'Tenley', 'MARTHA.TENLEY@TODO.IO', 0, '256ERLk3', 'jGAd2VSb', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (28, 'a3221496-6bbb-4f82-848f-9dc9d45caf11', 'Leah.Remington', 'LEAH.REMINGTON', 'Leah.Remington@todo.io', 'Leah', 'Remington', 'LEAH.REMINGTON@TODO.IO', 0, 'ytzCPX6h', '7y3w6iHT', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (29, 'e915b363-feb7-4eee-bf02-7b40a4280eb1', 'Emily.Crawford', 'EMILY.CRAWFORD', 'Emily.Crawford@todo.io', 'Emily', 'Crawford', 'EMILY.CRAWFORD@TODO.IO', 0, 'gcMTu2Lt', 'L654gD2e', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (30, '958462d2-020b-4ac4-af15-343553c40109', 'Willow.Branson', 'WILLOW.BRANSON', 'Willow.Branson@todo.io', 'Willow', 'Branson', 'WILLOW.BRANSON@TODO.IO', 0, 'T0HLkatH', 'LQTcYRSv', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (31, '3423828a-ef4e-4d78-8fa5-d72a5198f510', 'Thea.Hampton', 'THEA.HAMPTON', 'Thea.Hampton@todo.io', 'Thea', 'Hampton', 'THEA.HAMPTON@TODO.IO', 0, 'RhDaJt2u', 'sEv2TPhY', 0, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (32, 'f8bd1131-fc82-4b1f-b7e8-df744266b2d8', 'Sophie.Clive', 'SOPHIE.CLIVE', 'Sophie.Clive@todo.io', 'Sophie', 'Clive', 'SOPHIE.CLIVE@TODO.IO', 0, '5jfCodsP', 'm6vFEn9C', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (33, 'a387fa8c-eaa5-496a-af47-99240b9ec6bc', 'Bella.Law', 'BELLA.LAW', 'Bella.Law@todo.io', 'Bella', 'Law', 'BELLA.LAW@TODO.IO', 1, 'bLvAhqVZ', 'yoGm8yiJ', 1, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (34, '2d410622-337e-473c-8ab3-ba97ac63860d', 'Izzy.Hayley', 'IZZY.HAYLEY', 'Izzy.Hayley@todo.io', 'Izzy', 'Hayley', 'IZZY.HAYLEY@TODO.IO', 0, 'K0evZhWc', 'hSjfulEs', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (35, '095f5e70-4a18-42fe-a8a9-09d98adf513e', 'Jasmine.Hayhurst', 'JASMINE.HAYHURST', 'Jasmine.Hayhurst@todo.io', 'Jasmine', 'Hayhurst', 'JASMINE.HAYHURST@TODO.IO', 1, 'prb6Ce49', 'jWzr68q6', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (36, '2ec19834-99e5-48ad-bddf-9a323ea3d30e', 'Mollie.Hadlee', 'MOLLIE.HADLEE', 'Mollie.Hadlee@todo.io', 'Mollie', 'Hadlee', 'MOLLIE.HADLEE@TODO.IO', 0, 'Hvc9OcOn', 'mpqlj21D', 0, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (37, '0713a7b1-0b13-4f69-a685-8dbe8dfe4164', 'Bella.Taylor', 'BELLA.TAYLOR', 'Bella.Taylor@todo.io', 'Bella', 'Taylor', 'BELLA.TAYLOR@TODO.IO', 1, 'qNvb2mEo', '73E918Dr', 1, 0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (38, 'b19bf33f-7d9a-40e8-9bb2-259e0094138b', 'Hollie.Read', 'HOLLIE.READ', 'Hollie.Read@todo.io', 'Hollie', 'Read', 'HOLLIE.READ@TODO.IO', 0, '91qly3NN', '1KH6hNp7', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (39, '6ea2bd78-672a-40d6-a48c-97b7e6925b41', 'Robyn.Trollope', 'ROBYN.TROLLOPE', 'Robyn.Trollope@todo.io', 'Robyn', 'Trollope', 'ROBYN.TROLLOPE@TODO.IO', 0, '8JCdIhHG', 'yXLLhOzX', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (40, 'a47fa035-3854-4a76-8d2e-4e906999b2d1', 'Lily.Garrick', 'LILY.GARRICK', 'Lily.Garrick@todo.io', 'Lily', 'Garrick', 'LILY.GARRICK@TODO.IO', 1, 'xZAzVhZZ', 'kWW7IYWW', 1, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (41, '5a936956-aa30-4618-a9f5-d8c49fcb7254', 'Shaun.Pickering', 'SHAUN.PICKERING', 'Shaun.Pickering@todo.io', 'Shaun', 'Pickering', 'SHAUN.PICKERING@TODO.IO', 1, 'HPexnyKH', 'UAVts0td', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (42, '0bc25bde-c5b8-4728-a9e4-9dbf60c88ec4', 'Lewis.Beckwith', 'LEWIS.BECKWITH', 'Lewis.Beckwith@todo.io', 'Lewis', 'Beckwith', 'LEWIS.BECKWITH@TODO.IO', 1, 'lrFwyPbK', 'U7vltYbV', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (43, '829d95ba-5422-4db1-908a-75235e5519af', 'Finn.Wakefield', 'FINN.WAKEFIELD', 'Finn.Wakefield@todo.io', 'Finn', 'Wakefield', 'FINN.WAKEFIELD@TODO.IO', 1, 'E9iJRPRy', 'TQkac8ss', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (44, 'ae580961-84f7-4d80-9a94-4a4c2f02ea8a', 'Seth.Braxton', 'SETH.BRAXTON', 'Seth.Braxton@todo.io', 'Seth', 'Braxton', 'SETH.BRAXTON@TODO.IO', 0, 'CXvUgcla', '0KUjmQnc', 0, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (45, '2179b3b3-f38c-4d0a-9207-3e8cb0af9e82', 'Lincoln.Ford', 'LINCOLN.FORD', 'Lincoln.Ford@todo.io', 'Lincoln', 'Ford', 'LINCOLN.FORD@TODO.IO', 1, '1MjCblhp', 'MxWDsf8H', 0, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (46, '9d7d8219-e920-4c5e-93e3-cdeabf5f8086', 'Zachary.Curtis', 'ZACHARY.CURTIS', 'Zachary.Curtis@todo.io', 'Zachary', 'Curtis', 'ZACHARY.CURTIS@TODO.IO', 1, 'Y1Hxv7gs', 'mgU8j2HM', 0, 1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
  (47, 'cb92938e-56de-44f6-85d2-946981f131c2', 'Bradley.Tattersall', 'BRADLEY.TATTERSALL', 'Bradley.Tattersall@todo.io', 'Bradley', 'Tattersall', 'BRADLEY.TATTERSALL@TODO.IO', 1, 'c3HaT6S6', '7Zg30REK', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (48, 'eb1168c4-9a5b-4cb3-9455-7d651618eb7e', 'Joe.Landon', 'JOE.LANDON', 'Joe.Landon@todo.io', 'Joe', 'Landon', 'JOE.LANDON@TODO.IO', 1, 'YuJUp40z', 'm5LboxWt', 1, 1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
  (49, '6e474fd3-ad67-4911-9dbe-0ba83290bd5e', 'Dexter.Shepherd', 'DEXTER.SHEPHERD', 'Dexter.Shepherd@todo.io', 'Dexter', 'Shepherd', 'DEXTER.SHEPHERD@TODO.IO', 0, 'CZ9e87wq', 'MplkMYY7', 1, 0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4')
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO


SET IDENTITY_INSERT [dbo].[Todo] ON
GO
INSERT INTO [dbo].[Todo]
  ([Id], [UniqueId], [Summary], [Detail], [IsDone], [AssigneeGuid])
VALUES
  (1, 'b7837fd6-79b6-4d36-8a27-99e92ff31772', 'anivvuwY', 'HX1MfCbxvVt2dFrCOJcqTE34xKPnQhC6QoBuqa9BZckYODURjr', 1, null),
  (2, '025fa8e5-dd07-4026-ae7b-d34b8bb07ef0', 'ngQKD5nt', '5HBZt4UKjuN42jhHw47I MwB8NNLCuXJYLAS4wY6TBmoDsntNi', 1, null),
  (3, 'bdd0337c-625c-4cc9-bbed-fdb970d90e28', 'BexdcJp0', 'pbbcjyQgivjdtzgMi5vknpAUXqonu2Pipi5bCFEy qdNIqIGh ', 0, null),
  (4, 'c283e1b8-743f-4a74-942a-b7d9e32703de', '9kmp8gBH', 'hdOUqcwKpm1lJE926rhfNypDr6jl6WGDEhVQYARDamblTvwHyj', 0, null),
  (5, '790f0bed-e167-46ef-927b-1cc654981cff', 'Vw5ovjZX', 'f6gTt4yyYI7r5mFaKp3j9BIUPTRKOzYqFoNEzk0KhR3kIV93Ay', 0, null),
  (6, 'd4527e95-486f-4359-84bc-2b32986b6f71', 'r9awQOLh', 'NfTRtU62N6xSvg7R51MfMaRpAz7xka0aSI 9YLHhPF0Nqw4TmC', 0, null),
  (7, '5c16e725-242f-4b64-acac-f6c52f4f73e4', 'HqmdxLNF', '1CaXQOfpFnayqOwD9x92TxXLZWJ1B5N61q5UTLuexQh1EZBcz8', 1, null),
  (8, 'f3093acc-7a74-49b2-820e-205237d44d7d', '40Dxh6xi', 'eh34I8SXYzpqVAtbEZekL VNzA42mPlOV4ooZu1lrcwGXh vVJ', 0, null),
  (9, 'abb0cf19-f08b-4017-b44c-44a2a72c0820', 'FrWn3UBe', 'mk2IQ4IppoSkWYrzrsZS3hUaY7Z6uBRy2s8qO0zQ42lUooHaIa', 1, null),
  (10, 'e41a5d81-f2b6-432f-a6a4-1a1afeed977c', 'Xkqbesug', '9aHsroCUyexCcrX69itxElRQYjGPrcGR5ownPa1QZJxNpWgwuu', 0, null),
  (11, '11d7e64e-9b5a-4acc-a771-f53612064c3a', 'bmrQzhJm', 'eNbBD8rvSsdkfRzgldj3SYVEvT6x4TCNi7eb6Z32EV8sDdZyJZ', 0, null),
  (12, '606d3a25-7afc-4076-8448-b1c34cb1259d', 'fHASKLJ3', '1cbx0WrpB0X56MpGB4Kub9Y9WyiTgYfQT 8ezUu53ZQ1N1sDEe', 1, null),
  (13, 'd9f7f21c-5629-46c6-be1f-b1591ddeff0c', 'O9WENFBD', 'RdRtEAgpaiV95PerMkX578EN8XabP1hCdPhkzSZeL gPBBJv2J', 1, null),
  (14, '98d3f37b-2468-4453-8a08-9e8aaa96bf77', 'hiCVNPns', '8XcNukdIeLesjYOAtjHmboPhwkuQjNw0M5dkVHTfW iF5Cqgc6', 0, null),
  (15, '84ba8808-02e4-45bf-bf25-04ad0f220c54', 'kOc9YbrF', 'VTCs3ZeDv0TJDNK9CNsdaTdIXnbyUFqp4rhYulw8AYN bf3paL', 1, null),
  (16, 'ce6e3254-6d2b-4dc0-a413-57ea0c92a005', 'VykXC13f', 'jpD4nSWt1Cc7uKSOAv6qRqvdlj6sk4ifm47PQpXMwWxfOp d2P', 0, null),
  (17, '9cdbeb7b-e3e7-4b0c-b55a-0e610399f689', '3pq6fvCe', 'VlrWIBy44ZJqUtxtXAKHLMQ7NfaQljhY9YM9uwpdHfCZK3fZT ', 0, null),
  (18, '23d96864-3482-443b-93ae-0aade26a59e4', 'ibATDpb3', 'UVwVDCSkepBXcZkMd8xzKd9SxuI7eBnaSXrQyauaUy08HfDoTC', 0, null),
  (19, 'f863a2f2-fb35-4172-89ce-7ef2d8c517f8', 'OXeJSVzh', 'yyZzm4zYMmpUg7xt5vuMWFRE1Hu8n9Me7bcFnyCfV0rMwExqGl', 1, null)
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
