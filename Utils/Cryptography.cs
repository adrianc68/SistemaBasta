using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Utils {
    public class Cryptography {

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
