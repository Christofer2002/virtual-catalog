-- Insert initial Roles
IF NOT EXISTS (SELECT * FROM Role WHERE Name = 'Admin')
BEGIN
    INSERT INTO Role (Name) VALUES ('Admin');
END

IF NOT EXISTS (SELECT * FROM Role WHERE Name = 'User')
BEGIN
    INSERT INTO Role (Name) VALUES ('User');
END

-- Insert initial Privileges
-- Privileges for Products
IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'View Products')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('View Products');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Create Products')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Create Products');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Update Products')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Update Products');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Delete Products')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Delete Products');
END

-- Privileges for Categories
IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'View Categories')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('View Categories');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Create Categories')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Create Categories');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Update Categories')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Update Categories');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Delete Categories')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Delete Categories');
END

-- Privileges for Users
IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'View Users')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('View Users');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Create Users')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Create Users');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Update Users')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Update Users');
END

IF NOT EXISTS (SELECT * FROM Privilege WHERE Name = 'Delete Users')
BEGIN
    INSERT INTO Privilege (Name) VALUES ('Delete Users');
END

-- Insert initial Users
IF NOT EXISTS (SELECT * FROM [User] WHERE Email = 'admin@example.com')
BEGIN
    INSERT INTO [User] (Name, LastName, Email, Identification, Password)
    VALUES ('Admin', 'User', 'admin@example.com', '123456789', 'admin123');
END

IF NOT EXISTS (SELECT * FROM [User] WHERE Email = 'user@example.com')
BEGIN
    INSERT INTO [User] (Name, LastName, Email, Identification, Password)
    VALUES ('Christofer', 'Chaves', 'user@example.com', '987654321', 'user123');
END

-- Assign Roles to Users
IF NOT EXISTS (SELECT * FROM UserRole WHERE UserId = 1 AND RoleId = 1)
BEGIN
    INSERT INTO UserRole (UserId, RoleId) VALUES (1, 1); -- Admin User
END

IF NOT EXISTS (SELECT * FROM UserRole WHERE UserId = 2 AND RoleId = 2)
BEGIN
    INSERT INTO UserRole (UserId, RoleId) VALUES (2, 2); -- Regular User
END

-- Assign Privileges to Roles
-- Admin Privileges
DECLARE @AdminRoleId INT = (SELECT Id FROM Role WHERE Name = 'Admin');
DECLARE @UserRoleId INT = (SELECT Id FROM Role WHERE Name = 'User');

-- Admin Privileges for Products, Categories, and Users
IF NOT EXISTS (SELECT * FROM RolePrivilege WHERE RoleId = @AdminRoleId)
BEGIN
    INSERT INTO RolePrivilege (RoleId, PrivilegeId)
    SELECT @AdminRoleId, Id FROM Privilege;
END

-- Regular User Privileges (View only for Products and Categories)
IF NOT EXISTS (SELECT * FROM RolePrivilege WHERE RoleId = @UserRoleId AND PrivilegeId IN 
    (SELECT Id FROM Privilege WHERE Name IN ('View Products', 'View Categories')))
BEGIN
    INSERT INTO RolePrivilege (RoleId, PrivilegeId)
    SELECT @UserRoleId, Id FROM Privilege WHERE Name IN ('View Products', 'View Categories');
END

-- Insert initial Categories
IF NOT EXISTS (SELECT * FROM Category WHERE Name = 'Electronics')
BEGIN
    INSERT INTO Category (Name) VALUES ('Electronics');
END

IF NOT EXISTS (SELECT * FROM Category WHERE Name = 'Books')
BEGIN
    INSERT INTO Category (Name) VALUES ('Books');
END

-- Insert initial Products
IF NOT EXISTS (SELECT * FROM Product WHERE Name = 'Laptop')
BEGIN
    INSERT INTO Product (Name, Description, Price, Stock, CategoryId)
    VALUES ('Laptop', 'High performance laptop', 999.99, 10, 1); -- Category: Electronics
END

IF NOT EXISTS (SELECT * FROM Product WHERE Name = 'Book')
BEGIN
    INSERT INTO Product (Name, Description, Price, Stock, CategoryId)
    VALUES ('Book', 'Interesting novel', 19.99, 50, 2); -- Category: Books
END
