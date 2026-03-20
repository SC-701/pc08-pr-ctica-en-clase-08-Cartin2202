-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarProductos
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
	INSERT INTO [dbo].[Producto]
           (
        Id,
        IdSubCategoria,
        Nombre,
        Descripcion,
        Precio,
        Stock,
        CodigoBarras
    )
    VALUES
    (
        @Id,
        @IdSubCategoria,
        @Nombre,
        @Descripcion,
        @Precio,
        @Stock,
        @CodigoBarras
    );
    SELECT @Id
    COMMIT TRANSACTION
END