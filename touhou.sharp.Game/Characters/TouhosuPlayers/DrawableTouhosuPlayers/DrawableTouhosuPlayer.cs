using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using touhou.sharp.Game.Characters.VitaruPlayers.DrawableVitaruPlayers;
using touhou.sharp.Game.Gameplay;

namespace touhou.sharp.Game.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableTouhosuPlayer : DrawableTHSharpPlayer
    {
        public readonly TouhosuPlayer TouhosuPlayer;

        public double Energy { get; protected set; }

        public Action<THSharpAction> Spell;

        protected bool SpellActive { get; set; }

        protected double SpellStartTime { get; set; } = double.MaxValue;

        protected double SpellDeActivateTime { get; set; } = double.MinValue;

        protected double SpellEndTime { get; set; } = double.MinValue;

        protected bool EnergyHacks { get; private set; }

        //reset after healing is done
        public double EnergyGainMultiplier = 1;

        public DrawableTouhosuPlayer(THSharpPlayfield playfield, TouhosuPlayer player, THSharpNetworkingClientHandler vitaruNetworkingClientHandler) : base(playfield, player, vitaruNetworkingClientHandler)
        {
            TouhosuPlayer = player;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Puppet)
                DebugToolkit.GeneralDebugItems.Add(new DebugAction(() => { EnergyHacks = !EnergyHacks; }) { Text = "Energy Hacks" });
        }

        protected override void Update()
        {
            base.Update();

            if (EnergyHacks)
                Energy = TouhosuPlayer.MaxEnergy;

            SpellUpdate();
        }

        #region Spell Handling
        protected virtual bool SpellActivate(THSharpAction action)
        {
            if (Energy >= TouhosuPlayer.EnergyCost && !SpellActive)
            {
                if (TouhosuPlayer.EnergyDrainRate == 0)
                    Energy -= TouhosuPlayer.EnergyCost;

                SpellActive = true;
                SpellStartTime = Time.Current;
                Spell?.Invoke(action);
                return true;
            }
            else
                return false;
        }

        protected virtual void SpellDeactivate(THSharpAction action)
        {
            SpellActive = false;
        }

        protected virtual void SpellUpdate()
        {
            if (HealingBullets.Count > 0)
            {
                double fallOff = 1;

                for (int i = 0; i < HealingBullets.Count - 1; i++)
                    fallOff *= HEALING_FALL_OFF;

                foreach (HealingBullet HealingBullet in HealingBullets)
                    Energy = Math.Min(((Clock.ElapsedFrameTime / 500) * (GetBulletHealingMultiplier(HealingBullet.EdgeDistance) * fallOff)) + Energy, TouhosuPlayer.MaxEnergy);
            }

            if (Energy <= 0)
            {
                Energy = 0;
                SpellActive = false;
            }
        }
        #endregion

        protected override void Pressed(THSharpAction action)
        {
            base.Pressed(action);

            if (action == THSharpAction.Spell)
                SpellActivate(action);
        }

        protected override void Released(THSharpAction action)
        {
            base.Released(action);

            if (action == THSharpAction.Spell)
                SpellDeactivate(action);
        }

        //TODO: I feel like this TODO should be obvious (figure out this bindable thing)
        public static DrawableTouhosuPlayer GetDrawableTouhosuPlayer(THSharpPlayfield playfield, string name, THSharpNetworkingClientHandler vitaruNetworkingClientHandler, Bindable<int> bindableInt = null)
        {
            switch (name)
            {
                default:
                    return new DrawableTouhosuPlayer(playfield, TouhosuPlayer.GetTouhosuPlayer(name), vitaruNetworkingClientHandler);

                case "ReimuHakurei":
                    return new DrawableReimu(playfield, vitaruNetworkingClientHandler);
                case "RyukoyHakurei":
                    return new DrawableRyukoy(playfield, vitaruNetworkingClientHandler, bindableInt);
                case "TomajiHakurei":
                    return new DrawableTomaji(playfield, vitaruNetworkingClientHandler);

                case "SakuyaIzayoi":
                    return new DrawableSakuya(playfield, vitaruNetworkingClientHandler);
                case "RemiliaScarlet":
                    return new DrawableTouhosuPlayer(playfield, TouhosuPlayer.GetTouhosuPlayer(name), vitaruNetworkingClientHandler);
                case "FlandreScarlet":
                    return new DrawableTouhosuPlayer(playfield, TouhosuPlayer.GetTouhosuPlayer(name), vitaruNetworkingClientHandler);

                case "AliceLetrunce":
                    return new DrawableAlice(playfield, vitaruNetworkingClientHandler);
                case "VasterLetrunce":
                    return new DrawableTouhosuPlayer(playfield, TouhosuPlayer.GetTouhosuPlayer(name), vitaruNetworkingClientHandler);

                case "MarisaKirisame":
                    return new DrawableTouhosuPlayer(playfield, TouhosuPlayer.GetTouhosuPlayer(name), vitaruNetworkingClientHandler);
            }
        }
    }
}
