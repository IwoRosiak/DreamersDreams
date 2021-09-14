using RimWorld;

namespace DreamersDream
{
    public class Thought_Dream : Thought_Memory
    {
        public DreamDef dreamDef
        {
            get
            {
                return (DreamDef)def;
            }
        }

        protected override float BaseMoodOffset
        {
            get
            {
                return dreamDef.tags[0].moodOffset;
            }
        }

        public override string LabelCap
        {
            get
            {
                return def.label;
            }
        }

        public override string Description
        {
            get
            {
                return def.description + AppendDreamAuthor();
            }
        }

        public string AppendDreamAuthor()
        {
            if (dreamDef.dreamedBy != "")
            {
                return " dreamed by " + dreamDef.dreamedBy;
            }
            return "";
        }
    }
}