using RimWorld;
using System.Text;
using Verse;

namespace DreamersDream
{
    public static class DreamBuilder
    {
        public static void CreateNewDream(DreamDef baseDream, ref Dream currentDream, Pawn pawn)
        {
            currentDream.defName = baseDream.defName + pawn.ThingID;
            currentDream.stackLimit = baseDream.stackLimit;
            currentDream.showBubble = baseDream.showBubble;
            //currentDream.IsMemory = baseDream.IsMemory;
            currentDream.durationDays = baseDream.durationDays;
            currentDream.tags = baseDream.tags;

            StringBuilder desc = new StringBuilder();
            desc.Append(baseDream.stages[0].description);

            if (baseDream.dreamedBy != null)
            {
                desc.Append(" Dreamed by " + baseDream.dreamedBy);
            }
            var stage = new ThoughtStage();
            stage.description = desc.ToString();
            stage.label = baseDream.defName + pawn.ThingID;
            stage.baseMoodEffect = baseDream.stages[0].baseMoodEffect;

            currentDream.stages.Add(stage);
        }
    }
}