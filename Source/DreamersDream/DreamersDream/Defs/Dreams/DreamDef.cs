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
        public List<BackstoryCategory> conflictingBackstory = new List<BackstoryCategory>();

        public List<StandingStatus> requiredStandingStatus = new List<StandingStatus>();
        public List<StandingStatus> conflictingStandingStatus = new List<StandingStatus>();

        public List<HealthStatus> requiredHealthStatus = new List<HealthStatus>();
        public List<HealthStatus> conflictingHealthStatus = new List<HealthStatus>();

        public List<SocialStatus> requiredSocialStatus = new List<SocialStatus>();
        public List<SocialStatus> conflictingSocialStatus = new List<SocialStatus>();

        public MoodStatus minMood;
        public MoodStatus maxMood;
    }
}