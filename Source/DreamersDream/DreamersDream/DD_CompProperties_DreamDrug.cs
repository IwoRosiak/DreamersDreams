using RimWorld;

namespace DreamersDream
{
    public class DD_CompProperties_DreamDrug : CompProperties_Drug
    {
        public bool Addictive
        {
            get
            {
                return this.addictiveness > 0f;
            }
        }

        public bool CanCauseOverdose
        {
            get
            {
                return this.overdoseSeverityOffset.TrueMax > 0f;
            }
        }

        public string TheDreamMessage
        {
            get
            {
                return this.DreamMessage;
            }
        }

        public DD_CompProperties_DreamDrug()
        {
            this.compClass = typeof(DD_CompDrug);
        }

        public string DreamMessage;







    }

}

