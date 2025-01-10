-- Create Role if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Role' AND relkind = 'r') THEN
        CREATE TABLE "Role" (
            "Id" SERIAL PRIMARY KEY,
            "Name" VARCHAR(100) NOT NULL
        );
    END IF;
END $$;

-- Create Privilege if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Privilege' AND relkind = 'r') THEN
        CREATE TABLE "Privilege" (
            "Id" SERIAL PRIMARY KEY,
            "Name" VARCHAR(100) NOT NULL
        );
    END IF;
END $$;

-- Create User if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'User' AND relkind = 'r') THEN
        CREATE TABLE "User" (
            "Id" SERIAL PRIMARY KEY,
            "Name" VARCHAR(100) NOT NULL,
            "LastName" VARCHAR(100) NOT NULL,
            "Email" VARCHAR(255) NOT NULL UNIQUE,
            "Identification" VARCHAR(20) NOT NULL,
            "Password" VARCHAR(255) NOT NULL
        );
    END IF;
END $$;

-- Create UserRole if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'UserRole' AND relkind = 'r') THEN
        CREATE TABLE "UserRole" (
            "UserId" BIGINT NOT NULL,
            "RoleId" BIGINT NOT NULL,
            PRIMARY KEY ("UserId", "RoleId"),
            FOREIGN KEY ("UserId") REFERENCES "User"("Id") ON DELETE CASCADE,
            FOREIGN KEY ("RoleId") REFERENCES "Role"("Id") ON DELETE CASCADE
        );
    END IF;
END $$;

-- Create RolePrivilege if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'RolePrivilege' AND relkind = 'r') THEN
        CREATE TABLE "RolePrivilege" (
            "RoleId" BIGINT NOT NULL,
            "PrivilegeId" BIGINT NOT NULL,
            PRIMARY KEY ("RoleId", "PrivilegeId"),
            FOREIGN KEY ("RoleId") REFERENCES "Role"("Id") ON DELETE CASCADE,
            FOREIGN KEY ("PrivilegeId") REFERENCES "Privilege"("Id") ON DELETE CASCADE
        );
    END IF;
END $$;

-- Create Category if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Category' AND relkind = 'r') THEN
        CREATE TABLE "Category" (
            "Id" SERIAL PRIMARY KEY,
            "Name" VARCHAR(100) NOT NULL
        );
    END IF;
END $$;

-- Create Product if it does not exist
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'Product' AND relkind = 'r') THEN
        CREATE TABLE "Product" (
            "Id" SERIAL PRIMARY KEY,
            "Name" VARCHAR(100) NOT NULL,
            "Description" TEXT,
            "Price" NUMERIC(18,2) NOT NULL,
            "Stock" INT NOT NULL,
            "CategoryId" BIGINT NOT NULL,
            FOREIGN KEY ("CategoryId") REFERENCES "Category"("Id") ON DELETE CASCADE
        );
    END IF;
END $$;
