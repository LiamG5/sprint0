using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Factories;
using sprint0.Interfaces;
using sprint0.Managers;
using sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sprint0.Loaders
{
    public class ItemLoader
    {
        private ItemFactory items = ItemFactory.Instance;
        public Texture2D border;
        public List<IItem> itemList;
        private Dictionary<int, List<IItem>> LoadedItems;
        private int roomId = 2;
        private ItemDroper itemDroper;



        public ItemLoader(ItemFactory items , ItemDroper itemDroper)
        {
            this.items = items;
            itemList = new List<IItem>();
            LoadedItems = new Dictionary<int, List<IItem>>();
            this.itemDroper = itemDroper;
            
        }

        public void LoadItems(int roomId)
        {
           if (LoadedItems.ContainsKey(this.roomId))
            {   
                LoadedItems[this.roomId] = itemList;

            }
            else
            {
                LoadedItems.Add(this.roomId, itemList);
            }
            this.roomId = roomId;

            itemList = new List<IItem>();

            if (LoadedItems.ContainsKey(roomId))
            {
                itemList = new List<IItem>(LoadedItems[roomId]);
                
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
            //itemList.Add(items.BuildRecoveryHeart(new Vector2(420, 240)));
            //itemList.Add(items.BuildSmallKey(new Vector2(478, 384)));
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

            //itemList.Add(items.BuildSmallKey(new Vector2(388, 144)));
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
            //drops now
            //itemList.Add(items.BuildSmallKey(new Vector2(478, 384)));   
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

           // itemList.Add(items.BuildSmallKey(new Vector2(388, 144)));
            
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

        foreach (IItem item in itemList)
        {
            item.Update(gameTime);
        }

        if(itemDroper.HasItem())
        itemList.Add(itemDroper.GetItem());
        

        
        for(int i = itemList.Count -1 ; i >= 0; i--)
            {
                if (itemList[i].IsCollected())
                {
                    itemList.RemoveAt(i);
                }
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
            roomId = 2;
        }
        


        

    }
}