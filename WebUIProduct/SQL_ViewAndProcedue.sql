USE [ECommerceDemo]
GO
/****** Object:  View [dbo].[TblProductAttributeLookupView]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TblProductAttributeLookupView]
AS
SELECT AttributeId, ProdCatId, AttributeName
FROM     dbo.ProductAttributeLookup

GO
/****** Object:  View [dbo].[TblProductAttributeView]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TblProductAttributeView]
AS
SELECT ProductId, AttributeId, AttributeValue
FROM     dbo.ProductAttribute

GO
/****** Object:  View [dbo].[TblProductCategoryView]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TblProductCategoryView]
AS
SELECT ProdCatId, CategoryName
FROM     dbo.ProductCategory

GO
/****** Object:  View [dbo].[TblProductView]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TblProductView]
AS
SELECT dbo.Product.ProductId, dbo.Product.ProdCatId, dbo.Product.ProdName, dbo.Product.ProdDescription, dbo.ProductCategory.CategoryName
FROM     dbo.Product INNER JOIN
                  dbo.ProductCategory ON dbo.Product.ProdCatId = dbo.ProductCategory.ProdCatId

GO
/****** Object:  StoredProcedure [dbo].[InsertOrUpdateOrDeleteProduct]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chirag Sathvara
-- Create date: 19 March 2021
-- Description:	Insert or Update Product
-- =============================================
CREATE PROCEDURE [dbo].[InsertOrUpdateOrDeleteProduct]
	@ProductId bigint=0,
	@ProdCatId int,
	@ProdName varchar(250),
	@ProdDescription varchar(250),
	@Action int = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RowEffect int=0
	if @Action = 1  --for Insert Statement
	Begin
		-- Insert statements for procedure here
	DECLARE @IsFindProduct INT;
	SELECT @IsFindProduct=count(*) FROM [dbo].[Product] WHERE [ProdName]=@ProdName and @ProdCatId=ProdCatId

	if @IsFindProduct = 1 
	begin
		SELECT 0 as RowEffect,'Warning: Product name already exist.' as [message]; 
	end
	else
	begin
	INSERT INTO [dbo].[Product]
           ([ProdCatId]
           ,[ProdName]
           ,[ProdDescription])
     VALUES
           (@ProdCatId,
		   @ProdName,
		   @ProdDescription)
	SELECT @@ROWCOUNT as RowEffect,SCOPE_IDENTITY() as [ProductId],'Product name inserted.' as [message];
	end
	
	END

	if @Action = 2  --for Update Statement
	BEGIN
	-- Update statements for procedure here
		UPDATE [dbo].[Product]
			SET [ProdName] = @ProdName,
			[ProdDescription] = @ProdDescription,
			[ProdCatId] = @ProdCatId
			WHERE [ProductId]=@ProductId;
			SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were updated' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Product name updated.' as [message];
			END			
	END

	if @Action = 3  --for Delete Statement
	BEGIN
		DELETE FROM [dbo].[Product] WHERE ProductId=@ProductId
		SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were deleted' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Product name deleted.' as [message];
			END	 
	END
END

GO
/****** Object:  StoredProcedure [dbo].[InsertOrUpdateOrDeleteProductAttribute]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chirag Sathvara
-- Create date: 19 March 2021
-- Description:	Insert or Update Product
-- =============================================
CREATE PROCEDURE [dbo].[InsertOrUpdateOrDeleteProductAttribute]
	@AttributeId int,
	@ProductId bigint,
	@AttributeValue varchar(250),
	@Action int = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RowEffect int=0
	if @Action = 1  --for Insert Statement
	Begin
		-- Insert statements for procedure here
	DECLARE @IsFindProductAttribute INT;
	SELECT @IsFindProductAttribute=count(*) FROM [dbo].[ProductAttribute] WHERE [AttributeValue]=@AttributeValue and ProductId=@ProductId AND ProductId=@ProductId

	IF @IsFindProductAttribute = 1 
		BEGIN
				SELECT 0 as RowEffect,'Warning: Attribute value already exist.' as [message];
		END
	ELSE
		BEGIN
		INSERT INTO [dbo].[ProductAttribute]
			   ([ProductId]
			   ,[AttributeId]
			   ,[AttributeValue])
		 VALUES
			   (@ProductId,
			   @AttributeId,
			   @AttributeValue)
		SELECT @@ROWCOUNT as RowEffect,SCOPE_IDENTITY() as [ProductId],'Attribute value inserted.' as [message];
		END
	END
	if @Action = 2  --for Update Statement
	BEGIN
	-- Update statements for procedure here
		UPDATE [dbo].[ProductAttribute]
			SET [AttributeValue] = @AttributeValue
			WHERE [ProductId]=@ProductId AND [AttributeId] = @AttributeId;
			SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were updated' as [message];
			END
		ELSE
			BEGIN
				SELECT @@ROWCOUNT as RowEffect,'Attribute value updated.' as [message];
			END			
	END

	if @Action = 3  --for Delete Statement
	BEGIN
		DELETE FROM [dbo].[ProductAttribute] WHERE [ProductId]=@ProductId AND [AttributeId] = @AttributeId;
		SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were deleted' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Attribute value deleted.' as [message];
			END	 
	END
END

GO
/****** Object:  StoredProcedure [dbo].[InsertOrUpdateOrDeleteProductAttributeLookup]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chirag Sathvara
-- Create date: 19 March 2021
-- Description:	Insert or Update Product
-- =============================================
CREATE PROCEDURE [dbo].[InsertOrUpdateOrDeleteProductAttributeLookup]
	@AttributeId int=0,
	@ProdCatId int,
	@AttributeName varchar(250),
	@Action int = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RowEffect int=0
	if @Action = 1  --for Insert Statement
	Begin
		-- Insert statements for procedure here
	DECLARE @IsFindProductAttributeLookup INT;
	SELECT @IsFindProductAttributeLookup=count(*) FROM [dbo].[ProductAttributeLookup] WHERE [AttributeName]=@AttributeName and @ProdCatId=ProdCatId

	if @IsFindProductAttributeLookup = 1 
	begin
		SELECT 0 as RowEffect,'Warning: Attribute name already exist.' as [message]; 

	end
	else
	begin
	INSERT INTO [dbo].[ProductAttributeLookup]
           ([ProdCatId]
           ,[AttributeName])
     VALUES
           (@ProdCatId,
		   @AttributeName)
	SELECT @@ROWCOUNT as RowEffect,SCOPE_IDENTITY() as [AttributeId],'Attribute name inserted.' as [message];
	end
	
	END

	if @Action = 2  --for Update Statement
	BEGIN
	-- Update statements for procedure here
		UPDATE [dbo].[ProductAttributeLookup]
			SET [AttributeName] = @AttributeName,
				[ProdCatId] = @ProdCatId
			WHERE [AttributeId]=@AttributeId;
		SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were updated' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Attribute name updated.' as [message];
			END			
	END

	if @Action = 3  --for Delete Statement
	BEGIN
		DELETE FROM [dbo].[ProductAttributeLookup] WHERE [AttributeId]=@AttributeId;
		SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were deleted' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Attribute name deleted.' as [message];
			END	 
	END
END

GO
/****** Object:  StoredProcedure [dbo].[InsertOrUpdateOrDeleteProductCategory]    Script Date: 3/23/2021 1:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chirag Sathvara
-- Create date: 19 March 2021
-- Description:	Insert or Update Product Category
-- =============================================
CREATE PROCEDURE [dbo].[InsertOrUpdateOrDeleteProductCategory]
	@ProdCatId int=0,
	@CategoryName varchar(250),
	@Action int = 1
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @RowEffect int=0
		
	if @Action = 1  --for Insert Statement
	Begin
		-- Insert statements for procedure here
	DECLARE @IsFindCategory INT;
	SELECT @IsFindCategory=count(*) FROM [dbo].[ProductCategory] WHERE [CategoryName]=@CategoryName

	if @IsFindCategory = 1 
	begin
		SELECT 0 as RowEffect,'Warning: Category name already exist.' as [message];
	end
	else
	begin
	INSERT INTO [dbo].[ProductCategory]
           ([CategoryName])
     VALUES
           (@CategoryName)
	SELECT @@ROWCOUNT as RowEffect,SCOPE_IDENTITY() as [ProdCatId],'Product category inserted.' as [message];
	end
	
	END

	if @Action = 2  --for Update Statement
	BEGIN
	-- Update statements for procedure here
		UPDATE [dbo].[ProductCategory]
			SET [CategoryName] = @CategoryName
			WHERE [ProdCatId]=@ProdCatId;
		SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were updated' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Product category updated.' as [message];
			END			
	END

	if @Action = 3  --for Delete Statement
	BEGIN
		DELETE FROM [dbo].[ProductCategory] WHERE ProdCatId=@ProdCatId
		SET @RowEffect = @@ROWCOUNT
		IF @RowEffect = 0
			BEGIN
				SELECT @RowEffect as RowEffect,'Warning: No rows were deleted' as [message];
			END
		ELSE
			BEGIN
				SELECT @RowEffect as RowEffect,'Product category deleted.' as [message];
			END	 
	END
END

