using RimWorld;
using Verse;

namespace DreamersDream
{
    public class Dream : Thought_Memory
    {
        public override string LabelCap
        {
            get
            {
                return def.label;
            }
        }

        protected override float BaseMoodOffset
        {
            get
            {
                return def.baseMoodEffect;
            }
        }

        public override int DurationTicks
        {
            get
            {
                return this.def.DurationTicks;
            }
        }

        public override string Description
        {
            get
            {
                return def.description;
            }
        }

        public override int CurStageIndex
        {
            get
            {
                Log.Warning("Something is accessing stage index!");
                return 0;
            }
        }

        public override bool VisibleInNeedsTab
        {
            get
            {
                return base.VisibleInNeedsTab;
            }
        }

        public override bool GroupsWith(Thought other)
        {
            Dream dream = other as Dream;
            return dream != null && base.GroupsWith(other) && this.LabelCap == dream.LabelCap;
        }

        public override float MoodOffset()
        {
            if (ThoughtUtility.ThoughtNullified(this.pawn, this.def))
            {
                return 0f;
            }
            float num = this.BaseMoodOffset;
            if (this.def.effectMultiplyingStat != null)
            {
                num *= this.pawn.GetStatValue(this.def.effectMultiplyingStat, true);
            }
            if (this.def.Worker != null)
            {
                num *= this.def.Worker.MoodMultiplier(this.pawn);
            }
            return BaseMoodOffset;
        }

        public new DreamDef def;

        public int moodOffset;
    }
}