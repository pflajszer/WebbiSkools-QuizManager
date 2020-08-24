CREATE TABLE [dbo].[Questions] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Deleted]      BIT            NOT NULL,
    [CreatedOn]    DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [LastModified] DATETIME2 (7)  NULL,
    [Text]         NVARCHAR (MAX) NULL,
    [QuizId]       INT            NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Questions_Quizes_QuizId] FOREIGN KEY ([QuizId]) REFERENCES [dbo].[Quizes] ([Id]) ON DELETE SET NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Questions_QuizId]
    ON [dbo].[Questions]([QuizId] ASC);

