using System;
using System.IO.Ports;
using sprint0.Commands;
using sprint0.Interfaces;
using sprint0.PlayerStates;

namespace sprint0.Classes
{
    public sealed class Esp32Controller : IController, IDisposable
    {
        private readonly Game1 game;
        private readonly SerialPort port;
        private int mask;

        private bool prevInventory;
        private bool prevItem1;
        private bool prevItem2;

        private bool prevUp;
        private bool prevDown;
        private bool prevLeft;
        private bool prevRight;

        public bool IsConnected
        {
            get { return port != null && port.IsOpen; }
        }

        public Esp32Controller(Game1 game, string portName = "/dev/cu.SLAB_USBtoUART")
        {
            this.game = game;

            try
            {
                port = new SerialPort(portName, 115200);
                port.ReadTimeout = 5;
                port.NewLine = "\n";
                port.Open();
                System.Console.WriteLine("[Esp32Controller] Connected on " + portName);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("[Esp32Controller] Failed to open port: " + ex.Message);
            }
        }

        public void Update()
        {
            if (IsConnected)
            {
                try
                {
                    while (port.BytesToRead > 0)
                    {
                        string line = port.ReadLine().Trim();
                        if (int.TryParse(line, out int value))
                        {
                            mask = value;
                        }
                    }
                }
                catch (TimeoutException)
                {
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("[Esp32Controller] Read error: " + ex.Message);
                }
            }

            bool up        = (mask & (1 << 0)) != 0;
            bool down      = (mask & (1 << 1)) != 0;
            bool left      = (mask & (1 << 2)) != 0;
            bool right     = (mask & (1 << 3)) != 0;
            bool inventory = (mask & (1 << 4)) != 0;
            bool item1     = (mask & (1 << 5)) != 0;
            bool item2     = (mask & (1 << 6)) != 0;

            bool inventoryOpen = (game.currentState == Game1.GameState.Inventory);

            if (game.currentState == Game1.GameState.GameOver ||
                game.currentState == Game1.GameState.Win)
            {
                prevInventory = inventory;
                prevItem1 = item1;
                prevItem2 = item2;

                prevUp = up;
                prevDown = down;
                prevLeft = left;
                prevRight = right;
                return;
            }

            if (inventoryOpen)
            {
                if (up && !prevUp)
                {
                    game.NavigateInventory(InventoryNavigateCommand.Direction.Up);
                }
                if (down && !prevDown)
                {
                    game.NavigateInventory(InventoryNavigateCommand.Direction.Down);
                }
                if (left && !prevLeft)
                {
                    game.NavigateInventory(InventoryNavigateCommand.Direction.Left);
                }
                if (right && !prevRight)
                {
                    game.NavigateInventory(InventoryNavigateCommand.Direction.Right);
                }

                if (inventory && !prevInventory)
                {
                    game.currentState = Game1.GameState.Gameplay;
                }

                prevUp = up;
                prevDown = down;
                prevLeft = left;
                prevRight = right;
                prevInventory = inventory;
                prevItem1 = item1;
                prevItem2 = item2;
                return;
            }

            if (game.currentState == Game1.GameState.Gameplay)
            {
                if (inventory && !prevInventory)
                {
                    new OpenInventoryCommand(game).Execute();
                }

                if (item1 && !prevItem1)
                {
                    new AttackCommand(game.link).Execute();
                }

                if (item2 && !prevItem2)
                {
                    new UseItemCommand(game.link, 2).Execute();
                }

                if (!game.KeyboardMovementActive)
                {
                    if (left)
                    {
                        new MoveLeftCommand(game.link).Execute();
                    }
                    if (right)
                    {
                        new MoveRightCommand(game.link).Execute();
                    }
                    if (up)
                    {
                        new MoveUpCommand(game.link).Execute();
                    }
                    if (down)
                    {
                        new MoveDownCommand(game.link).Execute();
                    }
                }
            }

            prevInventory = inventory;
            prevItem1 = item1;
            prevItem2 = item2;

            prevUp = up;
            prevDown = down;
            prevLeft = left;
            prevRight = right;
            return;
        }


        public void SendWin()
        {
            SendCommand("WIN");
        }

        public void SendLose()
        {
            SendCommand("LOSE");
        }

        private void SendCommand(string cmd)
        {
            if (IsConnected)
            {
                try
                {
                    port.WriteLine(cmd);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("[Esp32Controller] Write error: " + ex.Message);
                }
            }
        }

        public void Dispose()
        {
            if (IsConnected)
            {
                port.Close();
            }
        }
    }
}
