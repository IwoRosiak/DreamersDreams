using UnityEngine;
using Verse;

namespace DreamersDream
{
    [StaticConstructorOnStartup]
    public static class Textures
    {
        public static readonly Texture2D IncrementButton = ContentFinder<Texture2D>.Get("Things/Item/Equipment/WeaponRanged/button", true);

        public static readonly Texture2D SettingsBackGround = ContentFinder<Texture2D>.Get("Things/Item/Equipment/WeaponRanged/background", true);

        public static readonly Texture2D TableEntryBGChance1 = ContentFinder<Texture2D>.Get("Things/Item/Equipment/WeaponRanged/TableEntryBGChance1", true);

        public static readonly Texture2D TableEntryBGChance2 = ContentFinder<Texture2D>.Get("Things/Item/Equipment/WeaponRanged/TableEntryBGChance2", true);

        public static readonly Texture2D TableEntryBGCat1 = ContentFinder<Texture2D>.Get("Things/Item/Equipment/WeaponRanged/TableEntryBGCat1", true);

        public static readonly Texture2D TableEntryBGCat2 = ContentFinder<Texture2D>.Get("Things/Item/Equipment/WeaponRanged/TableEntryBGCat2", true);
    }
}