namespace SitePBL.Models
{
	public class FuncionarioViewModel
	{
		//ID do funcionario, não deve deixar o usuario dar o numero, deve ser o ultimo
		public int id { get; set; }
		
		//nome do funcionario
		public string nome { get; set; }

		//descricao do funcionario
		public string cargo { get; set;}

		//Foto do funcionario salvo em bytes, lembrar de converter
		public byte[]? foto { get; set;}
	}
}
