using Basta.GUI.Validator;
using Database.DAO;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Basta.GUI.Login.SignUp {
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp: Window {
        Dictionary<Control, bool> elementsValid;
        public SignUp() {
            InitializeComponent();
            InitializeValidator();
        }

        private void SignUpButtonClicked( object sender, RoutedEventArgs e ) {
            systemLabel.Content = "";
            if ( IsAllInputValid() ) {
                registerPlayer();
            } else {
                systemLabel.Content = Basta.Properties.Resource.SystemInvalidData;
            }
        }

        private void registerPlayer() {
            Player player = new Player();
            player.Age = (int) signUpComboBoxAge.SelectedValue;
            player.Country = signUpCountryTextBox.Text.Trim();
            player.Name = signUpFirstNameTextBox.Text.Trim() + " " + signUpLastNameTextBox.Text.Trim();
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Username = signUpUsernameTextBox.Text.Trim();
            accessAccount.Email = signUpEmailTextBox.Text.Trim();
            accessAccount.Password = passwordTextBox.Password.Trim();
            accessAccount.AccountState = AccountState.FREE;
            player.AccessAccount = accessAccount;
            PlayerDAO playerDAO = new PlayerDAO();
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            if ( accessAccountDAO.verifyExistingEmail( accessAccount.Email ) ) {
                systemLabel.Content = Basta.Properties.Resource.SystemExistingEmail;
            } else if ( accessAccountDAO.verifyExistingUsername( accessAccount.Username ) ) {
                systemLabel.Content = Basta.Properties.Resource.SystemExistingUsername;
            } else {
                playerDAO.AddPlayerAccount( player );
                Close();
            }
        }

        private void SignUpComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<int> data = new List<int>();
            for ( int i = 4; i < 120; i++ ) {
                data.Add( i );
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 12;
        }

        private void nameTextChanged( object sender, TextChangedEventArgs e ) {
            if ( InputValidator.IsValidName( signUpFirstNameTextBox.Text ) ) {
                signUpFirstNameTextBox.BorderBrush = Brushes.Green;
                signUpFirstNameTextBox.BorderThickness = new Thickness( 0, 0, 0, 1 );
                elementsValid[signUpFirstNameTextBox] = true;
            } else {
                signUpFirstNameTextBox.BorderBrush = Brushes.Red;
                signUpFirstNameTextBox.BorderThickness = new Thickness( 0, 0, 0, 3 );
                elementsValid[signUpFirstNameTextBox] = false;

            }
        }

        private void lastNameTextChanged( object sender, TextChangedEventArgs e ) {
            if ( InputValidator.IsValidName( signUpLastNameTextBox.Text ) ) {
                signUpLastNameTextBox.BorderBrush = Brushes.Green;
                signUpLastNameTextBox.BorderThickness = new Thickness( 0, 0, 0, 1 );
                elementsValid[signUpLastNameTextBox] = true;
            } else {
                signUpLastNameTextBox.BorderBrush = Brushes.Red;
                signUpLastNameTextBox.BorderThickness = new Thickness( 0, 0, 0, 3 );
                elementsValid[signUpLastNameTextBox] = false;
            }
        }

        private void countryTextChanged( object sender, TextChangedEventArgs e ) {
            if ( InputValidator.IsValidCountry( signUpCountryTextBox.Text ) ) {
                signUpCountryTextBox.BorderBrush = Brushes.Green;
                signUpCountryTextBox.BorderThickness = new Thickness( 0, 0, 0, 1 );
                elementsValid[signUpCountryTextBox] = true;
            } else {
                signUpCountryTextBox.BorderBrush = Brushes.Red;
                signUpCountryTextBox.BorderThickness = new Thickness( 0, 0, 0, 3 );
                elementsValid[signUpCountryTextBox] = false;
            }
        }

        private void emailTextChanged( object sender, TextChangedEventArgs e ) {
            if ( InputValidator.IsValidEmail( signUpEmailTextBox.Text ) ) {
                signUpEmailTextBox.BorderBrush = Brushes.Green;
                signUpEmailTextBox.BorderThickness = new Thickness( 0, 0, 0, 1 );
                elementsValid[signUpEmailTextBox] = true;
            } else {
                signUpEmailTextBox.BorderBrush = Brushes.Red;
                signUpEmailTextBox.BorderThickness = new Thickness( 0, 0, 0, 3 );
                elementsValid[signUpEmailTextBox] = false;
            }
        }

        private void usernameTextChanged( object sender, TextChangedEventArgs e ) {
            if ( InputValidator.IsValidUsername( signUpUsernameTextBox.Text ) ) {
                signUpUsernameTextBox.BorderBrush = Brushes.Green;
                signUpUsernameTextBox.BorderThickness = new Thickness( 0, 0, 0, 1 );
                elementsValid[signUpUsernameTextBox] = true;
            } else {
                signUpUsernameTextBox.BorderBrush = Brushes.Red;
                signUpUsernameTextBox.BorderThickness = new Thickness( 0, 0, 0, 3 );
                elementsValid[signUpUsernameTextBox] = false;
            }
        }

        private void passwordTextChanged( object sender, RoutedEventArgs e ) {
            if ( InputValidator.IsValidPassword( passwordTextBox.Password ) ) {
                passwordTextBox.BorderBrush = Brushes.Green;
                passwordTextBox.BorderThickness = new Thickness( 0, 0, 0, 1 );
                elementsValid[passwordTextBox] = true;
            } else {
                passwordTextBox.BorderBrush = Brushes.Red;
                passwordTextBox.BorderThickness = new Thickness( 0, 0, 0, 3 );
                elementsValid[passwordTextBox] = false;
            }
        }

        private void InitializeValidator() {
            elementsValid = new Dictionary<Control, bool>();
            elementsValid.Add( passwordTextBox, false );
            elementsValid.Add( signUpCountryTextBox, false );
            elementsValid.Add( signUpEmailTextBox, false );
            elementsValid.Add( signUpFirstNameTextBox, false );
            elementsValid.Add( signUpLastNameTextBox, false );
            elementsValid.Add( signUpUsernameTextBox, false );
        }

        private bool IsAllInputValid() {
            bool isAllElementsValid = true;
            foreach ( var element in elementsValid ) {
                if ( element.Value == false ) {
                    isAllElementsValid = false;
                    break;
                }
            }
            return isAllElementsValid;
        }

    }
}
