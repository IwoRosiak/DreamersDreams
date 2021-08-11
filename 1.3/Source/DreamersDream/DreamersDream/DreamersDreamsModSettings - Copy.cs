using UnityEngine;
using Verse;

namespace DreamersDream
{
    public class DD_Mods : Mod
    {
        private DD_Settings settings;

        private static Vector2 ScrollPos = Vector2.zero;

        public DD_Mods(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<DD_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            inRect.width /= 2;
            Listing_Standard listing_Standard = new Listing_Standard();
            Rect rect = new Rect(0f, 0f, inRect.width - 50, 936f);
            rect.xMax *= 0.9f;
            //listing_Standard.Begin(rect);
            GUI.EndGroup();
            Widgets.BeginScrollView(inRect, ref scrolli, rect, true);

            Widgets.Label(rect, "lol");

            Widgets.EndScrollView();
        }

        private static Vector2 scrolli = Vector2.zero;

        public override string SettingsCategory()
        {
            return "Dreamer's Dreams - test";
        }
    }
}