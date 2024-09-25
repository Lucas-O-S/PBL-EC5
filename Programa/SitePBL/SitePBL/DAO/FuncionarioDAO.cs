using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class FuncionarioDAO : PadraoDAO<FuncionarioViewModel>
    {
        protected override void SetTabela() { nomeTabela = "funcionario"; }

        //Criar parametros de funcionario
        protected override SqlParameter[] CriaParametrosId(FuncionarioViewModel funcionario)
		{
			SqlParameter[] parametros = new SqlParameter[4];
			parametros[0] = new SqlParameter("id", funcionario.id);
			parametros[1] = new SqlParameter("nome", funcionario.nome);
			if (funcionario.foto != null)
			{
				//evita problemas de conversão de foto
                parametros[2] = new SqlParameter("foto", SqlDbType.VarBinary)
                {
                    Value = funcionario.foto

                };
            }
			else
			{
                parametros[2] = new SqlParameter("foto", SqlDbType.VarBinary)
                {
                    Value = DBNull.Value

                };

            }
			parametros[3] = new SqlParameter("cargo", funcionario.cargo);


			return parametros;
		}
        
        //Criar parametros sem id
        protected override SqlParameter[] CriaParametrosNoId(FuncionarioViewModel funcionario)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("nome", funcionario.nome);
            if (funcionario.foto != null)
            {
                //evita problemas de conversão de foto
                parametros[1] = new SqlParameter("foto", SqlDbType.VarBinary)
                {
                    Value = funcionario.foto

                };
            }
            else
            {
                parametros[1] = new SqlParameter("foto", SqlDbType.VarBinary)
                {
                    Value = DBNull.Value

                };

            }
            parametros[2] = new SqlParameter("cargo", funcionario.cargo);


            return parametros;
        }


        //Monta uma model de funcionario com base do datarow
        protected override FuncionarioViewModel MontaModel(DataRow registro)
		{
			FuncionarioViewModel funcionario = new FuncionarioViewModel(); ;

			funcionario.id = Convert.ToInt32(registro["id"]);
			funcionario.nome = Convert.ToString(registro["nome"]);
			if (registro["foto"] != DBNull.Value)
			{
				funcionario.foto = (byte[])registro["foto"];

			}
			funcionario.cargo = Convert.ToString(registro["cargo"]);
			return funcionario;
		}


	
	}
}
