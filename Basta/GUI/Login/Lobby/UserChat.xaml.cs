using Domain.Domain;
using System.Windows.Controls;
using System.Windows.Input;

namespace Basta.GUI.Login.Lobby {
    /// <summary>
    /// Lógica de interacción para UserChat.xaml
    /// </summary>
    public partial class UserChat: UserControl {
        public Player Player { get; set; }

        public UserChat() {
            InitializeComponent();
        }

        private void UserChatButtonPressed( object sender, MouseButtonEventArgs e ) {
            
        }
    }
}
