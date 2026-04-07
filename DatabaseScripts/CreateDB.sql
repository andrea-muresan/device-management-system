IF NOT EXISTS (SELECT * FROM sys.databases where name = 'DeviceManagement')
BEGIN
    CREATE DATABASE DeviceManagement;
END
GO

USE DeviceManagement;
go


