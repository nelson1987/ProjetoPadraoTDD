using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Domain.Entities
{
    public class FornecedorVendorlist
    {
        public FornecedorVendorlist(string cnpj, Usuario usuario)
        {
            Responsavel = usuario;
            Cnpj = cnpj;
        }
        public string Cnpj { get; private set; }
        public IUsuario Responsavel { get; private set; }
    }

    public class ClienteWebFormat
    {
        public ClienteWebFormat(Usuario usuario)
        {
            Responsavel = usuario;
        }
        public IUsuario Responsavel { get; private set; }
    }
    
    public class AdesaoService : IAdesaoService
    {
        public AdesaoService(IAdesaoRepository repository, IApiPagamento pagSeguro)
        {
            _pagSeguro = pagSeguro;
            _repository = repository;
        }
        private readonly IAdesaoRepository _repository;
        private readonly IApiPagamento _pagSeguro;
        public void CriarAdesao(Adesao adesao)
        {
            throw new NotImplementedException();
        }
    }
    
    public class Compartilhamento
    {
        public Compartilhamento(List<Documento> documentos, int responsavel)
        {
            Documentos = documentos;
            Responsavel = responsavel;
        }
        public List<Documento> Documentos { get; private set; }
        public int Responsavel { get; private set; }
    }
    
}
