﻿using RimWorld;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public List<DreamTagDef> tags = new List<DreamTagDef>();
    }
}