using Basta.GUI.Login.Main;
using Basta.GUI.Login.RecoveryPassword;
using Domain.Exceptions;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using Utils;

namespace Basta.GUI.Login {
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login: Window {
        private bool language = false;
        public Login() {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "es-ES" );
            InitializeComponent();
        }

        private void SignUpButtonClicked( object sender, RoutedEventArgs e ) {
            GUI.Login.SignUp.SignUp signUp = new SignUp.SignUp();
            signUp.ShowDialog();
        }

        private void LoginButtonClicked( object sender, RoutedEventArgs e ) {
            Autentication autentication = Autentication.GetInstance();
            try {
                if ( autentication.LogIn( usernameTextField.Text.Trim(), Cryptography.SHA256_Hash( passwordTextField.Password.Trim() ) ) ) {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                }
            } catch ( AccessAccountNotFoundException ) {
                systemLabel.Content = Properties.Resource.SystemLoginError;
            } catch ( BannedAccountException ) {
                systemLabel.Content = Properties.Resource.SystemLoginAccountBanned;
            } catch ( LimitReachedException ) {
                systemLabel.Content = Properties.Resource.SystemAttemptsLimitReached;
            } catch ( Exception ex ) {
                Console.WriteLine(ex);
            }
        }

        private void RecoveryPasswordLabelClicked( object sender, System.Windows.Input.MouseButtonEventArgs e ) {
            RecoveryPasswordWindow recoveryPasswordWindow = new RecoveryPasswordWindow();
            recoveryPasswordWindow.ShowDialog();
        }

        private void ChangeLanguageButtonClicked( object sender, RoutedEventArgs e ) {
            if ( language ) {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "en-US" );
            } else {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "es-ES" );
            }
            Properties.Settings.Default.Save();
            InitializeComponent();
            DataContext = this;
           
        }
    }
}
