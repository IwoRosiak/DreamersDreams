using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class DD_Utility
    {
        private enum TypeOfDream { sleepwalk, normal, inspiration };
        static DD_Utility()
        {
        }

        private static TypeOfDream CheckTypeOfDream(DD_ThoughtDef dream)
        {
            TypeOfDream typeOfDream;
            if (dream.triggers != null)
            {
                typeOfDream = TypeOfDream.sleepwalk;
                return typeOfDream;
            }
            else if (dream.inspiration != null)
            {
                typeOfDream = TypeOfDream.inspiration;
                return typeOfDream;
            }
            else
            {
                typeOfDream = TypeOfDream.normal;
                return typeOfDream;
            }
        }

        public static float CheckDreamChance(DD_ThoughtDef dream, Pawn pawn)
        {
            switch (CheckTypeOfDream(dream))
            {
                case TypeOfDream.sleepwalk:
                    return dream.chance * DD_CalcTools.CheckSettingsSleepwalk(dream) * DD_CalcTools.EnvironmentDreamChance(dream, pawn) * DD_CalcTools.TraitDreamChance(dream, pawn);
                    break;
                case TypeOfDream.inspiration:
                    return dream.chance * 10000;//DD_CalcTools.CheckSettingsDream(dream.stages[0].baseMoodEffect) * DD_CalcTools.EnvironmentDreamChance(dream, pawn);
                    break;
                case TypeOfDream.normal:
                    return dream.chance * DD_CalcTools.CheckSettingsDream(dream.stages[0].baseMoodEffect) * DD_CalcTools.EnvironmentDreamChance(dream, pawn);
                    break;
                default:
                    return 0;
                    break;
            }
        }





        /*
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
        }*/


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
