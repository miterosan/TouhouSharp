using touhou.sharp.Game.Screens;

namespace touhou.sharp.Game
{
    public class THSharpGame : THSharpBaseGame
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            var homeScreen = new HomeScreen();

            Add(homeScreen);

            homeScreen.Exited += _ => Scheduler.AddDelayed(Exit, 500);
        }
    }
}
