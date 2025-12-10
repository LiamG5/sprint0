using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using sprint0.Classes;

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
        }
        private void CheatCreate1()
        {
            // WW AA DD 12 22 
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
            // 12 WS AD WD S2 
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
    public void Update(){
        
            if(CheatCodeCheck == 1){
                Inventory.AddRupees(100);
                Inventory.AddBombs(100);
                            }
            else if(CheatCodeCheck == 2){
                Inventory.GetClock();
                            }
            else if(CheatCodeCheck == 3){
                Inventory.GetHeartContainer();
                            }
            else if(CheatCodeCheck == 4){
                Inventory.SetSuperLink();
            }              
            else if(CheatCodeCheck == 5){
                
                }
        
            
            CheatCodeCheck = 0;
}
}
}
    
