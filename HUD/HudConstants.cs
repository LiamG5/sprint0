using Microsoft.Xna.Framework;

namespace sprint0.HUD
{
    public static class HudConstants
    {
        
        public const int HudHeight = 96;
        public const int HudPadding = 16;
        
        public static readonly Vector2 TopLeft = new Vector2(HudPadding, HudPadding);

        // Minimap and Lable
        public static readonly Vector2 LevelLabelPos = TopLeft + new Vector2(0, -8);
        public static readonly Vector2 MinimapPos = TopLeft + new Vector2(0, 24);
        
        // Counters and Slots
        public static readonly Vector2 CountersPos = TopLeft + new Vector2(180, -12);
        public const int CounterLineHeight = 32;
        
        // Inventory slots
        public static readonly Vector2 SlotBPos = TopLeft + new Vector2(280, 30);
        public static readonly Vector2 SlotAPos = TopLeft + new Vector2(328, 30);
        public static readonly Point  SlotSize = new(36, 42);
        
        // Life
        public static readonly Vector2 LifeLabelPos = TopLeft + new Vector2(400, 4);
        public static  readonly Vector2 HeartsPos = TopLeft + new Vector2(400, 48);
        public const int HeartSpacing = 34;
    }
}
