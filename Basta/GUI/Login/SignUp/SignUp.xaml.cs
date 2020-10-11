using System;
using System.Windows;

namespace Basta.GUI.Login.SignUp
{
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUpButtonClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine( signUpFirstName.Text.Trim() );
        }
    }
}
