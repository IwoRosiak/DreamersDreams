﻿using RimWorld;
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
            if (TagsOddsTracker == null || OddsTracker == null)
            {
                TagsOddsTracker = new PawnDreamTagsOddsTracker(pawn);
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
                var RandomTag = DreamSelector.ChooseRandomDreamTag(TagsOddsTracker.GetUpdatedTagsWithChances());

                var RandomDream = DreamSelector.ChooseRandomDream(OddsTracker.GetUpdatedDreamsWithChances(RandomTag));

                if (RandomTag.isSpecial)
                {
                    Messages.Message(pawn.Name.ToStringShort + " is experiencing " + RandomTag.defName.ToLower() + " dream!", pawn, MessageTypeDefOf.NeutralEvent);
                }

                TriggerDreamEffects(RandomDream);
            }
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

            foreach (var dreamTag in dream.tags)
            {
                if (dreamTag.defName == "Sleepwalk")
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
            }
        }

        private bool HasDreamAlready()
        {
            foreach (DreamDef dream in DreamTracker.GetAllDreams)
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

        private bool IsPawnCapableOfDreaming()
        {
            return pawn.needs?.rest != null && !pawn.Dead && (pawn.Spawned || pawn.IsCaravanMember());
        }

        private PawnDreamTagsOddsTracker TagsOddsTracker;

        private PawnDreamOddsTracker OddsTracker;
    }
}