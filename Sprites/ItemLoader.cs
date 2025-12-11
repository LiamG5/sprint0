using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sprint0.Sprites
{
    public class ItemLoader
    {
        private ItemFactory items = ItemFactory.Instance;
        public Texture2D border;
        public List<IItem> itemList;
        private Dictionary<int, List<IItem>> LoadedItems;
        private int roomId = 1;
        private ItemDroper itemDroper;



        public ItemLoader(ItemFactory items , ItemDroper itemDroper)
        {
            this.items = items;
            this.itemList = new List<IItem>();
            this.LoadedItems = new Dictionary<int, List<IItem>>();
            this.itemDroper = itemDroper;
            
        }

        public void LoadItems(int roomId)
        {
            if (this.itemList.Count > 0)
            {
                var uncollectedItems = new List<IItem>();
                foreach (var item in this.itemList)
                {
                    if (!item.IsCollected())
                    {
                        uncollectedItems.Add(item);
                    }
                }
                
                if (LoadedItems.ContainsKey(this.roomId))
                {
                    LoadedItems[this.roomId] = uncollectedItems;
                }
                else
                {
                    LoadedItems.Add(this.roomId, uncollectedItems);
                }
            }

            this.itemList = new List<IItem>();

            if (LoadedItems.ContainsKey(roomId))
            {
                this.itemList = new List<IItem>(LoadedItems[roomId]);
                this.roomId = roomId;
                return;
            }



            itemList.Clear();
            
            
                switch (roomId)
                {
                    case 1:
                        Room1Items();
                        break;
                    case 2:
                        Room2Items();
                        break;
                    case 3:
                        Room3Items();
                        break;
                    case 4:
                        Room4Items();
                        break;
                    case 5:
                        Room5Items();
                        break;
                    case 6:
                        Room6Items();
                        break;
                    case 7:
                        Room7Items();
                        break;
                    case 8:
                        Room8Items();
                        break;
                    case 9:
                        Room9Items();
                        break;
                    case 10:
                        Room10Items();
                        break;
                    case 11:
                        Room11Items();
                        break;
                    case 12:
                        Room12Items();
                        break;
                    case 13:
                        Room13Items();
                        break;
                    case 14:
                        Room14Items();
                        break;
                    case 15:
                        Room15Items();
                        break;
                    case 16:
                        Room16Items();
                        break;
                    case 17:
                        Room17Items();
                        break;
                    default:
                        break;

                }
            
        }

        private void Room1Items()
        {
            itemList.Add(items.BuildRecoveryHeart(new Vector2(420, 240)));
            itemList.Add(items.BuildSmallKey(new Vector2(478, 384)));
            itemList.Add(items.BuildBow(new Vector2(400, 300)));
        }

        private void Room2Items()
        {
        }

        private void Room3Items()
        {
            
            
        }

        private void Room4Items()
        {
          
        }

        private void Room5Items()
        {
            
          
        }

        private void Room6Items()
        {

            itemList.Add(items.BuildSmallKey(new Vector2(388, 144)));
        }
        
         private void Room7Items()
        {
            if (!Inventory.HasCompass())
            {
                itemList.Add(items.BuildCompass(new Vector2(580, 240)));
            }
        }


        private void Room8Items()
        {
            
            
        }

        private void Room9Items()
        {
            
        }

        private void Room10Items()
        {
            if (!Inventory.HasMap())
            {
                itemList.Add(items.BuildDungeonMap(new Vector2(580, 240)));
            }
        }

        private void Room11Items()
        {
            if (!Inventory.HasBoomerang())
            {
                itemList.Add(items.BuildBoomerang(new Vector2(388, 144)));
            }
        }

        private void Room12Items()
        {
            itemList.Add(items.BuildSmallKey(new Vector2(478, 384)));   
        }

        private void Room13Items()
        {
            
            
        }

        private void Room14Items()
        {
            itemList.Add(items.BuildHeartContainer(new Vector2(580,240)));
            
        }

        private void Room15Items()
        {
        itemList.Add(items.BuildTriforceFragment(new Vector2(360,250)));

        }

        private void Room16Items()
        {


        }
        private void Room17Items()
        {

            itemList.Add(items.BuildSmallKey(new Vector2(388, 144)));
            
        }

        public void Dropkey()
        {
            
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IItem item in itemList)
            {
                item.Draw(spriteBatch, item.GetPosition());
            }
        }

        public void Update(GameTime gameTime)
        {       
        
        if(itemDroper.HasItem())
        itemList.Add(itemDroper.GetItem());

        foreach (IItem item in itemList)
        {
            item.Update(gameTime);
        }

        } 
        public List<IItem> GetItems()
        {
            return itemList;
        }
                

        public void Reset()
        {
            LoadedItems.Clear();
            itemList.Clear();
            this.roomId = 1;
        }
        


        

    }
}