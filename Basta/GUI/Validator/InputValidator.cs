using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Basta.GUI.Validator {
    public class InputValidator {
        public const int MAX_EMAIL_LENGTH = 120;
        public const int MAX_COUNTRY_LENGTH = 120;
        public const int MAX_NAME_LENGTH = 120;
        public const int MAX_USERNAME_LENGTH = 120;
        public const int MAX_PASSWORD_LENGTH = 120;

        public static bool IsValidEmail( string userName ) {
            Regex userNameRegularExpression = new Regex( "[A-Za-z0-9\\-ñÑ_]{1,}(\\.([A-Za-z0-9\\-ñÑ_]{1,}))*@(([a-zA-Z]{2,})(\\.([a-z]{1,})){1,})" );
            return userNameRegularExpression.IsMatch( userName );
        }

        public static bool IsValidCountry( string userName ) {
            Regex userNameRegularExpression = new Regex( "([A-Za-záéíóúüÁÉÍÓÚÜñÑ]{2,}( [A-Za-záéíóúüÁÉÍÓÚÜñÑ]{2,})*)" );

            return userNameRegularExpression.IsMatch( userName );
        }

        public static bool IsValidName( string userName ) {
            Regex userNameRegularExpression = new Regex( "([A-Za-záéíóúüÁÉÍÓÚÜñÑ]{2,}( [A-Za-záéíóúüÁÉÍÓÚÜñÑ]{2,})*)" );
            return userNameRegularExpression.IsMatch( userName );
        }

        public static bool IsValidUsername( string userName ) {
            Regex userNameRegularExpression = new Regex( "([A-Za-z0-9.-áéíóúüÁÉÍÓÚÜñÑ]{1,}( [A-Za-z0-9.-áéíóúüÁÉÍÓÚÜñÑ]{1,})*)" );
            return userNameRegularExpression.IsMatch( userName );
        }

        public static bool IsValidPassword( string userName ) {
            Regex userNameRegularExpression = new Regex( "[a-zA-Z0-9]{8,}" );
            return userNameRegularExpression.IsMatch( userName );
        }

    }
}
