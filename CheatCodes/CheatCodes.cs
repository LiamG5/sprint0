using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using sprint0.Classes;
using sprint0.Sounds;

namespace sprint0.Cheats
{
    public class CheatCodes
    {
        private  List<Keys> buffer = new List<Keys>();
        private  List<Keys> cheat1 = new List<Keys>();
        private  List<Keys> cheat2 = new List<Keys>();
        private  List<Keys> cheat3 = new List<Keys>();
        private  List<Keys> cheat4 = new List<Keys>();
        private  List<Keys> cheat5 = new List<Keys>();
        private  List<Keys> cheat6 = new List<Keys>();
        private  List<Keys> cheat7 = new List<Keys>();
        private  List<Keys> cheat8 = new List<Keys>();
      

        public int CheatCodeCheck = 0;
        private  int maxLength;


        public CheatCodes()
        {
            this.maxLength = 10;
            CreatCheatCodes();
        }

        

        public void AddKeyPress(Keys key)
        {
            
            buffer.Add(key);
            if (buffer.Count > maxLength) buffer.RemoveAt(0);

            if(key == Keys.C)
            {
                if (buffer.SequenceEqual(cheat1))
                {
                    CheatCodeCheck = 1;
                }
                else if (buffer.SequenceEqual(cheat2))
                {
                    CheatCodeCheck = 2;
                }
                else if (buffer.SequenceEqual(cheat3))
                {
                    CheatCodeCheck = 3;
                }
                else if (buffer.SequenceEqual(cheat4))
                {
                    CheatCodeCheck = 4;
                }
                else if (buffer.SequenceEqual(cheat5))
                {
                    CheatCodeCheck = 5;
                }
                else if (buffer.SequenceEqual(cheat6))
                {
                    CheatCodeCheck = 6;
                }
                else if (buffer.SequenceEqual(cheat7))
                {
                    CheatCodeCheck = 7;
                }
                else if (buffer.SequenceEqual(cheat8))
                {
                    CheatCodeCheck = 8;
                }
                buffer.Clear();

            }
        }

        private void CreatCheatCodes()
        {
            CheatCreate1();
            CheatCreate2();
            CheatCreate3();
            CheatCreate4();
            CheatCreate5();
            CheatCreate6();
            CheatCreate7();
            CheatCreate8();
        }
        private void CheatCreate1()
        {
            // WW AA DD 12 2C 
            cheat1.Add(Keys.W);
            cheat1.Add(Keys.W);
            cheat1.Add(Keys.A);
            cheat1.Add(Keys.A);
            cheat1.Add(Keys.D);
            cheat1.Add(Keys.D);
            cheat1.Add(Keys.D1);
            cheat1.Add(Keys.D2);
            cheat1.Add(Keys.D2);
            cheat1.Add(Keys.C);
        }
        private void CheatCreate2()
        {
            // 12 WS AD WD SC 
            cheat2.Add(Keys.D1);
            cheat2.Add(Keys.D2);
            cheat2.Add(Keys.W);
            cheat2.Add(Keys.S);
            cheat2.Add(Keys.A);
            cheat2.Add(Keys.D);
            cheat2.Add(Keys.W);
            cheat2.Add(Keys.D);
            cheat2.Add(Keys.S);
            cheat2.Add(Keys.C);
        }
        private void CheatCreate3()
        {
            // AA DD AD DA W2 
            cheat3.Add(Keys.A);
            cheat3.Add(Keys.A);
            cheat3.Add(Keys.D);
            cheat3.Add(Keys.D);
            cheat3.Add(Keys.A);
            cheat3.Add(Keys.D);
            cheat3.Add(Keys.D);
            cheat3.Add(Keys.A);
            cheat3.Add(Keys.W);
            cheat3.Add(Keys.C);
        
        }
        private void CheatCreate4()
        {
            // WS WS WS WS WC
            cheat4.Add(Keys.W);
            cheat4.Add(Keys.S);

            cheat4.Add(Keys.W);
            cheat4.Add(Keys.S);

            cheat4.Add(Keys.W);
            cheat4.Add(Keys.S);

            cheat4.Add(Keys.W);
            cheat4.Add(Keys.S);

            cheat4.Add(Keys.W);
            cheat4.Add(Keys.C);
           
        }
           
        private void CheatCreate5()
        {
            // AD AD AD DA SC 
            cheat5.Add(Keys.A);
            cheat5.Add(Keys.D);

            cheat5.Add(Keys.A);
            cheat5.Add(Keys.D);

            cheat5.Add(Keys.A);
            cheat5.Add(Keys.D);

            cheat5.Add(Keys.D);
            cheat5.Add(Keys.A);

            cheat5.Add(Keys.S);
            cheat5.Add(Keys.C);

        }
       
        private void CheatCreate6()
        {
            // A1 S1 D1 W1 1C 
            cheat6.Add(Keys.A);
            cheat6.Add(Keys.D1);

            cheat6.Add(Keys.S);
            cheat6.Add(Keys.D1);

            cheat6.Add(Keys.D);
            cheat6.Add(Keys.D1);

            cheat6.Add(Keys.W);
            cheat6.Add(Keys.D1);

            cheat6.Add(Keys.D1);
            cheat6.Add(Keys.C);

        }
        private void CheatCreate7()
        {
            // AD AD WS WS 1C 
            cheat7.Add(Keys.A);
            cheat7.Add(Keys.D);

            cheat7.Add(Keys.A);
            cheat7.Add(Keys.D);

            cheat7.Add(Keys.W);
            cheat7.Add(Keys.S);

            cheat7.Add(Keys.W);
            cheat7.Add(Keys.S);

            cheat7.Add(Keys.D1);
            cheat7.Add(Keys.C);

        }
        private void CheatCreate8()
        {
            // AD WS DA 11 2C 
            cheat8.Add(Keys.A);
            cheat8.Add(Keys.D);

            cheat8.Add(Keys.W);
            cheat8.Add(Keys.S);

            cheat8.Add(Keys.D);
            cheat8.Add(Keys.A);

            cheat8.Add(Keys.D1);
            cheat8.Add(Keys.D1);

            cheat8.Add(Keys.D2);
            cheat8.Add(Keys.C);


        }
    public void Update(){

        switch(CheatCodeCheck){
            case 1: Inventory.AddRupees(100); Inventory.AddBombs(100); break;
            case 2: Inventory.GetClock(); break;
            case 3: Inventory.GetHeartContainer(); break;      
            case 4: Inventory.AcquireMap(); break;
            case 5: Inventory.GetFairy(); break;
            case 6: 
            
                    Inventory.SetRupees(0);
                    Inventory.SetBombs(0);
                    Inventory.SetKeys(0);
                    Inventory.SetBoomerang(false);
                    Inventory.SetBow(false);
                    Inventory.SetCompass(false); 
                    Inventory.SetMap(false);
                    Inventory.SetHealth(1);
                    SoundStorage.PlayLaugh();

            break;
            case 7:
                    
                    Inventory.AddRupees(100); 
                    Inventory.AddBombs(100);
                    
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();
                    Inventory.GetKeys();

                    Inventory.GetFairy();

                    Inventory.SetBoomerang(true);
                    Inventory.SetBow(true);
                    Inventory.SetCompass(true);
                    Inventory.SetMap(true);
                     break;
            case 8: 
                if(!Inventory.GetSuperLink()){
                    SoundStorage.PlaySuperLink();
                    Inventory.SetSuperLink(true);
                }
                    break;
    }
        CheatCodeCheck = 0;
}
    }
}
    
