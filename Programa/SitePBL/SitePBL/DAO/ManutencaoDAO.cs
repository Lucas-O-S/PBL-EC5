using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class ManutencaoDAO : PadraoDAO<ManutencaoViewModel>
    {

        public enum estados { completo, incompleto, Cancelado };
        /// <summary>
        /// Define a tabela como manutenção
        /// </summary>
        protected override void SetTabela() { nomeTabela = "manutencao"; }

        /// <summary>
        /// Cria parametros de manutenção
        /// </summary>
        /// <param name="manutencao">classe de manutenção</param>
        /// <returns></returns>
        protected override SqlParameter[] CriaParametros(ManutencaoViewModel manutencao)
        {
            SqlParameter[] sp;
            if (manutencao.id != 0 || manutencao.id != null)
            {
                sp = new SqlParameter[]
                {
                    new SqlParameter("id", manutencao.id),

                    new SqlParameter("data_hora", manutencao.data_hora),
                    new SqlParameter("fk_sensor_id", manutencao.idSensor),

                    new SqlParameter("fk_funcionario_id", manutencao.idFuncionario),

                    new SqlParameter("estado", manutencao.estadoId)
                };
            }
            else
            {
                sp = new SqlParameter[]
{
                    new SqlParameter("data_hora", manutencao.data_hora),
                    new SqlParameter("fk_sensor_id", manutencao.idSensor),

                    new SqlParameter("fk_funcionario_id", manutencao.idFuncionario),

                    new SqlParameter("estado", manutencao.estadoId)
};
            }



                return sp;
            }
        /// <summary>
        /// Monta uma model de manutencao  com base do datarow
        /// </summary>
        /// <param name="registro">DataRow da tabela</param>
        /// <returns></returns>
        protected override ManutencaoViewModel MontarModel(DataRow registro)
        {
            ManutencaoViewModel manutencao = new ManutencaoViewModel(); ;

            manutencao.data_hora = Convert.ToDateTime(registro["data_hora"]);
            manutencao.idFuncionario = Convert.ToInt32(registro["fk_funcionario_id"]);
            manutencao.idSensor = Convert.ToInt32(registro["fk_sensor_id"]);
            manutencao.estadoId = Convert.ToInt32(registro["estado"]);
            
            //Pega do enumerador o nome do estado com base no id dele
            manutencao.estadoNome = Enum.GetName(typeof(estados), manutencao.estadoId);

            FuncionarioDAO fDao = new FuncionarioDAO();
            manutencao.nomeFuncionario = fDao.Consulta(manutencao.idFuncionario).nome;

            SensorDAO sDAO = new SensorDAO();

            manutencao.descricaoSensor = sDAO.Consulta(manutencao.idSensor).descricao;

            return manutencao;
        }
    }












}

