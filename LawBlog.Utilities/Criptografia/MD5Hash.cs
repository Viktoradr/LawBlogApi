using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace LawBlog.Utilities.Criptografia
{
    public static class MD5Hash
    {
        public static string Criptografar(string Senha)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);
                byte[] hash = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch (Exception) { return null; }
        }

        public static string GeraSenha()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random random = new Random();

            string senha = string.Empty;
            for (int i = 0; i <= 5; i++)
                senha += guid.Substring(random.Next(1, guid.Length), 1);

            return senha;
        }

        public static bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
        {
            var encrypted = Criptografar(senhaDigitada);
            return Utilitarios.CompararStrings(encrypted, senhaCadastrada);
        }
    }
}
