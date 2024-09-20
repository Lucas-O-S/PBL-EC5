using SitePBL.Models;
using System.Data;
using System.Data.SqlClient;

namespace SitePBL.DAO
{
	public class AcessoDAO
	{
		//Criar parametros de acessos
		private SqlParameter[] CriarParametros(AcessoViewModel acesso)
		{
			SqlParameter[] parametros = new SqlParameter[3];
			parametros[0] = new SqlParameter("id", acesso.id);
			parametros[1] = new SqlParameter("senha", acesso.senha);
			parametros[2] = new SqlParameter("fk_empresa_id", acesso.empresa);

			return parametros;
		}


		//Monta uma model de acesso com base do datarow
		private AcessoViewModel MontarAcesso(DataRow registro)
		{
			AcessoViewModel acesso = new AcessoViewModel(); ;

			acesso.id = Convert.ToInt32(registro["id"]);
			acesso.senha = Convert.ToString(registro["senha"]);
			acesso.empresa = Convert.ToInt32(registro["fk_empresa_id"]);
			return acesso;
		}

		//Classe para inserir um novo acess
		//Alterar depois para uma stored precedure
		public void Inserir (AcessoViewModel acesso)
		{

		}


	}
}
