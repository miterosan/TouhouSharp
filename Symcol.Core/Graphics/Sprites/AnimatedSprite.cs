using osu.Framework.Graphics.Textures;
using System;
using System.Linq;

namespace Symcol.Core.Graphics.Sprites
{
    /// <summary>
    /// A Sprite that will cycle through multiple Textures after a fixed time (Speed)
    /// </summary>
    public class AnimatedSprite : SymcolSprite
    {
        /// <summary>
        /// The list of Textures we will cycle through
        /// </summary>
        public Texture[] Textures { get; set; }

        /// <summary>
        /// Length of time each sprite is shown for
        /// </summary>
        public double UpdateRate { get; set; } = 250;

        /// <summary>
        /// Called when we cycle back to the first texture (technically just before it by one line)
        /// </summary>
        public Action OnAnimationRestart;

        private double lastUpdate = double.MaxValue;

        /// <summary>
        /// Reset to first Texture
        /// </summary>
        public void Reset()
        {
            Texture = Textures.First();
            lastUpdate = Time.Current;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Texture = Textures.First();
            lastUpdate = Time.Current;
        }

        protected override void Update()
        {
            base.Update();

            if (lastUpdate + UpdateRate <= Time.Current)
            {
                bool next = false;
                foreach (Texture texture in Textures)
                {
                    if (Texture == texture)
                        next = true;
                    else if (next)
                    {
                        Texture = texture;
                        lastUpdate = Time.Current;
                        break;
                    }

                    //If this texture is last better cycle back to the first!
                    if (Texture == texture && texture == Textures.Last())
                    {
                        OnAnimationRestart?.Invoke();
                        Texture = Textures.First();
                        lastUpdate = Time.Current;
                        break;
                    }
                }
            }
                
        }
    }
}
