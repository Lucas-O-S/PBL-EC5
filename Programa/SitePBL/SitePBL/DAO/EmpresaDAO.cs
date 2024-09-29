using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class EmpresaDAO : PadraoDAO<EmpresaViewModel>
    {

        //teste
        protected override void SetTabela() { nomeTabela = "empresa"; }

        //Criar parametros de empresa sem usar ID
        protected override SqlParameter[] CriaParametrosNoId(EmpresaViewModel empresa)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("nome", empresa.nome);
            if (empresa.logo != null)
            {
                //Evita problemas de conversão de imagem
                parametros[1] = new SqlParameter("logo", SqlDbType.VarBinary)
                {
                    Value = empresa.logo

                };

            }
            else
            {
                parametros[1] = new SqlParameter("logo", SqlDbType.VarBinary)
                {
                    Value = DBNull.Value

                };

            }
            parametros[2] = new SqlParameter("sede", empresa.sede);


            return parametros;
        }

        //Criar parametros de empresa usando ID
        protected override SqlParameter[] CriaParametrosId(EmpresaViewModel empresa)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("id", empresa.id);
            parametros[1] = new SqlParameter("nome", empresa.nome);
            if (empresa.logo != null)
            {
                //Evita problemas de conversão de imagem
                parametros[2] = new SqlParameter("logo", SqlDbType.VarBinary)
                {
                    Value = empresa.logo

                };

            }
            else
            {
                parametros[2] = new SqlParameter("logo", SqlDbType.VarBinary)
                {
                    Value = DBNull.Value

                };

            }
            parametros[3] = new SqlParameter("sede", empresa.sede);


            return parametros;
        }


        //Monta uma model de empresa com base do datarow
        protected override EmpresaViewModel MontaModel(DataRow registro)
        {
            EmpresaViewModel empresa = new EmpresaViewModel(); ;

            empresa.id = Convert.ToInt32(registro["id"]);
            empresa.nome = Convert.ToString(registro["nome"]);
            if (registro["logo"] != DBNull.Value)
            {
                empresa.logo = (byte[])registro["logo"];

            }
            empresa.sede = Convert.ToString(registro["sede"]);
            return empresa;
        }

        //Busca o id da empresa com base no nome
        public int buscaID(string nomeEmpresa)
        {
            var parametro = new SqlParameter[]
            {
                new SqlParameter ("nome",nomeEmpresa)
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("sp_busca_id_empresa", parametro);

            if (tabela != null)
            {
                int id = Convert.ToInt32(tabela.Rows[0]["id"]);
                return id;
            }

            return -1;
        }

        public List<EmpresaViewModel> Listagem(string tabela, string ordem)
        {
            List<EmpresaViewModel> lista = new List<EmpresaViewModel>();
            string sql = "sp_listagem_generic";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter ("tabela",tabela),
                new SqlParameter ("ordem",ordem)
            };
            DataTable t = HelperDAO.ExecutaProcSelect(sql, parametros);
            foreach (DataRow registro in t.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

        public void Inserir(EmpresaViewModel empresa)
        {
            string sql = "sp_insert_empresa";
            HelperDAO.ExecutaProc(sql, CriaParametrosNoId(empresa));
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

        public void Alterar(EmpresaViewModel empresa)
        {
            string sql = "sp_update_empresa";

            HelperDAO.ExecutaProc(sql, CriaParametrosId(empresa));
        }

        public EmpresaViewModel Consulta(int id, string tabela)
        {
            string sql = "sp_consulta_generic";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("id",id),
                new SqlParameter("tabela",tabela)
            };

            DataTable tab = HelperDAO.ExecutaProcSelect(sql, parametros);
            if (tab.Rows.Count == 0)
                return null;
            else
                return MontaModel(tab.Rows[0]);
        }

    }
}
