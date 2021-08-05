using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    public class PawnDreamOddsTracker
    {
        public Dictionary<DreamDef, float> GetUpdatedDreamsWithChances(DreamQualityDef quality)
        {
            UpdateOddsForDreams(quality);
            ConvertOddsForDreamsToPercent();
            return DreamOddsPercent;
        }

        public PawnDreamOddsTracker(Pawn parent)
        {
            pawn = parent;
        }

        private Dictionary<DreamDef, float> UpdateOddsForDreams(DreamQualityDef quality)
        {
            DreamOdds.Clear();

            foreach (var dream in IsolateDreamsOfQuality(quality))
            {
                float chanceForDream = dream.CalculateChanceFor(pawn);

                DreamOdds.Add(dream, chanceForDream);
            }
            return DreamOdds;
        }

        private void ConvertOddsForDreamsToPercent()
        {
            DreamOddsPercent.Clear();
            float chanceForQualityPercent = 0;
            foreach (var dreamQuality in DreamOdds)
            {
                chanceForQualityPercent += ChanceInPercentages(dreamQuality.Value, AddUpChancesForDreams());

                DreamOddsPercent.Add(dreamQuality.Key, chanceForQualityPercent);
            }
        }

        private float AddUpChancesForDreams()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in DreamOdds)
            {
                sumOfCollectionChances += item.Value;
            }
            return sumOfCollectionChances;
        }

        private List<DreamDef> IsolateDreamsOfQuality(DreamQualityDef quality)
        {
            List<DreamDef> MatchingDreams = new List<DreamDef>();

            foreach (var dream in DreamTracker.GetAvailibleDreamsForPawn)
            {
                if (dream.quality == quality)
                {
                    MatchingDreams.Add(dream);
                }
            }
            if (MatchingDreams.NullOrEmpty())
            {
                Log.Error(quality.defName + " has no compatible dreams for this pawn or is empty.");
            }
            return MatchingDreams;
        }

        private float ChanceInPercentages(float chance, float sumOfChances)
        {
            return (chance / sumOfChances) * 100;
        }

        private Pawn pawn;

        private Dictionary<DreamDef, float> DreamOddsPercent = new Dictionary<DreamDef, float>();

        private Dictionary<DreamDef, float> DreamOdds = new Dictionary<DreamDef, float>();
    }
}