using System;
using System.ServiceModel;
using System.Windows;
using Basta.Properties;
using Utils;

namespace Basta.GUI.Login.ChangePassword {
    /// <summary>
    /// Lógica de interacción para ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow: Window {
        private string email;
        public ChangePasswordWindow( string email ) {
            this.email = email;
            InitializeComponent();
        }

        private void ClearData() {
            systemLabel.Content = "";
        }

        private bool isValidPassword( string email ) {
            bool isValidPassword = Validator.InputValidator.IsValidPassword( email );
            return isValidPassword;
        }

        private bool ChangePassword() {
            bool isPasswordChanged = false;
            Proxy.LoginServiceClient server = new Proxy.LoginServiceClient();
            try {
                isPasswordChanged = server.ChangePassword( email, Cryptography.SHA256_Hash( firstPasswordTextBox.Text.Trim() ) );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                }
            }
            return isPasswordChanged;
        }

        private void ChangeButtonClicked( object sender, RoutedEventArgs e ) {
            ClearData();
            if ( isValidPassword( firstPasswordTextBox.Text.Trim() ) ) {
                if ( firstPasswordTextBox.Text.Trim().Equals( secondPasswordTextBox.Text.Trim() ) ) {
                    if ( ChangePassword() ) {
                        Close();
                    } else {
                        systemLabel.Content = Resource.SystemPasswordChangingError;
                    }
                } else {
                    systemLabel.Content = Resource.SystemPasswordNoMatch;
                }
            } else {
                systemLabel.Content = Resource.SystemInvalidData;
            }

        }
    }
}
