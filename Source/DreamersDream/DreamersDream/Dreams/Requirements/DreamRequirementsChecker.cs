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
                //category of backstory

                case Requirements.pirate:
                    if (pawn.BackstoryHasThisCat("Pirate"))
                    {
                        return false;
                    }
                    break;

                case Requirements.imperialCommon:
                    if (pawn.BackstoryHasThisCat("ImperialCommon"))
                    {
                        return false;
                    }
                    break;

                case Requirements.offworld:
                    if (pawn.BackstoryHasThisCat("Offworld"))
                    {
                        return false;
                    }
                    break;

                case Requirements.outlander:
                    if (pawn.BackstoryHasThisCat("Outlander"))
                    {
                        return false;
                    }
                    break;

                case Requirements.tribal:
                    if (pawn.BackstoryHasThisCat("Tribal"))
                    {
                        return false;
                    }
                    break;

                //not implemented
                case Requirements.hungry:
                    break;

                case Requirements.married:
                    break;

                case Requirements.bonded:
                    break;

                case Requirements.befriended:
                    break;

                case Requirements.hasEx:
                    break;

                case Requirements.lonely:
                    break;

                default:
                    Log.Warning("Dreamer's Dreams: For some reason " + requirement.ToString() + " does not have implementation. If you see that please do report that on mod page :) It is just a warning, not error. It is not game breaking in any way.");
                    return false;
            }

            return true;
        }

        private static bool BackstoryHasThisCat(this Pawn pawn, string backstoryCat)
        {
            foreach (var cats in pawn.story.GetBackstory(BackstorySlot.Adulthood)?.spawnCategories)
            {
                if (cats == backstoryCat)
                {
                    return true;
                }
            }

            foreach (var cats in pawn.story.GetBackstory(BackstorySlot.Childhood)?.spawnCategories)
            {
                if (cats == backstoryCat)
                {
                    return true;
                }
            }

            return false;
        }
    }
}