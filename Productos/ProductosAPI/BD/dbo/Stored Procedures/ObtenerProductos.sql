-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerProductos]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT 
      p.Id,
      p.Nombre AS nombre,
      p.Descripcion,
      p.Precio,
      p.Stock,
      p.CodigoBarras,
      sc.Nombre AS SubCategoria,
	  c.Nombre AS categoría
FROM dbo.Producto p
INNER JOIN dbo.SubCategorias sc 
    ON p.IdSubCategoria = sc.Id
INNER JOIN dbo.Categorias c on sc.IdCategoria = c.Id;

  
END