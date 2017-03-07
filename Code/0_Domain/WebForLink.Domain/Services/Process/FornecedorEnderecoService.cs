using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorEnderecoWebForLinkService : IService<FORNECEDOR_ENDERECO>
    {
        FORNECEDOR_ENDERECO BuscarPorID(int id);
        List<FORNECEDOR_ENDERECO> BuscarPorContratantePJPFId(int id);
        void AlterarFornecedorEndereco(List<FORNECEDOR_ENDERECO> enderecos, int contratantePjPfId);
    }

    public class FornecedorEnderecoWebForLinkService : Service<FORNECEDOR_ENDERECO>,
        IFornecedorEnderecoWebForLinkService
    {
        private readonly IFornecedorEnderecoWebForLinkRepository _fornecedorEnderecoRepository;

        public FornecedorEnderecoWebForLinkService(IFornecedorEnderecoWebForLinkRepository fornecedorEnderecoRepository)
            : base(fornecedorEnderecoRepository)
        {
            try
            {
                _fornecedorEnderecoRepository = fornecedorEnderecoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDOR_ENDERECO BuscarPorID(int id)
        {
            try
            {
                return _fornecedorEnderecoRepository.Get(id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados de endereço.", e);
            }
        }

        public List<FORNECEDOR_ENDERECO> BuscarPorContratantePJPFId(int id)
        {
            try
            {
                return _fornecedorEnderecoRepository.Find(x => x.CONTRATANTE_PJPF_ID == id).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados de endereço.", e);
            }
        }

        public void AlterarFornecedorEndereco(List<FORNECEDOR_ENDERECO> enderecos, int contratantePjPfId)
        {
            var ids = enderecos.Select(x => x.ID).ToArray();
            _fornecedorEnderecoRepository.Delete(
                _fornecedorEnderecoRepository.Find(x => ids.Contains(x.ID) && x.CONTRATANTE_PJPF_ID == contratantePjPfId)
                    .ToList());

            foreach (var item in enderecos)
            {
                if (string.IsNullOrEmpty(item.LATITUDE))
                //{
                //    GeoEndereco teste = new GeoEndereco()
                //    {
                //        Bairro = item.BAIRRO,
                //        Cidade = item.CIDADE,
                //        Cep = item.CEP,
                //        Rua = item.ENDERECO,
                //        Numero = item.NUMERO,
                //        Pais = item.PAIS,
                //        Estado = item.UF,
                //    };
                //    GeoResponse resposta = GeoLocation.GetAddress(teste);
                //    if (resposta.Status == "OK")
                //    {
                //        item.LATITUDE = resposta.Resultados[0].GeoPosicao.Localizacao.Latitude;
                //        item.LONGITUDE = resposta.Resultados[0].GeoPosicao.Localizacao.Longitude;
                //    }
                //}
                if (item.ID == 0)
                {
                    item.CONTRATANTE_PJPF_ID = contratantePjPfId;
                    _fornecedorEnderecoRepository.Add(item);
                }
                else
                {
                    var endereco = _fornecedorEnderecoRepository.Get(item.ID);
                    //FORNECEDOR_ENDERECO mescla = _fornecedorEnderecoRepository.MesclarObjetos(endereco, item);
                    //endereco.TP_ENDERECO_ID = item.TP_ENDERECO_ID;
                    //endereco.ENDERECO = item.ENDERECO;
                    //endereco.NUMERO = item.NUMERO;
                    //endereco.COMPLEMENTO = item.COMPLEMENTO;
                    //endereco.CEP = item.CEP;
                    //endereco.BAIRRO = item.BAIRRO;
                    //endereco.CIDADE = item.CIDADE;
                    //endereco.UF = item.UF;
                    //endereco.PAIS = item.PAIS;
                    //mescla.CONTRATANTE_PJPF_ID = contratantePjPfId;
                    //_fornecedorEnderecoRepository.Update(mescla);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}