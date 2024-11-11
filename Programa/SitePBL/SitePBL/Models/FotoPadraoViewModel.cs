namespace SitePBL.Models
{
    /// <summary>
    /// Classe abstrada derivada de padrão view model que utiliza imagens
    /// </summary>
    public abstract class FotoPadraoViewModel : PadraoViewModel
    {
        /// <summary>
        /// Imagem em IFormFile
        /// </summary>
        public IFormFile? imagem {  get; set; }
        
        /// <summary>
        ///Imagem em Bytes 
        /// </summary>
        public byte[]? imagembyte { get; set; }

        /// <summary>
        /// Imagem em base 64 com base na imagem em bytes, somente get
        /// </summary>
        public string? foto64 {
            get
            {
                if (imagembyte != null)
                    return Convert.ToBase64String(imagembyte);
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converte a imagem em IFormFile em Byte
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }
    }
}
