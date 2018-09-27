using System.Buffers;

namespace RefPipelines
{
    public static class Hammer
    {
        // Touching System.Buffers via Pipelines (expecting 4.0.2 DLL) - goes boom
        public static void CantTouchThis() => System.IO.Pipelines.PipeOptions.Default.Pool.Rent(10000);
    }
}
