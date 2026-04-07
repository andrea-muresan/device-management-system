use deviceManagement;
go

-- users table
IF NOT EXISTS (SELECT 1 FROM users WHERE name = 'Violet Sorrengail')
BEGIN
    INSERT INTO users (name, role, location) 
    VALUES
        ('Violet Sorrengail', 'Developer', 'London'),
        ('Dorian Havilliard', 'Manager', 'London'),
        ('Scarlett Dragna', 'Tester', 'Cluj-Napoca'),
        ('Nesta Archeron', 'Manager', 'Cluj-Napoca'),
        ('Percy Jackson', 'Developer', 'New York');
end 
go

-- devices table
IF NOT EXISTS (SELECT 1 FROM devices WHERE name = 'iPhone 14')
BEGIN
    INSERT INTO devices (name, manufacturer, type, os, osVersion, processor, ram, description, userId)
    VALUES
        ('iPhone 14', 'Apple', 'Phone', 'iOS', '16.4', 'A15 Bionic', 6, 'Company phone for development', 1),
        ('Samsung Galaxy S23', 'Samsung', 'Phone', 'Android', '14', 'Snapdragon 8 Gen 2', 8, 'Main testing phone', 3),
        ('iPad Pro 11', 'Apple', 'Tablet', 'iPadOS', '17.0', 'M2', 8, 'Tablet for managers', 2),
        ('Google Pixel 7', 'Google', 'Phone', 'Android', '13', 'Google Tensor G2', 8, 'Device used for QA testing', 5),
        ('Samsung Galaxy Tab S8', 'Samsung', 'Tablet', 'Android', '13', 'Snapdragon 8 Gen 1', 8, 'Tablet used for presentations', 4),
        ('iPhone SE', 'Apple', 'Phone', 'iOS', '15.7', 'A13 Bionic', 4, 'Old device kept for compatibility testing', NULL);
end
go

select * from devices;
select * from users;
