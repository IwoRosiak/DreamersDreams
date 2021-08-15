using RimWorld;

namespace DreamersDream
{
    [DefOf]
    public static class DreamTagDefOf
    {
        static DreamTagDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DreamTagDefOf));
        }

        public static DreamTagDef Sleepwalk;
    }
}