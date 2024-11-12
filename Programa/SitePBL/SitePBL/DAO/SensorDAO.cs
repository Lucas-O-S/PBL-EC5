using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    /// <summary>
    /// Dao do sensor
    /// </summary>
    public class SensorDAO : PadraoDAO<SensorViewModel>
    {
        /// <summary>
        /// define a tabela do sensor
        /// </summary>
        protected override void SetTabela() { nomeTabela = "sensor"; }

        /// <summary>
        /// Criar parametros de Sensor 
        /// </summary>
        /// <param name="Sensor">Classe sensor</param>
        /// <returns></returns>
        protected override SqlParameter[] CriaParametros(SensorViewModel Sensor)
        {
            SqlParameter[] sp;
            if (Sensor.id != 0 || Sensor.id != null)
            {
                sp = new SqlParameter[]
                {
                    new SqlParameter("id", Sensor.id),
                    new SqlParameter("descricao", Sensor.descricao),
                    new SqlParameter("fk_empresa_id", Sensor.empresaId)
                };
            }



            else
            {
                sp = new SqlParameter[]
                {
                    new SqlParameter("descricao", Sensor.descricao),
                    new SqlParameter("fk_empresa_id", Sensor.empresaId)
                };


            }
            return sp;
        }

        /// <summary>
        /// Monta uma model de Sensor com base do datarow
        /// </summary>
        /// <param name="registro">DataRow de uma tabela</param>
        /// <returns></returns>
        protected override SensorViewModel MontarModel(DataRow registro)
        {
            SensorViewModel sensor = new SensorViewModel();

            sensor.id = Convert.ToInt32(registro["id"]);
            sensor.descricao = Convert.ToString(registro["descricao"]);
            sensor.empresaId = Convert.ToInt32(registro["fk_empresa_id"]);

            EmpresaDAO cDAO = new EmpresaDAO();

            sensor.empresaNome = cDAO.Consulta(sensor.empresaId).nome;

            return sensor;
        }
    }






}

