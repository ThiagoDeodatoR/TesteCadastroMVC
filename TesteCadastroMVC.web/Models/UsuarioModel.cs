using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteCadastroMVC.web.Models
{
    public class UsuarioModel
    {
        public static bool ValidarUsuario(string login, string senha)
        {
            var ret = false;
            using (var conexao = new NpgsqlConnection())
            {
                conexao.ConnectionString = "Server = localhost; Port = 5432; User Id = postgres; Password = 1234; Database = Csharp";
                conexao.Open();
                using (var comando = new NpgsqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT COUNT(*) FROM usuarios WHERE login = '{0}' and senha = '{1}'", login, senha);
                    ret = (long)comando.ExecuteScalar() > 0;
                    // ret = ((int)comando.ExecuteScalar() > 0);  >>> Casting para int não funciona
                    conexao.Close();
                }
            }
            return ret;
        }
    }
}