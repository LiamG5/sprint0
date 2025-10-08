using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Sprites
{
    public class EnemySpriteFactory 
    { 
        // Aquamentus is the boss
        public enum EnemyType
        {
            BladeTrap, Gel, RedGoriya, Keese, Stalfos, Wallmaster
        }

        private EnemyType currEnemy = EnemyType.BladeTrap;

        public EnemyType getEnemy()
        {
            return currEnemy;
        }

        public Texture2D enemySpritesheet;
        private static EnemySpriteFactory instance = new EnemySpriteFactory(); 
        private EnemySpriteFactory() 
        { 
            enemySpritesheet = Texture2DStorage.GetEnemiesSpriteSheet(); 
        } 
        public static EnemySpriteFactory Instance 
        { 
            get {
                return instance; 
            } 
        }
        public ISprite SpawnBladeTrap(SpriteBatch sprite)
        {
            currEnemy = EnemyType.BladeTrap;
            return new EnemyBladeTrap(enemySpritesheet);
        }

        public ISprite SpawnGel(SpriteBatch sprite)
        {
            currEnemy = EnemyType.Gel;
            return new EnemyGel(enemySpritesheet);
        }
        public ISprite SpawnRedGoriya(SpriteBatch sprite)
        {
            currEnemy = EnemyType.RedGoriya;
            return new EnemyRedGoriya(enemySpritesheet);
        }
        public ISprite SpawnKeese(SpriteBatch sprite)
        {
            currEnemy = EnemyType.Keese;
            return new EnemyKeese(enemySpritesheet);
        }
        public ISprite SpawnStalfos(SpriteBatch sprite)
        {
            currEnemy = EnemyType.Stalfos;
            return new EnemyStalfos(enemySpritesheet);
        }
        public ISprite SpawnWallmaster(SpriteBatch sprite)
        {
            currEnemy = EnemyType.Wallmaster;
            return new EnemyWallmaster(enemySpritesheet);
        }


    }
}
