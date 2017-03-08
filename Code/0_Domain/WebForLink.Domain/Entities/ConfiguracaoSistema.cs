using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class ConfiguracaoSistema : ISelfValidation
    {
        private ConfiguracaoSistema()
        {
        }

        public ConfiguracaoSistema(string caminhoArquivo, Contratante contratante)
            : this()
        {
            CaminhoArquivo = caminhoArquivo;
            Contratante = contratante;
        }

        public int Id { get; set; }
        public bool EhValido { get; }
        public ValidationResult ValidationResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public string CaminhoArquivo { get; private set; }
        public Contratante Contratante { get; private set; }
    }
}