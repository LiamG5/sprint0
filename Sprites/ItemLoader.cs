using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Managers;
using System;
using System.Collections.Generic;

namespace sprint0.Sprites
{
    public class ItemLoader
    {
        private ItemFactory items = ItemFactory.Instance;
        private int roomId;
        private RoomManager roomManager;
        public Texture2D border;

        public ItemLoader(ItemFactory items, int roomNum)
        {
            this.items = items;
            this.roomId = roomNum;
        }

        public void LoadItems(int roomId)
        {
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
                default:
                    break;
            }
        }

        private void Room1Items()
        {
        
            items.BuildDungeonMap(new Vector2(100, 100));
        }

        private void Room2Items()
        {
            
            items.BuildCompass(new Vector2(150, 100));
        }

        private void Room3Items()
        {
            
            items.BuildRupee(new Vector2(120, 100));
            items.BuildRupee(new Vector2(140, 100));
            items.BuildRupee(new Vector2(160, 100));
            items.BuildRupee(new Vector2(180, 100));
            items.BuildRupee(new Vector2(200, 100));
        }

        private void Room4Items()
        {
            // Boomerang
            items.BuildBoomerang(new Vector2(100, 100));
        }

        private void Room5Items()
        {
            
            items.BuildHeartContainer(new Vector2(150, 100));
        }

        private void Room6Items()
        {
            
            items.BuildSmallKey(new Vector2(130, 100));
        }


        private void Room8Items()
        {
            
            items.BuildSmallKey(new Vector2(120, 100));
        }

        private void Room9Items()
        {
            
            items.BuildRupee(new Vector2(110, 100));
            items.BuildRupee(new Vector2(130, 100));
            items.BuildRupee(new Vector2(150, 100));
        }

        private void Room10Items()
        {
            
            items.BuildBow(new Vector2(100, 100));
        }

        private void Room11Items()
        {
            
            items.BuildFairy(new Vector2(150, 100));
        }

        private void Room12Items()
        {
            
            items.BuildHeartContainer(new Vector2(140, 100));
        }

        private void Room13Items()
        {
            
            items.BuildBomb(new Vector2(120, 100));
        }

        private void Room14Items()
        {
            
            items.BuildClock(new Vector2(130, 100));
        }

        private void Room15Items()
        {

            items.BuildHeartContainer(new Vector2(100, 100));
        }
        


        

    }
}