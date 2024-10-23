using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
	public class ManutencaoDAO : PadraoDAO<ManutencaoViewModel>
    {
        protected override void SetTabela() { nomeTabela = "manutencao"; }

        //Não usar, não há id
        protected override SqlParameter[] CriaParametros(ManutencaoViewModel manutencao )
		{
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("data_hora", manutencao.data_hora);
            parametros[1] = new SqlParameter("fk_sensor_id", manutencao.idSensor);

            parametros[2] = new SqlParameter("fk_funcionario_id", manutencao.idFuncionario);

            parametros[3] = new SqlParameter("estado", manutencao.estado);


            return parametros;
        }




        //Monta uma model de manutencao  com base do datarow
        protected override ManutencaoViewModel MontaModel(DataRow registro)
		{
			ManutencaoViewModel manutencao = new ManutencaoViewModel(); ;

			manutencao.data_hora = Convert.ToDateTime(registro["data_hora"]);
			manutencao.idFuncionario = Convert.ToInt32(registro["fk_funcionario_id"]);
			manutencao.idSensor = Convert.ToInt32(registro["fk_sensor_id"]);
			manutencao.estado = Convert.ToString(registro["estado"]);
			return manutencao;
		}

	

		



	}
}
