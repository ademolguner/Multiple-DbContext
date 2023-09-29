# Multiple-DbContext


yeni makale

Generic Repositpry patter ile mulştiple dbcontext  mongo and postgre and sql server






MultipleContext


IEntity, IEntity<TKey>
IBaseRepository<T,TKey>


MultipleContext.Data

MultipleContext.Data.Domain.Mongo    => IEntityMongo
MultipleContext.Data.Domain.Postgre  => IEntity
MultipleContext.Data.Domain.Sql

MultipleContext.Data.Mongo
MultipleContext.Data.Postgre
MultipleContext.Data.Sql


MultipleContext.Api.Product (Mongo)
MultipleContext.Api.Order   (Postgre)
MultipleContext.Api.Katalog (Sql Server)