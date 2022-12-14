USE [Test]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Precio] [real] NOT NULL,
	[IdTipoProducto] [int] NOT NULL,
	[Created_At] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [real] NOT NULL,
	[Created_At] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_StockProducto]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_StockProducto] 
as
select p.nombre, p.precio, s.cantidad from Producto as P inner join Stock as S on S.idProducto = P.id;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoProducto]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoProducto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Created_At] [datetime2](7) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_TipoProducto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221115004613_InitDB', N'5.0.16')
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([Id], [Nombre], [Precio], [IdTipoProducto], [Created_At], [Deleted]) VALUES (1, N'Detergente ', 85, 3, CAST(N'2022-11-21T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Producto] ([Id], [Nombre], [Precio], [IdTipoProducto], [Created_At], [Deleted]) VALUES (2, N'Sprite 2,5Ml', 360, 4, CAST(N'2022-11-15T13:35:58.0160370' AS DateTime2), 0)
INSERT [dbo].[Producto] ([Id], [Nombre], [Precio], [IdTipoProducto], [Created_At], [Deleted]) VALUES (5, N'Limpia vidrio', 300, 3, CAST(N'2022-10-10T00:00:00.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Stock] ON 

INSERT [dbo].[Stock] ([Id], [IdProducto], [Cantidad], [Created_At], [Deleted]) VALUES (1, 1, 54, CAST(N'2022-11-21T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Stock] ([Id], [IdProducto], [Cantidad], [Created_At], [Deleted]) VALUES (2, 2, 285, CAST(N'2022-11-15T14:07:48.6621453' AS DateTime2), 0)
INSERT [dbo].[Stock] ([Id], [IdProducto], [Cantidad], [Created_At], [Deleted]) VALUES (4, 5, 6, CAST(N'2022-12-22T00:00:00.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Stock] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoProducto] ON 

INSERT [dbo].[TipoProducto] ([Id], [Descripcion], [Created_At], [Deleted]) VALUES (3, N'Limpieza', CAST(N'2022-11-21T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TipoProducto] ([Id], [Descripcion], [Created_At], [Deleted]) VALUES (4, N'Bebida', CAST(N'2022-11-15T13:20:15.9670111' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[TipoProducto] OFF
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_TipoProducto_IdTipoProducto] FOREIGN KEY([IdTipoProducto])
REFERENCES [dbo].[TipoProducto] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_TipoProducto_IdTipoProducto]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Producto_IdProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Producto_IdProducto]
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarProducto]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_EliminarProducto]
@id int
as 
delete from Producto where id = @id;
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarProducto]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_InsertarProducto]
@tipoProducto int,
@nombre as nvarchar(50),
@precio as float
as
insert into Producto(idTipoProducto, nombre, precio) values(@tipoProducto, @nombre, @precio)
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificarProducto]    Script Date: 15/11/2022 15:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ModificarProducto]
@tipoProducto int,
@nombre as nvarchar(50),
@precio as float,
@busqueda nvarchar(50)
as
UPDATE Producto SET idTipoProducto = @tipoProducto, nombre = @nombre, precio = @precio
  WHERE nombre = @busqueda;
GO
