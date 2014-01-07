using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.IO;
/// <summary>
///********************************************************************************************//
    //This class is use for Encypt and Decypt the the  data 
//********************************************************************************************//
/// </summary>
public class clsencrypt
{
    public enum HashType : int
    {
        SHA1,
        SHA256,
        SHA384,
        SHA512,
        MD5,
        RIPEMD160
    }
    #region HASH KEY USING HASH ALOGO
    //AIM: THIS FUNCTION IS USE to encrypt the string and create hask key using diff hash methods
    public string FromString(string input, HashType hashtype)
        {
            Byte[] clearBytes;
            Byte[] hashedBytes;
            string output = String.Empty;

            switch (hashtype)
            {
                case HashType.RIPEMD160:
                    clearBytes = new UTF8Encoding().GetBytes(input);
                    RIPEMD160 myRIPEMD160 = RIPEMD160Managed.Create();
                    hashedBytes = myRIPEMD160.ComputeHash(clearBytes);
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case HashType.MD5:
                    clearBytes = new UTF8Encoding().GetBytes(input);
                    hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case HashType.SHA1:
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    sha1.ComputeHash(clearBytes);
                    hashedBytes = sha1.Hash;
                    sha1.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case HashType.SHA256:
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA256 sha256 = new SHA256Managed();
                    sha256.ComputeHash(clearBytes);
                    hashedBytes =sha256.Hash;
                    
                    sha256.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case HashType.SHA384:
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA384 sha384 = new SHA384Managed();
                    sha384.ComputeHash(clearBytes);
                    hashedBytes = sha384.Hash;
                    sha384.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case HashType.SHA512:
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA512 sha512 = new SHA512Managed();
                    sha512.ComputeHash(clearBytes);
                    hashedBytes = sha512.Hash;
                    sha512.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
            }
            return output;
        }

     #endregion

    #region Function for throw the exception
        private byte Byte(byte[] p)
    {
        throw new Exception("The method or operation is not implemented.");
    }
    #endregion

    #region  FUNCTION USE FOR ENCRYPT THE DATA
    // AIM: THIS FUNCTION IS USE FOR ENCRYPT THE USER PASSWORD FOR SECURITY CONCERN
    // USE PASS THE PASSWORD AS STRING THEN IT WRITTEN THE THE DATA INTO ENCRYPTED FORM.
    public string Encrypt(string stringToEncrypt)
    {
        string sEncryptionKey = "54276498933473847874";
        byte[] key = { };
        byte[] IV = { 0xAB, 0x90, 0xEF, 0xCD, 0x34, 0x78, 0x12, 0x56 };
        byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length)

        try
        {
            key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {            
            return (string.Empty);
        }
    }

    #endregion

    #region

    // AIM: THIS FUNCTION IS USE FOR DECRYPT THE USER PASSWORD FOR SECURITY CONCERN
    // USE PASS THE PASSWORD AS STRING THEN IT WRITTEN THE THE DATA INTO DECRYPTED FORM.
    public string Decrypt(string stringToDecrypt)
    {
        stringToDecrypt = stringToDecrypt.Replace(" ", "+");
        string sEncryptionKey = "54276498933473847874";
        byte[] key = { };
        byte[] IV = { 0xAB, 0x90, 0xEF, 0xCD, 0x34, 0x78, 0x12, 0x56 };
        byte[] inputByteArray = new byte[stringToDecrypt.Length];
        try
        {
            key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (System.Exception ex)
        {            
            return (string.Empty);
        }
    }

    #endregion

    #region
    public string ScrapIdEncrypt(string stringToEncrypt)
    {
        string sEncryptionKey = "54276498933473847874";
        byte[] key = { };
        byte[] IV = { 0xAB, 0x90, 0xEF, 0xCD, 0x34, 0x78, 0x12, 0x56 };
        byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length)

        try
        {
            key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
            return (string.Empty);
        }
    }
    #endregion
}

