using Microsoft.Xna.Framework;

namespace sprint0.HUD
{
    public static class HudConstants
    {
        // HUD layout based on the first image:
        // Left: Level label + Minimap
        // Middle: Counters (rupees, keys, bombs) + Inventory slots (B, A)
        // Right: Life label + Hearts
        
        // HUD box dimensions - made bigger for more space
        public const int HudHeight = 96; // Height of the black HUD box (increased from 56)
        public const int HudPadding = 16;  // Padding inside HUD box (increased from 8)
        
        // Top-left position inside the HUD box (with padding)
        public static readonly Vector2 TopLeft = new Vector2(HudPadding, HudPadding);

        // Left side - Level and Minimap
        public static readonly Vector2 LevelLabelPos = TopLeft + new Vector2(0, 0); // Top of HUD
        public static readonly Vector2 MinimapPos = TopLeft + new Vector2(0, 24); // Below level label, with proper spacing
        
        // Middle - Counters and Slots
        public static readonly Vector2 CountersPos = TopLeft + new Vector2(180, 8); // Start of counters (moved right, down)
        public const int CounterLineHeight = 24; // Increased spacing between counters
        
        // Inventory slots (B on left, A on right)
        public static readonly Vector2 SlotBPos = TopLeft + new Vector2(320, 8); // B slot (moved right)
        public static readonly Vector2 SlotAPos = TopLeft + new Vector2(368, 8); // A slot (right of B, more spacing)
        public static readonly Point  SlotSize = new(36, 36); // Bigger slots
        
        // Right side - Life
        public static readonly Vector2 LifeLabelPos = TopLeft + new Vector2(440, 4); // Moved further right
        public static readonly Vector2 HeartsPos = TopLeft + new Vector2(440, 28); // Below "-LIFE-" label, more spacing
        public const int HeartSpacing = 20; // Increased spacing between hearts
    }
}
