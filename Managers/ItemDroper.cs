using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Factories;
using sprint0.Interfaces;
using sprint0.Managers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace sprint0.Sprites
{
    public class ItemDroper
    {
        private ItemFactory items = ItemFactory.Instance;
        public Texture2D border;
        private IItem item;
        private bool spawnItem = false;

        private Vector2 position;
        private Func<Vector2> playerPositionProvider;


        public ItemDroper()
        {
            this.items = ItemFactory.Instance;
            this.item =  null;
        }

        public void SetPlayerPositionProvider(Func<Vector2> provider)
        {
            this.playerPositionProvider = provider;
        }

       
    

        public void LoadItem(Vector2 position)
        {
            Random rand = new Random();
            //50/50 chance to drop an item
            int itemType = rand.Next(0, 2); 
            // random item
            if(itemType == 0)
            {
                itemType = rand.Next(0, 100); 
                spawnItem = true;
            }
            else
            {
                spawnItem = false;
                return;
            }

            if(itemType <= 25) {
                item = items.BuildRupeeBlue(position);
            } else if(itemType <= 50) {
                item = items.BuildBomb(position);
            } else if(itemType <= 75) {
                item = items.BuildRecoveryHeart(position);
            } else if(itemType <= 90) {
                item = items.BuildRupee(position);
            } else if(itemType <= 95) {
                item = items.BuildFairy(position, playerPositionProvider);
            } else {
                item = items.BuildClock(position);
            }
            
            this.position = position;

        }
        
        public void Dropkey(int roomNum)
        {
            switch (roomNum)
            {
                case 1:
                    item = items.BuildSmallKey(new Vector2(478, 384));
                    spawnItem = true;
                    break;
                case 6:
                    item = items.BuildSmallKey(new Vector2(388, 144));
                    spawnItem = true;
                    break;
                case 12:
                    item = items.BuildSmallKey(new Vector2(478, 384));
                    spawnItem = true;
                    break;
                case 17:
                    item = items.BuildSmallKey(new Vector2(388, 144));
                    spawnItem = true;
                    break;
            
        }
        }
        public void DropKey(Vector2 position)
        {
            System.Console.WriteLine("new key");
            item = items.BuildSmallKey(position);
            spawnItem = true;
        }


        public bool HasItem()
        {
            return spawnItem;
        }

        public IItem GetItem()
        {
            spawnItem = false;
            return item;
        }
        

    }
}