using Basta.Contracts.Faults;
using Basta.GUI.Login.Main;
using Basta.GUI.Login.RecoveryPassword;
using System.ServiceModel;
using System.Windows;

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
            try {
                autentication.LogIn( usernameTextField.Text.Trim(), passwordTextField.Password.Trim() );
            } catch ( FaultException<AccessAccountNotFoundFault> ) {
                systemLabel.Content = Properties.Resource.SystemLoginError;
            } catch ( FaultException<BannedAccountFault> ) {
                systemLabel.Content = Properties.Resource.SystemLoginAccountBanned;
            } catch ( FaultException<LimitReachedFault> ) {
                systemLabel.Content = Properties.Resource.SystemAttemptsLimitReached;
            } catch ( FaultException<AccountAlreadyLoggedFault> ) {
                systemLabel.Content = Properties.Resource.SystemLoginAccountAlreadyLogged;
            } catch ( FaultException ) {
                systemLabel.Content = Properties.Resource.SystemFatalError;
            } catch ( EndpointNotFoundException ) {
                systemLabel.Content = Properties.Resource.SystemServerNotFound;
            }

            if ( autentication.Player != null ) {
                Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                Show();
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
