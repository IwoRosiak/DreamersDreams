using RimWorld;
using Verse;

namespace DreamersDream
{
    public class DD_CompDrug : CompDrug
    {
        public DD_CompProperties_DreamDrug Props
        {
            get
            {
                return (DD_CompProperties_DreamDrug)this.props;
            }
        }
        public override void PrePostIngested(Pawn ingester)
        {
            base.PrePostIngested(ingester);
            Messages.Message(this.Props.TheDreamMessage.Translate(), MessageTypeDefOf.NeutralEvent);
        }
    }





    /*base.Impact(hitThing);
    if (Props != null && hitThing != null && hitThing is Pawn hitPawn)
    {
        float rand = Rand.Value;
        if (rand <= Props.addHediffChance)
        {
            // successful hit message
            "TST_PlagueBullet_SuccessMessage".Translate(this.launcher.Label, hitPawn.Label);
            // unsuccessful hit mote
            "TST_PlagueBullet_FailureMote".Translate(Props.addHediffChance);
            Messages.Message("TST_PlagueBullet_SuccessMessage".Translate(
            this.launcher.Label, hitPawn.Label
            ), MessageTypeDefOf.NeutralEvent);
            Hediff plagueOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Props.hediffToAdd);
            float randomSeverity = Rand.Range(0.15f, 0.30f);
            if (plagueOnPawn != null)
            {
                plagueOnPawn.Severity += randomSeverity;
            }
            else
            {
                Hediff hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
                hediff.Severity = randomSeverity;
                hitPawn.health.AddHediff(hediff);
            }
        }
        else
        {
            MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "TST_PlagueBullet_FailureMote".Translate(Props.addHediffChance), 12f);
        }

    }*/

}

