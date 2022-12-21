using AutoMapper;
using Demo.Payments.Api.Infra.DbContext.Contracts;

namespace Demo.Payments.Api.Infra.DbContext.Factories
{
    public class AutoMapperDynamoDbFactoryModel<TEntity, TModel> : IDynamoDbFactoryModel<TEntity, TModel> where TEntity : class where TModel : class
    {
        private readonly IMapper _mapper;

        public AutoMapperDynamoDbFactoryModel(IMapper mapper) => _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public TModel ToModel(TEntity entity) => _mapper.Map<TModel>(entity);

        public TEntity ToEntity(TModel model) => _mapper.Map<TEntity>(model);

        public TResult ToEntity<TResult>(TModel model) where TResult : TEntity => _mapper.Map<TResult>(model);

        public IEnumerable<TEntity> ToEntities(IEnumerable<TModel> models) => _mapper.Map<IEnumerable<TEntity>>(models);

        public IEnumerable<TModel> ToModels(IEnumerable<TEntity> entities) => _mapper.Map<IEnumerable<TModel>>(entities);
    }
}