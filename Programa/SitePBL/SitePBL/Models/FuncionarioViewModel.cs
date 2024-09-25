﻿namespace SitePBL.Models
{
	public class FuncionarioViewModel : PadraoViewModel
    {

		
		//nome do funcionario
		public string nome { get; set; }

		//descricao do funcionario
		public string cargo { get; set;}

		//Foto do funcionario salvo em bytes, lembrar de converter
		public byte[]? foto { get; set;}
	}
}
