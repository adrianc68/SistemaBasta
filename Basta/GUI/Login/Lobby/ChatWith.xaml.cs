using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Basta.GUI.Login.Lobby {
    /// <summary>
    /// Lógica de interacción para ChatWith.xaml
    /// </summary>
    public partial class ChatWith: UserControl {

        public Player Player { get; set; }

        public ChatWith() {
            InitializeComponent();
        }

        public void setInfoPlayer() {
            ageInfoLabel.Text = Player.Age.ToString();
            anotherPlayerNameLabel.Content = Player.Name;
            anotherPlayerNameSubLabel.Text = Player.Name;
            countryInfoLabel.Text = Player.Country;
            emailInfoLabel.Text = Player.Email;
        }

    }
}
