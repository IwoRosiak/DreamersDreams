using RimWorld;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public float chance = 1;

        public List<DD_MentalStateDef> triggers;

        public InspirationDef inspiration;

        public enum Sensitivities
        {
            ill,
            healthy,
            injured,
            goodTemp,
            hot,
            cold,
            hungry,
            malnourished
        }

        public List<Sensitivities> sensitivities;
    }
}