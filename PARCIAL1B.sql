USE PARCIAL1B
GO
CREATE TABLE [dbo].[Platos](
    [PlatoID] [int] IDENTITY(1,1) NOT NULL,
    [EmpresaID] [int] NULL,
    [GrupoID] [int] NULL,
    [NombrePlato] [varchar](50) NULL,
	[DescripcionPlato] [varchar](50) NULL,
    [costo] [numeric](18, 4) NULL,

 CONSTRAINT [PK_platos] PRIMARY KEY CLUSTERED 
(
    [PlatoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Elementos](
    [ElementoID] [int] IDENTITY(1,1) NOT NULL,
    [EmpresaID] [int] NULL,
    [Elemento] [varchar](50) NULL,
    [CantidadMinima] [int] NULL,
	[UnidadMedida] [varchar](50) NULL,
    [costo] [numeric](18, 4) NULL,
	[Estado] [varchar](50) NULL,

 CONSTRAINT [PK_elementos] PRIMARY KEY CLUSTERED 
(
    [ElementoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElementosPorPlato](
    [ElementoPorPlatoID] [int] IDENTITY(1,1) NOT NULL,
    [EmpresaID] [int] NULL,
    [PlatoID] [int] NULL,
    [ElementoID] [int] NULL,
	[Cantidad] [int] NULL,
	[Estado] [varchar](50) NULL,

 CONSTRAINT [PK_elementosporplato] PRIMARY KEY CLUSTERED 
(
    [ElementoPorPlatoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlatoPorCombo](
    [PlatosPorComboID] [int] IDENTITY(1,1) NOT NULL,
    [EmpresaID] [int] NULL,
    [ComboID] [int] NULL,
    [PlatoID] [int] NULL,
	[Estado] [varchar](50) NULL,

 CONSTRAINT [PK_PlatoPorCombo] PRIMARY KEY CLUSTERED 
(
    [PlatosPorComboID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--inserts
go
-- Inserts para la tabla Platos
INSERT INTO Platos (EmpresaID, GrupoID, NombrePlato, DescripcionPlato, costo)
VALUES (1, 1, 'Plato1', 'Descripción del Plato 1', 10.99),
       (1, 2, 'Plato2', 'Descripción del Plato 2', 15.50),
       (2, 1, 'Plato3', 'Descripción del Plato 3', 12.75);
go
-- Inserts para la tabla Elementos
INSERT INTO Elementos (EmpresaID, Elemento, CantidadMinima, UnidadMedida, costo, Estado)
VALUES (1, 'Elemento1', 100, 'kg', 5.25, 'Disponible'),
       (1, 'Elemento2', 50, 'unidades', 2.99, 'Disponible'),
       (2, 'Elemento3', 200, 'litros', 8.50, 'Disponible');
go
-- Inserts para la tabla ElementosPorPlato
INSERT INTO ElementosPorPlato (EmpresaID, PlatoID, ElementoID, Cantidad, Estado)
VALUES (1, 1, 1, 200, 'Usado'),
       (1, 1, 2, 100, 'Usado'),
       (1, 2, 3, 150, 'Usado');
go
-- Inserts para la tabla PlatoPorCombo
INSERT INTO PlatoPorCombo (EmpresaID, ComboID, PlatoID, Estado)
VALUES (1, '1', 1, 'Disponible'),
       (1, '1', 2, 'Disponible'),
       (2, '1', 3, 'Disponible');
go