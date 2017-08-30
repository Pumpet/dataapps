drop proc dbo.AnketProcess
go
create proc dbo.AnketProcess
  @AnketId int
, @MsgRet varchar(100) out
as
BEGIN TRY
-------------------------------------------------------------------------------
if not exists(select 1 from Ankets where AnketId = @AnketId)
  raiserror ('Не найдена анкета с Id = %d', 16, 1, @AnketId)

declare 
  @PopulId int
, @LinguaId int
, @Origin varchar(100)
, @RUSID varchar(100)

select
  @PopulId = PopulId
, @LinguaId = LinguaId
, @Origin = Origin
, @RUSID = RUSID
  from Ankets
 where AnketId = @AnketId

insert AnketRels (
  AnketId
, RelTypeId
, PopulId
, Origin
, LinguaId
, BirthPlace
)
select
  @AnketId
, r.ListId
, @PopulId
, @Origin
, @LinguaId
, ''
  from Lists r
 where r.TypeCode = 'RELTYPE'
   and not exists(select 1 from AnketRels ar where ar.AnketId = @AnketId and ar.RelTypeId = r.ListId)

declare @SampleTypeId int
select @SampleTypeId = min(r.ListId) from Lists r where r.TypeCode = 'SAMPLETYPE'

insert Samples (
  AnketId
, SampleTypeId
, SampleCode
)
select
  @AnketId
, r.ListId
, @RUSID
  from Lists r
 where r.ListId = @SampleTypeId
   and not exists(select 1 from Samples s where s.AnketId = @AnketId and s.SampleTypeId = r.ListId)

--select @MsgRet = ''
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

select * from Lists
select * from Ankets
select * from AnketRels
delete AnketRels where AnketId = 3

declare @ret int
exec @ret = AnketProcess 3
select @ret


*/
