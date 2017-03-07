using System;

namespace WebForLink.Domain.Entities
{
    [Serializable]
    public class PrincipalSerializeModel
    {
        public string Cnpj { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailLogin { get; set; }
        public bool FornecedorIndividual { get; set; }
        public PrincipalPerfilSerializeModel PerfilCorrente { get; set; }
        public PrincipalPerfilSerializeModel[] Perfis { get; set; }
        public PrincipalPapelSerializeModel PapelCorrente { get; set; }
        public PrincipalPapelSerializeModel[] Papel { get; set; }
    }

    [Serializable]
    public class PrincipalPerfilSerializeModel
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
    }

    [Serializable]
    public class PrincipalPapelSerializeModel
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
    }
}
