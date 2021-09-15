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

        public List<HealthStatus> requiredHealth = new List<HealthStatus>();
        public List<HealthStatus> requiredOneOfHealth = new List<HealthStatus>();
        public List<HealthStatus> conflictingHealth = new List<HealthStatus>();

        public List<SocialStatus> requiredSocial = new List<SocialStatus>();
        public List<SocialStatus> requiredOneOfSocial = new List<SocialStatus>();
        public List<SocialStatus> conflictingSocial = new List<SocialStatus>();

        public MoodStatus minMood;
        public MoodStatus maxMood;
    }
}