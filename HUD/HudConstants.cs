using Microsoft.Xna.Framework;

namespace sprint0.HUD
{
    public static class HudConstants
    {
        public static readonly Vector2 TopLeft = new(48, 56);

        public const int RowSpacing = 24;
        public const int ColSpacing = 48;

        public static readonly Vector2 HeartsPos = TopLeft + new Vector2(520, 64);
        public const int HeartSpacing = 16;

        public static readonly Vector2 CountersPos = TopLeft + new Vector2(360, 64);
        public const int CounterLineHeight = 18;

        public static readonly Vector2 SlotAPos = TopLeft + new Vector2(300, 0);
        public static readonly Vector2 SlotBPos = TopLeft + new Vector2(360, 0);
        public static readonly Point  SlotSize = new(28, 28);

        public static readonly Vector2 LevelLabelPos = TopLeft + new Vector2(0, 0);
    }
}
