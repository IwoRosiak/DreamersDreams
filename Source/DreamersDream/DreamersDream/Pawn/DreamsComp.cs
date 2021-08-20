using RimWorld;
using Verse;

namespace DreamersDream
{
    public class DreamsComp : ThingComp
    {
        public DreamsCompProperties Props
        {
            get
            {
                return (DreamsCompProperties)this.props;
            }
        }

        public Pawn pawn
        {
            get
            {
                return (Pawn)this.parent;
            }
        }

        public override void CompTickRare()
        {
            base.CompTickRare();

            if (TagsOddsTracker == null || OddsTracker == null)
            {
                TagsOddsTracker = new PawnDreamTagsOddsTracker(pawn);
                OddsTracker = new PawnDreamOddsTracker(pawn);
            }

            if (DD_Settings.isDreamingActive && pawn.CanGetDreamNow())
            {
                if (pawn.ShouldSleepwalkNow())
                {
                    ApplyDreamOfSpecificTag(DreamTagDefOf.Sleepwalk);
                }
                else
                {
                    ApplyRandomDream();
                }
            }

            if (DD_Settings.isDebugMode)
            {
                //DebugLogAllDreamsAndQualities();
            }
        }

        private void ApplyRandomDream()
        {
            var RandomTag = DreamSelector.ChooseRandomDreamTag(TagsOddsTracker.GetUpdatedTagsWithChances());

            ApplyDreamOfSpecificTag(RandomTag);
        }

        private void ApplyDreamOfSpecificTag(DreamTagDef randomTag)
        {
            var RandomDream = DreamSelector.ChooseRandomDream(OddsTracker.GetUpdatedDreamsWithChances(randomTag));

            if (randomTag.isSpecial)
            {
                Messages.Message(pawn.Name.ToStringShort + " is experiencing " + randomTag.defName.ToLower() + " dream!", pawn, MessageTypeDefOf.NeutralEvent, false);
            }

            TriggerDreamEffects(RandomDream);
        }

        private void TriggerDreamEffects(DreamDef dream)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(dream);

            foreach (var dreamTag in dream.tags)
            {
                if (dreamTag.defName == "Sleepwalk" && pawn.isSleepwalker())
                {
                    pawn.mindState.mentalStateHandler.TryStartMentalState(pawn.ChooseSleepwalkState(), null, true, false, null, false);
                }
            }
        }

        private void DebugLogAllDreamsAndQualities()
        {
            foreach (var tag in TagsOddsTracker.GetUpdatedTagsWithChances())
            {
                Log.Message(tag.Key.defName + " has " + tag.Value + "% chance upper threshold. And these are dreams for those tags: ");
                foreach (var dream in OddsTracker.GetUpdatedDreamsWithChances(tag.Key))
                {
                    Log.Message("     -" + dream.Key.defName + " has " + dream.Value + "% chance upper threshold.");
                }
            }
            Log.Message("CUTOFF");
        }

        private PawnDreamTagsOddsTracker TagsOddsTracker;

        private PawnDreamOddsTracker OddsTracker;
    }
}