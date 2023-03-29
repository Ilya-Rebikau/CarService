using AutoMapper;
using CarService.UserAPI.Interfaces;

namespace CarService.UserAPI.Automapper
{
    internal class ModelsConverter<TSource, TDestination> : IConverter<TSource, TDestination>
    {
        private readonly IMapper _mapper;
        public ModelsConverter(IMapper mapper)
        {
            _mapper = mapper;
        }
        public virtual Task<TDestination> ConvertSourceToDestination(TSource source)
        {
            var dto = _mapper.Map<TSource, TDestination>(source);
            return Task.FromResult(dto);
        }
        public virtual Task<TSource> ConvertDestinationToSource(TDestination destination)
        {
            var model = _mapper.Map<TDestination, TSource>(destination);
            return Task.FromResult(model);
        }
        public async Task<IEnumerable<TDestination>> ConvertSourceModelRangeToDestinationModelRange(IEnumerable<TSource> sourceModels)
        {
            var dtos = new List<TDestination>();
            foreach (var model in sourceModels)
            {
                dtos.Add(await ConvertSourceToDestination(model));
            }

            return dtos;
        }
        public async Task<IEnumerable<TSource>> ConvertDestinationModelRangeToSourceModelRange(IEnumerable<TDestination> destinationModels)
        {
            var models = new List<TSource>();
            foreach (var dto in destinationModels)
            {
                models.Add(await ConvertDestinationToSource(dto));
            }

            return models;
        }
    }
}