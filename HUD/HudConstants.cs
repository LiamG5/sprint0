using Microsoft.Xna.Framework;

namespace sprint0.HUD
{
    public static class HudConstants
    {
        
        public const int HudHeight = 96; // Height of the black HUD box
        public const int HudPadding = 16;  // Padding inside HUD box
        
        public static readonly Vector2 TopLeft = new Vector2(HudPadding, HudPadding);

        // Minimap and Lable
        public static readonly Vector2 LevelLabelPos = TopLeft + new Vector2(0, -8); // Moved higher (from -4 to -8)
        public static readonly Vector2 MinimapPos = TopLeft + new Vector2(0, 24);
        
        // Counters and Slots
        public static readonly Vector2 CountersPos = TopLeft + new Vector2(180, 20); // Aligned with item slots
        public const int CounterLineHeight = 24;
        
        // Inventory slots - moved left to prevent falling off screen
        public static readonly Vector2 SlotBPos = TopLeft + new Vector2(280, 20);
        public static readonly Vector2 SlotAPos = TopLeft + new Vector2(328, 20);
        public static readonly Point  SlotSize = new(36, 36);
        
        // Life - moved left and adjusted positions
        public static readonly Vector2 LifeLabelPos = TopLeft + new Vector2(400, 4);
        public static readonly Vector2 HeartsPos = TopLeft + new Vector2(400, 52); // Moved down more to avoid covering label
        public const int HeartSpacing = 20;
    }
}
