using Coling.Repositorio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes para AES-256
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("ABCDEF9876543210"); // 16 bytes


        public Task<string> DesencriptarPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(password)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return Task.FromResult(srDecrypt.ReadToEnd());
                        }
                    }
                }
            }
        }

        public Task<string> EncriptarPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }
                    return Task.FromResult(Convert.ToBase64String(msEncrypt.ToArray()));
                }
            }
        }

        public async Task<bool> VerificarCredenciales(string usuario, string password)
        {
            string passEnc = await EncriptarPassword(password);
            string consulta = "SELECT COUNT(idusuario) FROM usuario WHERE nombreuser= '"+ usuario +"' AND password = '"+ password +"'" ;
            int existe = conexion.EjecutarEscalar(consulta);
            return existe > 0? true: false;
        }
    }
}
