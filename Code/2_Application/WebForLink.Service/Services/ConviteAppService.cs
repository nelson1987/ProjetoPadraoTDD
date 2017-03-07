using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services;

namespace WebForLink.Application.Services
{
    public class ConviteAppService : AppService<MusicStoreContext>, IConviteAppService
    {
        public ConviteAppService(IEmailAppService convite, IConviteService repository)
        {
            _convite = convite;
            _repository = repository;
        }

        private readonly IEmailAppService _convite;
        private readonly IConviteService _repository;

        public void EnviarConvite(Categoria convite)
        {
            throw new NotImplementedException();
        }

        public void ReenviarConvite(int idConviteAnterior, List<Documento> documentos)
        {
            throw new NotImplementedException();
        }

        public void ValidarConvite(int idConvite)
        {
            throw new NotImplementedException();
        }

        public int DescriptografarLinkConvite(string chaveUrl)
        {
            Criptografia descripto = new Criptografia(EnumCripto.LinkDescriptografar, chaveUrl, "r10X310y");
            int retorno = 0;
            var parametroCriptografia = descripto.Resultados.FirstOrDefault(x => x.Name == "id");
            if (parametroCriptografia != null)
                int.TryParse(parametroCriptografia.Value, out retorno);
            return retorno;
        }

        public Categoria BuscarConvite(int idConvite)
        {
            throw new NotImplementedException();
        }
    }

}
