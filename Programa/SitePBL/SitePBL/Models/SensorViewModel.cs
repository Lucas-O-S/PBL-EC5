namespace SitePBL.Models
{
	/// <summary>
	/// Classe dos sensores
	/// </summary>
	public class SensorViewModel : PadraoViewModel
    {
		/// <summary>
		/// Descrição do sensor, é o id dele no Fiware
		/// </summary>
		public string descricao { get; set; }

		/// <summary>
		/// Id da empresa
		/// </summary>
		public int empresaId { get; set; }

		/// <summary>
		/// Nome da empresa
		/// </summary>
		public string? empresaNome { get; set; }

	}
}
