using Basta.GUI.Login.Main;
using Basta.GUI.Login.RecoveryPassword;
using Database.DAO;
using Database.Entity;
using Domain.Exceptions;
using System;
using System.Windows;
using System.Windows.Navigation;
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
            Hide();
            GUI.Login.SignUp.SignUp signUp = new SignUp.SignUp();
            signUp.ShowDialog();
            Show();
        }

        private void LoginButtonClicked( object sender, RoutedEventArgs e ) {
            Autentication autentication = Autentication.GetInstance();
            systemLabel.Content = "";
            try {
                if ( autentication.LogIn( usernameTextField.Text.Trim(), Cryptography.SHA256_Hash( passwordTextField.Password.Trim() ) ) ) {
                    Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                    Show();
                }
            } catch ( AccessAccountNotFoundException ) {
                systemLabel.Content = Properties.Resource.SystemLoginError;
            } catch ( BannedAccountException ) {
                systemLabel.Content = Properties.Resource.SystemLoginAccountBanned;
            } catch ( LimitReachedException ) {
                systemLabel.Content = Properties.Resource.SystemAttemptsLimitReached;
            } catch ( Exception ) {
                systemLabel.Content = Properties.Resource.SystemFatalError;
            }
        }

        private void RecoveryPasswordLabelClicked( object sender, System.Windows.Input.MouseButtonEventArgs e ) {
            Hide();
            RecoveryPasswordWindow recoveryPasswordWindow = new RecoveryPasswordWindow();
            recoveryPasswordWindow.ShowDialog();
            Show();
        }

        private void ChangeLanguageButtonClicked( object sender, RoutedEventArgs e ) {
            if ( language ) {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "en-US" );
                language = false;
            } else {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "es-ES" );
                language = true;
            }
            Properties.Settings.Default.Save();
            DataContext = this;
            

        }
    }
}
