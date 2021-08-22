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

                case Requirements.psychopath:
                    if (!pawn.story.traits.HasTrait(TraitDefOf.Psychopath))
                    {
                        return false;
                    }
                    break;

                case Requirements.cannibal:
                    if (!pawn.story.traits.HasTrait(TraitDefOf.Cannibal))
                    {
                        return false;
                    }
                    break;

                case Requirements.gourmand:
                    bool hasTrait = false;
                    foreach (var trait in pawn.story.traits.allTraits)
                    {
                        if (trait.def.defName == "Gourmand")
                        {
                            hasTrait = true;
                        }
                    }
                    if (!hasTrait)
                    {
                        return false;
                    }

                    break;

                case Requirements.noncannibal:
                    if (pawn.story.traits.HasTrait(TraitDefOf.Cannibal))
                    {
                        return false;
                    }
                    break;

                case Requirements.nonpsychopath:
                    if (pawn.story.traits.HasTrait(TraitDefOf.Psychopath))
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
                    break;

                case Requirements.healthy:
                    break;

                case Requirements.starving:
                    break;

                case Requirements.prisoner:
                    if (!pawn.IsPrisoner)
                    {
                        return false;
                    }
                    break;

                case Requirements.colonist:
                    if (!pawn.IsColonist)
                    {
                        return false;
                    }
                    break;

                case Requirements.killer:
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

                case Requirements.ill:
                    if (pawn.needs.mood.thoughts.)
                        break;

                case Requirements.hungry:
                    break;
            }

            return true;
        }
    }
}