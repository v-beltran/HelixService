using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HelixService.Utility
{
    public class HCryptography
    {
        /// <summary>
        /// Returns a hash value from a byte array.
        /// </summary>
        /// <param name="input">The byte array to hash.</param>
        /// <param name="bits">The length of the hash (256, 384, or 512).</param>
        /// <returns></returns>
        private static Byte[] BytesToHash(Byte[] input, Int32 bits)
        {
            Byte[] hash = new Byte[0];

            switch (bits)
            {
                case 256:
                    hash = new SHA256Cng().ComputeHash(input);
                    break;
                case 384:
                    hash = new SHA384Cng().ComputeHash(input);
                    break;
                case 512:
                    hash = new SHA512Cng().ComputeHash(input);
                    break;
                default:
                    hash = new SHA256Cng().ComputeHash(input);
                    break;
            }

            return hash;
        }

        /// <summary>
        /// Generate a random salt.
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static Byte[] GetRandomSalt(Int32 bits)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            Byte[] salt = new Byte[0];

            switch (bits)
            {
                case 256:
                    salt = new Byte[32];
                    break;
                case 384:
                    salt = new Byte[48];
                    break;
                case 512:
                    salt = new Byte[64];
                    break;
                default:
                    salt = new Byte[32];
                    break;
            }

            csprng.GetBytes(salt);

            return salt;
        }
        
        /// <summary>
        /// Returns a hexadecimal string from a hash/salt value.
        /// </summary>
        /// <param name="byte">A byte array that has been hashed.</param>
        /// <returns></returns>
        public static String BytesToHexString(Byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// Returns a salted-hash string from a string input.
        /// </summary>
        /// <param name="input">The string to hash.</param>
        /// <param name="salt">The hexadecimal string value to prepend to input string.</param>
        /// <param name="bits">The length of the hash (256, 384, or 512).</param>
        /// <returns></returns>
        public static String GetHashString(String input, String salt, Int32 bits)
        {
            // Create a byte array with salt prepended to a string value.
            Byte[] inputBytes = HEncoding.StringToBytes(salt + input);

            // Create a hash of a given length and the byte array created above.
            Byte[] hashBytes = HCryptography.BytesToHash(inputBytes, bits);

            // Get a string representation of the hash created above.
            String hashString = HCryptography.BytesToHexString(hashBytes);

            return hashString;
        }
    }
}