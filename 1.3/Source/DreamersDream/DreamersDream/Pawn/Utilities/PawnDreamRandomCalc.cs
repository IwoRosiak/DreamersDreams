using System.Collections.Generic;
using Verse;

namespace DreamersDream
{
    internal static class PawnDreamRandomCalc
    {
        public static DreamDef ChooseRandomDream()
        {
            DreamQualityDef rolledQuality = ChooseRandomDreamQuality();

            List<DreamDef> SelectedDreams = IsolateDreamsOfQuality(rolledQuality);

            return GetRandomDream(SelectedDreams);
        }

        public static DreamQualityDef ChooseRandomDreamQuality()
        {
            float roll = PerformRandomRoll();
            float rollOffset = 0;

            foreach (var item in PawnDreamTracker.GetDreamQualities)
            {
                if (roll <= ChanceInPercentages(item.chance + rollOffset, AddUpChancesForQualities()))
                {
                    return item;
                }

                rollOffset += item.chance;
            }
            return null;
        }

        private static DreamDef GetRandomDream(List<DreamDef> dreams)
        {
            float sumOfChances = AddUpChancesForDreams(dreams);

            float rollOffset = 0;
            float roll = PerformRandomRoll();

            //Log.Message("Roll " + roll);

            foreach (var dream in dreams)
            {
                //Log.Message("Roll progress " + rollOffset);
                //Log.Message("Chance for this dream " + ChanceInPercentages(dream.chance, sumOfChances) + " Name: " + dream.defName + " Of Quality: " + dream.quality);

                if (roll <= ChanceInPercentages(dream.chance + rollOffset, sumOfChances))
                {
                    return dream;
                }

                rollOffset += dream.chance;
            }

            return null;
        }

        private static List<DreamDef> IsolateDreamsOfQuality(DreamQualityDef quality)
        {
            List<DreamDef> MatchingDreams = new List<DreamDef>();

            foreach (var dream in PawnDreamTracker.GetAvailibleDreamsForPawn)
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

        private static float AddUpChancesForDreams(List<DreamDef> dreams)
        {
            float sumOfChances = 0;
            foreach (var dream in dreams)
            {
                sumOfChances += CalculateChanceFor(dream);
            }
            return sumOfChances;
        }

        internal static float AddUpChancesForQualities()
        {
            float sumOfCollectionChances = 0;
            foreach (var item in PawnDreamTracker.GetDreamQualities)
            {
                sumOfCollectionChances += item.chance;
            }
            return sumOfCollectionChances;
        }

        private static float CalculateChanceFor(DreamDef dream)
        {
            return PawnDreamChanceCalc.CalculateChanceFor(dream);
        }

        internal static float ChanceInPercentages(float chance, float sumOfChances)
        {
            return (chance / sumOfChances) * 100;
        }

        private static float PerformRandomRoll()
        {
            return Rand.Range(0, 100);
        }
    }
}