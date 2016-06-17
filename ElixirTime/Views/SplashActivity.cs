using Android.App;
using Android.Content;

namespace ElixirTime.Views
{
    [Activity(Theme = "@style/ElixirTime.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}