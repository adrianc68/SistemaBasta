using System;
using System.Windows;
using Basta.Properties;
using Basta.GUI.Login.ChangePassword;
using Basta.Contracts.Faults;
using System.ServiceModel;

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
                Proxy.LoginServiceClient server = new Proxy.LoginServiceClient();
                this.code = server.GenerateCode( email );
                string content = Basta.Properties.Resource.recoveryPassword_Message_Contenthtml;
                content = content.Replace( "$$$", code );
                if ( server.SendMessageByEmail( email, content ) ) {
                    sendCodeStackPanel.IsEnabled = false;
                    codeRecuperationStackPanel.IsEnabled = true;
                    sendCodeButton.Visibility = Visibility.Hidden;
                    verifyCodeButton.Visibility = Visibility.Visible;
                }
            } catch ( FaultException<EmailNotRegisteredYetFault> ) {
                systemLabel.Content = Resource.SystemNoExistingEmail;
            } catch ( FaultException<EmailSenderFault> ) {
                systemLabel.Content = Resource.SystemErrorSendMessage;
            } catch ( FaultException en ) {
                Console.WriteLine( "LOG MEEEEE" + en);
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Console.WriteLine("LOG ME" + ex.ToString() );
                }
            }
        }

        private void ClearData() {
            systemLabel.Content = "";
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

        private void RecoveryClosed( object sender, EventArgs e ) {
            Autentication.GetInstance().RoomServiceCallBack.LoginWindow.Show();
        }
    }
}
