To update the database, execute the following commands in "Package Manager Console":
  - Add-Migration MIGRATION_NAME -Project Infrastructure.Data.Context
  - Update-Database -Project Infrastructure.Data.Context

Note: You must target "Application" as the Default Project in PMC.