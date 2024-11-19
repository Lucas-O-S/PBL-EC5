using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class FuncionarioDAO : PadraoDAO<FuncionarioViewModel>
    {
        /// <summary>
        /// Define a tabela como funcionario
        /// </summary>
        protected override void SetTabela() { nomeTabela = "funcionario"; }

        /// <summary>
        /// Criar parametros de funcionario
        /// </summary>
        /// <param name="funcionario">classe de funcionario</param>
        /// <returns></returns>
        protected override SqlParameter[] CriaParametros(FuncionarioViewModel funcionario)
        {

            SqlParameter[] sp;
            object imgByte = funcionario.imagembyte;


            if (funcionario.id != 0 && funcionario.id != null)
            {
                sp = new SqlParameter[] {


                    new SqlParameter("id", funcionario.id),
                    new SqlParameter("nome", funcionario.nome),

                    new SqlParameter("cargo", funcionario.cargo),

                    new SqlParameter("foto", SqlDbType.VarBinary) { Value = imgByte ?? DBNull.Value },

                    new SqlParameter("dataContratacao", funcionario.dataContratacao)



                };

            }
            else
            {

                sp = new SqlParameter[] {


                    new SqlParameter("nome", funcionario.nome),

                    new SqlParameter("cargo", funcionario.cargo),

                    new SqlParameter("foto", SqlDbType.VarBinary) { Value = imgByte ?? DBNull.Value },
                    new SqlParameter("dataContratacao", funcionario.dataContratacao)



                };

            }
            return sp;
        }




        /// <summary>
        /// Monta uma model de funcionario com base do datarow
        /// </summary>
        /// <param name="registro">data row da tabela</param>
        /// <returns></returns>
        protected override FuncionarioViewModel MontarModel(DataRow registro)
        {
            FuncionarioViewModel funcionario = new FuncionarioViewModel(); ;

            funcionario.id = Convert.ToInt32(registro["id"]);
            funcionario.nome = Convert.ToString(registro["nome"]);

            funcionario.imagembyte = registro["foto"] as byte[];

            funcionario.cargo = Convert.ToString(registro["cargo"]);

            funcionario.dataContratacao = Convert.ToDateTime(registro["dataContratacao"]);
            return funcionario;
        }

        public List<FuncionarioViewModel> BuscaAvancada(
            DateTime data_hora_inicial,
            DateTime data_hora_final,
            string nome,
            string cargo
            )
        {

            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("data_hora_inicial",data_hora_inicial),
                new SqlParameter("data_hora_final",data_hora_final),
                new SqlParameter("cargo",cargo),
                new SqlParameter("nome",nome)
            };
            DataTable dt = HelperSqlDAO.ExecutaProcSelect("sp_avancado_func", sp);
            List<FuncionarioViewModel> lista = new List<FuncionarioViewModel>();

            foreach (DataRow dr in dt.Rows)
            {
                lista.Add(MontarModel(dr));
            }
            return lista;
        }
    }
}
