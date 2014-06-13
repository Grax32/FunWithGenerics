
namespace FunWithGenerics.Generics
{
    public static class GenericSingleton<TInstanceType>
        where TInstanceType : new()
    {
        private static readonly TInstanceType instance = new TInstanceType();

        public static TInstanceType Instance
        {
            get { return instance; }
        }
    }
}
