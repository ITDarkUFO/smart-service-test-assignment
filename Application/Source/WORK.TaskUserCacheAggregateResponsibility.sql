CREATE PROCEDURE [WORK].[TaskUserCacheAggregateResponsibility] @pTenantID smallint as
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
    transaction isolation level read uncommitted begin declare @AssignedTo tinyint = 2,
    @DistrictAvailable tinyint = 13 
    
    if(1 = 2) begin 
    
    create table #User(ID int);

     create table #Task
     (
         ID int,
         ApprovalWith int,
         EscalatedTo int
     );

     create table #UserTaskListCategory
     (
         UserID int,
         TaskListCategoryID tinyint
     );

     create table #TaskUserCache
     (
         TaskID int,
         UserID int,
         TaskListCategoryID tinyint
     );

     create table #TaskResponsibleUser
     (TaskID int, UserID int)
      end;

select
    toa.TaskID,
    toa.AssignedTo into #TaskAssigned
from
    WORK.TaskOnlineAssigned toa
where
    toa.TenantID = @pTenantID
    and toa.AssignedTo is not null
    and exists (
        select
            1
        from
            #Task t where t.ID = toa.TaskID
    );

insert into
    #TaskUserCache (TaskID, UserID, TaskListCategoryID)
select
    ta.TaskID,
    UserID = ta.AssignedTo,
    TaskListCategoryID = @AssignedTo
from
    #TaskAssigned ta
where
    (
        exists (
            select
                1
            from
                #User u
            where
                u.ID = ta.AssignedTo
        )
    );

if exists (
    select
        1
    from
        #UserTaskListCategory where TaskListCategoryID in (@DistrictAvailable)
) begin
insert into
    #TaskResponsibleUser(TaskID, UserID)
select
    distinct tu.TaskID,
    tu.UserID
from
    (
        select
            TaskID = t.ID,
            UserID = t.ApprovalWith
        from
            #Task t
        where
            t.ApprovalWith is not null
        union
        select
            TaskID = t.ID,
            UserID = t.EscalatedTo
        from
            #Task t
        where
            t.EscalatedTo is not null
        union
        select
            TaskID = ta.TaskID,
            UserID = ta.AssignedTo
        from
            #TaskAssigned ta) tu;
    end;

end