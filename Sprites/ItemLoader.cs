using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Managers;
using System;
using System.Collections.Generic;

namespace sprint0.Sprites
{
    public class ItemLoader
    {
        private ItemFactory items = ItemFactory.Instance;
        public Texture2D border;
        public List<IItem> itemList;
        private Dictionary<int, List<IItem>> LoadedItems;
        private int roomId = 1;
        



        public ItemLoader(ItemFactory items)
        {
            this.items = items;
            this.itemList = new List<IItem>();
            this.LoadedItems = new Dictionary<int, List<IItem>>();
            Room1Items();
        }

        public void LoadItems(int roomId)
        {
            //when new room loaded save last room's items if needed
            if (LoadedItems.ContainsKey(this.roomId))
            {   
                LoadedItems[this.roomId] = this.itemList;
            }
            else
            {
                LoadedItems.Add(this.roomId, this.itemList);
            }

            this.itemList = new List<IItem>();

            //load items if already loaded
            if (LoadedItems.ContainsKey(roomId))
            {   
                this.itemList = LoadedItems[roomId];
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
            
            itemList.Add(items.BuildSmallKey(new Vector2(478, 384)));
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
            itemList.Add(items.BuildCompass(new Vector2(580, 240)));
        }


        private void Room8Items()
        {
            
            
        }

        private void Room9Items()
        {
            
        }

        private void Room10Items()
        {
            
            itemList.Add(items.BuildDungeonMap(new Vector2(580, 240)));
        }

        private void Room11Items()
        {

            itemList.Add(items.BuildBoomerang(new Vector2(388, 144)));
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
        } 
        public List<IItem> GetItems()
        {
            return itemList;
        }
        


        

    }
}