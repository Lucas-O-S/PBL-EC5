using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class ManutencaoDAO : PadraoDAO<ManutencaoViewModel>
    {
        /// <summary>
        /// Enumerador de estados com os seguintes estados: Completo, Incompleto, Cancelado
        /// </summary>
        public enum estados { Completo, Incompleto, Cancelado };

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
            if (manutencao.id != 0 && manutencao.id != null)
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
            manutencao.id = Convert.ToInt32(registro["id"]);
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

            EmpresaDAO eDAO = new EmpresaDAO();

            manutencao.nomeEmpresa = eDAO.Consulta(sDAO.Consulta(manutencao.idSensor).empresaId).nome;


            return manutencao;
        }

		public List<ManutencaoViewModel> BuscaAvancada(
            DateTime data_hora_inicial,
            DateTime data_hora_final,
            string funcionario,
            string empresa,
            string sensor,
            int estado
            )
		{

			SqlParameter[] sp = new SqlParameter[]
			{
				new SqlParameter("data_hora_inicial",data_hora_inicial),
				new SqlParameter("data_hora_final",data_hora_final),
				new SqlParameter("funcionario",funcionario),
				new SqlParameter("sensor",sensor),
				new SqlParameter("empresa",empresa),
				new SqlParameter("estado",estado)
			};
			DataTable dt = HelperSqlDAO.ExecutaProcSelect("sp_avancado_manutencao", sp);
			List<ManutencaoViewModel> lista = new List<ManutencaoViewModel>();

			foreach (DataRow dr in dt.Rows)
			{
				lista.Add(MontarModel(dr));
			}
			return lista;
		}

		public List<ManutencaoViewModel> ObterQuantidadesEmpresas(int id, List<ManutencaoViewModel> empresas)
		{
			SqlParameter[] sql = new SqlParameter[]
			{
		        new SqlParameter("id", id)
			};

            // Executa a SP que traz quantos sensores em manutenção as empresas tem
            DataTable dt_empresas = HelperSqlDAO.ExecutaProcSelect("sp_quantidades_empresas", sql);

            // Itera sobre as linhas retornadas pela DataTable
            foreach (DataRow dr in dt_empresas.Rows)
			{
				ManutencaoViewModel manutencao = new ManutencaoViewModel
				{
					nomeEmpresa = dr["nome"].ToString(), // Acessa o nome da empresa
					qtdManutencao = Convert.ToInt32(dr["Total"]) // Acessa a quantidade (assumindo que a coluna se chama 'Total')
                };

				// Adiciona o objeto preenchido à lista
				empresas.Add(manutencao);
			}

			return empresas;
		}

        public List<ManutencaoViewModel> ObterSensoresManutencao(int id, List<ManutencaoViewModel> empresas)
        {
            SqlParameter[] sql = new SqlParameter[]
            {
                new SqlParameter("id", id)
            };

            // Executa a SP que traz o estado dos sensores
            DataTable dt_sensor = HelperSqlDAO.ExecutaProcSelect("sp_estados_sensor", sql);

            // Itera sobre as linhas retornadas pela DataTable
            foreach (DataRow dr in dt_sensor.Rows)
            {
                ManutencaoViewModel manutencao = new ManutencaoViewModel
                {
                    descricaoSensor = dr["descricao"].ToString(), // Nome do sensor
                    estadoNome = dr["estado"].ToString() // Estado do sensor
                };

                // Adiciona o objeto preenchido à lista
                empresas.Add(manutencao);
            }

            return empresas;
        }

    }












}

