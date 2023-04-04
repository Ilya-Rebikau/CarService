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
            var destination = _mapper.Map<TSource, TDestination>(source);
            return Task.FromResult(destination);
        }
        public virtual Task<TSource> ConvertDestinationToSource(TDestination destination)
        {
            var source = _mapper.Map<TDestination, TSource>(destination);
            return Task.FromResult(source);
        }
        public async Task<IEnumerable<TDestination>> ConvertSourceModelRangeToDestinationModelRange(IEnumerable<TSource> sourceModels)
        {
            var destinations = new List<TDestination>();
            foreach (var source in sourceModels)
            {
                destinations.Add(await ConvertSourceToDestination(source));
            }

            return destinations;
        }
        public async Task<IEnumerable<TSource>> ConvertDestinationModelRangeToSourceModelRange(IEnumerable<TDestination> destinationModels)
        {
            var sources = new List<TSource>();
            foreach (var destination in destinationModels)
            {
                sources.Add(await ConvertDestinationToSource(destination));
            }

            return sources;
        }
    }
}