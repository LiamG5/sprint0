using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class UseItemCommand : ICommand
    {
        private readonly Link link;
        private readonly int itemSlot;
        public UseItemCommand(Link link, int itemSlot) 
        { 
            this.link = link;
            this.itemSlot = itemSlot;
        }
        public void Execute() 
        {
            link.UseItemInSlot(itemSlot);
        }
    }
}