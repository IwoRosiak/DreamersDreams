using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    public class PawnDreamOddsTracker
    {
        public PawnDreamOddsTracker(Pawn parent)
        {
            pawn = parent;
        }

        public Dictionary<DreamDef, float> GetUpdatedDreamsWithChances(DreamTagDef tag)
        {
            UpdateOddsForDreams(tag);
            ConvertOddsForDreamsToPercent();
            return DreamOddsPercent;
        }

        private Dictionary<DreamDef, float> UpdateOddsForDreams(DreamTagDef tag)
        {
            DreamOdds.Clear();

            foreach (var dream in IsolateDreamsOfTag(tag))
            {
                float chanceForDream = dream?.CalculateChanceFor(pawn) ?? 0;

                if (chanceForDream != 0)
                {
                    DreamOdds.Add(dream, chanceForDream);
                }
            }
            return DreamOdds;
        }

        private void ConvertOddsForDreamsToPercent()
        {
            DreamOddsPercent.Clear();
            float chanceForTagPercent = 0;
            foreach (var dream in DreamOdds)
            {
                chanceForTagPercent += ChanceInPercentages(dream.Value, AddUpChancesForDreams());

                DreamOddsPercent.Add(dream.Key, chanceForTagPercent);
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

        private List<DreamDef> IsolateDreamsOfTag(DreamTagDef tag)
        {
            List<DreamDef> MatchingDreams = new List<DreamDef>();

            foreach (var dream in DreamTracker.GetAllDreams)
            {
                if (dream.tags.Contains(tag))
                {
                    MatchingDreams.Add(dream);
                }
            }
            if (MatchingDreams.NullOrEmpty())
            {
                Log.Error(tag.defName + " has no compatible dreams for this pawn or is empty.");
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