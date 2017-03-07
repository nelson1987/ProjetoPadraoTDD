using System;
using System.Collections.Generic;
using System.Linq;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Service;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class AdesaoWebForLinkAppService : AppService<WebForLinkContexto>, IAdesaoWebForLinkAppService
    {
        private readonly IFornecedorWebForLinkService _fichaCadastralService;
        private readonly IUsuarioWebForLinkService _usuarioService;
        private readonly IPerfilWebForLinkService _perfilService;

        public AdesaoWebForLinkAppService(IFornecedorWebForLinkService fichaCadastral
            , IUsuarioWebForLinkService usuarioService
            , IPerfilWebForLinkService perfilService)
        {
            _fichaCadastralService = fichaCadastral;
            _usuarioService = usuarioService;
            _perfilService = perfilService;
        }

        public Transaction BuscarTransacaoPagSeguro(string notificationCode)
        {
            try
            {
                AccountCredentials credentials = new AccountCredentials(
                "pagseguro@chconsultoria.com.br",
                        "86D588A7611E48FABA6125B049503F5F"
                    );
                var transacao = TransactionSearchService.SearchByCode(credentials, notificationCode);
                if (transacao.TransactionStatus == TransactionStatus.Paid)
                    if (transacao != null)
                    {
                        var idCriptografado = transacao.Reference.Replace("REF_", "");
                        //var descripto = new Criptografia(EnumCripto.Descriptografar, idCriptografado, "r10X310y");
                        //var cnpjIncluso = descripto.Resultado;
                        var cnpjIncluso = "65238377000129";
                        var registroCnpj = _fichaCadastralService.Find(x => x.CNPJ == cnpjIncluso);
                        if (registroCnpj.Any())
                            BuscarDadosParaMigrarFornecedorParaFornecedorIndividual(registroCnpj.ToList());
                        else
                            CriarFornecedorIndividual(cnpjIncluso,"");
                    }
                return transacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CriarFornecedorIndividual(string documento, string email)
        {
            _usuarioService.IncluirNovoUsuarioPadrao(new Usuario
            {
                ATIVO = true,
                CONTRATANTE_ID = null,
                CPF_CNPJ = documento,
                EMAIL = email,

            },
                new USUARIO_SENHAS
                {
                }, null, null);
            //Contratante
            //Usuario
            //Fornecedor
        }
        private void BuscarDadosParaMigrarFornecedorParaFornecedorIndividual(List<Fornecedor> fornecedores)
        {
            string cnpj = fornecedores.FirstOrDefault().CNPJ;
            var usuario = _usuarioService.Get(x => x.CPF_CNPJ == cnpj);
            usuario.WAC_PERFIL = _perfilService.Find(x => x.ID == 40).ToList();
            usuario.CONTRATANTE_ID = null;
            BeginTransaction();
            _usuarioService.Update(usuario);
            Commit();
        }


        //public ValidationResult Create(Fornecedor entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Fornecedor> Find(Expression<Func<Fornecedor, bool>> predicate, bool @readonly = false)
        //{
        //    throw new NotImplementedException();
        //}

        //public Fornecedor Get(string id, bool @readonly = false)
        //{
        //    throw new NotImplementedException();
        //}

        //public Fornecedor Get(int id, bool @readonly = false)
        //{
        //    throw new NotImplementedException();
        //}

        //public Fornecedor GetAllReferences(int id, bool @readonly = false)
        //{
        //    throw new NotImplementedException();
        //}

        //public ValidationResult Remove(Fornecedor entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public ValidationResult Update(Fornecedor entity)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<Fornecedor> IAppService<Fornecedor>.All(bool @readonly)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
