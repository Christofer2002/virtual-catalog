-- Insert initial Roles
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Role WHERE Name = 'Admin') THEN
        INSERT INTO Role (Name) VALUES ('Admin');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Role WHERE Name = 'User') THEN
        INSERT INTO Role (Name) VALUES ('User');
    END IF;
END $$;

-- Insert initial Privileges
DO $$
BEGIN
    -- Privileges for Products
    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'View Products') THEN
        INSERT INTO Privilege (Name) VALUES ('View Products');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Create Products') THEN
        INSERT INTO Privilege (Name) VALUES ('Create Products');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Update Products') THEN
        INSERT INTO Privilege (Name) VALUES ('Update Products');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Delete Products') THEN
        INSERT INTO Privilege (Name) VALUES ('Delete Products');
    END IF;

    -- Privileges for Categories
    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'View Categories') THEN
        INSERT INTO Privilege (Name) VALUES ('View Categories');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Create Categories') THEN
        INSERT INTO Privilege (Name) VALUES ('Create Categories');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Update Categories') THEN
        INSERT INTO Privilege (Name) VALUES ('Update Categories');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Delete Categories') THEN
        INSERT INTO Privilege (Name) VALUES ('Delete Categories');
    END IF;

    -- Privileges for Users
    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'View Users') THEN
        INSERT INTO Privilege (Name) VALUES ('View Users');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Create Users') THEN
        INSERT INTO Privilege (Name) VALUES ('Create Users');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Update Users') THEN
        INSERT INTO Privilege (Name) VALUES ('Update Users');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Privilege WHERE Name = 'Delete Users') THEN
        INSERT INTO Privilege (Name) VALUES ('Delete Users');
    END IF;
END $$;

-- Insert initial Users
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM "User" WHERE Email = 'admin@example.com') THEN
        INSERT INTO "User" (Name, LastName, Email, Identification, Password)
        VALUES ('Admin', 'User', 'admin@example.com', '123456789', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "User" WHERE Email = 'user@example.com') THEN
        INSERT INTO "User" (Name, LastName, Email, Identification, Password)
        VALUES ('Christofer', 'Chaves', 'user@example.com', '987654321', 'e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446');
    END IF;
END $$;

-- Insert initial Categories
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Category WHERE Name = 'Electronics') THEN
        INSERT INTO Category (Name) VALUES ('Electronics');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Category WHERE Name = 'Books') THEN
        INSERT INTO Category (Name) VALUES ('Books');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Category WHERE Name = 'Furniture') THEN
        INSERT INTO Category (Name) VALUES ('Furniture');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Category WHERE Name = 'Clothing') THEN
        INSERT INTO Category (Name) VALUES ('Clothing');
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Category WHERE Name = 'Toys') THEN
        INSERT INTO Category (Name) VALUES ('Toys');
    END IF;
END $$;

-- Insert initial Products
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Product WHERE Name = 'Laptop') THEN
        INSERT INTO Product (Name, Description, Price, Stock, CategoryId)
        VALUES ('Laptop', 'High performance laptop', 999.99, 10, (SELECT Id FROM Category WHERE Name = 'Electronics'));
    END IF;

    IF NOT EXISTS (SELECT 1 FROM Product WHERE Name = 'Book') THEN
        INSERT INTO Product (Name, Description, Price, Stock, CategoryId)
        VALUES ('Book', 'Interesting novel', 19.99, 50, (SELECT Id FROM Category WHERE Name = 'Books'));
    END IF;
END $$;