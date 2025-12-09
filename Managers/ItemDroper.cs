using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
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
        


        public ItemDroper()
        {
            this.items = ItemFactory.Instance;
            this.item =  null;
        }

       
    

        public void LoadItem(Vector2 position)
        {
            Random rand = new Random();
            //50/50 chance to drop an item
            int itemType = rand.Next(0, 2); 
            // random item
            if(itemType == 0)
            {
                itemType = rand.Next(0, 5); 
                spawnItem = true;
            }
            else
            {
                spawnItem = false;
                return;
            }

            switch (itemType)
            {
                case 0:
                    item = items.BuildRupee(position);
                    break;
                case 1:
                    item = items.BuildFairy(position);
                    break;
                case 2:
                    item = items.BuildBomb(position);
                    break;
                case 3:
                    item = items.BuildRupeeBlue(position);
                    break;
                case 4:
                    item = items.BuildRecoveryHeart(position);
                    break;

            }
            this.position = position;
            
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