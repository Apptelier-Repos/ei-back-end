CREATE TABLE [dbo].[TestSessionData]
(
	[Id] INT NOT NULL, 
    [CreationDate] DATETIME NOT NULL DEFAULT getdate(), 
CONSTRAINT [PK_TestSession] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [CK_TestSessionData_Id] CHECK (([Id]=(2) OR [Id]=(1)))
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1: Integration tests data. 2: Functional tests data.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'TestSessionData',
    @level2type = N'COLUMN',
    @level2name = N'Id'