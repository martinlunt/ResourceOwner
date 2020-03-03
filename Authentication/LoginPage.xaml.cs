using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IdentityModel.Client;

namespace Authentication
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
            CancelButton.Click += CancelButton_Click;
            OkButton.Click += OkButton_Click;
            this.ContentRendered += LoginPage_ContentRendered;
        }

        private void LoginPage_ContentRendered(object sender, EventArgs e)
        {
            UserIdTxtBox.Focus();
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            TokenClient client = new TokenClient(Constants.TokenEndpoint, "resourceownerclient", "secret");
            try
            {
                TokenResponse response = await client.RequestResourceOwnerPasswordAsync(UserIdTxtBox.Text, UserPwdBox.Password, "unifi");
                if (response.IsError)
                {
                    ErrorMessage.Text = "User id or password are not correct!";
                    ErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    Accestoken = response.AccessToken;
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(this, "Server is not available", "Login Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public string Accestoken { get; set; }


    }
}
