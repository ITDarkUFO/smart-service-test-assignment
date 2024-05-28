CREATE PROCEDURE [ADM].[Userlistcategoryget] @pTenantID SMALLINT
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
        IF( 1 != 1 )
            BEGIN
                CREATE TABLE #user
                (
                    id INT NOT NULL
                        PRIMARY KEY WITH (ignore_dup_key = on)
                );

                CREATE TABLE #listcategory
                (
                    id TINYINT NOT NULL
                        PRIMARY KEY (id) WITH (ignore_dup_key = on),
                    permissionextid SMALLINT
                );
            END;

      SELECT UserID = u.id,
             ListCategoryID = lc.id
      FROM   #user u
             CROSS JOIN #listcategory lc
      WHERE  EXISTS (SELECT 1
                     FROM   adm.userrole ur
                     WHERE  ur.tenantid = @pTenantID
                            AND ur.userid = u.id
                            AND ur.deleted IS NULL
                            AND EXISTS (SELECT 1
                                        FROM   adm.rolepermissionext rpe
                                        WHERE  rpe.tenantid = ur.tenantid
                                               AND rpe.roleid = ur.roleid
                                               AND rpe.permissionextid =
                                                   lc.permissionextid
                                               AND rpe.deleted IS NULL))
      UNION ALL
      SELECT UserID = u.id,
             ListCategoryID = lc.id
      FROM   #user u
             CROSS JOIN #listcategory lc
      WHERE  lc.permissionextid IS NULL;
  END 