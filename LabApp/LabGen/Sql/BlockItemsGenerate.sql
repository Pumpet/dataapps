if exists(select 1 from sysobjects where type='FN' and name='fGetLetterForNumber')
 drop function dbo.fGetLetterForNumber
go
create function dbo.fGetLetterForNumber(@n int, @s varchar(255))
returns varchar(255)
AS
begin
  if @n = 0
    return @s

  declare @z int, @d int
  select @z = 26
  select @d = @n/@z
  
  if @n = @d*@z
    select @d = @d - 1 

  if @d > 0
    select @s = dbo.fGetLetterForNumber(@d, @s) + @s
  
  select @s = @s + char(@n - @d*@z + 64)

  return @s
end
go

--select dbo.fGetLetterForNumber(703, '')

------------------------------------------------------------------------------------------------
drop proc dbo.BlockItemsGenerate
go
create proc dbo.BlockItemsGenerate
  @BlockId int
, @DimX int
, @DimY int
, @MsgRet varchar(100) out
as
BEGIN TRY
-------------------------------------------------------------------------------
if not exists(select 1 from Blocks where BlockId = @BlockId)
  raiserror ('Не найден штатив с Id = %d', 16, 1, @BlockId)

if exists(select 1 from BlockItems bi, SampleItems si
           where bi.BlockId = @BlockId 
             and si.BlockItemId = bi.BlockItemId)
  or exists(select 1 from BlockItems bi, DnkItems di
             where bi.BlockId = @BlockId 
               and di.BlockItemId = bi.BlockItemId)
begin
  select @MsgRet = 'В штативе есть образцы!'
  return 1
end

delete BlockItems where BlockId = @BlockId

declare @x int, @y int
select @y = 1
while @y <= @DimY
begin
  select @x = 1
  while @x <= @DimX
  begin
    
    insert BlockItems(BlockId, BlockItemCode)
    select @BlockId, dbo.fGetLetterForNumber(@y, '') + replicate('0', len(convert(varchar,@DimX))-len(convert(varchar,@x))) + convert(varchar,@x)
    
    select @x = @x + 1   
  end
  select @y = @y + 1
end

return 0
-------------------------------------------------------------------------------
END TRY
BEGIN CATCH  
	DECLARE @ErrorMessage NVARCHAR(4000);  
	DECLARE @ErrorSeverity INT;  
	DECLARE @ErrorState INT;  
  DECLARE @ErrorNumber INT; 
	SELECT   
		@ErrorMessage = ERROR_MESSAGE(),  
		@ErrorSeverity = ERROR_SEVERITY(),  
		@ErrorState = ERROR_STATE(),
    @ErrorNumber = 50000 + ERROR_NUMBER();  
  RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);  
  RETURN ERROR_NUMBER()
END CATCH;  
go

/*

declare @ret int, @msg
exec @ret = BlockItemsGenerate 1, 2, 3, @msg out
select @ret, @msg


*/
