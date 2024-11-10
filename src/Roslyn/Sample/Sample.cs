namespace Sample
{
    internal class Sample
    {
        public void Startup(string str)
        {
            Int @int = new Int();
            @int.AddExt("prefix");
        }
    }

    public static class Extg
    {
        public static string AddExt<T>(this IInt<T> s, string str2)
        {
            return s + str2;
        }

        public static string AddExt<T>(this IInt<T> s)
        {
            return ".ext";
        }
    }

    public interface IInt<T>
    {

    }

    public class Int : IInt<int>
    {

    }
}
