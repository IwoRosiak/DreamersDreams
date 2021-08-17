using RimWorld;
using Verse;

namespace DreamersDream
{
    internal static class DreamsPawnPersonality
    {
        public static bool isSleepwalker(this Pawn pawn)
        {
            return pawn.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker);
        }

        public static float CalculateAggressiveness(this Pawn pawn)
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

        public static DD_MentalStateDef ChooseSleepwalkState(this Pawn pawn)
        {
            switch (pawn.ChooseSleepwalkingType())
            {
                case SleepwalkingType.calm:
                    Messages.Message(pawn.Name.ToStringShort + " stood up from " + pawn.gender.GetPossessive() + " bed and started to wander around...", pawn, MessageTypeDefOf.NeutralEvent);
                    return DD_MentalStateDefOf.Sleepwalk;

                case SleepwalkingType.rage:
                    Messages.Message(pawn.Name.ToStringShort + " stood up from " + pawn.gender.GetPossessive() + " bed and started to attack others in murderous rage!", pawn, MessageTypeDefOf.NeutralEvent);
                    return DD_MentalStateDefOf.SleepwalkBerserk;

                case SleepwalkingType.food:

                case SleepwalkingType.drugs:

                case SleepwalkingType.tantrum:

                default:
                    Log.Warning("Dreamer's Dreams: Failed to choose sleepwalking state.");
                    Messages.Message(pawn.Name.ToStringShort + " stood up from " + pawn.gender.GetPossessive() + " bed and started to wander around...", pawn, MessageTypeDefOf.NeutralEvent);
                    return DD_MentalStateDefOf.Sleepwalk;
            }
        }

        private static SleepwalkingType ChooseSleepwalkingType(this Pawn pawn)
        {
            float totalChance = 1;

            totalChance += pawn.CalculateAggressiveness();

            float roll = Rand.Range(0, totalChance);

            if (roll <= 1)
            {
                return SleepwalkingType.calm;
            }
            else if (roll <= 1 + pawn.CalculateAggressiveness())
            {
                return SleepwalkingType.rage;
            }
            else
            {
                Log.Warning("Dreamer's Dreams: Failed to choose sleepwalking type.");
                return SleepwalkingType.calm;
            }
        }
    }
}