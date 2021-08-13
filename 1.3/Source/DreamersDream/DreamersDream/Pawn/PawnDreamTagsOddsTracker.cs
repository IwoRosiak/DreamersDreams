using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    public class PawnDreamTagsOddsTracker
    {
        public Dictionary<DreamTagDef, float> GetUpdatedQualitiesWithChances()
        {
            ConvertOddsForDreamQualitiesToPercent();
            return DreamQualityOddsPercent;
        }

        private Pawn pawn;

        private Dictionary<DreamTagDef, float> DreamQualityOddsPercent = new Dictionary<DreamTagDef, float>();

        public PawnDreamTagsOddsTracker(Pawn parent)
        {
            pawn = parent;
        }

        private Dictionary<DreamTagDef, float> UpdateOddsForDreamQualities()
        {
            Dictionary<DreamTagDef, float> DreamQualityOdds = new Dictionary<DreamTagDef, float>();
            foreach (var dreamQuality in DreamTracker.DreamTagsDefs)
            {
                float chanceForQuality = dreamQuality.CalculateChanceFor(pawn);

                DreamQualityOdds.Add(dreamQuality, chanceForQuality);
            }
            return DreamQualityOdds;
        }

        private void ConvertOddsForDreamQualitiesToPercent()
        {
            float chanceForQualityPercent = 0;
            DreamQualityOddsPercent.Clear();
            foreach (var dreamQuality in UpdateOddsForDreamQualities())
            {
                chanceForQualityPercent += ChanceInPercentages(dreamQuality.Value, AddUpChancesForQualities());

                DreamQualityOddsPercent.Add(dreamQuality.Key, chanceForQualityPercent);
            }
        }

        public float AddUpChancesForQualities()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in UpdateOddsForDreamQualities()) //DreamTracker.GetDreamQualities)
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