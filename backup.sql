USE [Proyecto]
GO
/****** Object:  Table [dbo].[Tb_Auditoria]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Auditoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Accion] [nvarchar](50) NOT NULL,
	[Fecha] [datetime] NULL,
	[Entidad] [nvarchar](50) NOT NULL,
	[Detalle] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Categoria]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Categoria](
	[Id] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Cotizacion]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Cotizacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Presupuesto] [int] NOT NULL,
	[TiempoEstadia] [int] NOT NULL,
	[MotivoViaje] [nvarchar](50) NOT NULL,
	[CantidadPersonas] [int] NOT NULL,
	[Resultado] [text] NOT NULL,
	[FechaGeneracion] [datetime] NULL,
	[ProvinciaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_CotizacionGuardada]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_CotizacionGuardada](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[HotelId] [int] NOT NULL,
	[Hotel] [nvarchar](100) NOT NULL,
	[CostoHotel] [decimal](10, 2) NOT NULL,
	[RestauranteId] [int] NOT NULL,
	[Restaurante] [nvarchar](100) NOT NULL,
	[CostoRestaurante] [decimal](10, 2) NOT NULL,
	[LugarId] [int] NOT NULL,
	[Lugar] [nvarchar](100) NOT NULL,
	[CostoLugar] [decimal](10, 2) NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
	[PresupuestoRestante] [decimal](10, 2) NOT NULL,
	[PorcentajePresupuesto] [decimal](5, 2) NOT NULL,
	[AjusteEspecial] [bit] NOT NULL,
	[FechaGuardado] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_DetalleNegocio]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_DetalleNegocio](
	[Id] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NULL,
	[Servicio] [nvarchar](100) NULL,
	[Precio] [decimal](19, 4) NULL,
	[Imagen] [nvarchar](100) NULL,
	[NegocioId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_EstadisticasIA]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_EstadisticasIA](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NOT NULL,
	[Interacciones] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NegocioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_EstadisticasRecomendacion]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_EstadisticasRecomendacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NOT NULL,
	[ServicioId] [int] NOT NULL,
	[TipoServicio] [nvarchar](20) NOT NULL,
	[FechaRecomendacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_EstadisticasVisita]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_EstadisticasVisita](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NOT NULL,
	[Visitas] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NegocioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Feedback]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Comentario] [text] NULL,
	[Calificacion] [int] NOT NULL,
	[Fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Historial]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Historial](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[CotizacionId] [int] NOT NULL,
	[FechaVisualizacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ImagenNegocio]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ImagenNegocio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NULL,
	[Ruta] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Negocio]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Negocio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NULL,
	[Nombre] [nvarchar](70) NULL,
	[TipoNegocioId] [int] NULL,
	[ProvinciaId] [int] NULL,
	[Direccion] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](9) NOT NULL,
	[Descripcion] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Provincia]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Provincia](
	[Id] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ServicioHotel]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ServicioHotel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NULL,
	[CantidadPersonas] [int] NULL,
	[WiFi] [bit] NULL,
	[AguaCaliente] [bit] NULL,
	[RoomService] [bit] NULL,
	[Cochera] [bit] NULL,
	[Cable] [bit] NULL,
	[DesayunoIncluido] [bit] NULL,
	[Precio] [decimal](10, 2) NULL,
	[Fotos] [text] NULL,
	[Estado] [nvarchar](50) NULL,
	[Nomre] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ServicioRestaurante]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ServicioRestaurante](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NULL,
	[NombrePlato] [nvarchar](100) NULL,
	[TipoPlato] [nvarchar](20) NULL,
	[Precio] [decimal](10, 2) NULL,
	[Descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ServicioTuristico]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ServicioTuristico](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NegocioId] [int] NULL,
	[Provincia] [nvarchar](50) NULL,
	[NombreLugar] [nvarchar](100) NULL,
	[Precio] [decimal](10, 2) NULL,
	[Descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_TipoNegocio]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_TipoNegocio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[CategoriaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_TipoUsuario]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_TipoUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_Usuario]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DniRut] [varchar](8) NULL,
	[Ruc] [varchar](11) NULL,
	[NombresCompleto] [nvarchar](50) NOT NULL,
	[Correo] [nvarchar](200) NOT NULL,
	[TipoUsuarioId] [int] NULL,
	[Estado] [int] NOT NULL,
	[Contraseña] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_ValoracionCotizacion]    Script Date: 19/04/2025 04:43:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_ValoracionCotizacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Estrellas] [int] NOT NULL,
	[Comentario] [nvarchar](max) NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tb_Auditoria] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Tb_Cotizacion] ADD  DEFAULT (getdate()) FOR [FechaGeneracion]
GO
ALTER TABLE [dbo].[Tb_CotizacionGuardada] ADD  DEFAULT ((0)) FOR [AjusteEspecial]
GO
ALTER TABLE [dbo].[Tb_CotizacionGuardada] ADD  DEFAULT (getdate()) FOR [FechaGuardado]
GO
ALTER TABLE [dbo].[Tb_EstadisticasIA] ADD  DEFAULT ((0)) FOR [Interacciones]
GO
ALTER TABLE [dbo].[Tb_EstadisticasRecomendacion] ADD  DEFAULT (getdate()) FOR [FechaRecomendacion]
GO
ALTER TABLE [dbo].[Tb_EstadisticasVisita] ADD  DEFAULT ((0)) FOR [Visitas]
GO
ALTER TABLE [dbo].[Tb_Feedback] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Tb_Historial] ADD  DEFAULT (getdate()) FOR [FechaVisualizacion]
GO
ALTER TABLE [dbo].[Tb_Negocio] ADD  DEFAULT ('Sin descripción') FOR [Descripcion]
GO
ALTER TABLE [dbo].[Tb_ServicioRestaurante] ADD  DEFAULT ('almuerzo') FOR [TipoPlato]
GO
ALTER TABLE [dbo].[Tb_ValoracionCotizacion] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Tb_Cotizacion]  WITH CHECK ADD FOREIGN KEY([ProvinciaId])
REFERENCES [dbo].[Tb_Provincia] ([Id])
GO
ALTER TABLE [dbo].[Tb_Cotizacion]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Tb_Usuario] ([Id])
GO
ALTER TABLE [dbo].[Tb_DetalleNegocio]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_EstadisticasIA]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_EstadisticasRecomendacion]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_EstadisticasVisita]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_Feedback]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_Feedback]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Tb_Usuario] ([Id])
GO
ALTER TABLE [dbo].[Tb_Historial]  WITH CHECK ADD FOREIGN KEY([CotizacionId])
REFERENCES [dbo].[Tb_Cotizacion] ([Id])
GO
ALTER TABLE [dbo].[Tb_Historial]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Tb_Usuario] ([Id])
GO
ALTER TABLE [dbo].[Tb_ImagenNegocio]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tb_Negocio]  WITH CHECK ADD FOREIGN KEY([ProvinciaId])
REFERENCES [dbo].[Tb_Provincia] ([Id])
GO
ALTER TABLE [dbo].[Tb_Negocio]  WITH CHECK ADD FOREIGN KEY([TipoNegocioId])
REFERENCES [dbo].[Tb_TipoNegocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_Negocio]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Tb_Usuario] ([Id])
GO
ALTER TABLE [dbo].[Tb_ServicioHotel]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_ServicioRestaurante]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_ServicioTuristico]  WITH CHECK ADD FOREIGN KEY([NegocioId])
REFERENCES [dbo].[Tb_Negocio] ([Id])
GO
ALTER TABLE [dbo].[Tb_EstadisticasRecomendacion]  WITH CHECK ADD CHECK  (([TipoServicio]='restaurante' OR [TipoServicio]='lugar' OR [TipoServicio]='hotel'))
GO
ALTER TABLE [dbo].[Tb_ServicioRestaurante]  WITH CHECK ADD CHECK  (([TipoPlato]='cena' OR [TipoPlato]='almuerzo' OR [TipoPlato]='entrada' OR [TipoPlato]='desayuno' OR [TipoPlato]='postre' OR [TipoPlato]='bebida'))
GO
ALTER TABLE [dbo].[Tb_ValoracionCotizacion]  WITH CHECK ADD CHECK  (([Estrellas]>=(1) AND [Estrellas]<=(5)))
GO
