using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using OpenTK;
using Symcol.Core.Graphics.Containers;

namespace touhou.sharp.Game.Characters.Pieces
{
    public class Totem : SymcolContainer
    {
        public readonly Character ParentCharacter;
        private readonly THSharpPlayfield vitaruPlayfield;

        public float StartAngle { get; set; } = 0;

        public Totem(Character vitaruCharacter, THSharpPlayfield playfield)
        {
            ParentCharacter = vitaruCharacter;
            vitaruPlayfield = playfield;
        }

        public void Shoot()
        {
            DrawableSeekingBullet s;
            vitaruPlayfield.GameField.Add(s = new DrawableSeekingBullet(new SeekingBullet
            {
                Team = ParentCharacter.Team,
                BulletSpeed = 0.8f,
                BulletDamage = 5,
                ColorOverride = ParentCharacter.PrimaryColor,
                StartAngle = StartAngle,
            }, vitaruPlayfield));
            s.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), s));
        }

        protected override void LoadComplete()
        {
            Masking = true;
            Size = new Vector2(6);
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            BorderThickness = 2;
            BorderColour = ParentCharacter.PrimaryColor;
            CornerRadius = 3;
            Child= new Box
            {
                RelativeSizeAxes = Axes.Both
            };
        }
    }
}
