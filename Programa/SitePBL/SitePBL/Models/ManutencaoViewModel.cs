namespace SitePBL.Models
{
	public class ManutencaoViewModel
	{
		// Data e hora marcada da manutenção
		public DateTime data_hora { get; set; }

		//id do sensor que terá manutenção, usar descrição ou id mesmo no front
		public int idSensor { get; set; }

		//id do funcionario responsavel, usar nome no front de preferencia, ou id mesmo
		public int idFuncionario { get; set; }

		//Estado atual da manutenção, pode ser feita, em aguardo, cancelada...
		public string estado { get; set; }
	}
}
