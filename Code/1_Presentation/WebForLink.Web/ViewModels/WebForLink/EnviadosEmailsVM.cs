using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class EnviadosEmailsVM
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public bool VisualizouAlgumDoc { get; set; }
        public List<string> DocumentosVisualizados { get; set; }
    }
}