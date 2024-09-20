namespace SitePBL.Models
{
	public class AcessoViewModel
	{
		//ID do acesso, não deve deixar o usuario dar o numero, deve ser o ultimo
		public int id { get; set; }

		//Senha do usuario
		public string senha { get; set; }

		//Id da empresa relacionada ao acesso, deve buscar com base no nome e verificar com o id depois
		public int empresa { get; set; }
	}
}
