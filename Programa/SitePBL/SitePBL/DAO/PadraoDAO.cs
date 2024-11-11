using SitePBL.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace SitePBL.DAO
{
    public abstract class PadraoDAO<T> where T : PadraoViewModel
    {
        //Construtor que usa o metodo set nomeTabela
        public PadraoDAO()
        {
            SetTabela();
        }

        //Tabela usada pela classe
        protected string nomeTabela { get; set; }

        //Nome da sp de listagem
        protected string nomeSpListagem { get; set; } = "sp_listagem_generic";

        //Metodo abstrato que precisa ser criado para criar paraneta
        protected abstract SqlParameter[] CriaParametros(T model);


        //Metodo abstrato para criar uma classe
        protected abstract T MontaModel(DataRow registro);

        ///Metodo abstrato que precisa ser criado que seleciona a nomeTabela
        protected abstract void SetTabela();

        public virtual void Insert(T model)
        {
            HelperSqlDAO.ExecutaProc("sp_insert_" + nomeTabela, CriaParametros(model));
        }

        public virtual void Update(T model)
        {
            HelperSqlDAO.ExecutaProc("sp_update_" + nomeTabela, CriaParametros(model));
        }

        public virtual void Delete(int id)
        {

            HelperSqlDAO.ExecutaProc("sp_delete_generic", HelperSqlDAO.CriarParametros(id));
        }

        //Delete que usa model ao inves de ID
        public virtual void Delete(T model)
        {
            HelperSqlDAO.ExecutaProc("sp_delete_" + nomeTabela, CriaParametros(model));
        }

        public virtual T Consulta(int id)
        {


            var tabela = HelperSqlDAO.ExecutaProcSelect("sp_consulta_generic", HelperSqlDAO.CriarParametros(id, nomeTabela));

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public virtual T Consulta(T model)
        {

       
            var tabela = HelperSqlDAO.ExecutaProcSelect("sp_consulta_" + nomeTabela, CriaParametros(model));

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }
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
                lista.Add(MontaModel(registro));
            return lista;
        }

    }
}
