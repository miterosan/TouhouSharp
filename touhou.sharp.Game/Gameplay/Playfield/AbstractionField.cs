using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using OpenTK;
using Symcol.Core.Graphics.Containers;
using touhou.sharp.Game.Gameplay.Characters;
using touhou.sharp.Game.Gameplay.Characters.VitaruPlayers.DrawableVitaruPlayers;

namespace touhou.sharp.Game.Gameplay.Playfield
{
    public class AbstractionField : SymcolContainer
    {
        public readonly Bindable<int> AbstractionLevel;

        public SymcolContainer Current = new SymcolContainer { RelativeSizeAxes = Axes.Both, Name = "Current" };
        public SymcolContainer QuarterAbstraction = new SymcolContainer { RelativeSizeAxes = Axes.Both, Alpha = 0.5f, Name = "QuarterAbstraction" };
        public SymcolContainer HalfAbstraction = new SymcolContainer { RelativeSizeAxes = Axes.Both, Alpha = 0.25f, Name = "HalfAbstraction" };
        public SymcolContainer FullAbstraction = new SymcolContainer { RelativeSizeAxes = Axes.Both, Alpha = 0.125f, Name = "FullAbstraction" };

        public AbstractionField(Bindable<int> abstraction = null)
        {
            if (abstraction == null)
                abstraction = new Bindable<int> { Value = 0 };

            AbstractionLevel = abstraction;

            RelativeSizeAxes = Axes.Both;

            Name = "AbstractionField";

            Children = new Drawable[]
            {
                FullAbstraction.WithEffect(new GlowEffect
                {
                    Strength = 8f,
                    BlurSigma = new Vector2(16)
                }),
                HalfAbstraction.WithEffect(new GlowEffect
                {
                    Strength = 4f,
                    BlurSigma = new Vector2(8)
                }),
                QuarterAbstraction.WithEffect(new GlowEffect
                {
                    Strength = 2f,
                    BlurSigma = new Vector2(4)
                }),
                Current
            };

            AbstractionLevel.ValueChanged += (value) =>
            {
                List<Drawable> q = new List<Drawable>();
                List<Drawable> h = new List<Drawable>();
                List<Drawable> f = new List<Drawable>();

                foreach (Drawable draw in QuarterAbstraction)
                    q.Add(draw);

                foreach (Drawable draw in HalfAbstraction)
                    h.Add(draw);

                foreach (Drawable draw in FullAbstraction)
                    f.Add(draw);

                foreach (Drawable draw in q)
                {
                    QuarterAbstraction.Remove(draw);
                    Current.Add(draw);
                }

                foreach (Drawable draw in h)
                {
                    HalfAbstraction.Remove(draw);
                    Current.Add(draw);
                }

                foreach (Drawable draw in f)
                {
                    FullAbstraction.Remove(draw);
                    Current.Add(draw);
                }

                if (value >= 1)
                {
                    List<Drawable> quarter = new List<Drawable>();

                    foreach (Drawable draw in Current)
                    {
                        //if (draw is DrawableBullet bullet && bullet.Bullet.Abstraction < value)
                        //quarter.Add(bullet);
                        if (draw is DrawableTHSharpPlayer player && player.Abstraction < value)
                            quarter.Add(player);
                        if (draw is Enemy enemy && enemy.Abstraction < value)
                            quarter.Add(enemy);
                    }

                    foreach (Drawable draw in quarter)
                    {
                        Current.Remove(draw);
                        QuarterAbstraction.Add(draw);
                    }
                }

                if (value >= 2)
                {
                    List<Drawable> half = new List<Drawable>();

                    foreach (Drawable draw in QuarterAbstraction)
                    {
                        //if (draw is DrawableBullet bullet && bullet.Bullet.Abstraction < value - 1)
                        //half.Add(bullet);
                        if (draw is DrawableTHSharpPlayer player && player.Abstraction < value - 1)
                            half.Add(player);
                        if (draw is Enemy enemy && enemy.Abstraction < value - 1)
                            half.Add(enemy);
                    }

                    foreach (Drawable draw in half)
                    {
                        QuarterAbstraction.Remove(draw);
                        HalfAbstraction.Add(draw);
                    }
                }

                if (value >= 3)
                {
                    List<Drawable> full = new List<Drawable>();

                    foreach (Drawable draw in HalfAbstraction)
                    {
                        //if (draw is DrawableBullet bullet && bullet.Bullet.Abstraction < value - 2)
                        //full.Add(bullet);

                        if (draw is DrawableTHSharpPlayer player && player.Abstraction < value - 2)
                            full.Add(player);

                        if (draw is Enemy enemy && enemy.Abstraction < value - 2)
                            full.Add(enemy);
                    }

                    foreach (Drawable draw in full)
                    {
                        HalfAbstraction.Remove(draw);
                        FullAbstraction.Add(draw);
                    }
                }
            };
            AbstractionLevel.TriggerChange();
        }

        public new void Add(Drawable drawable)
        {
            Current.Add(drawable);
            AbstractionLevel.TriggerChange();
        }

        public new void Remove(Drawable drawable)
        {
            foreach (Drawable draw in Current)
                if (draw == drawable)
                {
                    Current.Remove(drawable);
                    return;
                }

            foreach (Drawable draw in QuarterAbstraction)
                if (draw == drawable)
                {
                    QuarterAbstraction.Remove(drawable);
                    return;
                }

            foreach (Drawable draw in HalfAbstraction)
                if (draw == drawable)
                {
                    HalfAbstraction.Remove(drawable);
                    return;
                }

            foreach (Drawable draw in FullAbstraction)
                if (draw == drawable)
                {
                    FullAbstraction.Remove(drawable);
                    return;
                }
        }
    }
}
