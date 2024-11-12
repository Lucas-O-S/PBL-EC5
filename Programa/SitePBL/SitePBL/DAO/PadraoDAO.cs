using SitePBL.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace SitePBL.DAO
{
    /// <summary>
    /// Classe abstratada pai das DAO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PadraoDAO<T> where T : PadraoViewModel
    {
        /// <summary>
        /// Construtor que usa o metodo set nomeTabela
        /// </summary>
        public PadraoDAO()
        {
            SetTabela();
        }

        /// <summary>
        /// Define a tabela usada pela classe
        /// </summary>
        protected string nomeTabela { get; set; }

        /// <summary>
        /// Nome da sp de listagem
        /// </summary>
        protected string nomeSpListagem { get; set; } = "sp_listagem_generic";

        /// <summary>
        /// Metodo abstrato que precisa ser criado para criar parametros
        /// </summary>
        /// <param name="model">Classe que trabalharemos</param>
        /// <returns></returns>
        protected abstract SqlParameter[] CriaParametros(T model);


        /// <summary>
        /// Metodo abstrato para criar uma classe
        /// </summary>
        /// <param name="registro">data row de uma tabela</param>
        /// <returns></returns>
        protected abstract T MontarModel(DataRow registro);

        /// <summary>
        /// Metodo abstrato que precisa ser criado que seja definido o nome da tabela usada
        /// </summary>
        protected abstract void SetTabela();

        /// <summary>
        /// metodo para inserir dados na tabela
        /// </summary>
        /// <param name="model">classe que trabalharemos</param>
        public virtual void Insert(T model)
        {
            HelperSqlDAO.ExecutaProc("sp_insert_" + nomeTabela, CriaParametros(model));
        }

        /// <summary>
        /// Metodo pa atualizar um registro
        /// </summary>
        /// <param name="model">Classe que trabalharemos</param>
        public virtual void Update(T model)
        {
            HelperSqlDAO.ExecutaProc("sp_update_" + nomeTabela, CriaParametros(model));
        }

        /// <summary>
        /// Metodo para deletar um registro
        /// </summary>
        /// <param name="id">id da classe que trabalharemos</param>
        public virtual void Delete(int id)
        {

            HelperSqlDAO.ExecutaProc("sp_delete_generic", HelperSqlDAO.CriarParametros(id));
        }


        /// <summary>
        /// Metodo para consultar um registro
        /// </summary>
        /// <param name="id">id da classe que trabalharemos</param>
        /// <returns></returns>
        public virtual T Consulta(int id)
        {


            var tabela = HelperSqlDAO.ExecutaProcSelect("sp_consulta_generic", HelperSqlDAO.CriarParametros(id, nomeTabela));

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontarModel(tabela.Rows[0]);
        }

        /// <summary>
        /// Metodo para listar os dados de uma tabela
        /// </summary>
        /// <returns></returns>
        public virtual List<T> Listagem()
        {

            var p = new SqlParameter[]
            {
                 new SqlParameter("tabela", nomeTabela),
                new SqlParameter("Ordem", "1")
            };
            var tabela = HelperSqlDAO.ExecutaProcSelect(nomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontarModel(registro));
            return lista;
        }

        public static implicit operator PadraoDAO<T>(SensorDAO v)
        {
            throw new NotImplementedException();
        }
    }
}
