using SitePBL.DAO;

namespace SitePBL.Models
{
	public class EmpresaViewModel : PadraoViewModel
    {
	

		//Nome da empresa
		public string nome { get; set; }

		// Imagem da logo da empresa salva em bytes, lembrar de converter
		public byte[]? logo { get; set; }

		//Nome da sede da empresa
		public string sede { get; set; }

	
	}
}
