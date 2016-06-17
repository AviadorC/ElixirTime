using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace ElixirTime.Views
{
    [Activity]
    public class ResultActivity : AppCompatActivity
    {
        private TextView resultText;

        private Button recalculateButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Result);

            resultText = FindViewById<TextView>(Resource.Id.resultText);
            var extras =  this.Intent.Extras;
            resultText.Text = string.Format(Resources.GetString(Resource.String.sessionTime) ,extras.GetString("sessionTime"));

            recalculateButton = FindViewById<Button>(Resource.Id.recalculateButton);
            recalculateButton.Click += RecalculateButton_Click;
        }

        private void RecalculateButton_Click(object sender, System.EventArgs e)
        {
            Finish();
        }
    }
}