using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;


    public  static class AssetEncrypter
    {
        public static bool IsEncrypt = true;
        public static string DefaultKey = "dac6befe8fa4062f";
        public static bool Check(string filePath)
        {
            if (IsEncrypt == false)
                return false;
            
            if (filePath.Contains("/lua/"))
            {
                return true;
            }
            return false;
        }

        public static string Encrypt(string data)
        {
            return XXTEA.EncryptToBase64String(data, DefaultKey);
        }

        public static string Decrypt(string data)
        {
           return XXTEA.DecryptBase64StringToString(data, DefaultKey);
        }

        #region quick xor 
        //------------------------------------------------------------
        // Game Framework
        // Copyright © 2013-2021 Jiang Yin. All rights reserved.
        // Homepage: https://gameframework.cn/
        // Feedback: mailto:ellan@gameframework.cn
        //------------------------------------------------------------
        internal const int QuickEncryptLength = 220;

        public static string XEncrypt(string data, string hash)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var code = Encoding.UTF8.GetBytes(hash);
            XEncryptInplace(bytes, code);
            return Convert.ToBase64String(bytes);
        }
        public static string QuickXEncrypt(string data, string hash)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var code = Encoding.UTF8.GetBytes(hash);
            QuickXEncryptInplace(bytes, code);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// input bytes will be modifed to output result
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="code"></param>
        public static void XEncryptInplace(byte[] bytes, byte[] code)
        {
            XorBytesInplace(bytes, 0, bytes.Length, code);
        }

        public static byte[] XEncrypt(byte[] bytes, byte[] code)
        {
            return XorBytes(bytes, 0, bytes.Length, code);
        }

        /// <summary>
        /// input bytes will be modifed to output result
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="code"></param>
        public static void QuickXEncryptInplace(byte[] bytes, byte[] code)
        {
            XorBytesInplace(bytes, 0, QuickEncryptLength, code);
        }

        public static byte[] QuickXEncrypt(byte[] bytes, byte[] code)
        {
            return XorBytes(bytes, 0, QuickEncryptLength, code);
        }

        public static byte[] XorBytes(byte[] bytes, int startIndex, int length, byte[] code)
        {
            if (bytes == null)
            {
                return null;
            }

            int bytesLength = bytes.Length;
            byte[] results = new byte[bytesLength];
            Array.Copy(bytes, 0, results, 0, bytesLength);
            XorBytesInplace(results, startIndex, length, code);
            return results;
        }

        public static void XorBytesInplace(byte[] bytes, int startIndex, int length, byte[] code)
        {
            if (bytes == null || code == null)
            {
                return;
            }

            int codeLength = code.Length;
            if (codeLength <= 0)
            {
                throw new Exception("Code length is invalid.");
            }

            if (startIndex < 0 || length < 0 || startIndex + length > bytes.Length)
            {
                throw new Exception("Start index or length is invalid.");
            }

            int codeIndex = startIndex % codeLength;
            for (int i = startIndex; i < length; i++)
            {
                bytes[i] ^= code[codeIndex++];
                codeIndex %= codeLength;
            }
        }
    }
    #endregion
