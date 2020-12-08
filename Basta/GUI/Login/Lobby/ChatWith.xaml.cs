using Domain.Domain;
using System.Windows.Controls;

namespace Basta.GUI.Login.Lobby {
    /// <summary>
    /// Lógica de interacción para ChatWith.xaml
    /// </summary>
    public partial class ChatWith: UserControl {

        public ChatWith() {
            InitializeComponent();
        }

        public void setInfoPlayer(Player player) {
            ageInfoLabel.Text = player.Age.ToString();
            anotherPlayerNameLabel.Content = player.Name;
            anotherPlayerNameSubLabel.Text = player.Name;
            countryInfoLabel.Text = player.Country;
            emailInfoLabel.Text = player.Email;
        }
    }
}
