using RimWorld;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DD_ThoughtDef : ThoughtDef
    {
        public float chance = 1;

        public List<DD_MentalStateDef> triggers;

    }

}
