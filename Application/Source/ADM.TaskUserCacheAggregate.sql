CREATE PROCEDURE [ADM].[TaskUserCacheAggregate] @pTenantID smallint as
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
    transaction isolation level read uncommitted 
    begin declare @DistrictAvailable tinyint = 13;

if(1 = 2) begin 
    create table #TaskResponsibleUser 
    (
        TaskID int, 
        UserID int
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

end;

if exists (
    select
        1
    from
        #UserTaskListCategory where TaskListCategoryID = @DistrictAvailable
    )
    
begin
    insert into
        #TaskUserCache(TaskID, UserID, TaskListCategoryID)
    select
        tu.TaskID,
        tlc.UserID,
        tlc.TaskListCategoryID
    from
        #UserTaskListCategory tlc
        cross join #TaskResponsibleUser tu
    where
        tlc.TaskListCategoryID = @DistrictAvailable
        and exists (
            select
                1
            from
                ADM.UserDistrict ud
            where
                ud.TenantID = @pTenantID
                and ud.UserID = tlc.UserID 
                and ud.Deleted is null
                and exists (
                    select
                        1
                    from
                        ADM.UserDistrict ud1
                    where
                        ud1.TenantID = ud.TenantID
                        and ud1.DistrictID = ud.DistrictID
                        and ud1.UserID = tu.UserID
                        and ud1.Deleted is null
                )
        );
end;

end
