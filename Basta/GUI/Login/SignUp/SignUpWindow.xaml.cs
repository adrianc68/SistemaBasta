
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Basta.GUI.Login.SignUp {
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Window {
        public SignUp() {
            InitializeComponent();
        }

        private void SignUpButtonClicked(object sender, RoutedEventArgs e) {
            Console.WriteLine( signUpFirstName.Text.Trim() );
            Console.WriteLine( signUpLastName.Text.Trim() );
            Console.WriteLine( signUpEmail.Text.Trim() );
            Console.WriteLine( signUpCountry.Text.Trim() );
            Console.WriteLine( signUpUsername.Text.Trim() );
            Console.WriteLine( signUpComboBoxAge.Text.Trim() );
            Console.WriteLine( passwordTextField.Password.Trim() );
            /*
            Player player = new Player();
            player.Age = (int) signUpComboBoxAge.SelectedValue;
            player.Country = signUpCountry.Text.Trim();
            player.Name = signUpFirstName.Text.Trim() + signUpLastName.Text.Trim();
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Email = signUpEmail.Text.Trim();
            accessAccount.Password = passwordTextField.Password.Trim();

            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            playerDAO.AddPlayerAccount(player);
            */

        }

        private void SignUpComboBoxLoaded(object sender, RoutedEventArgs e) {
            List<int> data = new List<int>();
            for(int i = 4; i < 120; i++) {
                data.Add(i);
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 12;
        }

        private void SignUpComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
/* var selectedComboItem = sender as ComboBox;
            int item = (int) selectedComboItem.SelectedItem;
            MessageBox.Show( item.ToString() );
*/
        }
    }
}
