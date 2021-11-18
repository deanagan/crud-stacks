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

SET IDENTITY_INSERT [dbo].[Users] ON
GO
INSERT INTO [dbo].[Users](
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
(1, 'da7f7329-4f1a-446b-9d1e-ea2904cbf5a9', 'mPBy70fB.MMvBnskd', 'MPBY70FB.MMVBNSKD', 'HVPhIipi@todo.io', 'mPBy70fB', 'MMvBnskd','HVPHIIPI@TODO.IO', 1, '3b0jymbn', 'JxEezvCg', 0,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(2, '653cfd12-6a59-46ab-b008-e49a8f532fb0', '4YqBiHIi.kwhRARKw', '4YQBIHII.KWHRARKW', 'kK3d6DqL@todo.io', '4YqBiHIi', 'kwhRARKw','KK3D6DQL@TODO.IO', 0, 'qBBZIvvZ', 'Uz9NyyfJ', 1,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(3, '7e8b5094-8dcb-4e86-b481-2b5da2ef9e27', 'o1UquYp3.poYRME18', 'O1UQUYP3.POYRME18', 'y7HPWnnt@todo.io', 'o1UquYp3', 'poYRME18','Y7HPWNNT@TODO.IO', 0, 'fjVYyF6F', 'AcgkKAff', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(4, '08661f2f-4fef-4bd9-ada2-764a6a908d35', '9F20gtkh.ROuy6DBu', '9F20GTKH.ROUY6DBU', 'V25HyuW0@todo.io', '9F20gtkh', 'ROuy6DBu','V25HYUW0@TODO.IO', 1, '0K0XfK89', 'K7PmkaHd', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(5, '668567a5-56da-4041-b434-dc779596e552', 'DUIt8kVk.U24mTS5Z', 'DUIT8KVK.U24MTS5Z', 'yiqXQcC7@todo.io', 'DUIt8kVk', 'U24mTS5Z','YIQXQCC7@TODO.IO', 0, 'C7CUAhJw', 'OU2Ozcem', 0,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(6, 'cde588ce-a5d3-4a50-b29a-10b7b43f878b', 'AKJdyrrm.dYUq1oPA', 'AKJDYRRM.DYUQ1OPA', 'lSTnBMgW@todo.io', 'AKJdyrrm', 'dYUq1oPA','LSTNBMGW@TODO.IO', 0, 'rVMmrkSL', '47l3dSI1', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(7, 'a4a404e4-c24c-4bc9-b5bc-59608d67073e', 'idlwRpzH.DlO5JfJw', 'IDLWRPZH.DLO5JFJW', '1fxpQLkI@todo.io', 'idlwRpzH', 'DlO5JfJw','1FXPQLKI@TODO.IO', 0, 'xDTJSzvK', 'mb8ou5Ga', 0,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(8, '1d223e12-0277-4144-bdae-f83af3fc0951', 'QOZO5Ln8.Q7PJMuHC', 'QOZO5LN8.Q7PJMUHC', 'xehaYROk@todo.io', 'QOZO5Ln8', 'Q7PJMuHC','XEHAYROK@TODO.IO', 0, 'knFOaAh5', 'urexBo6A', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(9, '0736fc87-88df-4d13-95b5-a085c25c11d6', '2zrp27pG.WQpJ3nyN', '2ZRP27PG.WQPJ3NYN', '579qIk6H@todo.io', '2zrp27pG', 'WQpJ3nyN','579QIK6H@TODO.IO', 0, 'yL5aV5KA', 'oatAl3q8', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(10, 'ccdb932f-3cb1-4198-930d-9bebaf9b2994', 'Hpn3jbR2.3LJywKnJ', 'HPN3JBR2.3LJYWKNJ', '0IeTIgnG@todo.io', 'Hpn3jbR2', '3LJywKnJ','0IETIGNG@TODO.IO', 0, '2QkTbVVN', 'y0cP6OaX', 1,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(11, '91736a56-673f-4eda-a874-dfbef93a0708', 'KbqOcWTM.GW7CkwCe', 'KBQOCWTM.GW7CKWCE', 'eTAfPCQH@todo.io', 'KbqOcWTM', 'GW7CkwCe','ETAFPCQH@TODO.IO', 0, 'ejs6JtCR', 'zSG4Zz8T', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(12, 'a697d465-a7fe-4023-b6b5-f677878cb955', 'qZQpeeI9.glyDpA8H', 'QZQPEEI9.GLYDPA8H', 'DxbO4yDz@todo.io', 'qZQpeeI9', 'glyDpA8H','DXBO4YDZ@TODO.IO', 0, 'X3ZXESzD', 'RczDvxkT', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(13, '50ae5a90-8aca-4f2e-9ca4-fa59eba9dd97', 'MD6FE28t.IyGcQhkQ', 'MD6FE28T.IYGCQHKQ', 'xcba4JqO@todo.io', 'MD6FE28t', 'IyGcQhkQ','XCBA4JQO@TODO.IO', 0, 'Ay7gchhn', '2NYIodq0', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(14, '7896513b-97b1-4a6b-bf3e-3dbf5812816f', 'WmNKUHiO.G8z9vXta', 'WMNKUHIO.G8Z9VXTA', 'NGw2SOXs@todo.io', 'WmNKUHiO', 'G8z9vXta','NGW2SOXS@TODO.IO', 0, 'Oi4k9EVY', 'l1LXelwT', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(15, 'f8b3abd8-58ff-4c27-b5c5-acd9a8be30f4', 'bulFypCx.8LMYIDRR', 'BULFYPCX.8LMYIDRR', '0AadXQux@todo.io', 'bulFypCx', '8LMYIDRR','0AADXQUX@TODO.IO', 1, 'ApxCgAVp', 'lkDRmwV6', 0,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(16, '86a49d82-d3b8-4d21-bf5a-98b3cf94df65', 'MtXPwgDB.V9rUftLX', 'MTXPWGDB.V9RUFTLX', 'ZyswvRQW@todo.io', 'MtXPwgDB', 'V9rUftLX','ZYSWVRQW@TODO.IO', 1, 'IqbpKErJ', '1Xoma1bK', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(17, '0ae67cf8-5223-4ec6-b975-8e51338a7a0b', 'UbAN92A1.zLfZoyik', 'UBAN92A1.ZLFZOYIK', 'jvBFVnQq@todo.io', 'UbAN92A1', 'zLfZoyik','JVBFVNQQ@TODO.IO', 1, 'VL7Z7vN0', 'OhuzVx1Y', 1,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(18, 'fa17531b-ce5d-4902-bc52-006385755846', '2XA9VJsp.8FVwjxtA', '2XA9VJSP.8FVWJXTA', 'ZlZU4RoN@todo.io', '2XA9VJsp', '8FVwjxtA','ZLZU4RON@TODO.IO', 0, 'NqBEMlRU', '8HgMntho', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(19, 'f1a5e9a1-807d-46f2-a205-a3050770306c', 'zGD94WMx.IRnWIyZA', 'ZGD94WMX.IRNWIYZA', 'cXT6wKRv@todo.io', 'zGD94WMx', 'IRnWIyZA','CXT6WKRV@TODO.IO', 0, 'WYviPJ1M', 'Q0Z0HgWv', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(20, '658b8ea0-a7de-46ca-8d12-808d1190e06d', '1hMWyKGm.CZJTwd2f', '1HMWYKGM.CZJTWD2F', '88tbkhiS@todo.io', '1hMWyKGm', 'CZJTwd2f','88TBKHIS@TODO.IO', 1, 'BdJZE4Pu', 'dl1xTLjt', 0,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(21, '71c85e49-d633-45bd-aaab-4b7060fac709', 'qSKu0sf4.PVA3ImOD', 'QSKU0SF4.PVA3IMOD', 'gFECCp0R@todo.io', 'qSKu0sf4', 'PVA3ImOD','GFECCP0R@TODO.IO', 0, 'IEnZchvB', 'Sgqm4uid', 1,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(22, 'b5522e30-929f-48eb-8a8f-96d56246d3c1', '8blvN2jw.FHTtX1Wn', '8BLVN2JW.FHTTX1WN', 'F06Z3BcH@todo.io', '8blvN2jw', 'FHTtX1Wn','F06Z3BCH@TODO.IO', 1, 'lSYciKun', 'SVJqDCkT', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(23, 'f895c4e0-235d-4f5d-b3b6-6e367d3df969', 'fC54Zj5x.E56KagF0', 'FC54ZJ5X.E56KAGF0', 'TLpamAuf@todo.io', 'fC54Zj5x', 'E56KagF0','TLPAMAUF@TODO.IO', 0, 'epOrcJId', 'qjeI3u55', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(24, 'd23f21ed-a4c3-4812-9dbf-d489413303c8', 'Ng4PWF3O.DQ2T3r9q', 'NG4PWF3O.DQ2T3R9Q', 'UN7nQTaC@todo.io', 'Ng4PWF3O', 'DQ2T3r9q','UN7NQTAC@TODO.IO', 1, 'VRXe1qNt', 'mgxH6je9', 0,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(25, '822fb028-945f-482d-bb0c-de0d0fcfd471', 'TraVzAuN.awaB4FoN', 'TRAVZAUN.AWAB4FON', '03RfHAcM@todo.io', 'TraVzAuN', 'awaB4FoN','03RFHACM@TODO.IO', 1, 'roe67xLk', 'ZhS86KaE', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(26, 'b5d8c173-4a0d-4eac-ac28-e156c1908c9e', '0N38gJiC.Hcn1V6Hg', '0N38GJIC.HCN1V6HG', 'VVI2OVRa@todo.io', '0N38gJiC', 'Hcn1V6Hg','VVI2OVRA@TODO.IO', 1, 'gjlwrCw8', 'prmENcs0', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(27, '146ecb0f-721c-4b3a-84ef-b8fab0b25f8a', 'L3WNgYE6.ezzbtqLQ', 'L3WNGYE6.EZZBTQLQ', 'hrQVBCR6@todo.io', 'L3WNgYE6', 'ezzbtqLQ','HRQVBCR6@TODO.IO', 1, 'RbLvlJnH', 'yralaEp4', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(28, '190e3fbc-e876-469e-b8ba-e0e1513fd6cb', '2bZAWNqJ.gh7bTVMQ', '2BZAWNQJ.GH7BTVMQ', 'SCG7aJ66@todo.io', '2bZAWNqJ', 'gh7bTVMQ','SCG7AJ66@TODO.IO', 0, 'Ysn6qng2', 'WMNFHIcL', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(29, '92a16199-9310-4a6c-b5f8-261063634e2e', 'WglX6pcq.29YfPlSx', 'WGLX6PCQ.29YFPLSX', 'qUEotZVo@todo.io', 'WglX6pcq', '29YfPlSx','QUEOTZVO@TODO.IO', 1, '9uUDobR6', 'gIwoRrVI', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(30, '008c8952-f926-4817-8f2c-c76ef1eca31a', 'Qso1SgJM.VrPoApty', 'QSO1SGJM.VRPOAPTY', '4xLrqTYd@todo.io', 'Qso1SgJM', 'VrPoApty','4XLRQTYD@TODO.IO', 1, '2ngASrWa', 'iMXwXjlx', 1,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(31, '478701c9-f329-452f-a0b3-9c722860fc23', 'xtRmL0ez.yPwdNkC7', 'XTRML0EZ.YPWDNKC7', 'JhVEJ9j2@todo.io', 'xtRmL0ez', 'yPwdNkC7','JHVEJ9J2@TODO.IO', 0, 'Reinkp7u', 'jXuRSaf6', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(32, 'd79fb881-a73d-42af-b56c-117fc6ba6c7e', '7fVjB63F.1TyCkQng', '7FVJB63F.1TYCKQNG', 'oAsS5cH9@todo.io', '7fVjB63F', '1TyCkQng','OASS5CH9@TODO.IO', 1, 'wv46mpGb', 'DCpnoGlU', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(33, '3e713b21-5c48-4a55-89d8-4a9d2a21e476', '1iCNcB4X.t9pCsSFz', '1ICNCB4X.T9PCSSFZ', '9bcaOywb@todo.io', '1iCNcB4X', 't9pCsSFz','9BCAOYWB@TODO.IO', 0, 'H1AL17qJ', 'NJKeoS0U', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(34, '1b342815-6816-4418-8cc2-23def7a38713', 'LZSvCtHm.dwFNxkYO', 'LZSVCTHM.DWFNXKYO', 'HOe1PcMJ@todo.io', 'LZSvCtHm', 'dwFNxkYO','HOE1PCMJ@TODO.IO', 1, 'DgXV3Nx6', 'h3brrCpF', 1,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(35, '39bc95c6-33d8-4a8e-a760-b69978741dd5', 'sm1NsFwR.bp9fO0Vu', 'SM1NSFWR.BP9FO0VU', 'jvtmR6Ob@todo.io', 'sm1NsFwR', 'bp9fO0Vu','JVTMR6OB@TODO.IO', 0, 'l85gREUY', 'A5wWjZ3z', 0,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(36, 'aaf73389-ea91-4bfb-a786-72594bb4c01f', 'DcIzBFeq.S6uyacqQ', 'DCIZBFEQ.S6UYACQQ', 'MKavNvxK@todo.io', 'DcIzBFeq', 'S6uyacqQ','MKAVNVXK@TODO.IO', 0, '7yCUVQCO', 'LWbt4eJM', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(37, 'e78c7090-6cf7-4fcf-91bc-bae2e1dea1f7', '8USHsSmC.20oyMFT8', '8USHSSMC.20OYMFT8', 'jj0F8gzX@todo.io', '8USHsSmC', '20oyMFT8','JJ0F8GZX@TODO.IO', 1, '5pTSTZ3q', 'kagZPozK', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(38, '7fc2cb69-cadf-42e2-b8aa-280373840af8', 'BRdNh6g1.EBxqmJbV', 'BRDNH6G1.EBXQMJBV', 'GgpMD4YL@todo.io', 'BRdNh6g1', 'EBxqmJbV','GGPMD4YL@TODO.IO', 1, 'gRw7IqKQ', 'EZwO66YL', 0,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(39, '0e4c3f49-2418-4643-b68f-f52a20ad96b7', 'WWBamcaG.IIIvytEL', 'WWBAMCAG.IIIVYTEL', 'wy3sLoiJ@todo.io', 'WWBamcaG', 'IIIvytEL','WY3SLOIJ@TODO.IO', 1, 'tWL0cDGi', 'a1SxW2If', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(40, '6711800b-be71-4343-9f15-72852f644635', 'gDb9bPzX.aZ359pJZ', 'GDB9BPZX.AZ359PJZ', 'uGJC6utl@todo.io', 'gDb9bPzX', 'aZ359pJZ','UGJC6UTL@TODO.IO', 0, 'hT0kgEZr', 'DB67AtFh', 0,1, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(41, '90b8c901-c71a-4b7a-bce1-c53b28c67d0b', 'T2H0VL1r.gAUWETaT', 'T2H0VL1R.GAUWETAT', 'ja0ifzIb@todo.io', 'T2H0VL1r', 'gAUWETaT','JA0IFZIB@TODO.IO', 1, 'DPP3REYj', 'ibN8hdco', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(42, '89db9a82-9559-4e89-a08b-e05358429035', 'SGCUrp24.wXUVksd7', 'SGCURP24.WXUVKSD7', 'MNXGgtAZ@todo.io', 'SGCUrp24', 'wXUVksd7','MNXGGTAZ@TODO.IO', 1, 'wPT7uQOx', 'i9q6BzgG', 0,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(43, 'cd8bc822-3de3-4586-aa21-21c329079d9c', 'iKOxjNmC.z5WDhg8a', 'IKOXJNMC.Z5WDHG8A', 'GNx12mk7@todo.io', 'iKOxjNmC', 'z5WDhg8a','GNX12MK7@TODO.IO', 0, '2GfzC1gJ', 'Rw11nmOo', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(44, '31d3e42e-fbb3-4341-b4d8-53a974277974', 'CNgoEbHA.SeNs9zgm', 'CNGOEBHA.SENS9ZGM', 'e0fx6dnN@todo.io', 'CNgoEbHA', 'SeNs9zgm','E0FX6DNN@TODO.IO', 1, 'GJAEw5eY', 'H8J9Kl1L', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(45, '0d22c1bd-349a-4c13-b748-12d779287953', 'Xd5I8un9.ZgC24o00', 'XD5I8UN9.ZGC24O00', 'ZcWZIy8f@todo.io', 'Xd5I8un9', 'ZgC24o00','ZCWZIY8F@TODO.IO', 1, 'uLpkeTIV', 'm9vIlbbs', 1,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(46, '8c35889f-87ab-48ad-ba3d-f1a2437ebe04', 'lyda5Jre.PzVqfVpT', 'LYDA5JRE.PZVQFVPT', 'YmB2nvoe@todo.io', 'lyda5Jre', 'PzVqfVpT','YMB2NVOE@TODO.IO', 0, 'SIqOUo0k', 'YtT9xpGI', 0,0, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(47, '1feb46a7-5f03-4e65-868e-e89b2a5b5e07', 'zFUHPkWf.DNXMDW42', 'ZFUHPKWF.DNXMDW42', 'AnWqthqp@todo.io', 'zFUHPkWf', 'DNXMDW42','ANWQTHQP@TODO.IO', 0, 'w3jgOfOf', 'lCT0sVc1', 1,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C') ,
(48, '3c7eb92f-71bd-483f-aa59-304ac43db3fe', 'vzfmxi8h.cwa91AVj', 'VZFMXI8H.CWA91AVJ', '747PScFL@todo.io', 'vzfmxi8h', 'cwa91AVj','747PSCFL@TODO.IO', 1, 'I54DvGVg', 'Mzwrh8XW', 1,1, 0, 'B7D50830-622B-443C-8BC8-AAB6D1C6C3C4') ,
(49, '0a6cb650-82c1-4b20-a149-b246d2da8b5a', 'enY3wErW.57wJpHIu', 'ENY3WERW.57WJPHIU', 'hVPthRjs@todo.io', 'enY3wErW', '57wJpHIu','HVPTHRJS@TODO.IO', 0, '4GXvHPSC', 'HjXQyWjG', 0,0, 0, '804F7003-5777-4471-B1D4-B793D3FB643C')
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO


SET IDENTITY_INSERT [dbo].[Todo] ON
GO
INSERT INTO [dbo].[Todo]([Id], [UniqueId], [Summary], [Detail], [IsDone], [AssigneeGuid])
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
