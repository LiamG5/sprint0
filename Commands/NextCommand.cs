using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class NextCommand : ICommand
    {
        private readonly ICarousel carousel;
        public NextCommand(ICarousel carousel) { this.carousel = carousel; }
        public void Execute() => carousel.Next();
    }
}
