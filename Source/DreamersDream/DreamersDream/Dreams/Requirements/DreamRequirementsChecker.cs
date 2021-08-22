using RimWorld;
using Verse;

namespace DreamersDream
{
    internal static class DreamRequirementsChecker
    {
        public static bool IsMeetingRequirements(this DreamDef dream, Pawn pawn)
        {
            foreach (var tag in dream.tags)
            {
                if (!IsMeetingRequirements(tag, pawn))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsMeetingRequirements(this DreamTagDef tag, Pawn pawn)
        {
            foreach (var requirement in tag.requirements)
            {
                if (!requirement.CheckRequirementForPawn(pawn))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckRequirementForPawn(this Requirements requirement, Pawn pawn)
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
                    foreach (var trait in pawn.story.traits.allTraits)
                    {
                        if (trait.def.defName == "Gourmand")
                        {
                            break;
                        }
                    }
                    return false;

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
                    if (!(pawn.needs.mood.CurLevel > 0.9f))
                    {
                        return false;
                    }
                    break;

                case Requirements.aboutToBreak:
                    //this.CurLevel < this.pawn.mindState.mentalBreaker.BreakThresholdMinor
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

                case Requirements.stressed:
                    break;

                case Requirements.ill:
                    break;

                case Requirements.hungry:
                    break;
            }

            return true;
        }
    }
}