using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class PrevCommand : ICommand
    {
        private readonly ICarousel carousel;
        public PrevCommand(ICarousel carousel) { this.carousel = carousel; }
        public void Execute() => carousel.Prev();
    }
}
