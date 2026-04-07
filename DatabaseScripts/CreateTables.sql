use deviceManagement;
go

-- users table
IF NOT EXISTS (SELECT * FROM sys.objects where object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
BEGIN
    CREATE TABLE dbo.users (
        id INT PRIMARY KEY IDENTITY,
        name NVARCHAR(100) NOT NULL,
        role NVARCHAR(50) NOT NULL,
        location NVARCHAR(100) NOT NULL
    );
END
GO


-- devices table
IF NOT EXISTS (SELECT * FROM sys.objects where object_id = OBJECT_ID(N'[dbo].[devices]') AND type in (N'U'))
BEGIN
    CREATE TABLE dbo.devices (
        id INT PRIMARY KEY IDENTITY,
        name NVARCHAR(100) NOT NULL,
        manufacturer NVARCHAR(100) NOT NULL,
        type NVARCHAR(50) NOT NULL,
        os NVARCHAR(50) NOT NULL,
        osVersion NVARCHAR(50) NOT NULL,
        processor NVARCHAR(100) NOT NULL,
        ram int NULL,
        description NVARCHAR(500) NULL,
        userId INT NULL,
        CONSTRAINT FK_Devices_Users FOREIGN KEY (userId) 
            REFERENCES dbo.users(id)
            ON DELETE SET NULL
    );
end
go




