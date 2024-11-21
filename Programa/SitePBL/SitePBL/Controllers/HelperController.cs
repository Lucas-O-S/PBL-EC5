namespace SitePBL.Controllers
{
    public static class HelperController
    {
        public static Boolean VerificaUserLogado(ISession session) {
            string logado = session.GetString("Logado");
            if(logado == null) 
                return false;
            else
                return true;
        }
    }
}
