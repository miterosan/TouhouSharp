using Symcol.Core.Graphics.Containers;

namespace touhou.sharp.Game.Gameplay.Projectiles.DrawableProjectiles
{
    public abstract class DrawableProjectile<P> : SymcolContainer
        where P : Projectile
    {
        public P Projectile { get; }

        protected DrawableProjectile(P p)
        {
            Projectile = p;
        }
    }
}
