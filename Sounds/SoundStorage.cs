using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace sprint0.Sounds
{
    public static class SoundStorage
    {
        public static SoundEffect LOZ_Arrow_Boomerang;
        public static SoundEffect LOZ_Bomb_Blow;
        public static SoundEffect LOZ_Bomb_Drop;
        public static SoundEffect LOZ_Boss_Hit;
        public static SoundEffect LOZ_Boss_Scream1;
        public static SoundEffect LOZ_Boss_Scream2;
        public static SoundEffect LOZ_Boss_Scream3;
        public static SoundEffect LOZ_Candle;
        public static SoundEffect LOZ_Door_Unlock;
        public static SoundEffect LOZ_Enemy_Die;
        public static SoundEffect LOZ_Enemy_Hit;
        public static SoundEffect LOZ_Fanfare;
        public static SoundEffect LOZ_Get_Heart;
        public static SoundEffect LOZ_Get_Item;
        public static SoundEffect LOZ_Get_Rupee;
        public static SoundEffect LOZ_Key_Appear;
        public static SoundEffect LOZ_Link_Die;
        public static SoundEffect LOZ_Link_Hurt;
        public static SoundEffect LOZ_LowHealth;
        public static SoundEffect LOZ_MagicalRod;
        public static SoundEffect LOZ_Recorder;
        public static SoundEffect LOZ_Refill_Loop;
        public static SoundEffect LOZ_Secret;
        public static SoundEffect LOZ_Shield;
        public static SoundEffect LOZ_Shore;
        public static SoundEffect LOZ_Stairs;
        public static SoundEffect LOZ_Sword_Combined;
        public static SoundEffect LOZ_Sword_Shoot;
        public static SoundEffect LOZ_Sword_Slash;
        public static SoundEffect LOZ_Text;
        public static SoundEffect LOZ_Text_Slow;
        public static SoundEffect LOZ_SuperLink;
        public static SoundEffect COD_Laugh;
        public static Song dungeon;

        private static float sfxVolume = 0.5f; // Master volume for sfx
        public static void LoadAllSounds(ContentManager Content)
        {
            LOZ_Arrow_Boomerang = Content.Load<SoundEffect>(@"sounds/LOZ_Arrow_Boomerang");
            LOZ_Bomb_Blow = Content.Load<SoundEffect>(@"sounds/LOZ_Bomb_Blow");
            LOZ_Bomb_Drop = Content.Load<SoundEffect>(@"sounds/LOZ_Bomb_Drop");
            LOZ_Boss_Hit = Content.Load<SoundEffect>(@"sounds/LOZ_Boss_Hit");
            LOZ_Boss_Scream1 = Content.Load<SoundEffect>(@"sounds/LOZ_Boss_Scream1");
            LOZ_Boss_Scream2 = Content.Load<SoundEffect>(@"sounds/LOZ_Boss_Scream2");
            LOZ_Boss_Scream3 = Content.Load<SoundEffect>(@"sounds/LOZ_Boss_Scream3");
            LOZ_Candle = Content.Load<SoundEffect>(@"sounds/LOZ_Candle");
            LOZ_Door_Unlock = Content.Load<SoundEffect>(@"sounds/LOZ_Door_Unlock");
            LOZ_Enemy_Die = Content.Load<SoundEffect>(@"sounds/LOZ_Enemy_Die");
            LOZ_Enemy_Hit = Content.Load<SoundEffect>(@"sounds/LOZ_Enemy_Hit");
            LOZ_Fanfare = Content.Load<SoundEffect>(@"sounds/LOZ_Fanfare");
            LOZ_Get_Heart = Content.Load<SoundEffect>(@"sounds/LOZ_Get_Heart");
            LOZ_Get_Item = Content.Load<SoundEffect>(@"sounds/LOZ_Get_Item");
            LOZ_Get_Rupee = Content.Load<SoundEffect>(@"sounds/LOZ_Get_Rupee");
            LOZ_Key_Appear = Content.Load<SoundEffect>(@"sounds/LOZ_Key_Appear");
            LOZ_Link_Die = Content.Load<SoundEffect>(@"sounds/LOZ_Link_Die");
            LOZ_Link_Hurt = Content.Load<SoundEffect>(@"sounds/LOZ_Link_Hurt");
            LOZ_LowHealth = Content.Load<SoundEffect>(@"sounds/LOZ_LowHealth");
            LOZ_MagicalRod = Content.Load<SoundEffect>(@"sounds/LOZ_MagicalRod");
            LOZ_Recorder = Content.Load<SoundEffect>(@"sounds/LOZ_Recorder");
            LOZ_Refill_Loop = Content.Load<SoundEffect>(@"sounds/LOZ_Refill_Loop");
            LOZ_Secret = Content.Load<SoundEffect>(@"sounds/LOZ_Secret");
            LOZ_Shield = Content.Load<SoundEffect>(@"sounds/LOZ_Shield");
            LOZ_Shore = Content.Load<SoundEffect>(@"sounds/LOZ_Shore");
            LOZ_Stairs = Content.Load<SoundEffect>(@"sounds/LOZ_Stairs");
            LOZ_Sword_Combined = Content.Load<SoundEffect>(@"sounds/LOZ_Sword_Combined");
            LOZ_Sword_Shoot = Content.Load<SoundEffect>(@"sounds/LOZ_Sword_Shoot");
            LOZ_Sword_Slash = Content.Load<SoundEffect>(@"sounds/LOZ_Sword_Slash");
            LOZ_Text = Content.Load<SoundEffect>(@"sounds/LOZ_Text");
            LOZ_Text_Slow = Content.Load<SoundEffect>(@"sounds/LOZ_Text_Slow");
            LOZ_SuperLink = Content.Load<SoundEffect>(@"sounds/SuperLink");
            COD_Laugh = Content.Load<SoundEffect>(@"sounds/Cod_Laugh");

            dungeon = Content.Load<Song>(@"music/dungeon");
        }
        public static void PlayLOZArrowBoomerang()
        {
            LOZ_Arrow_Boomerang.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZBombBlow()
        {
            LOZ_Bomb_Blow.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZBombDrop()
        {
            LOZ_Bomb_Drop.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZBossHit()
        {
            LOZ_Boss_Hit.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZBossScream1()
        {
            LOZ_Boss_Scream1.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZBossScream2()
        {
            LOZ_Boss_Scream2.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZBossScream3()
        {
            LOZ_Boss_Scream3.Play(sfxVolume, 0f, 0f);

        }

        public static void PlayLOZCandle()
        {
            LOZ_Candle.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZDoorUnlock()
        {
            LOZ_Door_Unlock.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZEnemyDie()
        {
            LOZ_Enemy_Die.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZEnemyHit()
        {
            LOZ_Enemy_Hit.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZFanfare()
        {
            LOZ_Fanfare.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZGetHeart()
        {
            LOZ_Get_Heart.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZGetItem()
        {
            LOZ_Get_Item.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZGetRupee()
        {
            LOZ_Get_Rupee.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZKeyAppear()
        {
            LOZ_Key_Appear.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZLinkDie()
        {
            LOZ_Link_Die.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZLinkHurt()
        {
            LOZ_Link_Hurt.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZLowHealth()
        {
            LOZ_LowHealth.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZMagicalRod()
        {
            LOZ_MagicalRod.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZRecorder()
        {
            LOZ_Recorder.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZRefillLoop()
        {
            LOZ_Refill_Loop.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZSecret()
        {
            LOZ_Secret.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZShield()
        {
            LOZ_Shield.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZShore()
        {
            LOZ_Shore.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZStairs()
        {
            LOZ_Stairs.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZSwordCombined()
        {
            LOZ_Sword_Combined.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZSwordShoot()
        {
            LOZ_Sword_Shoot.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZSwordSlash()
        {
            LOZ_Sword_Slash.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZText()
        {
            LOZ_Text.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLOZTextSlow()
        {
            LOZ_Text_Slow.Play(sfxVolume, 0f, 0f);
        }
        public static void PlaySuperLink()
        {
            LOZ_SuperLink.Play(sfxVolume, 0f, 0f);
        }

        public static void PlayLaugh()
        {
            COD_Laugh.Play(sfxVolume, 0f, 0f);
        }

    }
}
