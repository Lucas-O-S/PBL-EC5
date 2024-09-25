﻿namespace SitePBL.Models
{
	//Não sei sei como vamos usar a temperatura, mas melhor criar
	public class TemperaturaViewModel : PadraoViewModel
	{
		//Data e hora que o dado foi salvo
		public DateTime data_hora {  get; set; }

		// valor da temperatura
		public int valor { get; set; }

		//id do sensor, como não será cadastrado no front end, acho que pode mostrar o id mesmo ou a descrição talvez
		public int idSensor { get; set; }
	}

}
