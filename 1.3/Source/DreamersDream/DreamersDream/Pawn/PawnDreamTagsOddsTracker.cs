using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    public class PawnDreamTagsOddsTracker
    {
        public Dictionary<DreamTagDef, float> GetUpdatedTagsWithChances()
        {
            ConvertOddsForDreamTagsToPercent();
            return DreamQualityOddsPercent;
        }

        private Pawn pawn;

        private Dictionary<DreamTagDef, float> DreamQualityOddsPercent = new Dictionary<DreamTagDef, float>();

        public PawnDreamTagsOddsTracker(Pawn parent)
        {
            pawn = parent;
        }

        private Dictionary<DreamTagDef, float> UpdateOddsForDreamTags()
        {
            Dictionary<DreamTagDef, float> DreamTagOdds = new Dictionary<DreamTagDef, float>();
            foreach (var dreamTag in DreamTracker.DreamTagsDefs)
            {
                float chanceForTag = dreamTag.CalculateChanceFor(pawn);

                DreamTagOdds.Add(dreamTag, chanceForTag);
            }
            return DreamTagOdds;
        }

        private void ConvertOddsForDreamTagsToPercent()
        {
            float chanceForQualityPercent = 0;
            DreamQualityOddsPercent.Clear();
            foreach (var dreamQuality in UpdateOddsForDreamTags())
            {
                chanceForQualityPercent += ChanceInPercentages(dreamQuality.Value, AddUpChancesForTags());

                DreamQualityOddsPercent.Add(dreamQuality.Key, chanceForQualityPercent);
            }
        }

        public float AddUpChancesForTags()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in UpdateOddsForDreamTags()) //DreamTracker.GetDreamQualities)
            {
                sumOfCollectionChances += item.Value;
            }
            return sumOfCollectionChances;
        }

        public static float ChanceInPercentages(float chance, float sumOfChances)
        {
            return (chance / sumOfChances) * 100;
        }

        /*
        public void CalculateOddsForDreams()
        {
            DreamQualityOdds.Clear();
            foreach (var dream in DreamTracker.GetAvailibleDreamsForPawn)
            {
                float chanceForQuality = dream.CalculateChanceFor(pawn);

                DreamQualityOdds.Add(dream, chanceForQuality);
            }
        }
*/
    }
}