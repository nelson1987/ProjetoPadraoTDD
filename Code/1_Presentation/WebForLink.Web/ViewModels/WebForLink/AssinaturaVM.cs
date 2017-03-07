namespace WebForLink.Web.ViewModels
{
    public class AssinaturaEmailVM
    {
        public string Mensagem { get; set; }
        public string NomeContratante { get; set; }
        public string NomeSolicitante { get; set; }
        public string TelefoneSolicitante { get; set; }
        public string EmailSolicitante { get; set; }
        public string CelularSolicitante { get; set; }

        public AssinaturaEmailVM()
        { 
        }
        public void CriarMensagem()
        {
            Mensagem.Replace("^NomeEmpresa^", NomeContratante)
                .Replace("^NomeUsuario^", NomeSolicitante)
                .Replace("^FixoUsuario1^", TelefoneSolicitante)
                .Replace("^CelularUsuario1^", CelularSolicitante)
                .Replace("^EmailUsuario^", EmailSolicitante);
        }
    }
}