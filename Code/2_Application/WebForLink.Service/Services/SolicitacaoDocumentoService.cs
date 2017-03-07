using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class SolicitacaoAppService : AppService<ChMasterDataContext>, ISolicitacaoAppService
    {
        private readonly IFichaCadastralService _fichaCadastralService;
        private readonly ISolicitacaoService _Solicitacaoservice;
        private readonly IEmailAppService _emailService;

        public SolicitacaoAppService(ISolicitacaoService service, IFichaCadastralService fichaCadastral, IEmailAppService email)
        {
            _Solicitacaoservice = service;
            _fichaCadastralService = fichaCadastral;
            _emailService = email;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int DescriptografarLinkConvite(string chaveUrl)
        {
            chaveUrl = string.Format("{0}=", chaveUrl);
            var descripto = new Criptografia(EnumCripto.LinkDescriptografar, chaveUrl, "r10X310y");
            var retorno = 0;
            var parametroCriptografia = descripto.Resultados.FirstOrDefault(x => x.Key == "id");
            if (parametroCriptografia.Value != null)
                int.TryParse(parametroCriptografia.Value, out retorno);
            return retorno;
        }

        public IEnumerable<Solicitacao> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Solicitacao orderDetail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Solicitacao> Find(Expression<Func<Solicitacao, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Solicitacao Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Solicitacao Get(int id, bool @readonly = false)
        {
            return _Solicitacaoservice.Get(id, @readonly);
        }

        public Solicitacao GetArquivos(int id)
        {
            return _Solicitacaoservice.BuscarArquivo(id);
        }

        public ValidationResult Remove(Solicitacao orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Solicitacao solicitacao)
        {
            try
            {
                BeginTransaction();
                var sol = _Solicitacaoservice.GetAllReferences(solicitacao.Id, false);

                solicitacao.FichaCadastral.Banco.ToList()
                    .ForEach(x => { x.IdFichaCadastral = solicitacao.FichaCadastral.Id; });

                solicitacao.FichaCadastral.Contato.ToList()
                    .ForEach(x => { x.IdFichaCadastral = solicitacao.FichaCadastral.Id; });

                solicitacao.FichaCadastral.Endereco.ToList()
                    .ForEach(x => { x.IdFichaCadastral = solicitacao.FichaCadastral.Id; });

                sol.IncluirFichaCadastral(solicitacao.FichaCadastral);

                _Solicitacaoservice.Update(sol);

                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ValidationResult CriarSolicitacao(int idSolicitante, int idSolicitado, string mensagemEmail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult CriarSolicitacao(Solicitacao solicitacao)
        {
            throw new NotImplementedException();
        }

        public ValidationResult CriarSolicitacao(Solicitacao solicitacao, string mensagemEmail)
        {
            throw new NotImplementedException();
        }

        public Solicitacao GetAllReferences(int id, bool @readonly = false)
        {
            return _Solicitacaoservice.GetAllReferences(id, @readonly);
        }

        public Solicitacao GetAllReferencesFichaCadastral(int id)
        {
            return _Solicitacaoservice.GetAllReferencesFichaCadastral(id);
        }

        public ValidationResult SalvarFichaCadastral(FichaCadastral fichaCadastral)
        {
            try
            {
                BeginTransaction();
                fichaCadastral.Banco.ToList().ForEach(x => { x.IdFichaCadastral = fichaCadastral.Id; });
                fichaCadastral.Contato.ToList().ForEach(x => { x.IdFichaCadastral = fichaCadastral.Id; });
                fichaCadastral.Endereco.ToList().ForEach(x => { x.IdFichaCadastral = fichaCadastral.Id; });
                var validation = _fichaCadastralService.Update(fichaCadastral);
                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public FichaCadastral GetFichaCadastral(int id, bool @readonly = false)
        {
            return _fichaCadastralService.Get(id, @readonly);
        }

        public FichaCadastral GetFichaCadastralPorSolicitacao(int idSolicitacao, bool @readonly = false)
        {
            var solicitacao = _Solicitacaoservice.GetAllReferences(idSolicitacao, @readonly);
            return solicitacao.FichaCadastral;
        }

        public ValidationResult CriarSolicitacaoDocumento(Solicitacao solicitacao)
        {
            throw new NotImplementedException();
        }

        public ValidationResult CriarSolicitacaoDocumento(Solicitacao solicitacao, string mensagemEmail)
        {
            throw new NotImplementedException();
        }

        public void Visualizar(Solicitacao solicitacao)
        {
            try
            {
                var statusInicial = solicitacao.StatusSolicitacao;
                solicitacao.MudarStatusParaVisualizado();
                if (solicitacao.StatusSolicitacao != statusInicial)
                {
                    BeginTransaction();
                    var ficha = _fichaCadastralService.Incluir(new FichaCadastral(0, solicitacao));
                    _Solicitacaoservice.Update(solicitacao);
                    Commit();
                }
            }
            catch (StatusSolicitacaoException ex)
            {
                Debug.Write(ex);
            }
        }

        public void Finalizar(int idSolicitacao)
        {
            BeginTransaction();
            Solicitacao solicitacao = GetAllReferences(idSolicitacao, true);
            if (solicitacao.EhRespondida)
                throw new StatusSolicitacaoException("O arquivo já está finalizado.");

            if (!solicitacao.FichaCadastral.Contato.Any()
                || !solicitacao.FichaCadastral.Endereco.Any()
                || !solicitacao.FichaCadastral.Banco.Any())
                throw new StatusSolicitacaoException("Ainda existem pendências na ficha cadastral.");

            foreach (var documento in solicitacao.DocumentoSolicitacao)
            {
                foreach (var anexo in documento.DocumentoAnexados)
                {
                    if (!anexo.Arquivos.Any())
                        throw new StatusSolicitacaoException("Ainda existem pendências em documentos anexados.");

                }
            }
            solicitacao.MudarStatusParaRespondido();
            _Solicitacaoservice.Update(solicitacao);
            Commit();
            Debug.WriteLine(string.Format("Após finalizar solicitação: enviar email para {0}", solicitacao.Solicitante.Email));
            foreach (var item in solicitacao.Solicitado.Responsaveis)
            {
                Debug.WriteLine(string.Format("Após finalizar solicitação: enviar email para {0}", item.Email));
            }
            _emailService.IncluirAssuntoEmail("Teste de Email Finalizado");
            _emailService.IncluirMensagemEmail("Finalizado.");
            _emailService.EnviarEmail("nelson.neto@chconsultoria.com.br");
        }

        public Solicitacao BuscarFichaCadastral(string chave)
        {
            int id = DescriptografarLinkConvite(chave);
            Solicitacao solicitacao = _Solicitacaoservice.Get(id);
            if (solicitacao.IdFichaCadastral != null)
                solicitacao.FichaCadastral = _fichaCadastralService.GetAllReferences((int)solicitacao.IdFichaCadastral);
            return solicitacao;
        }
    }
}