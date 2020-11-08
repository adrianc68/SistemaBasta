using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Utils {
    /*
    The Cryptography class
    Contains all methods for encrypt data.
    */
    public class Cryptography {
        // It encrypts a specified string value.
        /// <summary>
        /// It encrypts a string value.
        /// </summary>
        /// <returns>
        /// returns a encrypted string.
        /// </returns>
        public static String SHA256_Hash(String value) {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create() ) {
                Encoding encoding = Encoding.UTF8;
                Byte[] result = hash.ComputeHash( encoding.GetBytes(value) );

                foreach (Byte byteInResult in result)
                    stringBuilder.Append( byteInResult.ToString("x2") );
            }
            return stringBuilder.ToString();
        }
    }

}
