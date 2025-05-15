
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SearchResults]') AND type in (N'U'))
DROP TABLE [dbo].[SearchResults]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SearchEngine]') AND type in (N'U'))
DROP TABLE [dbo].[SearchEngine]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchEngine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EngineId] [int] NOT NULL,
	[EngineDescription] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResults](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EngineId] [int] NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[Rank] [int] NULL,
	[Url] [nvarchar](200) NULL,
	[SearchTerm] [nvarchar](150) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SearchEngine] ON 
GO
INSERT [dbo].[SearchEngine] ([Id], [EngineId], [EngineDescription]) VALUES (1, 1, N'https://www.google.co.uk')
GO
INSERT [dbo].[SearchEngine] ([Id], [EngineId], [EngineDescription]) VALUES (2, 2, N'https://www.google.com')
GO
INSERT [dbo].[SearchEngine] ([Id], [EngineId], [EngineDescription]) VALUES (3, 3, N'https://www.bing.com')
GO
SET IDENTITY_INSERT [dbo].[SearchEngine] OFF
GO
SET IDENTITY_INSERT [dbo].[SearchResults] ON 
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (67, 2, CAST(N'2025-05-11T00:00:00.000' AS DateTime), 0, N'', N'land registry search')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (64, 3, CAST(N'2025-05-14T00:00:00.000' AS DateTime), 0, N'', N'land registry search')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (876, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 1, N'https://www.infotrack.co.uk', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (877, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 2, N'https://www.infotrack.co.uk › about', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (878, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 3, N'https://www.infotrack.co.uk › about › contact-us', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (879, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 9, N'https://www.infotrack.co.uk › sign-up-now', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (880, 1, CAST(N'2025-05-07T00:00:00.000' AS DateTime), 11, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (881, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 12, N'https://www.infotrack.co.uk › solutions', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (882, 3, CAST(N'2025-05-07T00:00:00.000' AS DateTime), 13, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (883, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 15, N'https://www.infotrack.co.uk › about › meet-the-team', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (884, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 16, N'https://www.infotrack.co.uk › solutions › due-diligence › aml-identity-ch…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (885, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 18, N'https://www.infotrack.co.uk › solutions › conveyancing › local-authority …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (840, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 1, N'https://www.infotrack.co.uk › about', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (841, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 2, N'https://www.infotrack.co.uk › about › contact-us', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (842, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 3, N'https://www.infotrack.co.uk › solutions › conveyancing › property-se…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (843, 2, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 4, N'https://www.infotrack.co.uk › sign-up-now', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (844, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 5, N'https://www.infotrack.co.uk › solutions', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (845, 3, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 11, N'https://www.infotrack.co.uk › solutions › conveyancing', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (846, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 12, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (847, 3, CAST(N'2025-05-12T00:00:00.000' AS DateTime), 13, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (848, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 16, N'https://www.infotrack.co.uk › about › meet …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (849, 2, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 17, N'https://www.infotrack.co.uk › sign-up-now', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (850, 1, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 19, N'https://www.infotrack.co.uk › solutions › due-diligence › a…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (851, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 20, N'https://www.infotrack.co.uk › integration', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (852, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 21, N'https://www.infotrack.co.uk › download-a-brochure', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (853, 3, CAST(N'2025-05-12T00:00:00.000' AS DateTime), 22, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (854, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 25, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (855, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 26, N'https://www.infotrack.co.uk › about › caree…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (856, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 27, N'https://www.infotrack.co.uk › about › our-v…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (857, 3, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 28, N'https://www.infotrack.co.uk › book-a-demo', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (858, 2, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 29, N'https://www.infotrack.co.uk › privacy-policy', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (859, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 30, N'https://www.infotrack.co.uk › solutions › conveyancing', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (860, 3, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 31, N'https://www.infotrack.co.uk › terms-conditions', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (861, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 34, N'https://www.infotrack.co.uk › solutions › corporate-servic…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (862, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 43, N'https://www.infotrack.co.uk › solutions › due-diligence › c…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (863, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 44, N'https://www.infotrack.co.uk › solutions › conveyancing › l…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (864, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 53, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (865, 3, CAST(N'2025-05-12T00:00:00.000' AS DateTime), 54, N'https://www.infotrack.co.uk › solutions › du…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (866, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 55, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (867, 3, CAST(N'2025-05-12T00:00:00.000' AS DateTime), 56, N'https://www.infotrack.co.uk › solutions › commercial-serv…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (868, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 57, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (869, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 63, N'https://www.infotrack.co.uk › about › blog › infotrack-enhances-custom…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (870, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 68, N'https://www.infotrack.co.uk › about › kings-award', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (871, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 69, N'https://www.infotrack.co.uk › integration › leap', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (872, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 70, N'https://www.infotrack.co.uk › solutions › conveyancing › …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (873, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 71, N'https://www.infotrack.co.uk › solutions › commercial-serv…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (874, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 77, N'https://www.infotrack.co.uk › solutions › corporate-servic…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (875, 3, CAST(N'2025-05-10T00:00:00.000' AS DateTime), 78, N'https://www.infotrack.co.uk › solutions › due-diligence', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (1097, 1, CAST(N'2025-05-15T00:00:00.000' AS DateTime), 0, N'', N'land registry search')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (1098, 3, CAST(N'2025-05-15T00:00:00.000' AS DateTime), 0, N'', N'land registry search')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (931, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 94, N'https://www.infotrack.co.uk › privacy-policy › consumer-privacy-policy', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (932, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 95, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (933, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 96, N'https://www.infotrack.co.uk › solutions › conveyancing › pre-completion', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (934, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 100, N'https://www.infotrack.co.uk › about › case-studies', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (935, 2, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 102, N'https://www.infotrack.co.uk › about › blog …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (936, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 104, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (937, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 105, N'https://www.infotrack.co.uk › solutions › due-diligence › confirmly', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (938, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 107, N'https://www.infotrack.co.uk › about › blog › how-to-prevent-financial-fra…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (939, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 108, N'https://www.infotrack.co.uk › about › blog › infotrack-announces-enhan…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (940, 2, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 110, N'https://www.infotrack.co.uk › solutions › corporate-services › dissolved …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (943, 1, CAST(N'2025-05-07T00:00:00.000' AS DateTime), 1, N'https://www.infotrack.co.uk', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (942, 3, CAST(N'2025-05-14T00:00:00.000' AS DateTime), 0, N'', N'land registry search')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (944, 1, CAST(N'2025-05-07T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (945, 1, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (946, 1, CAST(N'2025-05-13T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (947, 1, CAST(N'2025-05-07T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (948, 1, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (949, 1, CAST(N'2025-05-14T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (950, 2, CAST(N'2025-05-06T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (886, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 19, N'https://www.infotrack.co.uk › solutions › conveyancing › verification-of-i…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (887, 1, CAST(N'2025-05-14T00:00:00.000' AS DateTime), 20, N'https://www.infotrack.co.uk › integration', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (888, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 21, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (889, 3, CAST(N'2025-05-13T00:00:00.000' AS DateTime), 22, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (890, 3, CAST(N'2025-05-11T00:00:00.000' AS DateTime), 23, N'https://www.infotrack.co.uk › solutions › conveyancing › indemnities', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (891, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 24, N'https://www.infotrack.co.uk › solutions › conveyancing', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (892, 3, CAST(N'2025-05-13T00:00:00.000' AS DateTime), 27, N'https://www.infotrack.co.uk › solutions › corporate-services › insolvenc…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (893, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 28, N'https://www.infotrack.co.uk › solutions › conveyancing › verification-of-f…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (894, 2, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 29, N'https://www.infotrack.co.uk › download-a-brochure', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (895, 3, CAST(N'2025-05-11T00:00:00.000' AS DateTime), 30, N'https://www.infotrack.co.uk › about › careers-at-infotrack', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (896, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 31, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (897, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 33, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (898, 1, CAST(N'2025-05-13T00:00:00.000' AS DateTime), 35, N'https://www.infotrack.co.uk › about › our-values', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (899, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 36, N'https://www.infotrack.co.uk › solutions › conveyancing › smart-forms', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (900, 3, CAST(N'2025-05-14T00:00:00.000' AS DateTime), 37, N'https://www.infotrack.co.uk › solutions › conveyancing › post-completion', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (901, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 38, N'https://www.infotrack.co.uk › about › blog › digital-conveyancing-summi…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (902, 3, CAST(N'2025-05-11T00:00:00.000' AS DateTime), 40, N'https://www.infotrack.co.uk › book-a-demo', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (903, 2, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 43, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (904, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 44, N'https://www.infotrack.co.uk › integration › lms', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (905, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 45, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (906, 3, CAST(N'2025-05-11T00:00:00.000' AS DateTime), 46, N'https://www.infotrack.co.uk › about › events', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (907, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 47, N'https://www.infotrack.co.uk › solutions › due-diligence › bankruptcy', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (908, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 49, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (909, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 51, N'https://www.infotrack.co.uk › solutions › du…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (910, 3, CAST(N'2025-05-13T00:00:00.000' AS DateTime), 52, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (911, 3, CAST(N'2025-05-14T00:00:00.000' AS DateTime), 53, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (912, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 55, N'https://www.infotrack.co.uk › integration › sos', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (913, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 57, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (914, 2, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 58, N'https://www.infotrack.co.uk › about › blog › navigating-the-future-top-te…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (915, 1, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 59, N'https://www.infotrack.co.uk › privacy-policy', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (916, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 63, N'https://www.infotrack.co.uk › solutions › co…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (917, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 65, N'https://www.infotrack.co.uk › terms-conditions', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (918, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 67, N'https://www.infotrack.co.uk › about › testimonials', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (919, 1, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 68, N'https://www.infotrack.co.uk › solutions › due-diligence', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (920, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 69, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (921, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 70, N'https://www.infotrack.co.uk › about › blog › how-does-open-banking-im…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (922, 3, CAST(N'2025-05-09T00:00:00.000' AS DateTime), 71, N'https://www.infotrack.co.uk › integration › l…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (923, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 72, N'https://www.infotrack.co.uk › solutions › du…', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (924, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 75, N'https://www.infotrack.co.uk', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (925, 3, CAST(N'2025-05-11T00:00:00.000' AS DateTime), 83, N'https://www.infotrack.co.uk › about › kings …', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (926, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 84, N'https://www.infotrack.co.uk › solutions › corporate-services', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (927, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 85, N'https://www.infotrack.co.uk › integration › alb', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (928, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 87, N'https://www.infotrack.co.uk › integration › leap', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (929, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 88, N'https://www.infotrack.co.uk › solutions › commercial-services', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (930, 3, CAST(N'2025-05-08T00:00:00.000' AS DateTime), 92, N'https://www.infotrack.co.uk › about › blog', N'infotrack')
GO
INSERT [dbo].[SearchResults] ([Id], [EngineId], [EntryDate], [Rank], [Url], [SearchTerm]) VALUES (956, 2, CAST(N'2025-05-15T00:00:00.000' AS DateTime), 0, N'', N'infotrack')
GO
SET IDENTITY_INSERT [dbo].[SearchResults] OFF
GO
