using System;
using System.Collections.Generic;
using WebForLink.Domain.Enums;

namespace WebForLink.Domain.Administracao
{
    public class AdesaoContratante
    {
        public AdesaoContratante()
        {
            UsuarioComuns = new List<UsuarioAdesaoContratante>();
            UsuarioAdministrador = new UsuarioAdesaoContratante(true);
            TipoEmpresa = EnumTiposFornecedor.EmpresaNacional;
            TipoContratante = EnumTipoContratante.ContratanteAncora;
            Documento = "59.483.571/0001-77";
            RazaoSocial = "Teste Âncora";
            NomeFantasia = "Contratante TESTE";
            CodigoWebformat = "ANCORA";
            CodigoErp = "123456";
            Estilo = "Azul";
            Grupo = String.Format("Grupo {0}", RazaoSocial.ToUpper());
        }
        public EnumTiposFornecedor TipoEmpresa { get; set; }
        public EnumTipoContratante TipoContratante { get; set; }
        public string Documento { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CodigoWebformat { get; set; }
        public string CodigoErp { get; set; }
        public string Estilo { get; set; }
        public string Grupo { get; set; }
        public List<UsuarioAdesaoContratante> UsuarioComuns { get; set; }
        public UsuarioAdesaoContratante UsuarioAdministrador { get; set; }

    }
    public class UsuarioAdesaoContratante
    {
        public UsuarioAdesaoContratante()
        {
        }
        public UsuarioAdesaoContratante(bool administrador)
        {
            Cargo = "Administrador";
            Nascimento = DateTime.Now;
            Email = "nelson.neto@chconsultoria.com.br";
            Documento = "16496011909";
            Login = "16496011909";
            Administrador = administrador;
            Nome = "José Pereira";
        }
        public string Cargo { get; set; }
        public string Documento { get; set; }
        public DateTime Nascimento { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public bool Administrador { get; set; }
        public string Nome { get; set; }
    }
}
