using RimWorld.Planet;
using Verse;

namespace DreamersDream
{
    public class DreamsComp : ThingComp
    {
        private PawnDreamQualityOddsTracker QualityOddsTracker;

        private PawnDreamOddsTracker OddsTracker;

        public DreamsComp()
        {
        }

        public Pawn pawn
        {
            get
            {
                return (Pawn)this.parent;
            }
        }

        public DreamsCompProperties Props
        {
            get
            {
                return (DreamsCompProperties)this.props;
            }
        }

        public override void CompTickRare()
        {
            base.CompTick();
            if (QualityOddsTracker == null || OddsTracker == null)
            {
                QualityOddsTracker = new PawnDreamQualityOddsTracker(pawn);
                OddsTracker = new PawnDreamOddsTracker(pawn);
            }
            if (DD_Settings.isDreamingActive)
            {
                TryApplyDream();
            }
        }

        private void TryApplyDream()
        {
            if (CanGetDreamNow())
            {
                var RandomQuality = DreamRandomCalc.ChooseRandomDreamQuality(QualityOddsTracker.GetUpdatedQualitiesWithChances());
                var RandomDream = DreamRandomCalc.ChooseRandomDream(OddsTracker.GetUpdatedDreamsWithChances(RandomQuality));
                TriggerDreamEffects(RandomDream);
            }
        }

        private void DebugLogAllDreamsAndQualities()
        {
            foreach (var quality in QualityOddsTracker.GetUpdatedQualitiesWithChances())
            {
                Log.Message(quality.Key.defName + " has " + quality.Value + "% chance upper threshold. And these are dreams for this quality: ");
                foreach (var dream in OddsTracker.GetUpdatedDreamsWithChances(quality.Key))
                {
                    Log.Message("     -" + dream.Key.defName + " has " + dream.Value + "% chance upper threshold.");
                }
            }
            Log.Message("CUTOFF");
        }

        private bool CanGetDreamNow()
        {
            if (IsPawnCapableOfDreaming() && !IsAwake() && IsPawnRestedEnough() && !HasDreamAlready())
            {
                return true;
            }
            return false;
        }

        private bool IsPawnRestedEnough()
        {
            return pawn.needs.rest.CurCategory == RimWorld.RestCategory.Rested;
        }

        private bool IsAwake()
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return false;
            }
            else if (pawn.needs.rest.GUIChangeArrow == -1)
            {
                return true;
            }
            return false;
        }

        private void TriggerDreamEffects(DreamDef dream)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(dream, null);

            if (dream.isSleepwalk)
            {
                //TODO CHOOSE SLEEPWALK RANDOM AND DISPLAY SMALL MESSAGE
                pawn.mindState.mentalStateHandler.TryStartMentalState(DD_MentalStateDefOf.Sleepwalk, null, true, false, null, false);
            }
            /*
            if (dream.inspiration != null)
            {
                pawn.mindState.inspirationHandler.TryStartInspiration(dream.inspiration);
            } */
        }

        private bool HasDreamAlready()
        {
            foreach (DreamDef dream in DreamTracker.DreamDefs)
            {
                if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(dream) != null)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsPawnCapableOfDreaming()
        {
            return pawn.needs?.rest != null && !pawn.Dead && (pawn.Spawned || pawn.IsCaravanMember());
        }
    }
}