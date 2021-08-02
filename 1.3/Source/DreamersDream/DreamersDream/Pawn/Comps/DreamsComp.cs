using RimWorld.Planet;
using Verse;

namespace DreamersDream
{
    public class DreamsComp : ThingComp
    {
        public Pawn pawn
        {
            get
            {
                return (Pawn)this.parent;
            }
        }

        public DreamsCompProperties Props
        {
            get
            {
                return (DreamsCompProperties)this.props;
            }
        }

        public override void CompTickRare()
        {
            base.CompTick();
            TryApplyDream();
        }

        private void TryApplyDream()
        {
            if (CanGetDreamNow())
            {
                TriggerDreamEffects(PawnDreamRandomCalc.ChooseRandomDream());
            }
        }

        private bool CanGetDreamNow()
        {
            if (IsPawnCapableOfDreaming() && !IsAwake() && IsPawnRestedEnough() && !PawnDreamTracker.HasDreamAlready(pawn))
            {
                return true;
            }
            return false;
        }

        private bool IsPawnRestedEnough()
        {
            return pawn.needs.rest.CurCategory == RimWorld.RestCategory.Rested;
        }

        private bool IsAwake()
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return false;
            }
            else if (pawn.needs.rest.GUIChangeArrow == -1)
            {
                return true;
            }
            return false;
        }

        private void TriggerDreamEffects(DreamDef dream)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(dream, null);

            /*
            if (!dream.triggers.NullOrEmpty())
            {
                pawn.mindState.mentalStateHandler.TryStartMentalState(dream.triggers[Rand.RangeInclusive(0, dream.triggers.Count - 1)], null, true, false, null, false);
            }

            if (dream.inspiration != null)
            {
                pawn.mindState.inspirationHandler.TryStartInspiration(dream.inspiration);
            } */
        }

        private bool IsPawnCapableOfDreaming()
        {
            return pawn.needs?.rest != null && !pawn.Dead && (pawn.Spawned || pawn.IsCaravanMember());
        }
    }
}