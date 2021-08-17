using UnityEngine;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    internal static class Textures
    {
        public static readonly Texture2D SettingsBackGround = ContentFinder<Texture2D>.Get("UI/ModSettings/background", true);

        public static readonly Texture2D TableEntryBGChance1 = ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGChance1", true);

        public static readonly Texture2D TableEntryBGChance2 = ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGChance2", true);

        public static readonly Texture2D TableEntryBGCat1 = ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGCat1", true);

        public static readonly Texture2D TableEntryBGCat2 = ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGCat2", true);
    }
}