-- Library Management Database Tables
-- Run this script to create the tables in the existing LibraryManagement database

USE LibraryManagement;
GO

-- Authors Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Authors')
BEGIN
    CREATE TABLE Authors (
        Id INT PRIMARY KEY IDENTITY(1,1),
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        DateOfBirth DATE NULL,
        Biography NVARCHAR(MAX) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL
    );
    PRINT 'Authors table created successfully!';
END
ELSE
BEGIN
    PRINT 'Authors table already exists.';
END
GO

-- Books Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Books')
BEGIN
    CREATE TABLE Books (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Title NVARCHAR(200) NOT NULL,
        ISBN NVARCHAR(50) NOT NULL,
        AuthorId INT NULL,
        PublishedDate DATE NULL,
        TotalCopies INT NOT NULL DEFAULT 1,
        AvailableCopies INT NOT NULL DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL,
        FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE SET NULL
    );
    
    CREATE INDEX IX_Books_AuthorId ON Books(AuthorId);
    CREATE INDEX IX_Books_ISBN ON Books(ISBN);
    PRINT 'Books table created successfully!';
END
ELSE
BEGIN
    PRINT 'Books table already exists.';
END
GO

-- Members Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Members')
BEGIN
    CREATE TABLE Members (
        Id INT PRIMARY KEY IDENTITY(1,1),
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        PhoneNumber NVARCHAR(20) NULL,
        Address NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL
    );
    
    CREATE UNIQUE INDEX IX_Members_Email ON Members(Email);
    PRINT 'Members table created successfully!';
END
ELSE
BEGIN
    PRINT 'Members table already exists.';
END
GO

-- Borrowings Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Borrowings')
BEGIN
    CREATE TABLE Borrowings (
        Id INT PRIMARY KEY IDENTITY(1,1),
        BookId INT NOT NULL,
        MemberId INT NOT NULL,
        BorrowedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ReturnedDate DATETIME2 NULL,
        DueDate DATETIME2 NOT NULL,
        Status NVARCHAR(50) NULL DEFAULT 'Borrowed',
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL,
        FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
        FOREIGN KEY (MemberId) REFERENCES Members(Id) ON DELETE CASCADE
    );
    
    CREATE INDEX IX_Borrowings_BookId ON Borrowings(BookId);
    CREATE INDEX IX_Borrowings_MemberId ON Borrowings(MemberId);
    CREATE INDEX IX_Borrowings_Status ON Borrowings(Status);
    PRINT 'Borrowings table created successfully!';
END
ELSE
BEGIN
    PRINT 'Borrowings table already exists.';
END
GO

PRINT 'Database schema setup completed!';
GO


