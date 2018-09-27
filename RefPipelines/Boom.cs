using System.Buffers;

namespace RefPipelines
{
    public static class Boom
    {
        //public static ReadOnlySequence<int> CantTouchThis() => new ReadOnlySequence<int>();
        public static void CantTouchThis() => System.IO.Pipelines.PipeOptions.Default.Pool.Rent(10000);
    }
}
