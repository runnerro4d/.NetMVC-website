CREATE TABLE [dbo].[Customers] (
    [id]                 NVARCHAR (128) NOT NULL,
    [FName]              NVARCHAR (MAX) NOT NULL,
    [LName]              NVARCHAR (MAX) NULL,
    [DateOfRegistration] DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED ([id] ASC)
);


CREATE TABLE [dbo].[Staffs] (
    [id]                NVARCHAR (128) NOT NULL,
    [FName]             NVARCHAR (MAX) NOT NULL,
    [LName]             NVARCHAR (MAX) NULL,
    [DateOfJoining]     DATETIME       NOT NULL,
    [DateOfTermination] DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Staffs] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Hotels] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Street]      NVARCHAR (MAX) NOT NULL,
    [Suburb]      NVARCHAR (MAX) NOT NULL,
    [State]       NVARCHAR (MAX) NULL,
    [ZipCode]     INT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Hotels] PRIMARY KEY CLUSTERED ([id] ASC)
);


CREATE TABLE [dbo].[Rooms] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [Floor]         INT            NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [PricePerNight] FLOAT (53)     NOT NULL,
    [RoomCapacity]  INT            NOT NULL,
    [hotel_id]      INT            NOT NULL,
    CONSTRAINT [PK_dbo.Rooms] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_dbo.Rooms_dbo.Hotels_hotel_id] FOREIGN KEY ([hotel_id]) REFERENCES [dbo].[Hotels] ([id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_hotel_id]
    ON [dbo].[Rooms]([hotel_id] ASC);



CREATE TABLE [dbo].[Bookings] (
    [id]             INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]      DATETIME       NOT NULL,
    [EndDate]        DATETIME       NOT NULL,
    [NumberOfPeople] INT            NOT NULL,
    [TotalCost]      FLOAT (53)     NOT NULL,
    [cust_id]        NVARCHAR (128) NOT NULL,
    [room_id]        INT            NOT NULL,
    CONSTRAINT [PK_dbo.Bookings] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_dbo.Bookings_dbo.Customers_cust_id] FOREIGN KEY ([cust_id]) REFERENCES [dbo].[Customers] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Bookings_dbo.Rooms_room_id] FOREIGN KEY ([room_id]) REFERENCES [dbo].[Rooms] ([id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_cust_id]
    ON [dbo].[Bookings]([cust_id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_room_id]
    ON [dbo].[Bookings]([room_id] ASC);

