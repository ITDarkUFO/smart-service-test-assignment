CREATE PROCEDURE [WORK].[Taskusercacheaggregateresponsibility] @pTenantID
SMALLINT
AS
    SET nocount ON
    SET xact_abort ON
    SET ansi_nulls ON
    SET ansi_warnings ON
    SET ansi_padding ON
    SET ansi_null_dflt_on ON
    SET arithabort ON
    SET quoted_identifier ON
    SET concat_null_yields_null ON
    SET implicit_transactions OFF
    SET cursor_close_on_commit OFF
    SET TRANSACTION isolation level READ uncommitted

  BEGIN
      DECLARE @AssignedTo        TINYINT = 2,
              @DistrictAvailable TINYINT = 13

      IF( 1 = 2 )
        BEGIN
            CREATE TABLE #user
              (
                 id INT
              );

            CREATE TABLE #task
              (
                 id           INT,
                 approvalwith INT,
                 escalatedto  INT
              );

            CREATE TABLE #usertasklistcategory
              (
                 userid             INT,
                 tasklistcategoryid TINYINT
              );

            CREATE TABLE #taskusercache
              (
                 taskid             INT,
                 userid             INT,
                 tasklistcategoryid TINYINT
              );

            CREATE TABLE #taskresponsibleuser
              (
                 taskid INT,
                 userid INT
              )
        END;

      SELECT toa.taskid,
             toa.assignedto
      INTO   #taskassigned
      FROM   work.taskonlineassigned toa
      WHERE  toa.tenantid = @pTenantID
             AND toa.assignedto IS NOT NULL
             AND EXISTS (SELECT 1
                         FROM   #task t
                         WHERE  t.id = toa.taskid);

      INSERT INTO #taskusercache
                  (taskid,
                   userid,
                   tasklistcategoryid)
      SELECT ta.taskid,
             UserID = ta.assignedto,
             TaskListCategoryID = @AssignedTo
      FROM   #taskassigned ta
      WHERE  ( EXISTS (SELECT 1
                       FROM   #user u
                       WHERE  u.id = ta.assignedto) );

      IF EXISTS (SELECT 1
                 FROM   #usertasklistcategory
                 WHERE  tasklistcategoryid IN ( @DistrictAvailable ))
        BEGIN
            INSERT INTO #taskresponsibleuser
                        (taskid,
                         userid)
            SELECT DISTINCT tu.taskid,
                            tu.userid
            FROM   (SELECT TaskID = t.id,
                           UserID = t.approvalwith
                    FROM   #task t
                    WHERE  t.approvalwith IS NOT NULL
                    UNION
                    SELECT TaskID = t.id,
                           UserID = t.escalatedto
                    FROM   #task t
                    WHERE  t.escalatedto IS NOT NULL
                    UNION
                    SELECT TaskID = ta.taskid,
                           UserID = ta.assignedto
                    FROM   #taskassigned ta) tu;
        END;
  END 