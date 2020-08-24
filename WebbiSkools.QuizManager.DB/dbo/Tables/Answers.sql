CREATE TABLE [dbo].[Answers] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Deleted]      BIT            NOT NULL,
    [CreatedOn]    DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [LastModified] DATETIME2 (7)  NULL,
    [Text]         NVARCHAR (MAX) NULL,
    [IsCorrect]    BIT            NOT NULL,
    [QuestionId]   INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Answers_QuestionId]
    ON [dbo].[Answers]([QuestionId] ASC);

