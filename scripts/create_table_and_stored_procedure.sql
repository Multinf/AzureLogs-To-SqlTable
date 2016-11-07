/****** 

Deployment script for the Azure error logs


This code was generate by sql tool, so
if you find an error please make a issue. 

It is neccesary to have SQL Server 2016 in the Azure.

******/

PRINT N'Deleting existing table...'
GO

DROP TABLE IF EXISTS [dbo].[tbErrorData]
GO

PRINT N'Deleting OK.'
GO

PRINT N'Creating table...'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbErrorData](
	[id] [int] NOT NULL,
	[dtefullDate] [datetime] NOT NULL,
	[strname] [nvarchar](100) NOT NULL,
	[strdescription] [text] NOT NULL,
	[strplace] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_tbErrorData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
PRINT N'Creating OK.'
GO

PRINT N'Deleting the Stored Procedure...'
GO

DROP PROCEDURE IF EXISTS [spInsertErrorLog]
GO

PRINT N'Deleting OK.'
GO

PRINT N'Creating the stored procedure...'
GO
-- =============================================
-- Author:		Jesús Fernandez | Dmcory | Multinf.
-- Date:		2016-11-07
-- Description:	Saves the data from eventlog.xml
-- =============================================
CREATE PROCEDURE [dbo].[spInsertErrorLog]
	@Id INT,
	@Date DATETIME,
	@Name NVARCHAR(100),
	@Description TEXT,
	@Place NVARCHAR(500)
AS
BEGIN
	
	IF (SELECT 1 FROM tbErrorData WHERE id = @Id) = 0
		BEGIN
			INSERT INTO tbErrorData(id, dtefullDate, strname, strdescription, strplace)
			SELECT @Id, @Date, @Name, @Description, @Place
		END
END

GO

PRINT N'Creating OK.'
GO

