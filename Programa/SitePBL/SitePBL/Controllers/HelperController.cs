namespace SitePBL.Controllers
{
    public static class HelperController
    {
        /// <summary>
        /// verifica se a session esta logada
        /// </summary>
        /// <param name="session"></param>
        /// <returns>Resultado do login</returns>
        public static Boolean VerificaUserLogado(ISession session) {
            string logado = session.GetString("Logado");
            if(logado == null) 
                return false;
            else
                return true;
        }
    }
}
