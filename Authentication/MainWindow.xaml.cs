using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

namespace Authentication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Loaded += MeLoaded;
        }

        private async void MeLoaded(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage {Owner = this};
            if (loginPage.ShowDialog() == true)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    //Set access token
                    httpClient.SetBearerToken(loginPage.Accestoken);
                    //
                    httpClient.DefaultRequestHeaders.Remove("Accept");
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata.metadata=minimal");
                    string url = "https://"+Constants.Host + Constants.BasePath + "/folders";
                    //execute web api call
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        Console.WriteLine(responseMessage.ToString());
                        tbData.Text = responseMessage.ToString();
                        return;
                    }
                    //Get the HTTP content
                    string responseBody = await responseMessage.Content.ReadAsStringAsync();
                    //Iterate over all properties that were supplied as part of the JSON
                    JObject response = JObject.Parse(responseBody);
                    JArray folders = response["value"] as JArray;
                    StringBuilder sb = new StringBuilder();
                    foreach (JObject folder in folders)
                    {
                        foreach (JProperty prop in folder.Properties())
                            sb.AppendLine($"{prop.Name} = {prop.Value}");
                    }
                    //Set result on screen
                    tbData.Text = sb.ToString();
                }
                catch (Exception ex)
                {
                    tbData.Text = ex.ToString();
                }
            }
            else
                Close();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
