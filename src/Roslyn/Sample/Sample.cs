namespace Sample
{
    internal class Sample
    {
        public void Startup(string str)
        {
            Int @int = new Int();
            @int.AddExt()
                .AddExt("prefix")
                .AddExt();

            //@int.AddExt()
            //    .AddExt()
            //    .AddExt();

            @int.AddExt("prefix").AddExt().AddExt();

            var r = @int.AddExt("prefix");
            r.AddExt().AddExt();
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

        public static Int AddExt(this Int s)
        {
            return s;
        }

        public static string AddExt(this string s)
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
