CREATE DATABASE [PagoEfectivo]

USE [PagoEfectivo]
GO
/****** Object:  Table [dbo].[MaintenanceClient]    Script Date: 29/10/2021 07:52:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceClient](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MaintenanceClient] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceCode]    Script Date: 29/10/2021 07:52:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceCode](
	[IdCode] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[TimeExpire] [datetime] NOT NULL,
	[StatusCode] [int] NOT NULL,
	[RegisterDate] [datetime] NULL,
	[ExchageDate] [datetime] NULL,
 CONSTRAINT [PK_MaintenanceCode] PRIMARY KEY CLUSTERED 
(
	[IdCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceStatus]    Script Date: 29/10/2021 07:52:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceStatus](
	[Idstatus] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_MaintenanceStatus] PRIMARY KEY CLUSTERED 
(
	[Idstatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET IDENTITY_INSERT [dbo].[MaintenanceStatus] ON 

INSERT [dbo].[MaintenanceStatus] ([Idstatus], [Description]) VALUES (1, N'Generado')
INSERT [dbo].[MaintenanceStatus] ([Idstatus], [Description]) VALUES (2, N'Canjeado')
SET IDENTITY_INSERT [dbo].[MaintenanceStatus] OFF
ALTER TABLE [dbo].[MaintenanceCode]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceCode_MaintenanceClient1] FOREIGN KEY([idCliente])
REFERENCES [dbo].[MaintenanceClient] ([idCliente])
GO
ALTER TABLE [dbo].[MaintenanceCode] CHECK CONSTRAINT [FK_MaintenanceCode_MaintenanceClient1]
GO
ALTER TABLE [dbo].[MaintenanceCode]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceCode_MaintenanceStatus] FOREIGN KEY([StatusCode])
REFERENCES [dbo].[MaintenanceStatus] ([Idstatus])
GO
ALTER TABLE [dbo].[MaintenanceCode] CHECK CONSTRAINT [FK_MaintenanceCode_MaintenanceStatus]
GO


/****** Object:  StoredProcedure [dbo].[SP_EXCHANGE]    Script Date: 29/10/2021 07:52:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_EXCHANGE]
(@code varchar(20))
AS
BEGIN

DECLARE @exist int 
set @exist = (SELECT count(*) FROM MaintenanceCode WHERE Code =  @code and StatusCode = 1)

if(@exist>0)
begin 

	UPDATE MaintenanceCode 
	SET StatusCode = 2,ExchageDate = GETDATE()
	WHERE Code =  @code and StatusCode = 1

	SELECT 'El código fue canjeado exitosamente.'
end
else
begin 
	SELECT 'El código no existe o fue canjeado anteriormente.'
end


End
GO
/****** Object:  StoredProcedure [dbo].[SP_EXIST_EMAIL]    Script Date: 29/10/2021 07:52:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_EXIST_EMAIL](
@email varchar(80)
)
as
BEGIN

SELECT COUNT(*) from MaintenanceClient WHERE Email = @email

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SAVE_DATA]    Script Date: 29/10/2021 07:52:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_SAVE_DATA]
(
@email varchar(80),
@name varchar(100),
@code varchar(12),
@time datetime
)
as
begin 

Declare @id int;

INSERT INTO MaintenanceClient (email, name) VALUES(@email,@name);
print 'LLEGO'
SELECT @id =  SCOPE_IDENTITY()

SELECT @id,@code,@time,'Generado', GETDATE()
INSERT INTO MaintenanceCode (idCliente, Code,TimeExpire, StatusCode ,RegisterDate) VALUES(@id,@code,@time,1, GETDATE());


end 
GO
/****** Object:  StoredProcedure [dbo].[SP_SEARCH_ALL]    Script Date: 29/10/2021 07:52:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SEARCH_ALL]
AS 
BEGIN

SELECT a.Name,
a.Email,
B.Code,
c.Description,
b.TimeExpire
from MaintenanceClient A
INNER JOIN MaintenanceCode B on a.idCliente = b.idCliente
INNER JOIN MaintenanceStatus C ON b.StatusCode = c.Idstatus

END
GO
