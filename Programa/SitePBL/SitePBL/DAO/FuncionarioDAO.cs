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
                SqlParameter[] parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("nome", funcionario.nome);

                sp = new SqlParameter[] {


                    new SqlParameter("id", funcionario.id),
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

            funcionario.imagembyte = (byte[])registro["foto"];
           
            funcionario.cargo = Convert.ToString(registro["cargo"]);

            funcionario.dataContratacao = Convert.ToDateTime(registro["dataContratacao"]);
            return funcionario;
        }

    }
}
