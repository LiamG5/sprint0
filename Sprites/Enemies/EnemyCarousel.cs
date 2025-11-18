using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Sprites;

namespace sprint0.Sprites
{
    public class EnemyCarousel : ICarousel
    {
        private EnemySpriteFactory enemyFactory;
        private SpriteBatch spriteBatch;
        private ISprite currentEnemy;
        private int currentIndex = 0;
        private EnemySpriteFactory.EnemyType[] enemyTypes = {
            EnemySpriteFactory.EnemyType.BladeTrap,
            EnemySpriteFactory.EnemyType.Gel,
            EnemySpriteFactory.EnemyType.Keese,
            EnemySpriteFactory.EnemyType.RedGoriya,
            EnemySpriteFactory.EnemyType.Stalfos,
            EnemySpriteFactory.EnemyType.Wallmaster
        };

        public EnemyCarousel(EnemySpriteFactory enemyFactory, SpriteBatch spriteBatch)
        {
            this.enemyFactory = enemyFactory;
            this.spriteBatch = spriteBatch;
            currentEnemy = enemyFactory.SpawnBladeTrap();
        }

        public void Next()
        {
            currentIndex = (currentIndex + 1) % enemyTypes.Length;
            UpdateCurrentEnemy();
        }

        public void Prev()
        {
            currentIndex = (currentIndex - 1 + enemyTypes.Length) % enemyTypes.Length;
            UpdateCurrentEnemy();
        }

        private void UpdateCurrentEnemy()
        {
            switch (enemyTypes[currentIndex])
            {
                case EnemySpriteFactory.EnemyType.BladeTrap:
                    currentEnemy = enemyFactory.SpawnBladeTrap();
                    break;
                case EnemySpriteFactory.EnemyType.Gel:
                    currentEnemy = enemyFactory.SpawnGel();
                    break;
                case EnemySpriteFactory.EnemyType.Keese:
                    currentEnemy = enemyFactory.SpawnKeese();
                    break;
                case EnemySpriteFactory.EnemyType.RedGoriya:
                    currentEnemy = enemyFactory.SpawnRedGoriya();
                    break;
                case EnemySpriteFactory.EnemyType.Stalfos:
                    currentEnemy = enemyFactory.SpawnStalfos();
                    break;
                case EnemySpriteFactory.EnemyType.Wallmaster:
                    currentEnemy = enemyFactory.SpawnWallmaster();
                    break;
            }
        }

        public ISprite GetCurrentEnemy()
        {
            return currentEnemy;
        }
    }
}
