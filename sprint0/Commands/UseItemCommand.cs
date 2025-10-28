using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class UseItemCommand : ICommand
    {
        private readonly Link link;
        private readonly int itemNumber;
        public UseItemCommand(Link link, int itemNumber) 
        { 
            this.link = link;
            this.itemNumber = itemNumber;
        }
        public void Execute() 
        {
            switch (itemNumber)
            {
                case 1: link.UseItem1(); break;
                case 2: link.UseItem2(); break;
                case 3: link.UseItem3(); break;
            }
        }
    }
}