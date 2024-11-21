using SitePBL.Models;
using System.Data;
using System.Data.SqlClient;

namespace SitePBL.DAO
{
    public class AcessoDAO : PadraoDAO<AcessoViewModel>
    {
        /// <summary>
        /// Define a tabela como acesso
        /// </summary>
        protected override void SetTabela() { nomeTabela = "acesso"; }

        /// <summary>
        /// Cria parametros para a tabela acesso
        /// </summary>
        /// <param name="acesso">calsse de acesso</param>
        /// <returns></returns>
        protected override SqlParameter[] CriaParametros(AcessoViewModel acesso)
        {
            SqlParameter[] sp;
            if (acesso.id != 0 && acesso.id != null)
            {
                sp = new SqlParameter[]
                {
                    new SqlParameter("Nome_Usuario", acesso.nomeUsuario),
                    new SqlParameter("login_Usuario", acesso.loginUsuario),
                    new SqlParameter("senha", acesso.senha),
                    new SqlParameter("nomeEmpresa", acesso.nomeEmpresa)
                };
            }
            else
            {
                sp = new SqlParameter[]
                {
                    new SqlParameter("Nome_Usuario", acesso.nomeUsuario),
                    new SqlParameter("login_Usuario", acesso.loginUsuario),
                    new SqlParameter("senha", acesso.senha),
                    new SqlParameter("nomeEmpresa", acesso.nomeEmpresa)
                };
            }
            return sp;
        }

        /// <summary>
        /// Monta uma model de acesso com base do datarow
        /// </summary>
        /// <param name="registro">datarow da tabela</param>
        /// <returns></returns>
        protected override AcessoViewModel MontarModel(DataRow registro)
        {
            AcessoViewModel acesso = new AcessoViewModel(); ;

            acesso.id = Convert.ToInt32(registro["id"]);
            acesso.nomeUsuario = Convert.ToString(registro["Nome_Usuario"]);
            acesso.senha = Convert.ToString(registro["senha"]);
            acesso.empresaId = Convert.ToInt32(registro["fk_empresa_id"]);

            EmpresaDAO eDAO = new EmpresaDAO();
            acesso.nomeEmpresa = eDAO.Consulta(acesso.empresaId).nome;

            return acesso;
        }

        public bool Login(string loginUsuario, string senha)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("login_Usuario", loginUsuario),
                new SqlParameter("senha", senha)
             };
            string sql = "sp_login_acesso";

            DataTable tabela = HelperSqlDAO.ExecutaProcSelect(sql, parametros);

            if (tabela.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return Convert.ToString(tabela.Rows[0]["login_Usuario"]) == loginUsuario && Convert.ToString(tabela.Rows[0]["senha"]) == senha;
            }
        }

    }
}