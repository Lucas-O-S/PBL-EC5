using SitePBL.Models;
using System.Data;
using System.Data.SqlClient;

namespace SitePBL.DAO
{
    public class AcessoDAO : PadraoDAO<AcessoViewModel>
    {

        protected override void SetTabela() { nomeTabela = "acesso"; }

        //Criar parametros de acessos com id
        protected override SqlParameter[] CriaParametrosId(AcessoViewModel acesso)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("id", acesso.id);
			parametros[1] = new SqlParameter("Nome_Usuario", acesso.nomeUsuario);
			parametros[2] = new SqlParameter("senha", acesso.senha);
            parametros[3] = new SqlParameter("fk_empresa_id", acesso.empresa);

            return parametros;
        }

        //Criar parametros de acessos sem ID id
        protected override SqlParameter[] CriaParametrosNoId(AcessoViewModel acesso)
        {
            SqlParameter[] parametros = new SqlParameter[2];
			parametros[1] = new SqlParameter("Nome_Usuario", acesso.nomeUsuario);
			parametros[0] = new SqlParameter("senha", acesso.senha);
            parametros[1] = new SqlParameter("fk_empresa_id", acesso.empresa);

            return parametros;
        }



        //Monta uma model de acesso com base do datarow
        protected override AcessoViewModel MontaModel(DataRow registro)
        {
            AcessoViewModel acesso = new AcessoViewModel(); ;

            acesso.id = Convert.ToInt32(registro["id"]);
            acesso.nomeUsuario = Convert.ToString(registro["Nome_Usuario"]);
            acesso.senha = Convert.ToString(registro["senha"]);
            acesso.empresa = Convert.ToInt32(registro["fk_empresa_id"]);
            return acesso;
        }



        public bool Login(string nomeEmpresa, string nomeUsuario, string senha)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("NomeEmpresa", nomeEmpresa),
				new SqlParameter("Nome_Usuario", nomeUsuario),
				new SqlParameter("senha", senha)

             };
            string sql = "sp_login_acesso";

            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            if (tabela.Rows.Count > 0 && Convert.ToInt32(tabela.Rows[0]["resultado"]) >= 1)
                return true;



            return false;
        }
        public void Inserir(AcessoViewModel acesso)
        {
            string sql = "sp_insert_acesso";

            HelperDAO.ExecutaProc(sql, CriaParametrosNoId(acesso));
        }
        public void Excluir(int id, string tabela)
        {
            string sql = "sp_delete_generic";
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter ("id",id),
                new SqlParameter ("tabela",tabela)
            };
            HelperDAO.ExecutaProc(sql, parametros);
        }
    }
}
