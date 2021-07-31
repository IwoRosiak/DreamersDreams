using Verse;

namespace DreamersDream
{
    public class DreamComp : ThingComp
    {
        public Dreams_CompProperties Props
        {
            get
            {
                return (Dreams_CompProperties)this.props;
            }
        }

        public override void CompTickRare()
        {
            base.CompTick();
            //Log.Message("Hi! I'm " + parent.def.defName);

            PawnDreamHandler.Tick((Pawn)parent);
        }
    }
}