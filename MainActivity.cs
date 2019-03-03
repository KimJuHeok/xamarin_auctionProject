using Android.App;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;
using System;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Android.Gms.Auth.Api;
using Android.Support.V7.App;
using Android.Gms.Common;
using Android.Content;
using Android.Runtime;

namespace AuctionTest
{
    [Activity(Label = "AuctionTest", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        private GoogleApiClient mGoogleApiClient;
        public int SIGN_IN_ID = 9001;
        public static FirebaseApp app;
        FirebaseAuth auth;
        private TextView Accountinfo = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.main);
            base.OnCreate(savedInstanceState);
            Button SignInBtn = FindViewById<Button>(Resource.Id.myButton);
            Accountinfo = FindViewById<TextView>(Resource.Id.AccountInfo);
            Button SignOutBtn = FindViewById<Button>(Resource.Id.logOutButton);
            SignInBtn.Click += OnSignInClicked;
            SignOutBtn.Click += OnSignOutclicked;

            InitFirebaseAuth();
            ConfigureGoogleLogin();
        }

        private void OnSignOutclicked(object sender, EventArgs e)
        {

        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:381403326020:android:9da0b5584b8d51e6")
                .SetApiKey("AIzaSyCnJtwu4a6VVN28g35QPui7xYotw36CjnE")
                .Build();

            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            auth = FirebaseAuth.GetInstance(app);
        }

        private void OnSignInClicked(object sender, EventArgs e)
        {
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            //SetResult( Result.FirstUser, signInIntent);
            StartActivityForResult(signInIntent, SIGN_IN_ID);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == SIGN_IN_ID)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                HandlessSignInResult(result);
            }
        }

        private void HandlessSignInResult(GoogleSignInResult result)
        {
            if(result.IsSuccess)
            {
                var accountDetails = result.SignInAccount;
                Accountinfo.Text = accountDetails.Id;


            }
        }

        private void ConfigureGoogleLogin()
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                .RequestEmail()
                                                .Build();

            mGoogleApiClient = new GoogleApiClient.Builder(this)
                                        .EnableAutoManage(this, this)
                                        .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                                        .AddConnectionCallbacks(this)
                                                  .Build();
           
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            //throw new NotImplementedException();
        }

        public void OnConnected(Bundle connectionHint)
        {
            //throw new NotImplementedException();
        }

        public void OnConnectionSuspended(int cause)
        {
            //throw new NotImplementedException();
        }
    }

    internal interface IConnectionCallbacks
    {
    }
}