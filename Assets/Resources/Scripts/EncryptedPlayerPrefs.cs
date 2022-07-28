using UnityEngine;

/// <summary>
/// �Í���PlayerPrefs
/// </summary>
public static class EncryptedPlayerPrefs
{

    public static void SaveInt(string key, int value)
    {
        string valueString = value.ToString();
        SaveString(key, valueString);
    }
    public static void SaveFloat(string key, float value)
    {
        string valueString = value.ToString();
        SaveString(key, valueString);
    }
    public static void SaveBool(string key, bool value)
    {
        string valueString = value.ToString();
        SaveString(key, valueString);
    }
    public static void SaveString(string key, string value)
    {
        string encKey = Enc.EncryptString(key);
        string encValue = Enc.EncryptString(value.ToString());
        PlayerPrefs.SetString(encKey, encValue);
        PlayerPrefs.Save();
    }

    public static int LoadInt(string key, int defult)
    {
        string defaultValueString = defult.ToString();
        string valueString = LoadString(key, defaultValueString);

        int res;
        if (int.TryParse(valueString, out res))
        {
            return res;
        }
        return defult;
    }
    public static float LoadFloat(string key, float defult)
    {
        string defaultValueString = defult.ToString();
        string valueString = LoadString(key, defaultValueString);

        float res;
        if (float.TryParse(valueString, out res))
        {
            return res;
        }
        return defult;
    }
    public static bool LoadBool(string key, bool defult)
    {
        string defaultValueString = defult.ToString();
        string valueString = LoadString(key, defaultValueString);

        bool res;
        if (bool.TryParse(valueString, out res))
        {
            return res;
        }
        return defult;
    }
    public static string LoadString(string key, string defult)
    {
        string encKey = Enc.EncryptString(key);
        string encString = PlayerPrefs.GetString(encKey, string.Empty);
        if (string.IsNullOrEmpty(encString))
        {
            return defult;
        }
        string decryptedValueString = Enc.DecryptString(encString);
        return decryptedValueString;
    }

    /// <summary>
    /// ������̈Í����E������
    /// �Q�l�Fhttp://dobon.net/vb/dotnet/string/encryptstring.html
    /// </summary>
    private static class Enc
    {
        const string PASS = "bn3Qob4BguyKBosgKVrL";
        const string SALT = "Izvib87AIo7j4g9XJL5u";

        static System.Security.Cryptography.RijndaelManaged rijndael;

        static Enc()
        {
            //RijndaelManaged�I�u�W�F�N�g���쐬
            rijndael = new System.Security.Cryptography.RijndaelManaged();
            byte[] key, iv;
            GenerateKeyFromPassword(rijndael.KeySize, out key, rijndael.BlockSize, out iv);
            rijndael.Key = key;
            rijndael.IV = iv;
        }


        /// <summary>
        /// ��������Í�������
        /// </summary>
        /// <param name="sourceString">�Í������镶����</param>
        /// <returns>�Í������ꂽ������</returns>
        public static string EncryptString(string sourceString)
        {
            //��������o�C�g�^�z��ɕϊ�����
            byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(sourceString);
            //�Ώ̈Í����I�u�W�F�N�g�̍쐬
            System.Security.Cryptography.ICryptoTransform encryptor = rijndael.CreateEncryptor();
            //�o�C�g�^�z����Í�������
            byte[] encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
            //����
            encryptor.Dispose();
            //�o�C�g�^�z��𕶎���ɕϊ����ĕԂ�
            return System.Convert.ToBase64String(encBytes);
        }

        /// <summary>
        /// �Í������ꂽ������𕜍�������
        /// </summary>
        /// <param name="sourceString">�Í������ꂽ������</param>
        /// <returns>���������ꂽ������</returns>
        public static string DecryptString(string sourceString)
        {
            //��������o�C�g�^�z��ɖ߂�
            byte[] strBytes = System.Convert.FromBase64String(sourceString);
            //�Ώ̈Í����I�u�W�F�N�g�̍쐬
            System.Security.Cryptography.ICryptoTransform decryptor = rijndael.CreateDecryptor();
            //�o�C�g�^�z��𕜍�������
            //�������Ɏ��s����Ɨ�OCryptographicException������
            byte[] decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
            //����
            decryptor.Dispose();
            //�o�C�g�^�z��𕶎���ɖ߂��ĕԂ�
            return System.Text.Encoding.UTF8.GetString(decBytes);
        }

        /// <summary>
        /// �p�X���[�h���狤�L�L�[�Ə������x�N�^�𐶐�����
        /// </summary>
        /// <param name="password">��ɂȂ�p�X���[�h</param>
        /// <param name="keySize">���L�L�[�̃T�C�Y�i�r�b�g�j</param>
        /// <param name="key">�쐬���ꂽ���L�L�[</param>
        /// <param name="blockSize">�������x�N�^�̃T�C�Y�i�r�b�g�j</param>
        /// <param name="iv">�쐬���ꂽ�������x�N�^</param>
        private static void GenerateKeyFromPassword(int keySize, out byte[] key, int blockSize, out byte[] iv)
        {
            //�p�X���[�h���狤�L�L�[�Ə������x�N�^���쐬����
            //salt�����߂�
            byte[] salt = System.Text.Encoding.UTF8.GetBytes(SALT);//salt�͕K��8byte�ȏ�
            //Rfc2898DeriveBytes�I�u�W�F�N�g���쐬����
            System.Security.Cryptography.Rfc2898DeriveBytes deriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(PASS, salt);
            //.NET Framework 1.1�ȉ��̎��́APasswordDeriveBytes���g�p����
            //System.Security.Cryptography.PasswordDeriveBytes deriveBytes =
            //    new System.Security.Cryptography.PasswordDeriveBytes(password, salt);
            //���������񐔂��w�肷�� �f�t�H���g��1000��
            deriveBytes.IterationCount = 1000;
            //���L�L�[�Ə������x�N�^�𐶐�����
            key = deriveBytes.GetBytes(keySize / 8);
            iv = deriveBytes.GetBytes(blockSize / 8);
        }
    }

}