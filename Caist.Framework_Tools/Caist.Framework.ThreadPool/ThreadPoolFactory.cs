namespace Caist.Framework.ThreadPool
{
    public class ThreadPoolFactory
    {
        public static IThreadPool Create(StartInfo info, string name)
        {
            return new SingleThreadPool(info, name);
        }
    }
}
