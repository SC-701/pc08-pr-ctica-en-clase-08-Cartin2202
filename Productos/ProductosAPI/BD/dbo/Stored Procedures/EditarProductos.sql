-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarProductos
	-- Add the parameters for the stored procedure here
	@Id            UNIQUEIDENTIFIER,
    @IdSubCategoria UNIQUEIDENTIFIER,
    @Nombre        AS varchar(max),       
    @Descripcion   AS varchar(max),      
    @Precio        DECIMAL(18,0),
    @Stock         INT,
    @CodigoBarras  AS varchar(max)     
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    BEGIN TRANSACTION
	Update  [dbo].[Producto]
    set
        Id=@Id,
        IdSubCategoria=@IdSubCategoria,
        Nombre=@Nombre,
        Descripcion=@Descripcion,
        Precio=@Precio,
        Stock=@Stock,
        CodigoBarras=@CodigoBarras
        WHERE Id=@Id
        SELECT @Id
        COMMIT TRANSACTION
    
END