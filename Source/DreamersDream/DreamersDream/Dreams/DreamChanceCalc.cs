using Verse;

namespace DreamersDream
{
    internal static class DreamChanceCalc
    {
        public static float CalculateChanceFor(this DreamDef dream, Pawn pawn)
        {
            float chance = dream.tags[0].CalculateChanceFor(pawn);

            if (!dream.isMeetingRequirements(pawn))
            {
                chance = 0;
            }

            return chance;
        }

        public static float CalculateChanceFor(this DreamTagDef dreamTag)
        {
            return GetCustomChance(dreamTag);
        }

        public static float CalculateChanceFor(this DreamTagDef dreamTag, Pawn pawn)
        {
            float chance = GetCustomChance(dreamTag);

            if (!dreamTag.isMeetingRequirements(pawn))
            {
                chance = 0;
            }

            return chance;
        }

        private static float GetCustomChance(DreamTagDef tag)
        {
            if (DD_Settings.TagsCustomChances?.ContainsKey(tag.defName) == true && !DD_Settings.isDefaultSettings)
            {
                return DD_Settings.TagsCustomChances[tag.defName];
            }
            else
            {
                return tag.chance;
            }
        }

        private static bool isMeetingRequirements(this DreamDef dream, Pawn pawn)
        {
            foreach (var tag in dream.tags)
            {
                if (!isMeetingRequirements(tag, pawn))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool isMeetingRequirements(this DreamTagDef tag, Pawn pawn)
        {
            foreach (var requirement in tag.requirements)
            {
                switch (requirement)
                {
                    case Requirements.sleepwalker:
                        if (!pawn.isSleepwalker())
                        {
                            return false;
                        }
                        break;
                }
            }

            return true;
        }
    }
}