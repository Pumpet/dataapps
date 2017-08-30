set nocount on
go
-------------------------------------------------------------------------------
if object_id('dbo.fkBlocksStore') is not null alter table dbo.Blocks drop constraint fkBlocksStore
if object_id('dbo.fkSamplesAnket') is not null alter table dbo.Samples drop constraint fkSamplesAnket
if object_id('dbo.fkSamplesType') is not null alter table dbo.Samples drop constraint fkSamplesType
if object_id('dbo.fkBlockItemsBlock') is not null alter table dbo.BlockItems drop constraint fkBlockItemsBlock
if object_id('dbo.fkSampleItemsSample') is not null alter table dbo.SampleItems drop constraint fkSampleItemsSample
if object_id('dbo.fkSampleItemsType') is not null alter table dbo.SampleItems drop constraint fkSampleItemsType
if object_id('dbo.fkSampleItemsBlockItem') is not null alter table dbo.SampleItems drop constraint fkSampleItemsBlockItem
if object_id('dbo.fkDnkItemsSample') is not null alter table dbo.DnkItems drop constraint fkDnkItemsSample
if object_id('dbo.fkDnkItemsType') is not null alter table dbo.DnkItems drop constraint fkDnkItemsType
if object_id('dbo.fkDnkItemsBlockItem') is not null alter table dbo.DnkItems drop constraint fkDnkItemsBlockItem
if object_id('dbo.fkDnkExtractMethod') is not null alter table dbo.DnkItems drop constraint fkDnkExtractMethod
if object_id('dbo.fkResultSample') is not null alter table dbo.Results drop constraint fkResultSample
if object_id('dbo.fkResultPopul') is not null alter table dbo.Results drop constraint fkResultPopul

-------------------------------------------------------------------------------
-- места хранения
---------------------------------------
-- лаборатория - [холодильник - [отсек] - [полка]] - [контейнер]
-- все сочетание должно быть уникально
-- сочетание лаборатория - контейнер должно быть уникально, кроме Container = ''

if object_id('dbo.Stores') is not null
  drop table dbo.Stores
go
create table dbo.Stores (
  StoreId int identity
, Lab varchar(100) not null
, Fridge varchar(100) not null default ''
, FridgeModule varchar(100) not null default ''
, FridgeShelf varchar(100) not null default ''
, Container varchar(100) not null default ''
, constraint pkStores primary key nonclustered (StoreId)
, constraint akStores unique nonclustered (Lab, Fridge, FridgeModule, FridgeShelf, Container)
)
go
create nonclustered index idxStores on Stores(Container)
go
-------------------------------------------------------------------------------
-- объекты хранения (штативы)
---------------------------------------
-- если образец вдруг может храниться не в штативе (например как-то индивидуально), то нужен другой тип объекта и интерфейсное решение по его вводу
-- принцип должен быть тот же - объект хранения связан с местом хранения, а пробирка (его хранимая часть - SampleItems или DnkItems) - с позицией хранения в объекте хранения
-- при удалении нужно проверять наличие позиций хранения, связанных с пробирками и, если есть, - принимать решение удалять объект хранения или нет

if object_id('dbo.Blocks') is not null
  drop table dbo.Blocks
go
create table dbo.Blocks (
  BlockId int identity
, BlockType varchar(10) not null -- from Constant
, BlockCode varchar(100) not null 
, StoreId int null
, DimX int not null default 0
, DimY int not null default 0
, constraint pkBlocks primary key nonclustered (BlockId)
, constraint akBlocks unique nonclustered (BlockCode)
, constraint fkBlocksStore foreign key (StoreId) references Stores(StoreId)
)
go
-------------------------------------------------------------------------------
-- позиции хранения (в объекте хранения - например ячейки штатива)
---------------------------------------
if object_id('dbo.BlockItems') is not null
  drop table dbo.BlockItems
go
create table dbo.BlockItems (
  BlockItemId int identity
, BlockId int not null
, BlockItemCode varchar(100) not null -- например номер ячейки штатива. сформированный по определенному принципу, например "А1"
, constraint pkBlockItems primary key nonclustered (BlockItemId)
, constraint akBlockItems unique nonclustered (BlockId, BlockItemCode)
, constraint fkBlockItemsBlock foreign key (BlockId) references Blocks(BlockId) on delete cascade 
)
go
-------------------------------------------------------------------------------
-- образцы (взятые по анкете)
---------------------------------------
if object_id('dbo.Samples') is not null
  drop table dbo.Samples
go
create table dbo.Samples (
  SampleId int identity
, AnketId int not null
, SampleTypeId int not null
, SampleCode varchar(100) not null 
, constraint pkSamples primary key nonclustered (SampleId)
, constraint akSamples unique nonclustered (SampleCode)
, constraint fkSamplesAnket foreign key (AnketId) references Ankets(AnketId) 
, constraint fkSamplesType foreign key (SampleTypeId) references Lists(ListId)
)
go
-------------------------------------------------------------------------------
-- пробирки образцов
---------------------------------------
if object_id('dbo.SampleItems') is not null
  drop table dbo.SampleItems
go
create table dbo.SampleItems (
  SampleItemId int identity
, SampleId int not null
, SampleItemTypeId int not null
, BlockItemId int null
, Comment varchar(500) not null default ''
, constraint pkSampleItems primary key nonclustered (SampleItemId)
, constraint fkSampleItemsSample foreign key (SampleId) references Samples(SampleId) on delete cascade 
, constraint fkSampleItemsType foreign key (SampleItemTypeId) references Lists(ListId)
, constraint fkSampleItemsBlockItem foreign key (BlockItemId) references BlockItems(BlockItemId)
)
go
-------------------------------------------------------------------------------
-- пробирки ДНК
---------------------------------------
if object_id('dbo.DnkItems') is not null
  drop table dbo.DnkItems
go
create table dbo.DnkItems (
  DnkItemId int identity
, SampleId int not null
, DnkItemTypeId int not null
, BlockItemId int null
, Concentration float not null
, Quality float not null
, Volume float not null
, ExtractMethodId int not null
, Comment varchar(500) not null default ''
, constraint pkDnkItem primary key nonclustered (DnkItemId)
, constraint fkDnkItemsSample foreign key (SampleId) references Samples(SampleId) on delete cascade 
, constraint fkDnkItemsType foreign key (DnkItemTypeId) references Lists(ListId)
, constraint fkDnkItemsBlockItem foreign key (BlockItemId) references BlockItems(BlockItemId)
, constraint fkDnkExtractMethod foreign key (ExtractMethodId) references Lists(ListId)
)
go

-------------------------------------------------------------------------------
-- результат
---------------------------------------
if object_id('dbo.Results') is not null
  drop table dbo.Results
go
create table dbo.Results (
  ResultId int identity
, SampleId int not null
, ResultType tinyint not null default 0 -- 0 = mt, 1 = Y
, PopulId int not null
, Prediction varchar(100) not null
, Probability float not null
, Haplogroup varchar(100) not null
, Marker varchar(100) not null
, Comment varchar(500) not null default ''
-- for mt
, GWS1 float null
, GWS2 float null
-- for Y
, B_DYS389I float null
, B_DYS389II float null
, B_DYS390 float null
, B_DYS456 float null
, G_DYS19 float null
, G_DYS385 float null
, G_DYS385_2 float null
, G_DYS458 float null
, R_DYS437 float null
, R_DYS438 float null
, R_DYS448 float null
, R_Y_GATA_H4 float null
, Y_DYS391 float null
, Y_DYS392 float null
, Y_DYS393 float null
, Y_DYS439 float null
, Y_DYS635 float null
, DYS449 float null
, DYS460 float null
, DYS481 float null
, DYS518 float null
, DYS533 float null
, DYS570 float null
, DYS576 float null
, DYS627 float null
, DYF387S1 float null
, DYS447 float null
, constraint pkResult primary key nonclustered (ResultId)
, constraint fkResultSample foreign key (SampleId) references Samples(SampleId)
, constraint fkResultPopul foreign key (PopulId) references Populs(PopulId)
)
go
-------------------------------------------------------------------------------
-- постоянные типы, константы
---------------------------------------
if object_id('dbo.Consts') is not null
  drop table dbo.Consts
go
create table dbo.Consts (
  Code varchar(10) not null 
, Name varchar(100) not null 
, constraint pkConsts primary key nonclustered (Code)
)
insert Consts (Code, Name) values ('BLOCK', 'Штатив') -- для Blocks

-------------------------------------------------------------------------------