using Mapster;

namespace Core.Utilities.Mapper
{
    public static class MapsterTool
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }
        public static TDestination Map<TSource, TDestination>(TSource source, bool PreserveReference = true)
        {
            var forked = TypeAdapterConfig.GlobalSettings.Fork(config => config.Default.PreserveReference(PreserveReference)); //Child nesneler var ise bu ayarı kullanmak gerekiyor.yoksa program kendini kapatır.
            return source.Adapt<TDestination>(forked);
        }
    }
}
