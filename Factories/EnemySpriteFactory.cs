using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Factories
{
    public class EnemySpriteFactory 
    { 
        public enum EnemyType
        {
            BladeTrap, Gel, RedGoriya, Keese, Stalfos, StalfosKey, Wallmaster, Aquamentus, Flame
        }

        private EnemyType currEnemy = EnemyType.BladeTrap;

        public EnemyType getEnemy()
        {
            return currEnemy;
        }

        public Texture2D enemySpritesheet;
        public Texture2D bossSpritesheet;
        private static EnemySpriteFactory instance = new EnemySpriteFactory(); 
        private EnemySpriteFactory() 
        { 
            enemySpritesheet = Texture2DStorage.GetEnemiesSpriteSheet();
            bossSpritesheet = Texture2DStorage.GetBossSpriteSheet();
        } 
        public static EnemySpriteFactory Instance 
        { 
            get {
                return instance; 
            } 
        }
        
        public IEnemy SpawnBladeTrap()
        {
            currEnemy = EnemyType.BladeTrap;
            return new EnemyBladeTrap(enemySpritesheet);
        }
        public IEnemy SpawnBladeTrap(Vector2 position, Link link)
        {
            currEnemy = EnemyType.BladeTrap;
            return new EnemyBladeTrap(enemySpritesheet, position, link);
        }

        public IEnemy SpawnGel()
        {
            currEnemy = EnemyType.Gel;
            return new EnemyGel(enemySpritesheet);
        }
        public IEnemy SpawnGel(Vector2 position)
        {
            currEnemy = EnemyType.Gel;
            return new EnemyGel(enemySpritesheet, position);
        }
        
        public IEnemy SpawnRedGoriya()
        {
            currEnemy = EnemyType.RedGoriya;
            return new EnemyRedGoriya(enemySpritesheet);
        }
        public IEnemy SpawnRedGoriya(Vector2 position)
        {
            currEnemy = EnemyType.RedGoriya;
            return new EnemyRedGoriya(enemySpritesheet, position);
        }
        
        public IEnemy SpawnKeese()
        {
            currEnemy = EnemyType.Keese;
            return new EnemyKeese(enemySpritesheet);
        }
        public IEnemy SpawnKeese(Vector2 position)
        {
            currEnemy = EnemyType.Keese;
            return new EnemyKeese(enemySpritesheet, position);
        }
        
        public IEnemy SpawnStalfos()
        {
            currEnemy = EnemyType.Stalfos;
            return new EnemyStalfos(enemySpritesheet);
        }
        public IEnemy SpawnStalfos(Vector2 position)
        {
            currEnemy = EnemyType.Stalfos;
            return new EnemyStalfos(enemySpritesheet, position);
        }
        public IEnemy SpawnStalfosKey()
        {
            currEnemy = EnemyType.StalfosKey;
            return new EnemyStalfosKey(enemySpritesheet);
        }
        public IEnemy SpawnStalfosKey(Vector2 position)
        {
            currEnemy = EnemyType.StalfosKey;
            return new EnemyStalfosKey(enemySpritesheet, position);
        }
        
        public IEnemy SpawnWallmaster()
        {
            currEnemy = EnemyType.Wallmaster;
            return new EnemyWallmaster(enemySpritesheet);
        }
        public IEnemy SpawnWallmaster(Vector2 position)
        {
            currEnemy = EnemyType.Wallmaster;
            return new EnemyWallmaster(enemySpritesheet, position);
        }

        public IEnemy SpawnFlame()
        {
            currEnemy = EnemyType.Flame;
            return new EnemyFlame(enemySpritesheet);
        }
        public IEnemy SpawnFlame(Vector2 position)
        {
            currEnemy = EnemyType.Flame;
            return new EnemyFlame(enemySpritesheet, position);
        }

        public IEnemy SpawnAquamentus(Vector2 position, DungeonLoader dungeonLoader = null, Func<Vector2> playerPositionProvider = null)
        {
            currEnemy = EnemyType.Aquamentus;
            return new EnemyAquamentus(bossSpritesheet, position, dungeonLoader, playerPositionProvider);
        }


    }
}
