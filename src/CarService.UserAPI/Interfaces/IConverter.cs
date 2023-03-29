namespace CarService.UserAPI.Interfaces
{
    public interface IConverter<TSource, TDestination>
    {
        Task<TDestination> ConvertSourceToDestination(TSource source);
        Task<TSource> ConvertDestinationToSource(TDestination destination);
        Task<IEnumerable<TDestination>> ConvertSourceModelRangeToDestinationModelRange(IEnumerable<TSource> sourceModels);
        Task<IEnumerable<TSource>> ConvertDestinationModelRangeToSourceModelRange(IEnumerable<TDestination> destinationModels);
    }
}
