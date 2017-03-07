using System;

namespace WebForLink.Web.ViewModels
{
    public class AnexoVM
    {
        public int ID { get; set; }
        public DateTime DataUpload {get; set;}
        public DateTime DataValidade { get; set; }
        public bool ExigeValidade { get; set; }
        public byte[] Arquivo { get; set; }        
    }
}