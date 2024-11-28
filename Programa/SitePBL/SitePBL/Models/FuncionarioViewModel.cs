namespace SitePBL.Models
{
	/// <summary>
	/// Classe de Funcionario
	/// </summary>
	public class FuncionarioViewModel : FotoPadraoViewModel
    {
        /// <summary>
		/// nome do funcionario
		/// </summary>
        public string nome { get; set; }

		/// <summary>
		/// Cargo do funcionario
		/// </summary>
		public string cargo { get; set;}

		/// <summary>
		/// Data da Contratação
		/// </summary>
		public DateTime dataContratacao { get; set;}


	}
}
