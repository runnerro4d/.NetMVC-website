CREATE TABLE [dbo].[Staffs] (
    [id]                NVARCHAR (128) NOT NULL,
    [FName]             NVARCHAR (MAX) NOT NULL,
    [LName]             NVARCHAR (MAX) NULL,
    [DateOfJoining]     DATETIME       NOT NULL,
    [DateOfTermination] DATETIME       NOT NULL,
    [hotel_id] NCHAR(10) NOT NULL, 
    CONSTRAINT [PK_dbo.Staffs] PRIMARY KEY CLUSTERED ([id] ASC)
);

