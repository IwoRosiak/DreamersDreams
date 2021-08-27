using System;
using Verse;

namespace DreamersDream
{
    internal static class DreamsThoughtsHandler
    {
        internal static void TryApplyThought(this Pawn pawn, DreamDef def)
        {
            Dream thought = MakeThought(def);

            thought.moodOffset = (int)thought.MoodOffset();

            var memHandler = pawn.needs.mood.thoughts.memories;

            //Log.Message(thought.Description + " desc.");
            //Log.Message(thought.DurationTicks.ToString() + " ticks duration.");
            thought.pawn = pawn;

            memHandler.TryGainMemory(def);
        }

        internal static Dream MakeThought(DreamDef def)
        {
            Dream thought = (Dream)Activator.CreateInstance(typeof(Dream));
            thought.def = def;
            thought.Init();

            return thought;
        }
    }
}