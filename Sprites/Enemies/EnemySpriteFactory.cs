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
            enemySpritesheet = sprint0.Sprites.Texture2DStorage.GetEnemiesSpriteSheet(); 
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
        public IEnemy SpawnBladeTrap(Vector2 position)
        {
            currEnemy = EnemyType.BladeTrap;
            return new EnemyBladeTrap(enemySpritesheet, position);
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
        

        public IEnemy SpawnWallmaster()
        {
            currEnemy = EnemyType.Wallmaster;
            return new EnemyWallmaster(enemySpritesheet);
        }
        public IEnemy SpawnWallmaster(Vector2 position)
        {
            currEnemy = EnemyType.Wallmaster;
            return new EnemyWallmaster(enemySpritesheet,position);
        }


    }
}
