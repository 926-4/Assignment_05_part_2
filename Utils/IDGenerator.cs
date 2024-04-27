namespace UBB_SE_2024_Team_42.Utils
{
    internal class IDGenerator
    {
        static readonly Random generator = new();
        public static long RandomLong()
        {
            int prefix = generator.Next();
            int suffix = generator.Next();
            return (((long)prefix) << sizeof(int)) + suffix;
        }
    }
}
