/*
drop table dbo.RepSamples
go
create table dbo.RepSamples (
  spid int not null
, SampleId int not null 
, AnketId int not null
, SampleCode varchar(100) not null -- ����� �������
, ExpedName varchar(100) not null -- ����������
, PointRegion varchar(200) not null -- �����
, EtnoName varchar(100) not null -- ����� (���)
, PopulName varchar(100) not null -- ���������
, SampleType varchar(100) not null -- ��� �������
, Pol varchar(1) not null -- ���
, BirthYear int not null -- ��� ��������

, ItemId_BV int null -- ������� ����������
, ItemId_MV int null -- ��������� ����������
, ItemId_OK int null -- ������� �����
, ItemId_SK int null -- ����� ������� �����
, ItemId_KD int null -- ������������� ���
, ItemId_AD int null -- �������� ���
, ItemId_RD int null -- ������� ���
, ItemId_ND int null -- ��������������� ���

, EyesColor varchar(100) null -- ���� ����
, HairColor varchar(100) null -- ���� �����
, EtnoNameY varchar(100) null -- ����� �� Y
, HaploY varchar(100) null    -- ����������� Y
, MarkerY varchar(100) null   -- ������ Y
, EtnoNameMt varchar(100) null -- ����� �� ��
, HaploMt varchar(100) null    -- ����������� mt
, MarkerMt varchar(100) null   -- ������ mt
, GWS1 float null -- ���1
, GWS2 float null -- ���2
)  
go
grant all on dbo.RepSamples to PUBLIC
go

drop table dbo.RepSampleItems
go
create table dbo.RepSampleItems (
  spid int not null
, ItemKind int not null -- 1 - �������, 2 - ���
, ItemType varchar(100) not null -- ��� ������� �������
, ItemId int not null -- Id ������� ������� 
, Lab varchar(100) not null -- ����������� (��������)
, Fridge varchar(100) not null -- ���������
, FridgeModule varchar(100) not null -- �����
, FridgeShelf varchar(100) not null -- �����
, BlockCode varchar(100) not null -- ������
, BlockItemCode varchar(100) not null -- ������
, Concentration float null -- ������������ ��� 
, Quality float null -- �������� ���
, Volume float null -- ����� ��� 
)
go
grant all on dbo.RepSampleItems to PUBLIC
go
*/
-------------------------------------------------------------------------------
drop proc dbo.RepSamplesProc
go

create proc dbo.RepSamplesProc
  @ExpedId int = null
, @PopulId int = null
, @SampleCode varchar(100) = null
, @RUSID varchar(30) = null
, @GPID varchar(30) = null
as
begin
set nocount on
delete RepSamples where spid = @@spid
delete RepSampleItems where spid = @@spid

-- �������
insert RepSamples (
  spid
, SampleId
, AnketId 
, SampleCode
, ExpedName
, PointRegion
, EtnoName
, PopulName
, SampleType
, Pol
, BirthYear 
, EyesColor 
, HairColor 
, ItemId_BV 
, ItemId_MV 
, ItemId_OK 
, ItemId_SK 
, ItemId_KD 
, ItemId_AD 
, ItemId_RD 
, ItemId_ND 
)
select
  @@spid
, s.SampleId
, a.AnketId
, s.SampleCode
, ex.Name
, pt.Region
, e.Name
, p.Name
, st.Item
, case a.Man when 0 then '�' else '�' end
, datepart(yyyy, a.BirthDate)
, EyesColor = (select max(aa.Value) from AnketAttrs aa join Lists li on aa.TypeId = li.ListId and li.Code = 'EYESCOLOR' where aa.AnketId = a.AnketId)
, HairColor = (select max(aa.Value) from AnketAttrs aa join Lists li on aa.TypeId = li.ListId and li.Code = 'HAIRCOLOR' where aa.AnketId = a.AnketId)
, ItemId_BV = (select max(si.SampleItemId) from SampleItems si join Lists li on si.SampleItemTypeId = li.ListId and li.Code = 'STOREVACBIG' where si.SampleId = s.SampleId)
, ItemId_MV = (select max(si.SampleItemId) from SampleItems si join Lists li on si.SampleItemTypeId = li.ListId and li.Code = 'STOREVACSMALL' where si.SampleId = s.SampleId)
, ItemId_OK = (select max(si.SampleItemId) from SampleItems si join Lists li on si.SampleItemTypeId = li.ListId and li.Code = 'STOREBLOODREST' where si.SampleId = s.SampleId)
, ItemId_SK = (select max(si.SampleItemId) from SampleItems si join Lists li on si.SampleItemTypeId = li.ListId and li.Code = 'STOREBLOODDRY' where si.SampleId = s.SampleId)
, ItemId_KD = (select max(si.DnkItemId) from DnkItems si join Lists li on si.DnkItemTypeId = li.ListId and li.Code = 'DNKCOLL' where si.SampleId = s.SampleId)
, ItemId_AD = (select max(si.DnkItemId) from DnkItems si join Lists li on si.DnkItemTypeId = li.ListId and li.Code = 'DNKARCH' where si.SampleId = s.SampleId)
, ItemId_RD = (select max(si.DnkItemId) from DnkItems si join Lists li on si.DnkItemTypeId = li.ListId and li.Code = 'DNKWORK' where si.SampleId = s.SampleId)
, ItemId_ND = (select max(si.DnkItemId) from DnkItems si join Lists li on si.DnkItemTypeId = li.ListId and li.Code = 'DNKNORM' where si.SampleId = s.SampleId)
  from Samples s 
        join Ankets a on s.AnketId = a.AnketId
        join Points pt on a.PointId = pt.PointId
        join Expeds ex on pt.ExpedId = ex.ExpedId
        join Populs p on a.PopulId = p.PopulId
        join Etnos e on p.EtnoId = e.EtnoId
        join Lists st on s.SampleTypeId = st.ListId and st.TypeCode = 'SAMPLETYPE'
 where (s.SampleCode like @SampleCode or isnull(@SampleCode,'') = '')
   and (a.RUSID like @RUSID or isnull(@RUSID,'') = '')
   and (a.GPID like @GPID or isnull(@GPID,'') = '')
   and (a.PopulId = @PopulId or isnull(@PopulId,0) = 0)
   and (pt.ExpedId = @ExpedId or isnull(@ExpedId,0) = 0)

update RepSamples set
  EtnoNameY = e.Name 
, HaploY = r.Haplogroup    
, MarkerY = r.Marker   
  from Results r 
		join Populs p on r.PopulId = p.PopulId 
		join Etnos e on p.EtnoId = e.EtnoId
 where r.SampleId = RepSamples.SampleId
   and r.ResultType = 1

update RepSamples set
  EtnoNameMt = e.Name 
, HaploMt = r.Haplogroup        
, MarkerMt = r.Marker      
, GWS1 = r.GWS1 
, GWS2 = r.GWS2 
  from Results r 
		join Populs p on r.PopulId = p.PopulId 
		join Etnos e on p.EtnoId = e.EtnoId
 where r.SampleId = RepSamples.SampleId
   and r.ResultType = 0

-- ��������
insert RepSampleItems (
  spid
, ItemKind
, ItemType
, ItemId
, Lab
, Fridge
, FridgeModule
, FridgeShelf
, BlockCode
, BlockItemCode
)
select distinct
  @@spid
, 1
, li.Code
, si.SampleItemId
, st.Lab
, st.Fridge
, st.FridgeModule
, st.FridgeShelf
, b.BlockCode
, bi.BlockItemCode
  from SampleItems si 
        join (select ItemId_BV as id from RepSamples where spid = @@spid
              union select ItemId_MV as id from RepSamples where spid = @@spid
              union select ItemId_OK as id from RepSamples where spid = @@spid
              union select ItemId_SK as id from RepSamples where spid = @@spid) t on si.SampleItemId = t.id
        join Lists li on si.SampleItemTypeId = li.ListId
        join BlockItems bi on si.BlockItemId = bi.BlockItemId
        join Blocks b on bi.BlockId = b.BlockId
        join Stores st on b.StoreId = st.StoreId

-- ���
insert RepSampleItems (
  spid
, ItemKind
, ItemType
, ItemId
, Lab
, Fridge
, FridgeModule
, FridgeShelf
, BlockCode
, BlockItemCode
, Concentration
, Quality
, Volume
)
select distinct
  @@spid
, 2
, li.Code
, di.DnkItemId
, st.Lab
, st.Fridge
, st.FridgeModule
, st.FridgeShelf
, b.BlockCode
, bi.BlockItemCode
, di.Concentration
, di.Quality
, di.Volume
  from DnkItems di 
        join (select ItemId_BV as id from RepSamples where spid = @@spid
              union select ItemId_MV as id from RepSamples where spid = @@spid
              union select ItemId_OK as id from RepSamples where spid = @@spid
              union select ItemId_SK as id from RepSamples where spid = @@spid) t on di.DnkItemId = t.id
        join Lists li on di.DnkItemTypeId = li.ListId
        join BlockItems bi on di.BlockItemId = bi.BlockItemId
        join Blocks b on bi.BlockId = b.BlockId
        join Stores st on b.StoreId = st.StoreId

-- �����
select 
  r.SampleCode
, r.ExpedName 
, r.PointRegion 
, r.EtnoName 
, r.PopulName 
, r.SampleType 
, r.Pol 
-- ����� �������� ������� ������������
, bv.Lab            bvLab
, bv.Fridge         bvFridge
, bv.FridgeModule   bvFridgeModule
, bv.FridgeShelf    bvFridgeShelf
, bv.BlockCode      bvBlockCode
, bv.BlockItemCode  bvBlockItemCode
-- ����� �������� ��������� ������������ �����
, mv.Lab            mvLab  
, mv.Fridge         mvFridge
, mv.FridgeModule   mvFridgeModule
, mv.FridgeShelf    mvFridgeShelf
, mv.BlockCode      mvBlockCode
, mv.BlockItemCode  mvBlockItemCode
-- ����� �������� �������� �����
, ok.Lab            okLab
, ok.Fridge         okFridge
, ok.FridgeModule   okFridgeModule
, ok.FridgeShelf    okFridgeShelf
, ok.BlockCode      okBlockCode
, ok.BlockItemCode  okBlockItemCode
-- ����� �������� ����� �������� �����
, sk.Lab            skLab 
, sk.Fridge         skFridge 
, sk.FridgeModule   skFridgeModule
, sk.FridgeShelf    skFridgeShelf
, sk.BlockCode      skBlockCode
, sk.BlockItemCode  skBlockItemCode
-- ����� �������� "�������������" ���
, kd.Lab            kdLab 
, kd.Fridge         kdFridge
, kd.FridgeModule   kdFridgeModule
, kd.FridgeShelf    kdFridgeShelf
, kd.BlockCode      kdBlockCode
, kd.BlockItemCode  kdBlockItemCode
-- ����� �������� "��������" ���
, ad.Lab            adLab
, ad.Fridge         adFridge
, ad.FridgeModule   adFridgeModule
, ad.FridgeShelf    adFridgeShelf
, ad.BlockCode      adBlockCode
, ad.BlockItemCode  adBlockItemCode
-- ����� �������� "�������" ���
, rd.Lab            rdLab
, rd.Fridge         rdFridge
, rd.FridgeModule   rdFridgeModule
, rd.FridgeShelf    rdFridgeShelf
, rd.BlockCode      rdBlockCode
, rd.BlockItemCode  rdBlockItemCode
-- ����� �������� "���������������" ���
, nd.Lab            ndLab 
, nd.Fridge         ndFridge
, nd.FridgeModule   ndFridgeModule
, nd.FridgeShelf    ndFridgeShelf
, nd.BlockCode      ndBlockCode
, nd.BlockItemCode  ndBlockItemCode
-- ������������ "������������� ���"
, kd.Concentration as kdConcentration
-- -- �������� "������������� ���"
, case when kd.Quality < 1 then '��' else '' end as kdQuality1 -- 260/280
, case when kd.Quality > 1 then '��' else '' end as kdQuality2 -- 260/230
-- -- �������� "������� ���"
, case when rd.Quality < 1 then '��' else '' end as rdQuality1 -- 260/280
, case when rd.Quality > 1 then '��' else '' end as rdQuality2 -- 260/230
-- ����� ������������� ���
, kd.Volume as kdVolume
-- ����� ������� ���
, rd.Volume as rdVolume
---------------------
, r.BirthYear 
, r.EyesColor 
, r.HairColor 
, r.EtnoNameY 
, r.HaploY 
, r.MarkerY 
, r.EtnoNameMt 
, r.HaploMt 
, r.MarkerMt 
, r.GWS1 
, r.GWS2 
, '???' as HasFullGenome
, '???' as HasFoto
  from RepSamples r 
          left join RepSampleItems bv on r.ItemId_BV = bv.ItemId and bv.ItemKind = 1
          left join RepSampleItems mv on r.ItemId_MV = mv.ItemId and mv.ItemKind = 1
          left join RepSampleItems ok on r.ItemId_OK = ok.ItemId and ok.ItemKind = 1
          left join RepSampleItems sk on r.ItemId_SK = sk.ItemId and sk.ItemKind = 1
          left join RepSampleItems kd on r.ItemId_KD = kd.ItemId and kd.ItemKind = 2
          left join RepSampleItems ad on r.ItemId_AD = ad.ItemId and ad.ItemKind = 2
          left join RepSampleItems rd on r.ItemId_RD = rd.ItemId and rd.ItemKind = 2
          left join RepSampleItems nd on r.ItemId_ND = nd.ItemId and nd.ItemKind = 2
 where r.spid = @@spid
 order by r.SampleCode

delete RepSamples where spid = @@spid
delete RepSampleItems where spid = @@spid
end
go

grant execute on dbo.RepSamplesProc to public
go

-- exec dbo.RepSamplesProc