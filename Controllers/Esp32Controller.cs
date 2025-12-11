using System;
using System.IO.Ports;
using sprint0.Commands;
using sprint0.PlayerStates;

namespace sprint0.Classes
{
    public sealed class Esp32Controller : BaseController, IDisposable
    {
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
            : base(game, () => game.currentState == Game1.GameState.Inventory)
        {
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

        public override void Update()
        {
            ReadSerialInput();
            
            base.Update();
            
            UpdatePreviousStates();
        }

        private void ReadSerialInput()
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
        }

        private void UpdatePreviousStates()
        {
            bool up = (mask & (1 << 0)) != 0;
            bool down = (mask & (1 << 1)) != 0;
            bool left = (mask & (1 << 2)) != 0;
            bool right = (mask & (1 << 3)) != 0;
            bool inventory = (mask & (1 << 4)) != 0;
            bool item1 = (mask & (1 << 5)) != 0;
            bool item2 = (mask & (1 << 6)) != 0;

            prevUp = up;
            prevDown = down;
            prevLeft = left;
            prevRight = right;
            prevInventory = inventory;
            prevItem1 = item1;
            prevItem2 = item2;
        }

        private bool GetButtonState(int bit)
        {
            return (mask & (1 << bit)) != 0;
        }

        protected override void HandleGameOverOrWinState()
        {
            bool inventory = GetButtonState(4);
            
            if (inventory && !prevInventory)
            {
                new ResetCommand(game).Execute();
            }
        }

        protected override void HandleInventoryNavigation()
        {
            bool up = GetButtonState(0);
            bool down = GetButtonState(1);
            bool left = GetButtonState(2);
            bool right = GetButtonState(3);
            bool inventory = GetButtonState(4);
            
            if (up && !prevUp)
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Up);
            }
            if (down && !prevDown)
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Down);
            }
            if (left && !prevLeft)
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Left);
            }
            if (right && !prevRight)
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Right);
            }
            
            if (inventory && !prevInventory)
            {
                CloseInventory();
            }
        }

        protected override void HandleGameplayInput()
        {
            bool inventory = GetButtonState(4);
            bool item1 = GetButtonState(5);
            bool item2 = GetButtonState(6);
            bool left = GetButtonState(2);
            bool right = GetButtonState(3);
            bool up = GetButtonState(0);
            bool down = GetButtonState(1);

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
        
        public void SendWin()
        {
            SendCommand("WIN");
        }

        public void SendLose()
        {
            SendCommand("LOSE");
        }

        public void SendOn()
        {
            SendCommand("ON");
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
