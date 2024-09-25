namespace SitePBL.Models
{
	public class SensorViewModel : PadraoViewModel
    {

		//Descrição será informação util do sensor, tipo "Sensor sala 34" ou "sensor A"
		public string? descricao { get; set; }

		//Empresa será o id da empresa, deve ser criado a partir do nome da empresa e salvo como o seu numo/id
		//Pode ser buscando o nome dentro do DB u numa combo box
		public int? empresa { get; set; }


	}
}
