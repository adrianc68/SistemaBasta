using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils {
    /*
    The Cryptography class
    Contains all methods for encrypt data.
    */
    public class Cryptography {
        private static Random random = new Random();
        // It encrypts a specified string value.
        /// <summary>
        /// It encrypts a string value with SHA256.
        /// </summary>
        /// <returns>
        /// returns a encrypted string.
        /// </returns>
        public static String SHA256_Hash( String value ) {
            StringBuilder stringBuilder = new StringBuilder();
            using ( SHA256 hash = SHA256Managed.Create() ) {
                Encoding encoding = Encoding.UTF8;
                Byte[] result = hash.ComputeHash( encoding.GetBytes( value ) );

                foreach ( Byte byteInResult in result )
                    stringBuilder.Append( byteInResult.ToString( "x2" ) );
            }
            return stringBuilder.ToString();
        }

        // It encrypts a specified string value.
        /// <summary>
        /// It encrypts a string value with MD5.
        /// </summary>
        /// <returns>
        /// returns a encrypted string.
        /// </returns>
        public static string MD5_Hash( string @string ) {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder stringBuilder = new StringBuilder();
            stream = md5.ComputeHash( encoding.GetBytes( @string ) );
            for ( int i = 0; i < stream.Length; i++ ) stringBuilder.AppendFormat( "{0:x2}", stream[i] );
            return stringBuilder.ToString();
        }

        // Generate a random string.
        /// <summary>
        /// It encrypts a random number.
        /// </summary>
        /// <returns>
        /// returns a encrypted a random number.
        /// </returns>
        public static string RandomString() {
            double number = random.Next();
            string randomString = MD5_Hash( number.ToString() );
            return randomString;
        }

    }

}
