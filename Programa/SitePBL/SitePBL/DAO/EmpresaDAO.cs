using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class EmpresaDAO : PadraoDAO<EmpresaViewModel>
    {

        //teste
        protected override void SetTabela() { nomeTabela = "empresa"; }


        //Criar parametros 
        protected override SqlParameter[] CriaParametros(EmpresaViewModel empresa)
        {
            if (empresa.id != 0)
            {
                SqlParameter[] parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("id", empresa.id);
                parametros[1] = new SqlParameter("nome", empresa.nome);
                if (empresa.imagembyte != null)
                {
                    //Evita problemas de conversão de imagem
                    parametros[2] = new SqlParameter("logo", SqlDbType.VarBinary)
                    {
                        Value = empresa.imagembyte

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
            else
            {
                SqlParameter[] parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("nome", empresa.nome);
                if (empresa.imagembyte != null)
                {
                    //Evita problemas de conversão de imagem
                    parametros[1] = new SqlParameter("logo", SqlDbType.VarBinary)
                    {
                        Value = empresa.imagembyte

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
           
        }


        //Monta uma model de empresaId com base do datarow
        protected override EmpresaViewModel MontaModel(DataRow registro)
        {
            EmpresaViewModel empresa = new EmpresaViewModel(); ;

            empresa.id = Convert.ToInt32(registro["id"]);
            empresa.nome = Convert.ToString(registro["nome"]);
            if (registro["logo"] != DBNull.Value)
            {
                empresa.imagembyte = (byte[])registro["logo"];

            }
            empresa.sede = Convert.ToString(registro["sede"]);
            return empresa;
        }

        //Busca o id da empresaId com base no nome
        public int buscaID(string nomeEmpresa)
        {
            var parametro = new SqlParameter[]
            {
                new SqlParameter ("nome",nomeEmpresa)
            };
            DataTable tabela = HelperSqlDAO.ExecutaProcSelect("sp_busca_id_empresa", parametro);

            if (tabela != null)
            {
                int id = Convert.ToInt32(tabela.Rows[0]["id"]);
                return id;
            }

            return -1;
        }





  

      

    }
}
