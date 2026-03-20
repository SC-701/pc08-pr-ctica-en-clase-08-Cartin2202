-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EliminarProductos
	-- Add the parameters for the stored procedure here
	@Id AS UNIQUEIDENTIFIER
      
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	begin tranSACTION
	DELETE from  [dbo].[Producto]
    WHERE Id=@Id
	select @Id
	commit TRANSACTION
    
END