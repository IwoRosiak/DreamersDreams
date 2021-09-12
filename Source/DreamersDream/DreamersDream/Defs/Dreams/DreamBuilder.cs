using RimWorld;
using Verse;

namespace DreamersDream
{
    public static class DreamBuilder
    {
        public static void CreateNewDream(DreamDef baseDream, ref DreamDef currentDream, Pawn pawn)
        {
            currentDream = baseDream;

            currentDream.defName = "dreamForPawn" + pawn.ThingID;

            //StringBuilder desc = new StringBuilder();
            // desc.Append(baseDream.stages[0].description + "hello");
            stage.description = baseDream.stages[0].description + "hello";
            var stage = new ThoughtStage();
            //stage.description = desc.ToString();
            stage.label = baseDream.stages[0].label;
            stage.baseMoodEffect = baseDream.stages[0].baseMoodEffect;

            //stage.description += " Dreamed by " + currentDream.dreamedBy;

            currentDream.stages[0] = stage;

            DefDatabase<ThoughtDef>.Add(currentDream);
            DefDatabase<DreamDef>.Add(currentDream);
        }
    }
}