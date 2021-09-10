using RimWorld;
using Verse;

namespace DreamersDream
{
    internal static class DreamRequirementsChecker
    {
        internal static bool CheckRequirementForPawn(this Requirements requirement, Pawn pawn)
        {
            switch (requirement)
            {
                case Requirements.sleepwalker:
                    if (!pawn.IsSleepwalker())
                    {
                        return false;
                    }
                    break;

                case Requirements.happy:
                    if (pawn.needs.mood.CurLevel < 0.9f)
                    {
                        return false;
                    }
                    break;

                case Requirements.stressed:
                    if (pawn.needs.mood.CurLevel > pawn.mindState.mentalBreaker.BreakThresholdMinor)
                    {
                        return false;
                    }
                    break;

                case Requirements.aboutToBreak:
                    if (pawn.needs.mood.CurLevel > pawn.mindState.mentalBreaker.BreakThresholdExtreme)
                    {
                        return false;
                    }
                    break;

                case Requirements.wounded:
                    if (!pawn.health.hediffSet.HasNaturallyHealingInjury())
                    {
                        return false;
                    }
                    break;

                case Requirements.healthy:
                    if (pawn.health.hediffSet.HasNaturallyHealingInjury())
                    {
                        return false;
                    }
                    break;

                case Requirements.ill:
                    if (!pawn.health.hediffSet.AnyHediffMakesSickThought)
                    {
                        return false;
                    }
                    break;

                case Requirements.starving:
                    if (!pawn.needs.food.Starving)
                    {
                        return false;
                    }
                    break;

                case Requirements.prisoner:
                    if (!pawn.IsPrisoner)
                    {
                        return false;
                    }
                    break;

                case Requirements.killer:
                    if (pawn.records.GetValue(RecordDefOf.KillsHumanlikes) <= 0)
                    {
                        return false;
                    }
                    break;

                case Requirements.guilty:
                    if (!pawn.guilt.IsGuilty)
                    {
                        return false;
                    }
                    break;

                case Requirements.contentOrMore:
                    break;

                case Requirements.neutralOrMore:
                    break;

                case Requirements.stressedOrMore:
                    break;

                case Requirements.full:
                    if (pawn.needs.food.Starving)
                    {
                        return false;
                    }
                    break;

                default:
                    Log.Warning("Dreamer's Dreams: For some reason " + requirement.ToString() + " does not have implementation. If you see that please do report that on mod page :) It is just a warning, not error. It is not game breaking in any way.");
                    return false;
            }

            return true;
        }
    }
}