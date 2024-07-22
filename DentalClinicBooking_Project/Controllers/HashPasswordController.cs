using DentalClinicBooking_Project.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DentalClinicBooking_Project.Controllers
{
    public class HashPasswordController : Controller
    {
        DentalClinicBookingProjectContext _context;

        public HashPasswordController(DentalClinicBookingProjectContext context)
        {
            _context = context;
        }

		public static string EncryptString(string plainText, byte[] key, byte[] iv)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = iv;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
						return Convert.ToBase64String(msEncrypt.ToArray());
					}
				}
			}
		}

		public static string DecryptString(string cipherText, byte[] key, byte[] iv)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = iv;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}

		public IActionResult HashPassword()
		{
			var accounts = _context.Accounts.ToList();
			byte[] key = Encoding.UTF8.GetBytes("01234567890123456789012345678901"); // 32 bytes key
			byte[] iv = Encoding.UTF8.GetBytes("0123456789012345"); // 16 bytes IV

			foreach (var account in accounts)
			{
				string encryptedPassword = EncryptString(account.Password, key, iv);
				account.Password = encryptedPassword;

				//string decryptedPassword = HashPasswordController.DecryptString(account.Password, key, iv);
			}
			_context.SaveChanges();
			return View();
		}
	}
}
