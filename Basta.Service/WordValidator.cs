using System.IO;

namespace Basta.Service {
    public class WordValidator {

        public static bool NameValidator( string word ) {
                string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\names\names.txt";
                bool found = Validator( url, word );
                return found;
            }

        public static bool LastNameValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\lastNames\lastnames.txt";
                bool found = Validator( url, word );
                return found;
            }

        public static bool FruitsVegetablesValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\fruitsOrVegs\fruitsVegetable.txt";
            bool found = Validator( url, word );
            return found;
        }

        public static bool CountryValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\countries\countries.txt";
            bool found = Validator( url, word );
            return found;
        }

        public static bool AnimalValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\animals\animals.txt";
            bool found = Validator( url, word );
            return found;
        }

        public static bool ColorValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\colors\colors.txt";
            bool found = Validator( url, word );
            return found;
        }

        public static bool ObjectValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\things\things.txt";
            bool found = Validator( url, word );
            return found;
        }

        public static bool CityValidator( string word ) {
            string url = @"C:\Users\adrian\Desktop\SistemaBasta-main\Basta.Service\Words\towns\towns.txt.txt";
            bool found = Validator( url, word );
            return found;
        }

        private static bool Validator( string url, string word ) {
            bool found = false;
            try {
                string compared = "";
                StreamReader reader = new StreamReader( url );
                while ( ( compared = reader.ReadLine() ) != null ) {
                    if ( compared.ToUpper().Trim().Equals( word.ToUpper() ) ) {
                        found = true;
                    }
                }
                reader.Close();
            } catch ( IOException IOE ) {
                throw IOE;
            }
            return found;
        }

    }
}
