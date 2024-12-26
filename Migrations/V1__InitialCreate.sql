-- Create Role if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Role' AND xtype='U')
BEGIN
    CREATE TABLE Role (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL
    );
END

-- Create Privilege if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Privilege' AND xtype='U')
BEGIN
    CREATE TABLE Privilege (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL
    );
END

-- Create User if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User' AND xtype='U')
BEGIN
    CREATE TABLE [User] (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        Identification NVARCHAR(20) NOT NULL,
        Password NVARCHAR(255) NOT NULL
    );
END

-- Create UserRole if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRole' AND xtype='U')
BEGIN
    CREATE TABLE UserRole (
        UserId BIGINT NOT NULL,
        RoleId BIGINT NOT NULL,
        PRIMARY KEY (UserId, RoleId),
        FOREIGN KEY (UserId) REFERENCES [User](Id),
        FOREIGN KEY (RoleId) REFERENCES Role(Id)
    );
END

-- Create RolePrivilege if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RolePrivilege' AND xtype='U')
BEGIN
    CREATE TABLE RolePrivilege (
        RoleId BIGINT NOT NULL,
        PrivilegeId BIGINT NOT NULL,
        PRIMARY KEY (RoleId, PrivilegeId),
        FOREIGN KEY (RoleId) REFERENCES Role(Id),
        FOREIGN KEY (PrivilegeId) REFERENCES Privilege(Id)
    );
END

-- Create Category if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Category' AND xtype='U')
BEGIN
    CREATE TABLE Category (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL
    );
END

-- Create Product if it does not exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Product' AND xtype='U')
BEGIN
    CREATE TABLE Product (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(MAX),
        Price DECIMAL(18,2) NOT NULL,
        Stock INT NOT NULL,
        CategoryId BIGINT NOT NULL,
        FOREIGN KEY (CategoryId) REFERENCES Category(Id)
    );
END
