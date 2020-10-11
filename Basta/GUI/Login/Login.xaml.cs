using System.Windows;

namespace Basta.GUI.Login
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void SignUpButtonClicked(object sender, RoutedEventArgs e)
        {
            GUI.Login.SignUp.SignUp signUp = new SignUp.SignUp();
            signUp.ShowDialog();
        }

        private void LoginButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
