using SitePBL.Models;
using System.Data.SqlClient;
using System.Data;

namespace SitePBL.DAO
{
    public class EmpresaDAO : PadraoDAO<EmpresaViewModel>
    {

        /// <summary>
        /// define a tabela como empresa
        /// </summary>
        protected override void SetTabela() { nomeTabela = "empresa"; }


        /// <summary>
        /// Criar parametros de empreasa 
        /// </summary>
        /// <param name="empresa">classe de empresa</param>
        /// <returns>Parametro sql</returns>
        protected override SqlParameter[] CriaParametros(EmpresaViewModel empresa)
        {
            SqlParameter[] sp;

            object imgByte = empresa.imagembyte;
            if (imgByte == null)
                imgByte = DBNull.Value;
            
            if (empresa.id != 0 || empresa.id != null)
            {
                sp = new SqlParameter[] {

                    new SqlParameter("id", empresa.id),
                    new SqlParameter("nome", empresa.nome),
                    new SqlParameter("logo", imgByte),
                    new SqlParameter("sede", empresa.sede),
                };

            }
            else
            {
                sp = new SqlParameter[] {

                    new SqlParameter("nome", empresa.nome),
                    new SqlParameter("logo", imgByte),
                    new SqlParameter("sede", empresa.sede),
                };
            }
            return sp;
           
        }


        /// <summary>
        /// Monta uma model de empresaId com base do datarow
        /// </summary>
        /// <param name="registro">data row da tabela</param>
        /// <returns></returns>
        protected override EmpresaViewModel MontarModel(DataRow registro)
        {
            EmpresaViewModel empresa = new EmpresaViewModel(); ;

            empresa.id = Convert.ToInt32(registro["id"]);
            empresa.nome = Convert.ToString(registro["nome"]);

            empresa.imagembyte = registro["logo"] as byte[];

            
            empresa.sede = Convert.ToString(registro["sede"]);
            return empresa;
        }

    }
}
