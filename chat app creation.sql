CREATE DATABASE [ChatApp];
GO
USE [ChatApp]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupId] [int] IdENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatorId] [int] NOT NULL,
 CONSTRAINT [PK__Group__149AF30A6CBFBD41] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMember](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK__GroupMem__C5E27FC0D753D3BC] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NULL,
	[SenderId] [int] NOT NULL,
	[MessageContent] [varchar](max) NOT NULL,
	[DateAndTimeSent] [datetime] NOT NULL,
 CONSTRAINT [PK__Message__C87C037C3C70A85A] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[DateOfRegistration] [datetime] NOT NULL,
 CONSTRAINT [PK__User__1788CCACB00FB6B2] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Group] ON 
GO
INSERT [dbo].[Group] ([GroupId], [Name], [CreatorId]) VALUES (1, N'Group 1', 1)
GO
INSERT [dbo].[Group] ([GroupId], [Name], [CreatorId]) VALUES (2, N'Group 2', 2)
GO
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupMember] ON 
GO
INSERT [dbo].[GroupMember] ([GroupId], [UserId]) VALUES (1, 1)
GO
INSERT [dbo].[GroupMember] ([GroupId], [UserId]) VALUES (1, 2)
GO
INSERT [dbo].[GroupMember] ([GroupId], [UserId]) VALUES (2, 2)
GO
INSERT [dbo].[GroupMember] ([GroupId], [UserId]) VALUES (2, 3)
GO
SET IDENTITY_INSERT [dbo].[GroupMember] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [Username], [Password], [Email], [DateOfRegistration]) VALUES (1, N'user1', N'password1', N'user1@example.com', CAST(N'2023-03-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [Username], [Password], [Email], [DateOfRegistration]) VALUES (2, N'user2', N'password2', N'user2@example.com', CAST(N'2023-03-24T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [Username], [Password], [Email], [DateOfRegistration]) VALUES (3, N'user3', N'password3', N'user3@example.com', CAST(N'2023-03-24T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK__Group__CreatorId__36B12243] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK__Group__CreatorId__36B12243]
GO
ALTER TABLE [dbo].[GroupMember]  WITH CHECK ADD  CONSTRAINT [FK__GroupMemb__Group__398D8EEE] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([GroupId])
GO
ALTER TABLE [dbo].[GroupMember] CHECK CONSTRAINT [FK__GroupMemb__Group__398D8EEE]
GO
ALTER TABLE [dbo].[GroupMember]  WITH CHECK ADD  CONSTRAINT [FK__GroupMemb__UserI__3A81B327] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[GroupMember] CHECK CONSTRAINT [FK__GroupMemb__UserI__3A81B327]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK__Message__GroupId__3D5E1FD2] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([GroupId])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK__Message__GroupId__3D5E1FD2]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK__Message__SenderI__3E52440B] FOREIGN KEY([SenderId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK__Message__SenderI__3E52440B]
GO
ALTER TABLE [dbo].[User] ADD
	IsDisabled BIT NOT NULL DEFAULT 0,
	LastLoginDate DateTime
GO

ALTER TABLE [dbo].[Message] ADD
	IsDeleted BIT NOT NULL DEFAULT 0
GO
