using RimWorld;
using System.Text;
using Verse;

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
                return def.description + AppendDreamAuthor() + AppendDebugInfo();
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

        public string AppendDebugInfo()
        {
            if (DebugSettings.godMode)
            {
                StringBuilder debugInfo = new StringBuilder();
                debugInfo.AppendLine("");
                debugInfo.AppendLine("");
                debugInfo.AppendLine("Primary tag: " + dreamDef.tags[0]);
                debugInfo.AppendLine("Base chance: " + dreamDef.CalculateChanceFor(pawn));
                //debugInfo.AppendLine("Relative chance: " + relativeChance);
                debugInfo.AppendLine(RequirementsInString());

                return debugInfo.ToString();
            }
            return "";
        }

        public string RequirementsInString()
        {
            StringBuilder debugInfo = new StringBuilder();
            //traits
            if (!dreamDef.nullifyingTraits.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Conflicting traits: ");
                foreach (var req in dreamDef.nullifyingTraits)
                {
                    if (dreamDef.nullifyingTraits[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredTraits.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required traits: ");
                foreach (var req in dreamDef.requiredTraits)
                {
                    if (dreamDef.requiredTraits[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }

            //backstory
            if (!dreamDef.conflictingBackstory.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Conflicting backstory tags: ");
                foreach (var req in dreamDef.conflictingBackstory)
                {
                    if (dreamDef.conflictingBackstory[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredBackstory.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required backstory tags: ");
                foreach (var req in dreamDef.requiredBackstory)
                {
                    if (dreamDef.requiredBackstory[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredOneOfBackstory.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required one of backstory tags: ");
                foreach (var req in dreamDef.requiredOneOfBackstory)
                {
                    if (dreamDef.requiredOneOfBackstory[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            //social
            if (!dreamDef.conflictingMindStates.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Conflicting social tags: ");
                foreach (var req in dreamDef.conflictingMindStates)
                {
                    if (dreamDef.conflictingMindStates[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredMindStates.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required social tags: ");
                foreach (var req in dreamDef.requiredMindStates)
                {
                    if (dreamDef.requiredMindStates[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredOneOfBackstory.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required one of social tags: ");
                foreach (var req in dreamDef.requiredOneOfBackstory)
                {
                    if (dreamDef.requiredOneOfBackstory[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            //health
            if (!dreamDef.conflictingBodyStates.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Conflicting health tags: ");
                foreach (var req in dreamDef.conflictingBodyStates)
                {
                    if (dreamDef.conflictingBodyStates[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredBodyStates.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required health tags: ");
                foreach (var req in dreamDef.requiredBodyStates)
                {
                    if (dreamDef.requiredBodyStates[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredOneOfBodyStates.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required one of health tags: ");
                foreach (var req in dreamDef.requiredOneOfBodyStates)
                {
                    if (dreamDef.requiredOneOfBodyStates[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            //standing
            if (!dreamDef.conflictingStanding.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Conflicting standing tags: ");
                foreach (var req in dreamDef.conflictingStanding)
                {
                    if (dreamDef.conflictingStanding[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredStanding.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required standing tags: ");
                foreach (var req in dreamDef.requiredStanding)
                {
                    if (dreamDef.requiredStanding[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            if (!dreamDef.requiredOneOfStanding.NullOrEmpty())
            {
                debugInfo.AppendInNewLine("Required one of standing tags: ");
                foreach (var req in dreamDef.requiredOneOfStanding)
                {
                    if (dreamDef.requiredOneOfStanding[0] == req)
                    {
                        debugInfo.Append(req.ToString().ToLower());
                    }
                    else debugInfo.AppendWithComma(req.ToString().ToLower());
                }
            }
            //mood
            debugInfo.AppendInNewLine("Minimum mood: " + dreamDef.minMood.ToString());
            debugInfo.AppendInNewLine("Maximum mood: " + dreamDef.maxMood.ToString());
            return debugInfo.ToString();
        }

        // public float relativeChance;
    }
}