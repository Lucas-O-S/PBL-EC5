using SitePBL.DAO;

namespace SitePBL.Models
{
	/// <summary>
	/// Classe de empresas
	/// </summary>
	public class EmpresaViewModel : FotoPadraoViewModel
    {

		/// <summary>
		/// Nome da empresaId
		/// </summary>
		public string nome { get; set; }

		/// <summary>
		/// Local da sede da empresaId
		/// </summary>
		public string sede { get; set; }

	
	}
}
