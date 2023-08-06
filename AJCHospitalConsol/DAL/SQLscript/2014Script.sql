CREATE DATABASE AJCHospital;
GO
USE [AJCHospital]
GO
/****** Object:  Table [dbo].[Consultation_T]    Script Date: 06/08/2023 16:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consultation_T](
	[ConsultationID] [int] IDENTITY(1,1) NOT NULL,
	[PatID] [int] NOT NULL,
	[PatSocialSecurityID] [varchar](100) NOT NULL,
	[DocID] [int] NOT NULL,
	[DocName] [varchar](100) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Consultation_T] PRIMARY KEY CLUSTERED 
(
	[ConsultationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient_T]    Script Date: 06/08/2023 16:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient_T](
	[PatientID] [int] IDENTITY(1,1) NOT NULL,
	[SocialSecurityID] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[Adress] [varchar](100) NULL,
	[Tel] [varchar](100) NULL,
 CONSTRAINT [PK_Patient_T] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_SocialSecurityID_Patient_T] UNIQUE NONCLUSTERED 
(
	[SocialSecurityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_T]    Script Date: 06/08/2023 16:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_T](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[EmpCode] [varchar](100) NOT NULL,
 CONSTRAINT [PK_User_T] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_UserName_User_T] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Consultation_T]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Consultation] FOREIGN KEY([PatID])
REFERENCES [dbo].[Patient_T] ([PatientID])
GO
ALTER TABLE [dbo].[Consultation_T] CHECK CONSTRAINT [FK_Patient_Consultation]
GO
ALTER TABLE [dbo].[Consultation_T]  WITH CHECK ADD  CONSTRAINT [FK_User_Consultation] FOREIGN KEY([DocID])
REFERENCES [dbo].[User_T] ([UserID])
GO
ALTER TABLE [dbo].[Consultation_T] CHECK CONSTRAINT [FK_User_Consultation]
GO
USE [master]
GO
ALTER DATABASE [AJCHospital] SET  READ_WRITE 
GO

