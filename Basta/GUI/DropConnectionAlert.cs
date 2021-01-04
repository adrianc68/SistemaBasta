using System.Windows;

namespace Basta.GUI {
    public class DropConnectionAlert {
        public static void ShowDropConnectionAlert() {
            _ = MessageBox.Show( Properties.Resource.SystemServerNotFound, Properties.Resource.SystemMessageTitle, MessageBoxButton.OK, MessageBoxImage.Error );
        }
    }
}
