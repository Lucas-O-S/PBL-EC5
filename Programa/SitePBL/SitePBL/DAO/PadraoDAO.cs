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

        //Metodo abstrato que precisa ser criado para criar paranetro com ID
        protected abstract SqlParameter[] CriaParametrosId(T model);

        //Metodo abstrato que precisa ser criado para criar parametro sem ID
        protected abstract SqlParameter[] CriaParametrosNoId(T model);

        //Metodo abstrato para criar uma classe
        protected abstract T MontaModel(DataRow registro);

        ///Metodo abstrato que precisa ser criado que seleciona a nomeTabela
        protected abstract void SetTabela();

        public virtual void Insert(T model)
        {
            HelperDAO.ExecutaProc("sp_insert_" + nomeTabela, CriaParametrosNoId(model));
        }

        public virtual void Update(T model)
        {
            HelperDAO.ExecutaProc("sp_update_" + nomeTabela, CriaParametrosNoId(model));
        }

        public virtual void Delete(int id)
        {

            HelperDAO.ExecutaProc("sp_delete_generic", HelperDAO.CriarParametroID(id));
        }

        //Delete que usa model ao inves de ID
        public virtual void Delete(T model)
        {
            HelperDAO.ExecutaProc("sp_delete_" + nomeTabela, CriaParametrosNoId(model));
        }

        public virtual T Consulta(int id)
        {

            var parametros = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", nomeTabela)
            };

            var tabela = HelperDAO.ExecutaProcSelect("sp_consulta_generic", parametros);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public virtual T Consulta(T model)
        {

       
            var tabela = HelperDAO.ExecutaProcSelect("sp_consulta_" + nomeTabela, CriaParametrosNoId(model));

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
            var tabela = HelperDAO.ExecutaProcSelect(nomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }

    }
}
