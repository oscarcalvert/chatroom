CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(128) NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Offline'
);

CREATE TABLE Chatrooms (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    RoomName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    CreatedBy INT, 
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
);

CREATE TABLE Messages (
    MessageID BIGINT IDENTITY(1,1) PRIMARY KEY,
    RoomID INT NOT NULL,
    SenderID INT NOT NULL,
    MessageContent NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME2(7) NOT NULL,
    FOREIGN KEY (RoomID) REFERENCES Chatrooms(RoomID),
    FOREIGN KEY (SenderID) REFERENCES Users(UserID)
);

CREATE TABLE ChatroomMembers (
    MembershipID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    RoomID INT NOT NULL,
    IsModerator BIT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoomID) REFERENCES Chatrooms(RoomID),
    UNIQUE (UserID, RoomID)
);

CREATE TABLE PrivateMessages (
    MessageID BIGINT IDENTITY(1,1) PRIMARY KEY,
    SenderID INT NOT NULL,
    ReceiverID INT NOT NULL,
    MessageContent NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (SenderID) REFERENCES Users(UserID),
    FOREIGN KEY (ReceiverID) REFERENCES Users(UserID)
);