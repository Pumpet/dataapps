set nocount on
go
-------------------------------------------------------------------------------
if object_id('dbo.fkPopulsEtno') is not null alter table dbo.Populs drop constraint fkPopulsEtno
if object_id('dbo.fkPointsPopul') is not null alter table dbo.Points drop constraint fkPointsPopul
if object_id('dbo.fkPointsExped') is not null alter table dbo.Points drop constraint fkPointsExped
if object_id('dbo.fkListTypes') is not null alter table dbo.Lists drop constraint fkListTypes
if object_id('dbo.fkAnketsPoint') is not null alter table dbo.Ankets drop constraint fkAnketsPoint  
if object_id('dbo.fkAnketsPopul') is not null alter table dbo.Ankets drop constraint fkAnketsPopul
if object_id('dbo.fkAnketsLang') is not null alter table dbo.Ankets drop constraint fkAnketsLang
if object_id('dbo.fkAnketRelsPopul') is not null alter table dbo.AnketRels drop constraint fkAnketRelsPopul
if object_id('dbo.fkAnketRelsAnket') is not null alter table dbo.AnketRels drop constraint fkAnketRelsAnket
if object_id('dbo.fkAnketRelsRelType') is not null alter table dbo.AnketRels drop constraint fkAnketRelsRelType
if object_id('dbo.fkAnketRelsLang') is not null alter table dbo.AnketRels drop constraint fkAnketRelsLang
if object_id('dbo.fkAnketAttrsType') is not null alter table dbo.AnketAttrs drop constraint fkAnketAttrsType
if object_id('dbo.fkAnketAttrsAnket') is not null alter table dbo.AnketAttrs drop constraint fkAnketAttrsAnket
if object_id('dbo.fkAnketDocsAnket') is not null alter table dbo.AnketDocs drop constraint fkAnketDocsAnket
-- from stores.sql
if object_id('dbo.fkSamplesType') is not null alter table dbo.Samples drop constraint fkSamplesType
if object_id('dbo.fkSampleItemsType') is not null alter table dbo.SampleItems drop constraint fkSampleItemsType
if object_id('dbo.fkDnkItemsType') is not null alter table dbo.DnkItems drop constraint fkDnkItemsType
if object_id('dbo.fkDnkExtractMethod') is not null alter table dbo.DnkItems drop constraint fkDnkExtractMethod
if object_id('dbo.fkResultPopul') is not null alter table dbo.Results drop constraint fkResultPopul

-------------------------------------------------------------------------------
if object_id('dbo.Etnos') is not null
  drop table dbo.Etnos
go
create table dbo.Etnos (
  EtnoId int identity
, Name varchar(100) not null
, NameEn varchar(100) not null
, constraint pkEtnos primary key nonclustered (EtnoId)
, constraint akEtnos unique nonclustered (Name)
)
go
-------------------------------------------------------------------------------
if object_id('dbo.Populs') is not null
  drop table dbo.Populs
go
create table dbo.Populs (
  PopulId int identity
, EtnoId int not null
, Name varchar(100) not null
, NameEn varchar(100) not null
, Codes varchar(30) not null
, Comment varchar(500) not null default ''
, constraint pkPopuls primary key nonclustered (PopulId)
, constraint akPopuls unique nonclustered (Name)
, constraint fkPopulsEtno foreign key (EtnoId) references Etnos(EtnoId)
)
go
-- for stores.sql
if object_id('dbo.fkResultPopul') is null and object_id('dbo.Results') is not null
  alter table dbo.Results add constraint fkResultPopul foreign key (PopulId) references Populs(PopulId)

-------------------------------------------------------------------------------
if object_id('dbo.Expeds') is not null
  drop table dbo.Expeds
go
create table dbo.Expeds (
  ExpedId int identity
, Name varchar(100) not null
, DateStart smalldatetime not null 
, DateEnd smalldatetime not null 
, Region varchar(200) not null
, Head varchar(100) not null
, Info varchar(500) not null default '' 
, constraint pkExpeds primary key nonclustered (ExpedId)
, constraint akExpeds unique nonclustered (Name)
)
go
-------------------------------------------------------------------------------
if object_id('dbo.Points') is not null
  drop table dbo.Points
go
create table dbo.Points (
  PointId int identity
, PopulId int not null
, ExpedId int not null
, PointName varchar(100) not null
, Period varchar(100) not null 
, Region varchar(200) not null
, RegionEn varchar(200) not null
, LocName varchar(100) not null default ''
, LocX float not null
, LocY float not null
, Head varchar(100) not null
, Comment varchar(500) not null default ''
, constraint pkPoints primary key nonclustered (PointId)
, constraint akPoints unique nonclustered (ExpedId, PointName)
, constraint fkPointsPopul foreign key (PopulId) references Populs(PopulId)
, constraint fkPointsExped foreign key (ExpedId) references Expeds(ExpedId)
)
go
-------------------------------------------------------------------------------
if object_id('dbo.ListTypes') is not null
  drop table dbo.ListTypes
go
create table dbo.ListTypes (
  Code varchar(30) not null
, Name varchar(100) not null
, constraint pkListTypes primary key nonclustered (Code)
, constraint akListTypes unique nonclustered (Name)
)
go

insert ListTypes (Code, Name) values ('LANG', 'Языки')
insert ListTypes (Code, Name) values ('RELTYPE', 'Виды родства')
insert ListTypes (Code, Name) values ('ANKETATTR', 'Атрибуты анкет')
insert ListTypes (Code, Name) values ('SAMPLETYPE', 'Тип образца')
insert ListTypes (Code, Name) values ('SAMPLEITEMTYPE', 'Тип хранения образца')
insert ListTypes (Code, Name) values ('DNKITEMTYPE', 'Тип хранения ДНК')
insert ListTypes (Code, Name) values ('DNKEXTRACTMETHOD', 'Метод выделения ДНК')

-------------------------------------------------------------------------------
if object_id('dbo.Lists') is not null
  drop table dbo.Lists
go
create table dbo.Lists (
  ListId int identity
, TypeCode varchar(30) not null
, Item varchar(100) not null
, Code varchar(30) not null default ''
, constraint pkLists primary key nonclustered (ListId)
, constraint akLists unique nonclustered (TypeCode, Item)
, constraint fkListTypes foreign key (TypeCode) references ListTypes(Code)
)
go
-- for stores.sql
if object_id('dbo.fkSamplesType') is null and object_id('dbo.Samples') is not null
  alter table dbo.Samples add constraint fkSamplesType foreign key (SampleTypeId) references Lists(ListId)
if object_id('dbo.fkSampleItemsType') is null and object_id('dbo.SampleItems') is not null
  alter table dbo.SampleItems add constraint fkSampleItemsType foreign key (SampleItemTypeId) references Lists(ListId)
if object_id('dbo.fkDnkItemsType') is null and object_id('dbo.DnkItems') is not null
  alter table dbo.DnkItems add constraint fkDnkItemsType foreign key (DnkItemTypeId) references Lists(ListId)
if object_id('dbo.fkDnkExtractMethod') is null and object_id('dbo.DnkItems') is not null 
  alter table dbo.DnkItems add constraint fkDnkExtractMethod foreign key (ExtractMethodId) references Lists(ListId)
-------------------------------------------------------------------------------
if object_id('dbo.Ankets') is not null
  drop table dbo.Ankets
go
create table dbo.Ankets (
  AnketId int identity
, RUSID varchar(30) not null
, GPID varchar(30) not null
, Fio varchar(100) not null
, LivePlace varchar(200) not null -- Places.Name или произвольный
, LiveAddress varchar(100) not null
, PointId int not null
, PopulId int null
, Origin varchar(100) not null default '' 
, InDate date not null default '19000101'
, InPlace varchar(200) not null -- Places.Name или произвольный
, BirthDate date not null default '19000101'
, BirthPlace varchar(200) not null -- Places.Name или произвольный
, Man tinyint not null default 0 
, LinguaId int not null
, OtherAncestors varchar(20) not null default '00100100000' -- варианты (нет, да, мать, отец и т.д.)
, Comment varchar(500) not null default ''
, constraint pkAnkets primary key nonclustered (AnketId)
, constraint akAnkets unique nonclustered (RUSID)
, constraint ak2Ankets unique nonclustered (GPID)
, constraint fkAnketsPoint foreign key (PointId) references Points(PointId)
, constraint fkAnketsPopul foreign key (PopulId) references Populs(PopulId)
, constraint fkAnketsLang foreign key (LinguaId) references Lists(ListId)
)
go

-------------------------------------------------------------------------------
if object_id('dbo.AnketRels') is not null
  drop table dbo.AnketRels
go
create table dbo.AnketRels (
  AnketRelId int identity
, AnketId int not null
, RelTypeId int not null 
, PopulId int null
, Origin varchar(100) not null default '' 
, BirthPlace varchar(200) not null -- Places.Name или произвольный
, LinguaId int not null 
, constraint pkAnketRels primary key nonclustered (AnketRelId)
, constraint fkAnketRelsAnket foreign key (AnketId) references Ankets(AnketId) on delete cascade
, constraint fkAnketRelsPopul foreign key (PopulId) references Populs(PopulId)
, constraint fkAnketRelsRelType foreign key (RelTypeId) references Lists(ListId)
, constraint fkAnketRelsLang foreign key (LinguaId) references Lists(ListId)
)
go
-------------------------------------------------------------------------------
if object_id('dbo.AnketAttrs') is not null
  drop table dbo.AnketAttrs
go
create table dbo.AnketAttrs (
  AttrId int identity
, AnketId int not null
, TypeId int not null
, Value varchar(100) not null
, constraint pkAttrs primary key nonclustered (AttrId)
, constraint fkAnketAttrsType foreign key (TypeId) references Lists(ListId)
, constraint fkAnketAttrsAnket foreign key (AnketId) references Ankets(AnketId) on delete cascade
)
go

-------------------------------------------------------------------------------
if object_id('dbo.AnketDocs') is not null
  drop table dbo.AnketDocs
go
create table dbo.AnketDocs (
  AnketDocId int identity
, AnketId int not null
, Link varchar(500) not null 
, DocType varchar(100) not null -- на форме - Combo имеющихся с возможностью ввода нового
, Info varchar(200) null
, constraint pkAnketDocs primary key nonclustered (AnketDocId)
, constraint fkAnketDocsAnket foreign key (AnketId) references Ankets(AnketId) on delete cascade
)
go
-------------------------------------------------------------------------------
if object_id('dbo.Places') is not null
  drop table dbo.Places
go
create table dbo.Places (
  PlaceId int identity
, Name varchar(240) not null
, Region varchar(60) null
, Raion varchar(60) null
, City varchar(60) null
, Punkt varchar(60) null
, KladrCode varchar(20) null
, constraint pkPlaces primary key nonclustered (PlaceId)
)
go
create nonclustered index idxPlaces on Places(KladrCode);
go
-------------------------------------------------------------------------------