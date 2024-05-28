CREATE PROCEDURE [WORK].[TaskUserCacheAggregate] @pTenantID smallint as
set
    nocount on
set
    xact_abort on
set
    ansi_nulls on
set
    ansi_warnings on
set
    ansi_padding on
set
    ansi_null_dflt_on on
set
    arithabort on
set
    quoted_identifier on
set
    concat_null_yields_null on
set
    implicit_transactions off
set
    cursor_close_on_commit off
set
    transaction isolation level read uncommitted begin declare @IsOwnerOnly bit = 0,
    @AssignedTo tinyint = 2,
    @DistrictAvailable tinyint = 13,
    @UserWorkType tinyint = 16,
    @AllTaskAvailable tinyint = 19,
    @CreatedBy tinyint = 20,
    @Now datetime2(0) = getutcdate() if(1 = 2) begin create table #Task
    (
        ID int,
        AssetID int,
        ApprovalWith int,
        EscalatedTo int,
        CreatedBy int,
        RequestedBy int,
        WorkTypeID smallint,
        Archived datetime2(0),
        Deleted datetime2(0)
    );

create table #User(ID int);
create table #TaskUserCache
(
    TaskID int,
    UserID int,
    TaskListCategoryID tinyint
);

end create table #ListCategory
(
    ID tinyint,
    PermissionExtID smallint
);

create table #Employment
(
    UserID int,
    CompanyID smallint,
    TaskListCategoryID tinyint
);

create table #TaskResponsibleUser
(TaskID int, UserID int);

create table #UserTaskListCategory
(
    UserID int,
    TaskListCategoryID tinyint,
    primary key clustered (TaskListCategoryID, UserID) WITH (IGNORE_DUP_KEY = ON)
);

insert into
    #ListCategory(ID, PermissionExtID)
select
    tlc.ID,
    tlc.PermissionExtID
from
    WORK.TaskListCategory tlc
where
    tlc.ID in (
        @DistrictAvailable,
        @UserWorkType,
        @AllTaskAvailable
    );

insert into
    #UserTaskListCategory(UserID, TaskListCategoryID)
    exec ADM.UserListCategoryGet @pTenantID;

if exists (
    select
        1
    from
        #UserTaskListCategory where TaskListCategoryID = @AllTaskAvailable
) begin
insert into
    #TaskUserCache (TaskID, UserID, TaskListCategoryID)
select
    t.ID,
    tlc.UserID,
    tlc.TaskListCategoryID
from
    #UserTaskListCategory tlc
    cross join #Task t
where
    tlc.TaskListCategoryID = @AllTaskAvailable delete u
from
    #User u
where
    exists (
        select
            1
        from
            #TaskUserCache tuc
        where
            tuc.UserID = u.ID
            and tuc.TaskListCategoryID = @AllTaskAvailable
    );

end if not exists (
    select
        1
    from
        #User)
        return;

insert into
    #TaskUserCache(TaskID, UserID, TaskListCategoryID)
select
    t.ID,
    tm.UserID,
    @CreatedBy
from
    #Task             t
    join ADM.TenantMember tm on tm.ID = t.CreatedBy
where
    tm.TenantID = @pTenantID
    and exists (
        select
            1
        from
            #User u
        where
            u.ID = tm.UserID
    );

/* Пользователи, привязанные к виду работ, указанному в завяке */
if exists (
    select
        1
    from
        #UserTaskListCategory where TaskListCategoryID = @UserWorkType)
    insert into
        #TaskUserCache(TaskID, UserID, TaskListCategoryID)
    select
        t.ID,
        tlc.UserID,
        tlc.TaskListCategoryID
    from
        #UserTaskListCategory tlc
        cross join #Task t
    where
        tlc.TaskListCategoryID = @UserWorkType
        and exists (
            select
                1
            from
                PA.UserWorkType uwt
                join WORK.WorkType wt on wt.TenantID = uwt.TenantID
                and wt.ID = uwt.WorkTypeID
            where
                uwt.TenantID = @pTenantID
                and uwt.UserID = tlc.UserID
                and uwt.WorkTypeID = t.WorkTypeID
                and uwt.Deleted is null
                and wt.Deleted is null
        );

exec WORK.TaskUserCacheAggregateResponsibility @pTenantID = @pTenantID;

exec ADM.TaskUserCacheAggregate @pTenantID = @pTenantID;

end