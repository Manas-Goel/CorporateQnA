using AutoMapper;

namespace Services.Helpers
{
    public static class AutoMapperExtension
    {
        public static IMapper Mapper;

        public static TDest MapTo<TDest>(this object src)
        {
            return Mapper.Map<TDest>(src);
        }

    }
}