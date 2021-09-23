using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    public class PawnDreamTagsOddsTracker
    {
        public Dictionary<DreamTagDef, float> GetUpdatedTagsWithChances()
        {
            ConvertOddsForDreamTagsToPercent();
            return DreamTagOddsPercent;
        }

        private Pawn pawn;

        private Dictionary<DreamTagDef, float> DreamTagOddsPercent = new Dictionary<DreamTagDef, float>();

        public PawnDreamTagsOddsTracker(Pawn parent)
        {
            pawn = parent;
        }

        private Dictionary<DreamTagDef, float> UpdateOddsForDreamTags()
        {
            Dictionary<DreamTagDef, float> DreamTagOdds = new Dictionary<DreamTagDef, float>();
            foreach (var dreamTag in DreamTracker.GetAllDreamTags)
            {
                float chanceForTag = dreamTag?.CalculateChanceFor(pawn) ?? 0;

                if (chanceForTag != 0)
                {
                    DreamTagOdds.Add(dreamTag, chanceForTag);
                }
            }
            return DreamTagOdds;
        }

        private void ConvertOddsForDreamTagsToPercent()
        {
            float chanceForTagPercent = 0;
            DreamTagOddsPercent.Clear();
            foreach (var dreamTag in UpdateOddsForDreamTags())
            {
                chanceForTagPercent += ChanceInPercentages(dreamTag.Value, AddUpChancesForTags());

                DreamTagOddsPercent.Add(dreamTag.Key, chanceForTagPercent);
            }
        }

        public float AddUpChancesForTags()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in UpdateOddsForDreamTags())
            {
                sumOfCollectionChances += item.Value;
            }
            return sumOfCollectionChances;
        }

        public static float ChanceInPercentages(float chance, float sumOfChances)
        {
            return (chance / sumOfChances) * 100;
        }
    }
}