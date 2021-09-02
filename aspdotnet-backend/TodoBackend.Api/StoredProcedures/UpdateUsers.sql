
BEGIN TRANSACTION Test
declare @UniqueId uniqueidentifier;
set @UniqueId = 'c5c9fd97-0af2-4e46-8d89-074648faac25';
declare @FirstName nvarchar(100);
set @FirstName = 'Bobby';
declare @LastName nvarchar(100);
set @LastName = 'Builder';
declare @Email nvarchar(150);
set @Email = 'hello_world@gmail.com';
declare @Hash nvarchar(150);
set @Hash = '343533';
declare @Updated datetime;
set @Updated = GETUTCDATE()
declare @RoleId int;
set @RoleId = 2

select *
from Users u
    inner join Roles r on u.RoleId = r.Id
where u.UniqueId = 'c5c9fd97-0af2-4e46-8d89-074648faac25';

if object_id('tempdb.#NewValues') is not null
begin
    drop table #NewValues
end
create table #NewValues
(
    UniqueId uniqueidentifier,
    FirstName nvarchar(100),
    LastName nvarchar(100),
    Email nvarchar(150),
    [Hash] nvarchar(150),
    Updated datetime,
    RoleId int
)

insert into #NewValues
    (UniqueId, FirstName, LastName, Email, Hash, Updated, RoleId)
Select @UniqueId, @FirstName, @LastName, @Email, @Hash, @Updated, @RoleId

if object_id('tempdb.#Outcome') is not null
begin
    drop table #Outcome
end

create table #Outcome
(
    UserId int,
    UserCreated datetime,
    UserUpdated datetime,
    RoleUniqueId uniqueidentifier,
    RoleKind nvarchar(150),
    RoleDescription nvarchar(max),
    RoleCreated datetime,
    RoleUpdated datetime
);


update u
    set u.FirstName = @FirstName,
        u.LastName = @LastName,
        u.Email = @Email,
        u.Hash = @Hash,
        u.Updated = getutcdate(),
        u.RoleId = @RoleId
    from dbo.Users u
    where u.UniqueId = @UniqueId
    and exists
(
        select u.FirstName,
            u.LastName,
            u.Email,
            u.Hash,
            u.Updated,
            u.RoleId
        except
        select nv.FirstName,
            nv.LastName,
            nv.Email,
            nv.Hash,
            nv.Updated,
            nv.RoleId
        from #NewValues nv
        where nv.UniqueId = u.UniqueId
)

insert into #Outcome(UserId, UserCreated, UserUpdated, RoleUniqueId, RoleKind, RoleDescription, RoleCreated, RoleUpdated)
select u.Id,
       u.Created,
       u.Updated,
       r.UniqueId,
       r.Kind,
       r.Description,
       r.Created as RoleCreated,
       r.Updated as RoleUpdated
from dbo.Users as u with (nolock)
    inner join dbo.Roles as r with (nolock) on u.RoleId = r.Id
where u.UniqueId = @UniqueId;

select *
from Users u
    inner join Roles r on u.RoleId = r.Id
where u.UniqueId = 'c5c9fd97-0af2-4e46-8d89-074648faac25';

select *
from #Outcome;
ROLLBACK TRANSACTION Test