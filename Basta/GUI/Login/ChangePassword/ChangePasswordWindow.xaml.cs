﻿using System;
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
using System.Windows.Shapes;
using Basta.Properties;
using Utils;

namespace Basta.GUI.Login.ChangePassword {
    /// <summary>
    /// Lógica de interacción para ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow: Window {
        private string email;
        public ChangePasswordWindow( string email ) {
            this.email = email;
            InitializeComponent();
        }

        private void ClearData() {
            systemLabel.Content = "";
        }

        private bool isValidPassword( string email ) {
            bool isValidPassword = Validator.InputValidator.IsValidPassword( email );
            return isValidPassword;
        }

        private bool ChangePassword() {
            bool isPasswordChanged = false;
            Proxy.LoginServiceClient server = new Proxy.LoginServiceClient();
            isPasswordChanged = server.ChangePassword( email, Cryptography.SHA256_Hash( firstPasswordTextBox.Text.Trim() ) );
            return isPasswordChanged;
        }

        private void ChangeButtonClicked( object sender, RoutedEventArgs e ) {
            ClearData();
            if ( isValidPassword( firstPasswordTextBox.Text.Trim() ) ) {
                if ( firstPasswordTextBox.Text.Trim().Equals( secondPasswordTextBox.Text.Trim() ) ) {
                    if ( ChangePassword() ) {
                        Close();
                    } else {
                        systemLabel.Content = Resource.SystemPasswordChangingError;
                    }
                } else {
                    systemLabel.Content = Resource.SystemPasswordNoMatch;
                }
            } else {
                systemLabel.Content = Resource.SystemInvalidData;
            }

        }
    }
}
