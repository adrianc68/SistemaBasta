using Database.DAO;
using Domain.Exceptions;
using EmailSender;
using System;
using System.Windows;
using Basta.Properties;
using Basta.GUI.Login.ChangePassword;

namespace Basta.GUI.Login.RecoveryPassword {
    /// <summary>
    /// Lógica de interacción para RecoveryPasswordWindow.xaml
    /// </summary>
    public partial class RecoveryPasswordWindow: Window {
        private string code;
        public RecoveryPasswordWindow() {
            InitializeComponent();
        }

        private void SendCodeButtonClicked( object sender, RoutedEventArgs e ) {
            String email = emailTextField.Text.Trim();
            ClearData();
            try {
                VerifyEmailRegistered( email );
                this.code = GenerateCode( email );
                string content = Basta.Properties.Resource.recoveryPassword_Message_Contenthtml;
                if ( SendMessage( email, content.Replace( "$$$", code ) ) ) {
                    sendCodeStackPanel.IsEnabled = false;
                    codeRecuperationStackPanel.IsEnabled = true;
                    sendCodeButton.Visibility = Visibility.Hidden;
                    verifyCodeButton.Visibility = Visibility.Visible;
                }
            } catch ( EmailNotRegisteredYetException ex ) {
                systemLabel.Content = ex.Message;
            } catch ( Exception ) {
                systemLabel.Content = Resource.SystemErrorSendMessage;
            }
        }

        private void ClearData() {
            systemLabel.Content = "";
        }

        private string GenerateCode( string email ) {
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            string code = accessAccountDAO.GenerateRecoveryCodeByEmail( email );
            return code;
        }

        private bool SendMessage( string email, string message ) {
            bool isMessageSent = false;
            try {
                Email.SendMessage( email, message );
                isMessageSent = true;
            } catch ( Exception ) {
                throw;
            }
            return isMessageSent;
        }

        private bool VerifyEmailRegistered( string email ) {
            bool isEmailRegistered;
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            isEmailRegistered = accessAccountDAO.verifyExistingEmail( email );
            if ( !isEmailRegistered ) {
                throw new EmailNotRegisteredYetException( Basta.Properties.Resource.SystemNoExistingEmail );
            }
            return isEmailRegistered;
        }

        private void VerifyCodeButtonClicked( object sender, RoutedEventArgs e ) {
            if ( codeVerificationTextField.Text.Trim() == code ) {
                Hide();
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow( emailTextField.Text.Trim() );
                changePasswordWindow.ShowDialog();
                Close();
            } else {
                systemLabel.Content = Resource.SystemInvalidCode;
            }
        }

    }
}
