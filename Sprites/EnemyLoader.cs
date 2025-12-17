using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Factories;
using sprint0.Interfaces;
using sprint0.Managers;
using sprint0.Sprites.Enemies;
using System;
using System.Collections.Generic;


namespace sprint0.Sprites
{
    public class EnemyLoader
    {
        private EnemySpriteFactory enemies;
        private int roomId;
        public Texture2D border;
        private List<IEnemy> enemyList;
        private ItemDroper itemDroper;
        private Dictionary<int, List<IEnemy>> LoadedEnemies;
        private int RowStart = 96;
        private int ColStart = 96;

        private int RowMult = 48;
        private int ColMult = 48;
        private bool RoomOneSpawned = false; 

        // Store references for boss spawning
        private DungeonLoader dungeonLoader;
        private Func<Vector2> playerPositionProvider;
        private Link link;



    
        public EnemyLoader(EnemySpriteFactory enemies, ItemDroper itemDroper)
        {
            roomId = 2;
            this.enemies = enemies;
            this.enemyList = new List<IEnemy>();
            this.LoadedEnemies = new Dictionary<int, List<IEnemy>>();
            this.itemDroper = itemDroper;
            

        }

        public void LoadEnemies(int roomId)
        {
            //when new room loaded save last room's enemies if needed
            if (LoadedEnemies.ContainsKey(this.roomId))
            {   
                LoadedEnemies[this.roomId] = this.enemyList;
            }
            else
            {
                LoadedEnemies.Add(this.roomId, this.enemyList);
            }

            this.enemyList = new List<IEnemy>();

            //load enemies if already loaded
            if (LoadedEnemies.ContainsKey(roomId))
            {   
                this.enemyList = LoadedEnemies[roomId];
                this.roomId = roomId;
                return;
            }
            
            switch (roomId)
            {
                case 1:
                    Room1Enemies();
                    break;
                case 2:
                    Room2Enemies();
                    break;
                case 3:
                    Room3Enemies();
                    break;
                case 4:
                    Room4Enemies();
                    break;
                case 5:
                    Room5Enemies();
                    break;
                case 6:
                    Room6Enemies();
                    break;
                case 7:
                    Room7Enemies();
                    break;
                case 8:
                    Room8Enemies();
                    break;
                case 9:
                    Room9Enemies();
                    break;
                case 10:
                    Room10Enemies();
                    break;
                case 11:
                    Room11Enemies();
                    break;
                case 12:
                    Room12Enemies();
                    break;
                case 13:
                    Room13Enemies();
                    break;
                case 14:
                    Room14Enemies();
                    break;
                case 15:
                    Room15Enemies();
                    break;
                case 16:
                    Room16Enemies();
                    break;
                case 17:
                    Room17Enemies();
                    break;
                case 18:
                    SecretRoomEnemies();
                    break;
                default:
                    break;
            }
            this.roomId = roomId;
        }

        private void Room1Enemies()
        {
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 0, RowStart + RowMult * 1)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 4, RowStart + RowMult * 3)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 1, RowStart + RowMult * 5)));
            
        }

        private void Room2Enemies()
        {
        }

        private void Room3Enemies()
        {
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 5)));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 10, RowStart + RowMult * 5)));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 7, RowStart + RowMult *4 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 6, RowStart + RowMult *1 )));
            enemyList.Add(enemies.SpawnStalfosKey(new Vector2(ColStart + ColMult * 9, RowStart + RowMult *1 )));
            
        }

        private void Room4Enemies()
        {
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 3, RowStart + RowMult *0 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 4, RowStart + RowMult *2 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 3, RowStart + RowMult *5 )));
           
        }

        private void Room5Enemies()
        {
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 8, RowStart + RowMult * 0)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 8, RowStart + RowMult * 6)));

            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 0, RowStart + RowMult * 4)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 1, RowStart + RowMult * 5)));

            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 3)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 4)));
        }

        private void Room6Enemies()
        {
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 7, RowStart + RowMult *0 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 5, RowStart + RowMult *2 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 7, RowStart + RowMult *2 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 6, RowStart + RowMult *3 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 3, RowStart + RowMult *5 )));
        }

        private void Room7Enemies()
        {
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 0)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 10, RowStart + RowMult * 1)));

            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 6, RowStart + RowMult * 2)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 8, RowStart + RowMult * 2)));

            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 6, RowStart + RowMult * 4)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 8, RowStart + RowMult * 4)));

            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 11, RowStart + RowMult * 3)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 10, RowStart + RowMult * 5)));

        }

        private void Room8Enemies()
        {
            enemyList.Add(enemies.SpawnFlame(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 2)));
            enemyList.Add(enemies.SpawnFlame(new Vector2(ColStart + ColMult * 8 , RowStart + RowMult * 2)));
            
        }

        private void Room9Enemies()
        {
            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 5, RowStart + RowMult * 1)));
            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 3)));
            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 5, RowStart + RowMult * 5)));
            
        }

        private void Room10Enemies()
        {
           enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 2, RowStart + RowMult * 0)));
            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 0)));

            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 3, RowStart + RowMult * 2)));
            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 2)));

            enemyList.Add(enemies.SpawnGel(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 5)));
        }

        private void Room11Enemies()
        {
            enemyList.Add(enemies.SpawnRedGoriya(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 0)));
            enemyList.Add(enemies.SpawnRedGoriya(new Vector2(ColStart + ColMult * 9, RowStart + RowMult * 1)));
            enemyList.Add(enemies.SpawnRedGoriya(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 4)));
        }

        private void Room12Enemies()
        {
            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult * -1, RowStart + RowMult *5)));
            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult * -1, RowStart + RowMult *5)));

            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult * -1, RowStart + RowMult *5 )));
            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult * -1, RowStart + RowMult *5)));

            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult * -1, RowStart + RowMult *5 )));
            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult * -1, RowStart + RowMult *5 )));

            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult *-1, RowStart + RowMult *5 )));
            enemyList.Add(enemies.SpawnWallmaster(new Vector2(ColStart + ColMult *-1, RowStart + RowMult *5 )));
        }

        private void Room13Enemies()
        {
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 1, RowStart + RowMult *2 )));
            enemyList.Add(enemies.SpawnStalfos(new Vector2(ColStart + ColMult * 3, RowStart + RowMult *3 )));
            enemyList.Add(enemies.SpawnStalfosKey(new Vector2(ColStart + ColMult *10, RowStart + RowMult *3 )));
        }

        private void Room14Enemies()
        {
            // Boss now receives dungeonLoader (for projectiles) and playerPositionProvider (for chasing)
            enemyList.Add(enemies.SpawnAquamentus(new Vector2(96 + 48*5, 96 + 48*3), dungeonLoader, playerPositionProvider));
        }

        private void Room15Enemies()
        {
            // triforce room
        }

        private void Room16Enemies()
        {
            enemyList.Add(enemies.SpawnBladeTrap(new Vector2(ColStart + ColMult * 0, RowStart + RowMult * 0), link));
            enemyList.Add(enemies.SpawnBladeTrap(new Vector2(ColStart + ColMult * 11, RowStart + RowMult * 0), link));
            enemyList.Add(enemies.SpawnBladeTrap(new Vector2(ColStart + ColMult * 0, RowStart + RowMult * 6), link));
            enemyList.Add(enemies.SpawnBladeTrap(new Vector2(ColStart + ColMult * 11, RowStart + RowMult * 6), link));
        }

        private void Room17Enemies()
        {
            enemyList.Add(enemies.SpawnRedGoriya(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 0)));
            enemyList.Add(enemies.SpawnRedGoriya(new Vector2(ColStart + ColMult * 6, RowStart + RowMult * 2)));
            enemyList.Add(enemies.SpawnRedGoriya(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 4)));
        }

        private void SecretRoomEnemies()
        {
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 5, RowStart + RowMult * 3)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 6, RowStart + RowMult * 3)));
            enemyList.Add(enemies.SpawnKeese(new Vector2(ColStart + ColMult * 7, RowStart + RowMult * 3)));
        }

        
        public void Update()
        {
            
        for (int i = enemyList.Count - 1; i >= 0; i--){
                if (enemyList[i].IsDead())
                {
                if(enemyList[i] is EnemyStalfosKey){
                    itemDroper.DropKey(enemyList[i].GetPosition());
                    enemyList.RemoveAt(i);
                }
                else{
                itemDroper.LoadItem(enemyList[i].GetPosition());
                enemyList.RemoveAt(i);
                }

            if (enemyList.Count == 0){
                
                itemDroper.Dropkey(this.roomId);
            }


        } 
        }
        }


        public List<IEnemy> GetEnemies()
        {
            return enemyList;
        }

        // NEW: Methods to set references needed for boss spawning
        public void SetDungeonLoader(DungeonLoader loader)
        {
            this.dungeonLoader = loader;
        }

        public void SetPlayerPositionProvider(Func<Vector2> provider)
        {
            this.playerPositionProvider = provider;
        }

        public void SetLink(Link link)
        {
            this.link = link;
        }
    }
}