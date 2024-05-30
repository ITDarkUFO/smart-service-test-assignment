CREATE PROCEDURE [ADM].[Taskusercacheaggregate] @pTenantID SMALLINT
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
      DECLARE @DistrictAvailable TINYINT = 13;

      IF( 1 = 2 )
        BEGIN
            CREATE TABLE #taskresponsibleuser
              (
                 taskid INT,
                 userid INT
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
        END;

      IF EXISTS (SELECT 1
                 FROM   #usertasklistcategory
                 WHERE  tasklistcategoryid = @DistrictAvailable)
        BEGIN
            INSERT INTO #taskusercache
                        (taskid,
                         userid,
                         tasklistcategoryid)
            SELECT tu.taskid,
                   tlc.userid,
                   tlc.tasklistcategoryid
            FROM   #usertasklistcategory tlc
                   CROSS JOIN #taskresponsibleuser tu
            WHERE  tlc.tasklistcategoryid = @DistrictAvailable
                   AND EXISTS (SELECT 1
                               FROM   adm.userdistrict ud
                               WHERE  ud.tenantid = @pTenantID
                                      AND ud.userid = tlc.userid
                                      AND ud.deleted IS NULL
                                      AND EXISTS (SELECT 1
                                                  FROM   adm.userdistrict ud1
                                                  WHERE  ud1.tenantid =
                                                         ud.tenantid
                                                         AND ud1.districtid =
                                                             ud.districtid
                                                         AND ud1.userid =
                                                             tu.userid
                                                         AND ud1.deleted IS NULL
                                                 ))
            ;
        END;
  END 