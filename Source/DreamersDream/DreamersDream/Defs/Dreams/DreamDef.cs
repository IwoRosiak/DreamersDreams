using RimWorld;
using System.Collections.Generic;

namespace DreamersDream
{
    [DefOf]
    public class DreamDef : ThoughtDef
    {
        public List<DreamTagDef> tags = new List<DreamTagDef>();

        public string dreamedBy = "";

        public List<BackstoryCategory> requiredBackstory = new List<BackstoryCategory>();
        public List<BackstoryCategory> requiredOneOfBackstory = new List<BackstoryCategory>();
        public List<BackstoryCategory> conflictingBackstory = new List<BackstoryCategory>();

        public List<StandingStatus> requiredStanding = new List<StandingStatus>();
        public List<StandingStatus> requiredOneOfStanding = new List<StandingStatus>();
        public List<StandingStatus> conflictingStanding = new List<StandingStatus>();

        public List<BodyState> requiredBodyStates = new List<BodyState>();
        public List<BodyState> requiredOneOfBodyStates = new List<BodyState>();
        public List<BodyState> conflictingBodyStates = new List<BodyState>();

        public List<MindState> requiredMindStates = new List<MindState>();
        public List<MindState> requiredOneOfMindStates = new List<MindState>();
        public List<MindState> conflictingMindStates = new List<MindState>();

        public List<ThoughtDef> requiredThoughts = new List<ThoughtDef>();
        public List<ThoughtDef> requiredOneOfThoughts = new List<ThoughtDef>();
        public List<ThoughtDef> conflictingThoughts = new List<ThoughtDef>();

        public MoodStatus minMood;
        public MoodStatus maxMood;
    }
}