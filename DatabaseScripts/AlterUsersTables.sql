USE deviceManagement;
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.users') AND name = 'email')
BEGIN
    ALTER TABLE dbo.users 
    ADD 
        email NVARCHAR(256) NULL,
        password NVARCHAR(MAX) NULL
END
GO

UPDATE dbo.users SET email = 'v.sorrengail@gmail.com', password = 'TEMP_HASH' WHERE name = 'Violet Sorrengail';
UPDATE dbo.users SET email = 'd.havilliard@gmail.com', password = 'TEMP_HASH' WHERE name = 'Dorian Havilliard';
UPDATE dbo.users SET email = 's.dragna@gmail.com', password = 'TEMP_HASH' WHERE name = 'Scarlett Dragna';
UPDATE dbo.users SET email = 'n.archeron@gmail.com', password = 'TEMP_HASH' WHERE name = 'Nesta Archeron';
UPDATE dbo.users SET email = 'p.jackson@gmail.com', password = 'TEMP_HASH' WHERE name = 'Percy Jackson';

ALTER TABLE dbo.users ALTER COLUMN email NVARCHAR(256) NOT NULL;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'UQ_Users_Email' AND type = 'UQ')
BEGIN
    ALTER TABLE dbo.users 
    ADD CONSTRAINT UQ_Users_Email UNIQUE (email);
END
GO

select * from users;

