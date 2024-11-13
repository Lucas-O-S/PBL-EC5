namespace SitePBL.Models
{
	public class ManutencaoViewModel : PadraoViewModel
    {
		/// <summary>
		/// Data e hora marcada da manutenção
		/// </summary>
		public DateTime data_hora { get; set; }

		/// <summary>
		/// id do sensor que terá manutenção
		/// </summary>
		public int idSensor { get; set; }

		/// <summary>
		/// Descrição do sensor
		/// </summary>
		public string? descricaoSensor { get; set; }

		/// <summary>
		/// id do funcionario responsavel
		/// </summary>
		public int idFuncionario { get; set; }

		/// <summary>
		/// Nome do funcionario
		/// </summary>
		public string? nomeFuncionario { get; set; }

		/// <summary>
		/// Estado atual da manutenção, pode ser feita, em aguardo, cancelada...
		/// </summary>
		public int estadoId { get; set; }

		/// <summary>
		/// Nome do estado
		/// </summary>
		public string? estadoNome { get; set; }

		/// <summary>
		/// Nome da Empresa
		/// </summary>
		public string? nomeEmpresa {  get; set; }
	}
}
