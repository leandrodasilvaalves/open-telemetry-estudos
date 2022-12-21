namespace Demo.Payments.Api.Infra.DbContext.Contracts
{
    public interface IDynamoDbFactoryModel<TEntity, TModel> where TEntity : class where TModel : class
    {
        TModel ToModel(TEntity entity);
        TEntity ToEntity(TModel model);

        IEnumerable<TEntity> ToEntities(IEnumerable<TModel> models);
        IEnumerable<TModel> ToModels(IEnumerable<TEntity> entities);
    }
}