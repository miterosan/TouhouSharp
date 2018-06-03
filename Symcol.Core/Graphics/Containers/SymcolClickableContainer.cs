using osu.Framework.Graphics.Containers;

namespace Symcol.Core.Graphics.Containers
{
    public class SymcolClickableContainer : ClickableContainer
    {
        /// <summary>
        /// Delete this fucking object!
        /// </summary>
        public void Delete()
        {
            if (Parent is Container p)
                p.Remove(this);

            Dispose();
        }
    }
}
