CREATE TABLE [dbo].[Quizes] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Deleted]      BIT            NOT NULL,
    [CreatedOn]    DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [LastModified] DATETIME2 (7)  NULL,
    [Name]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Quizes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

