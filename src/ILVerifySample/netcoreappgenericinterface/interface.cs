namespace netcoreappgenericinterface
{
    public interface IGeneric<TIN, TOUT>
    {
        TOUT M1(TIN par, TIN par2);
    }


    public class GenericImplementationInt32 : IGeneric<int, int>
    {
        public int M1(int par, int par2)
        {
            return 0;
        }
    }

}
