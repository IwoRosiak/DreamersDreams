using RimWorld;
using RimWorld.Planet;
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

        public float getPawnAggressiveness
        {
            get
            {
                return CalculateAggressiveness();
            }
        }

        public override void CompTickRare()
        {
            base.CompTickRare();
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
            if (dream.quality.isSpecial)
            {
                Messages.Message(pawn.Name.ToStringShort + " is experiencing " + dream.quality.defName.ToLower() + " dreams!", pawn, MessageTypeDefOf.NeutralEvent);
            }

            pawn.needs.mood.thoughts.memories.TryGainMemory(dream, null);

            if (dream.isSleepwalk)
            {
                switch (this.ChooseSleepwalkingType())
                {
                    case SleepwalkingType.calm:
                        pawn.mindState.mentalStateHandler.TryStartMentalState(DD_MentalStateDefOf.Sleepwalk, null, true, false, null, false);
                        Messages.Message(pawn.Name.ToStringShort + " stood up from " + pawn.gender.GetPossessive() + " bed and started to wander around...", pawn, MessageTypeDefOf.NeutralEvent);
                        break;

                    case SleepwalkingType.food:
                        break;

                    case SleepwalkingType.drugs:
                        break;

                    case SleepwalkingType.rage:
                        pawn.mindState.mentalStateHandler.TryStartMentalState(DD_MentalStateDefOf.SleepwalkBerserk, null, true, false, null, false);
                        Messages.Message(pawn.Name.ToStringShort + " stood up from " + pawn.gender.GetPossessive() + " bed and started to attack others in murderous rage!", pawn, MessageTypeDefOf.NeutralEvent);
                        break;

                    case SleepwalkingType.tantrum:
                        break;

                    default:
                        break;
                }
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

        private float CalculateAggressiveness()
        {
            float aggressiveness = 0;

            foreach (var trait in pawn.story.traits.allTraits)
            {
                switch (trait.def.defName)
                {
                    case "Bloodlust":
                        aggressiveness++;
                        break;

                    case "Psychopath":
                        aggressiveness++;
                        break;

                    case "Cannibal":
                        aggressiveness += 0.5f;
                        break;

                    default:
                        break;
                }
            }

            return aggressiveness;
        }

        private bool IsPawnCapableOfDreaming()
        {
            return pawn.needs?.rest != null && !pawn.Dead && (pawn.Spawned || pawn.IsCaravanMember());
        }

        private PawnDreamQualityOddsTracker QualityOddsTracker;

        private PawnDreamOddsTracker OddsTracker;

        private float aggressiveness = 0;

        private float foodAttraction = 0;

        private float drugAttraction = 0;
    }
}