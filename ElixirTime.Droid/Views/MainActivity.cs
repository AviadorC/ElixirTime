using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using ElixirTime.Data;
using ElixirTime.Service.Model;
using System;
using System.ComponentModel;

namespace ElixirTime.Views
{
    [Activity]
    public class MainActivity : AppCompatActivity
    {
        private RelativeLayout root;

        private AppCompatSpinner sourceBankSpinner;

        private AppCompatSpinner targetBankSpinner;

        private Button timePickerButton;

        private Button calculateButton;

        private EditText sourceIbanEditText;

        private EditText targetIbanEditText;

        private MainViewModel viewModel;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            root = FindViewById<RelativeLayout>(Resource.Id.root);
            root.RequestFocus();

            sourceBankSpinner = FindViewById<AppCompatSpinner>(Resource.Id.sourceBankSpinner);
            targetBankSpinner = FindViewById<AppCompatSpinner>(Resource.Id.targetBankSpinner);
            sourceBankSpinner.ItemSelected += SourceBankSpinner_ItemSelected;
            targetBankSpinner.ItemSelected += TargetBankSpinner_ItemSelected;

            timePickerButton = FindViewById<Button>(Resource.Id.timePickerButton);
            timePickerButton.Click += TimePickerButton_Click;

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            calculateButton.Click += CalculateButton_Click;

            sourceIbanEditText = FindViewById<EditText>(Resource.Id.sourceBankIbanEdit);
            targetIbanEditText = FindViewById<EditText>(Resource.Id.targetBankIbanEdit);
            sourceIbanEditText.TextChanged += IbanEditText_TextChanged;
            targetIbanEditText.TextChanged += IbanEditText_TextChanged;

            viewModel = new MainViewModel();
            viewModel.PropertyChanged += Vm_PropertyChanged;
            viewModel.Initialize();

            SetSendTime(DateTime.Now);
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Banks":
                    PopulateSpinners();
                    break;
                case "BankInvalid":
                    new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle(Resources.GetString(Resource.String.bankInvalidTitle))
                        .SetMessage(Resources.GetString(Resource.String.bankInvalidMessage))
                        .Show();
                    break;
            }
        }

        private void PopulateSpinners()
        {
            var bankSpinnerAdapter = new ArrayAdapter<BankModel>(
                this,
                Android.Resource.Layout.SimpleSpinnerItem,
                viewModel.Banks);
            bankSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            sourceBankSpinner.Adapter = bankSpinnerAdapter;
            targetBankSpinner.Adapter = bankSpinnerAdapter;
        }

        private void TargetBankSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            viewModel.Target = viewModel.Banks[e.Position];
        }

        private void SourceBankSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            viewModel.Source = viewModel.Banks[e.Position];
        }

        private void IbanEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var properSpinner = sender == sourceIbanEditText ? sourceBankSpinner : targetBankSpinner;

            properSpinner.Enabled = string.IsNullOrEmpty(e.Text.ToString());
        }

        private async void CalculateButton_Click(object sender, EventArgs e)
        {
            var result =  await viewModel.CalculateTransfer(
                !string.IsNullOrEmpty(sourceIbanEditText.Text) ? sourceIbanEditText.Text : null,
                !string.IsNullOrEmpty(targetIbanEditText.Text) ? targetIbanEditText.Text : null);
            if(result != null && result.HasValue)
            {
                Intent intent = new Intent(this, typeof(ResultActivity));
                intent.PutExtra("sessionTime", result.Value.ToString("HH:mm"));
                StartActivity(intent);
            }
        }

        private void TimePickerButton_Click(object sender, EventArgs e)
        {
            var timePicker = new TimePickerDialog(
                this, 
                TimePickerCallback, 
                DateTime.Now.Hour, 
                DateTime.Now.Minute, 
                true);

            timePicker.Show();
        }

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            SetSendTime(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, e.HourOfDay, e.Minute, 0));
        }

        private void SetSendTime(DateTime time)
        {
            viewModel.SendTime = time;
            timePickerButton.Text = string.Format(
                Resources.GetString(Resource.String.sendTimeText),
                time.ToString("HH:mm"));
        }
    }
}

