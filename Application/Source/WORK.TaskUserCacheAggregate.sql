CREATE PROCEDURE [WORK].[Taskusercacheaggregate] @pTenantID SMALLINT
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
      DECLARE @IsOwnerOnly       BIT = 0,
              @AssignedTo        TINYINT = 2,
              @DistrictAvailable TINYINT = 13,
              @UserWorkType      TINYINT = 16,
              @AllTaskAvailable  TINYINT = 19,
              @CreatedBy         TINYINT = 20,
              @Now               DATETIME2(0) = Getutcdate()

      --IF( 1 = 2 )
      --  BEGIN
      --      CREATE TABLE #task
      --        (
                 --id           INT,
                 --assetid      INT,
                 --approvalwith INT,
                 --escalatedto  INT,
                 --createdby    INT,
                 --requestedby  INT,
                 --worktypeid   SMALLINT,
                 --archived     DATETIME2(0),
                 --deleted      DATETIME2(0)
      --        );

      --      CREATE TABLE #user
      --        (
      --           id INT
      --        );

      --      CREATE TABLE #taskusercache
      --        (
      --           taskid             INT,
      --           userid             INT,
      --           tasklistcategoryid TINYINT
      --        );
      --  END

      --CREATE TABLE #listcategory
      --  (
      --     id              TINYINT,
      --     permissionextid SMALLINT
      --  );

      --CREATE TABLE #employment
      --  (
      --     userid             INT,
      --     companyid          SMALLINT,
      --     tasklistcategoryid TINYINT
      --  );

      --CREATE TABLE #taskresponsibleuser
      --  (
      --     taskid INT,
      --     userid INT
      --  );

      --CREATE TABLE #usertasklistcategory
      --  (
      --     userid             INT,
      --     tasklistcategoryid TINYINT,
      --     PRIMARY KEY CLUSTERED (tasklistcategoryid, userid) WITH (ignore_dup_key =  on)
      --  );

      --INSERT INTO #listcategory (id, permissionextid)
      --SELECT tlc.id,
      --       tlc.permissionextid
      --FROM   work.tasklistcategory tlc
      --WHERE  tlc.id IN ( @DistrictAvailable, @UserWorkType, @AllTaskAvailable );


      --INSERT INTO #usertasklistcategory (userid, tasklistcategoryid)
      --EXEC adm.Userlistcategoryget @pTenantID;

      --IF EXISTS (SELECT 1
      --           FROM   #usertasklistcategory
      --           WHERE  tasklistcategoryid = @AllTaskAvailable)
        --BEGIN
        --    INSERT INTO #taskusercache
        --                (taskid,
        --                 userid,
        --                 tasklistcategoryid)
        --    SELECT t.id,
        --           tlc.userid,
        --           tlc.tasklistcategoryid
        --    FROM   #usertasklistcategory tlc
        --           CROSS JOIN #task t
        --    WHERE  tlc.tasklistcategoryid = @AllTaskAvailable

        --    DELETE u
        --    FROM   #user u
        --    WHERE  EXISTS (SELECT 1
        --                   FROM   #taskusercache tuc
        --                   WHERE  tuc.userid = u.id
        --                          AND tuc.tasklistcategoryid = @AllTaskAvailable
        --                  );
        --END

      --IF NOT EXISTS (SELECT 1
      --               FROM   #user)
      --  RETURN;

      --INSERT INTO #taskusercache
      --            (taskid,
      --             userid,
      --             tasklistcategoryid)
      --SELECT t.id,
      --       tm.userid,
      --       @CreatedBy
      --FROM   #task t
      --       JOIN adm.tenantmember tm
      --         ON tm.id = t.createdby
      --WHERE  tm.tenantid = @pTenantID
      --       AND EXISTS (SELECT 1
      --                   FROM   #user u
      --                   WHERE  u.id = tm.userid);

      /* Пользователи, привязанные к виду работ, указанному в завяке */
      IF EXISTS (SELECT 1
                 FROM   #usertasklistcategory
                 WHERE  tasklistcategoryid = @UserWorkType)

        INSERT INTO #taskusercache
                    (taskid,
                     userid,
                     tasklistcategoryid)
        SELECT t.id,
               tlc.userid,
               tlc.tasklistcategoryid
        --FROM   #usertasklistcategory tlc
        --       CROSS JOIN #task t
        WHERE  tlc.tasklistcategoryid = @UserWorkType
               AND EXISTS (SELECT 1
                           FROM   pa.userworktype uwt
                                  JOIN work.worktype wt
                                    ON wt.tenantid = uwt.tenantid
                                       AND wt.id = uwt.worktypeid
                           WHERE  uwt.tenantid = @pTenantID
                                  AND uwt.userid = tlc.userid
                                  AND uwt.worktypeid = t.worktypeid
                                  AND uwt.deleted IS NULL
                                  AND wt.deleted IS NULL);

      EXEC work.Taskusercacheaggregateresponsibility
        @pTenantID = @pTenantID;

      EXEC adm.Taskusercacheaggregate
        @pTenantID = @pTenantID;
  END 