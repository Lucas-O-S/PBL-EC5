namespace SitePBL.Models
{
	public class AcessoViewModel : PadraoViewModel
	{
		//id do usuario

		//Senha do usuario
		public string senha { get; set; }

		//id da empresa relacionada ao acesso, deve buscar com base no nome e verificar com o id depois
		public int empresa { get; set; }
	}
}
