using System;
using System.Security.Cryptography;
using System.Text;

public static class Decriptor
{
	public static string Password
    {
		get => "qwerty12345Q";
	}

	private static byte[] ConvertToKeyBytes(SymmetricAlgorithm algorithm, string password)
	{
		algorithm.GenerateKey();

		var keyBytes = Encoding.UTF8.GetBytes(password);
		var validKeySize = algorithm.Key.Length;

		if (keyBytes.Length != validKeySize)
		{
			var newKeyBytes = new byte[validKeySize];
			Array.Copy(keyBytes, newKeyBytes, Math.Min(keyBytes.Length, newKeyBytes.Length));
			keyBytes = newKeyBytes;
		}

		return keyBytes;
	}

	public static Container GetData(string containerJsonString, out EncryptData encryptData)
	{
		Container container = UnityEngine.JsonUtility.FromJson<Container>(containerJsonString);

		try
		{
			AESEncryptedText eSEncryptedText = UnityEngine.JsonUtility.FromJson<AESEncryptedText>(container.encryptString);
			encryptData = Decrypt(eSEncryptedText);
		}
		catch
        {
			encryptData = new EncryptData();
        }

		return container;
	}

	public static EncryptData Decrypt(AESEncryptedText aESEncryptedText)
	{
		Aes aes = Aes.Create();

		byte[] ivBytes = Convert.FromBase64String(aESEncryptedText.vector);
		byte[] encryptedTextBytes = Convert.FromBase64String(aESEncryptedText.codedString);

		ICryptoTransform decryptor = aes.CreateDecryptor(ConvertToKeyBytes(aes, Password), ivBytes);
		byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);

		return UnityEngine.JsonUtility.FromJson<EncryptData>(Encoding.UTF8.GetString(decryptedBytes));
	}

	[Serializable]
	public struct AESEncryptedText
	{
		public string vector;
		public string codedString;

		public AESEncryptedText(string vector, string codedString)
		{
			this.vector = vector;
			this.codedString = codedString;
		}
	}
}
