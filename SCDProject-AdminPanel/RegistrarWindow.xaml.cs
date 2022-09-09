using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using URSBackend.Models;
using URSBackend.Models.ViewModels;

namespace SCDProject_AdminPanel
{

    /// <summary>
    /// Interaction logic for RegistrarWindow.xaml
    /// </summary>
    public partial class RegistrarWindow : Window
    {
        List<JoiningRequestVM> joinings;
        HttpClient client;
        public RegistrarWindow()
        {
            client = new HttpClient(RequestHandler.handler);
            InitializeComponent();
            joinings = JsonConvert.DeserializeObject<List<JoiningRequestVM>>(RequestHandler.GetRecord("requests/get-all").Result);
            requestGrid.ItemsSource = joinings;
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            JoiningRequestVM requestVM = requestGrid.SelectedItem as JoiningRequestVM;
            if (requestVM != null)
            {
                var response = RequestHandler.UpdateRecord($"requests/approve-request/{requestVM.Id}", requestVM).Result;

                JoiningRequestVM fullRequest = JsonConvert.DeserializeObject<JoiningRequestVM>(response);
                requestVM.CopyFrom(fullRequest);
                requestGrid.ItemsSource = null;
                requestGrid.ItemsSource = joinings;
            }
            
            
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            JoiningRequestVM requestVM = requestGrid.SelectedItem as JoiningRequestVM;
            if (requestVM != null)
            {
                var response = RequestHandler.UpdateRecord($"requests/reject-request/{requestVM.Id}", requestVM).Result;

                JoiningRequestVM fullRequest = JsonConvert.DeserializeObject<JoiningRequestVM>(response);
                requestVM.CopyFrom(fullRequest);
                requestGrid.ItemsSource = null;
                requestGrid.ItemsSource = joinings;
            }
        }
    }
}
