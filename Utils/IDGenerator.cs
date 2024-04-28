namespace UBB_SE_2024_Team_42.Utils
{
    internal class IDGenerator
    {
        private static readonly Random Generator = new ();
        public static long RandomLong()
        {
            int prefix = Generator.Next();
            int suffix = Generator.Next();
            return (((long)prefix) << sizeof(int)) + suffix;
        }
    }
}
