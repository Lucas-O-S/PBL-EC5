namespace SitePBL.Models
{
	/// <summary>
	/// Classe de acessos
	/// </summary>
	public class AcessoViewModel : PadraoViewModel
	{
		//id do usuario

		/// <summary>
		/// Senha do usuario
		/// </summary>
		public string senha { get; set; }

		/// <summary>
		/// Id da empresaId
		/// </summary>
		public int empresaId { get; set; }

		/// <summary>
		/// nome da empresaId
		/// </summary>
		public string nomeEmpresa { get; set; }

		/// <summary>
		/// Login do Usuario
		/// </summary>
		public string loginUsuario {  get; set; }

		/// <summary>
		/// Nome do usuario
		/// </summary>
		public string nomeUsuario { get; set;}

    }
}
