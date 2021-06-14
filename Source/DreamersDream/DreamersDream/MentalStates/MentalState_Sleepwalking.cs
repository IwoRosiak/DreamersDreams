using RimWorld;
using Verse;
using Verse.AI;

namespace DreamersDream.Dreams
{
    [StaticConstructorOnStartup]
    public class MentalState_Sleepwalking : MentalState
    {
        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }
    }
}
