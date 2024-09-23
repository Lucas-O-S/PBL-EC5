using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class EmpresaDAO
	{
		//Criar parametros de empresa sem usar ID
		private SqlParameter[] CriarParametrosNoID(EmpresaViewModel empresa)
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
        private SqlParameter[] CriarParametrosID(EmpresaViewModel empresa)
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
        private EmpresaViewModel MontarEmpresas(DataRow registro)
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

		//Classe para inserir um novo empresa
		//Alterar depois para uma stored precedure
		public void Inserir(EmpresaViewModel empresa)
		{

			string sql = "sp_insert_empresa";
			HelperDAO.ExecutaProc(sql, CriarParametrosNoID(empresa));

		}

		//Classe para excluir um acesso
		//Adicionar SP
		public void Excluir(int id)
		{
            var p = new SqlParameter[]
			{
                new SqlParameter("id", id)
			};

            string sql = "sp_delete_empresa";
			HelperDAO.ExecutaProc(sql, p);

		}

		//Alterar empresa
		//Adicionar SP
		public void Alterar(EmpresaViewModel empresa)
		{
			string sql = "sp_update_empresa";
			HelperDAO.ExecutaProc(sql, CriarParametrosID(empresa));

		}


		//Consulta um empresa
		//Adicionar SP
		public EmpresaViewModel Consulta(int id)
		{
            var p = new SqlParameter[]
			{
                new SqlParameter("id", id)
			};

            string sql = "sp_busca_empresa";

			DataTable tabela = HelperDAO.ExecutaProcSelect(sql, p);

			if (tabela.Rows.Count == 0)
			{
				return null;
			}

			else
			{
				return MontarEmpresas(tabela.Rows[0]);
			}
		}

		//Lista todos os empresa
		//Adicionar SP
		public List<EmpresaViewModel> Listagem()
		{
			List<EmpresaViewModel> lista = new List<EmpresaViewModel>();
			string sql = "sp_listagem_empresa";
			DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarEmpresas(dr));
			return lista;
		}


	
	}
}
