using RimWorld;
using System;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public override IEnumerable<string> ConfigErrors()
        {
            IEnumerator<string> enumerator = null;
            if (this.workerClass != null && this.nextThought != null)
            {
                yield return "has a nextThought but also has a workerClass. nextThought only works for memories";
            }
            if (this.IsMemory && this.workerClass != null)
            {
                yield return "has a workerClass but is a memory. workerClass only works for situational thoughts, not memories";
            }
            if (!this.IsMemory && this.workerClass == null && this.IsSituational)
            {
                yield return "is a situational thought but has no workerClass. Situational thoughts require workerClasses to analyze the situation";
            }
            int num;
            for (int i = 0; i < this.stages.Count; i = num + 1)
            {
                if (this.stages[i] != null)
                {
                    foreach (string text2 in this.stages[i].ConfigErrors())
                    {
                        yield return text2;
                    }
                    enumerator = null;
                }
                num = i;
            }
            yield break;
            yield break;
        }

        public new Type thoughtClass = typeof(Dream);

        public List<DreamTagDef> tags = new List<DreamTagDef>();

        public List<Requirements> requirements = new List<Requirements>();

        new public string description;

        public int baseMoodEffect = 0;

        public string dreamedBy;
    }
}