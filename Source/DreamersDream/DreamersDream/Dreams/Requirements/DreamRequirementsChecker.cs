using RimWorld;
using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    internal static class DreamRequirementsChecker
    {
        public static bool CheckBackstory(this BackstoryCategory backstoryCat, Pawn pawn, bool invert)
        {
            bool flag = false;
            foreach (var cats in pawn.story.GetBackstory(BackstorySlot.Adulthood)?.spawnCategories)
            {
                if (cats == backstoryCat.ToString())
                {
                    flag = true;
                }
            }

            foreach (var cats in pawn.story.GetBackstory(BackstorySlot.Childhood)?.spawnCategories)
            {
                if (cats == backstoryCat.ToString())
                {
                    flag = true;
                }
            }
            if (invert)
            {
                flag = !flag;
            }
            return flag;
        }

        public static bool CheckStandingStatus(this StandingStatus standing, Pawn pawn, bool invert)
        {
            bool flag = true;
            switch (standing)
            {
                case StandingStatus.prisoner:
                    if (!pawn.IsPrisoner)
                    {
                        flag = false;
                    }
                    break;

                case StandingStatus.colonist:
                    if (!pawn.IsColonist)
                    {
                        flag = false;
                    }
                    break;

                case StandingStatus.guest:

                    break;

                case StandingStatus.slave:
                    if (!pawn.IsSlave)
                    {
                        flag = false;
                    }
                    break;

                default:
                    break;
            }
            if (invert)
            {
                flag = !flag;
            }
            return flag;
        }

        public static bool CheckBodyState(this BodyState health, Pawn pawn, bool invert)
        {
            bool flag = true;
            switch (health)
            {
                case BodyState.wounded:
                    if (!pawn.health.hediffSet.HasNaturallyHealingInjury())
                    {
                        flag = false;
                    }
                    break;

                case BodyState.healthy:
                    if (pawn.health.hediffSet.HasNaturallyHealingInjury() || pawn.health.hediffSet.AnyHediffMakesSickThought)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.ill:
                    if (!pawn.health.hediffSet.AnyHediffMakesSickThought)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.hungry:
                    if ((float)pawn.needs.food.CurCategory == 0)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.fed:
                    if ((float)pawn.needs.food.CurCategory != 0)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.starving:
                    if (!pawn.needs.food.Starving)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.young:
                    if (pawn.ageTracker.AgeBiologicalYears > 30)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.old:
                    if (pawn.ageTracker.AgeBiologicalYears < 55)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.male:
                    if (pawn.gender != Gender.Male)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.female:
                    if (pawn.gender != Gender.Female)
                    {
                        flag = false;
                    }
                    break;

                case BodyState.disabled:
                    if (!CheckIfPartsAreMissing(pawn))
                    {
                        flag = false;
                    }
                    break;

                /*case HealthStatus.reachedSkillCap:
                    foreach (var skill in pawn.skills.skills)
                    {
                        skill.LearningSaturatedToday;
                        flag = false;
                    }
                    break;*/

                default:
                    break;
            }
            if (invert)
            {
                flag = !flag;
            }
            return flag;
        }

        public static bool CheckIfPartsAreMissing(Pawn pawn)
        {
            List<BodyPartRecord> bodyParts = new List<BodyPartRecord>();

            bodyParts.AddRange(GetBodyPartsOfDef(BodyPartDefOf.Leg, pawn));
            bodyParts.AddRange(GetBodyPartsOfDef(BodyPartDefOf.Arm, pawn));

            foreach (var part in bodyParts)
            {
                if (pawn.health.hediffSet.PartIsMissing(part))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<BodyPartRecord> GetBodyPartsOfDef(BodyPartDef def, Pawn pawn)
        {
            List<BodyPartRecord> bodyParts = new List<BodyPartRecord>();
            foreach (var part in pawn.RaceProps.body.GetPartsWithDef(def))
            {
                bodyParts.Add(part);
            }
            return bodyParts;
        }

        public static bool CheckMindState(this MindState social, Pawn pawn, bool invert)
        {
            bool flag = true;
            switch (social)
            {
                case MindState.married:

                    break;

                case MindState.bonded:

                    break;

                case MindState.hasFriend:
                    break;

                case MindState.killer:
                    if (pawn.records.GetValue(RecordDefOf.KillsHumanlikes) <= 0)
                    {
                        flag = false;
                    }
                    break;

                case MindState.guilty:
                    if (!pawn.guilt.IsGuilty)
                    {
                        flag = false;
                    }
                    break;

                case MindState.hasEx:
                    break;

                case MindState.aloneMap:
                    if (pawn.Map.PlayerPawnsForStoryteller.EnumerableCount() != 1)
                    {
                        flag = false;
                    }
                    break;

                case MindState.aloneWorld:
                    if (Find.World.PlayerPawnsForStoryteller.EnumerableCount() != 1)
                    {
                        flag = false;
                    }
                    break;

                default:
                    break;
            }
            if (invert)
            {
                flag = !flag;
            }
            return flag;
        }

        private static void AssignMaxMoodValues(ref float moodValue, MoodStatus mood, Pawn pawn)
        {
            switch (mood)
            {
                case MoodStatus.happy:
                    moodValue = 1f;
                    break;

                case MoodStatus.content:
                    moodValue = 0.9f;
                    break;

                case MoodStatus.neutral:
                    moodValue = 0.65f;
                    break;

                case MoodStatus.stressed:
                    moodValue = pawn.mindState.mentalBreaker.BreakThresholdMinor;
                    break;

                case MoodStatus.onEdge:
                    moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme + 0.05f;
                    break;

                case MoodStatus.aboutToBreak:
                    moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme;
                    break;

                case MoodStatus.none:
                    moodValue = 1f;
                    break;

                default:
                    break;
            }
        }

        private static void AssignMinMoodValues(ref float moodValue, MoodStatus mood, Pawn pawn)
        {
            switch (mood)
            {
                case MoodStatus.happy:
                    moodValue = 0.9f;
                    break;

                case MoodStatus.content:
                    moodValue = 0.65f;
                    break;

                case MoodStatus.neutral:
                    moodValue = pawn.mindState.mentalBreaker.BreakThresholdMinor;
                    break;

                case MoodStatus.stressed:
                    moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme + 0.05f;
                    break;

                case MoodStatus.onEdge:
                    moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme;
                    break;

                case MoodStatus.aboutToBreak:
                    moodValue = 0;
                    break;

                case MoodStatus.none:
                    moodValue = 0;
                    break;

                default:
                    break;
            }
        }

        public static bool CheckMoodStatus(MoodStatus moodMin, MoodStatus moodMax, Pawn pawn)
        {
            float currentMood = pawn.needs.mood.CurLevel;

            float minMood = 0;
            float maxMood = 1;

            AssignMinMoodValues(ref minMood, moodMin, pawn);

            AssignMaxMoodValues(ref maxMood, moodMax, pawn);

            bool flag = true;

            if (minMood > currentMood || maxMood < currentMood)
            {
                //Log.Message("Min mood " + moodMin.ToString() + " max mood" + moodMax.ToString());
                //Log.Message("Min mood " + minMood + " current mood " + currentMood + " max mood " + maxMood);
                flag = false;
            }

            return flag;
        }

        internal static bool CheckRequirementForPawn(this DreamDef dream, Pawn pawn)
        {
            bool flag = true;
            //backstory
            foreach (var req in dream.conflictingBackstory)
            {
                if (!req.CheckBackstory(pawn, true))
                {
                    flag = false;
                    //Log.Message("21");
                }
            }
            foreach (var req in dream.requiredBackstory)
            {
                if (!req.CheckBackstory(pawn, false))
                {
                    flag = false;
                    //Log.Message("22");
                }
            }
            foreach (var req in dream.requiredOneOfBackstory)
            {
                if (req.CheckBackstory(pawn, false))
                {
                    break;
                    //Log.Message("23");
                }
            }
            //standing
            foreach (var req in dream.conflictingStanding)
            {
                if (!req.CheckStandingStatus(pawn, true))
                {
                    flag = false;
                    //Log.Message("31");
                }
            }
            foreach (var req in dream.requiredStanding)
            {
                if (!req.CheckStandingStatus(pawn, false))
                {
                    flag = false;
                    //Log.Message("32");
                }
            }
            foreach (var req in dream.requiredOneOfStanding)
            {
                if (req.CheckStandingStatus(pawn, false))
                {
                    break;
                    //Log.Message("33");
                }
            }
            //health
            foreach (var req in dream.conflictingBodyStates)
            {
                if (!req.CheckBodyState(pawn, true))
                {
                    flag = false;
                    //Log.Message("41");
                }
            }
            foreach (var req in dream.requiredBodyStates)
            {
                if (!req.CheckBodyState(pawn, false))
                {
                    flag = false;
                    //Log.Message("42");
                }
            }
            foreach (var req in dream.requiredOneOfBodyStates)
            {
                if (req.CheckBodyState(pawn, false))
                {
                    //Log.Message("43");
                    break;
                }
            }
            //social
            foreach (var req in dream.conflictingMindStates)
            {
                if (!req.CheckMindState(pawn, true))
                {
                    flag = false;
                    //Log.Message("51");
                }
            }
            foreach (var req in dream.requiredMindStates)
            {
                if (!req.CheckMindState(pawn, false))
                {
                    flag = false;
                    //Log.Message("52");
                }
            }
            foreach (var req in dream.requiredOneOfMindStates)
            {
                if (req.CheckMindState(pawn, false))
                {
                    //Log.Message("53");
                    break;
                }
            }
            //thought
            foreach (var req in dream.conflictingThoughts)
            {
                if (!CheckForThought(req, pawn, true))
                {
                    flag = false;
                }
            }
            foreach (var req in dream.requiredThoughts)
            {
                if (!CheckForThought(req, pawn, false))
                {
                    flag = false;
                }
            }
            foreach (var req in dream.requiredOneOfThoughts)
            {
                if (!CheckForThought(req, pawn, false))
                {
                    break;
                }
            }

            //mood
            if (!CheckMoodStatus(dream.minMood, dream.maxMood, pawn))
            {
                //Log.Message("6");
                flag = false;
            }
            return flag;
        }

        public static bool CheckForThought(ThoughtDef def, Pawn pawn, bool invert)
        {
            bool flag = false;

            if (pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(def) != null)
            {
                flag = true;
            }

            if (invert)
            {
                flag = !flag;
            }
            return flag;
        }
    }
}