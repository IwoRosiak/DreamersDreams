using RimWorld;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public List<DreamTagDef> tags = new List<DreamTagDef>();

        public List<Requirements> requirements = new List<Requirements>();

        public string dreamedBy = "no one";
    }
}