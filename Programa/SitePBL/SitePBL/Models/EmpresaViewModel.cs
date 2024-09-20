namespace SitePBL.Models
{
	public class EmpresaViewModel
	{
		//ID da empresa, não deve deixar o usuario dar o numero, deve ser o ultimo
		int id {  get; set; }
		
		//Nome da empresa
		string nome { get; set; }
		
		// Imagem da logo da empresa salva em bytes, lembrar de converter
		byte[]? logo { get; set; }

		//Nome da sede da empresa
		public string sede { get; set; }
	}
}
