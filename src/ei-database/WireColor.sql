CREATE TABLE [dbo].[WireColor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL DEFAULT getdate(),
	[Code] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[TranslatedName] [varchar](50) NOT NULL,
	[BaseColor] [varchar](7) NOT NULL,
	[StripeColor] [varchar](7) NULL, 
 CONSTRAINT [PK_WireColor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WireColor_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]