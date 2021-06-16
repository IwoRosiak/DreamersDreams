using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class DD_Utility
    {
        static DD_Utility()
        {
        }

        public static bool IsAwake(Pawn pawn)
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return true;
            }
            else if (pawn.needs.rest.GUIChangeArrow == -1)
            {
                return false;
            }
            return true;
        }

        public static float CheckDreamChance(DD_ThoughtDef dream)
        {
            /*if (CheckForHigh() && dream.defName == "VeryGoodDream")
            {
                return dream.chance + 1000;

            }*/
            return dream.chance;
        }

        public static bool CheckForHigh(Pawn pawn)
        {
            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                Hediff hediff = pawn.health.hediffSet.hediffs[i];

                if (hediff.def.defName == "DreamHigh")
                {
                    //Messages.Message("Dream High Detected!", MessageTypeDefOf.NeutralEvent);
                    return true;
                }
            }
            return false;
        }





        /* public virtual void MentalStateTick()
         {
             if (this.pawn.IsHashIntervalTick(150))
             {
                 this.age += 150;
                 if (this.age >= this.def.maxTicksBeforeRecovery || (this.age >= this.def.minTicksBeforeRecovery && this.CanEndBeforeMaxDurationNow && Rand.MTBEventOccurs(this.def.recoveryMtbDays, 60000f, 150f)) || (this.forceRecoverAfterTicks != -1 && this.age >= this.forceRecoverAfterTicks))
                 {
                     this.RecoverFromState();
                     return;
                 }
                 if (this.def.recoverFromSleep && !this.pawn.Awake())
                 {
                     this.RecoverFromState();
                     return;
                 }
             }
         } CHECK BASED ON AGE*/
    }
}
