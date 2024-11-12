namespace SitePBL.Models
{
    /// <summary>
    /// Struct para a leitura do sensor
    /// </summary>
    public struct LeituraViewModel
    {
        /// <summary>
        /// Temperatura lida, somente get
        /// </summary>
        public float temperatura { get; }

        /// <summary>
        /// data/hora da leitura, somente get
        /// </summary>
        public DateTime data { get; }

        /// <summary>
        /// Contrutor de leitura, exige temperatura e data
        /// </summary>
        /// <param name="temperatura"></param>
        /// <param name="data"></param>
        public LeituraViewModel(float temperatura, DateTime data)
        {
            this.data = data;
            this.temperatura = temperatura;
        }
    }
}
