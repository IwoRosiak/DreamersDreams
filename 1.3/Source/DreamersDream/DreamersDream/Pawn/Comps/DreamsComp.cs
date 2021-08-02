using Verse;

namespace DreamersDream
{
    public class DreamsComp : ThingComp
    {
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
            //Log.Message("Hi! I'm " + parent.def.defName);

            PawnDreamHandler.Tick((Pawn)parent);
        }
    }
}