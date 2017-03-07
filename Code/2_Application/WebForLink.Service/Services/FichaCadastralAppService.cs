using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services
{
    public class FichaCadastralAppService : AppService<ChMasterDataContext>, IFichaCadastralAppService
    {
        public FichaCadastralAppService(IFichaCadastralService fichaCadastral,
            IEnderecoAppService endereco,
            IBancoAppService banco,
            IContatoAppService contato)
        {
            FichaCadastralService = fichaCadastral;
            EnderecoService = endereco;
            BancoService = banco;
            ContatoService = contato;
        }

        private IFichaCadastralService FichaCadastralService { get; set; }
        private IEnderecoAppService EnderecoService { get; set; }
        private IContatoAppService ContatoService { get; set; }
        private IBancoAppService BancoService { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValidationResult IncluirFichaCadastral(FichaCadastral fichaCadastral)
        {
            try
            {
                BeginTransaction();
                //Solicitacao sol = _Solicitacaoservice.GetAllReferences(solicitacao.Id, true);

                //solicitacao.FichaCadastral.Bancos.ToList().ForEach(x =>
                //{
                //    x.IdFichaCadastral = solicitacao.FichaCadastral.Id;
                //});

                //solicitacao.FichaCadastral.Contatos.ToList().ForEach(x =>
                //{
                //    x.IdFichaCadastral = solicitacao.FichaCadastral.Id;
                //});

                //solicitacao.FichaCadastral.Enderecos.ToList().ForEach(x =>
                //{
                //    x.IdFichaCadastral = solicitacao.FichaCadastral.Id;
                //});

                //sol.FichaCadastral = solicitacao.FichaCadastral;

                FichaCadastralService.Update(fichaCadastral);

                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FichaCadastral> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(FichaCadastral orderDetail)
        {
            try
            {
                BeginTransaction();
                FichaCadastralService.Add(orderDetail);
                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FichaCadastral> Find(Expression<Func<FichaCadastral, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FichaCadastral Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FichaCadastral Get(int id, bool @readonly = false)
        {
            return FichaCadastralService.Get(id, @readonly);
        }

        public FichaCadastral GetAllReferences(int id, bool @readonly = false)
        {
            return FichaCadastralService.GetAllReferences(id, @readonly);
        }

        public ValidationResult Remove(FichaCadastral orderDetail)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FichaCadastral orderDetail)
        {
            try
            {
                BeginTransaction();
                var sol = FichaCadastralService.GetAllReferences(orderDetail.Id);

                orderDetail.Banco.ToList()
                    .ForEach(x => { x.IdFichaCadastral = orderDetail.Id; });

                orderDetail.Contato.ToList()
                    .ForEach(x => { x.IdFichaCadastral = orderDetail.Id; });

                orderDetail.Endereco.ToList()
                    .ForEach(x => { x.IdFichaCadastral = orderDetail.Id; });

                //sol.IncluirFichaCadastral(orderDetail);

                FichaCadastralService.Update(sol);

                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ValidationResult IncluirArquivo(int idSolicitacao, int idDocumentoSolicitado, string nomeOriginal,
            int size, string url)
        {
            try
            {
                BeginTransaction();
                FichaCadastralService.IncluirArquivo(idSolicitacao, idDocumentoSolicitado, nomeOriginal, size, url);
                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ValidationResult Incluir(FichaCadastral fichaCadastral, int idSolicitacao)
        {
            try
            {
                BeginTransaction();
                FichaCadastralService.Incluir(fichaCadastral, idSolicitacao);
                Commit();
                return ValidationResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ValidationResult IncluirArquivo(string name, int size, string url)
        {
            throw new NotImplementedException();
        }

        public List<Endereco> UpdateAdicionarEndereco(int idFichaCadastral, List<Endereco> enderecos)
        {
            try
            {
                BeginTransaction();
                var fichaCadastral = FichaCadastralService.Get(idFichaCadastral);

                EnderecoService.Remove(fichaCadastral.Endereco.ToList());

                foreach (var item in enderecos)
                {
                    item.FichaCadastral = fichaCadastral;
                    EnderecoService.Create(item);
                }
                Commit();
                return enderecos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Contato> UpdateAdicionarContato(int idFichaCadastral, List<Contato> enderecoLista)
        {
            try
            {
                BeginTransaction();
                var fichaCadastral = FichaCadastralService.Get(idFichaCadastral);

                ContatoService.Remove(fichaCadastral.Contato.ToList());

                foreach (var item in enderecoLista)
                {
                    item.FichaCadastral = fichaCadastral;
                    ContatoService.Create(item);
                }
                Commit();
                return enderecoLista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Banco> UpdateAdicionarBanco(int idFichaCadastral, List<Banco> enderecoLista)
        {
            try
            {
                BeginTransaction();
                var fichaCadastral = FichaCadastralService.Get(idFichaCadastral);

                BancoService.Remove(fichaCadastral.Banco.ToList());

                foreach (var item in enderecoLista)
                {
                    item.FichaCadastral = fichaCadastral;
                    BancoService.Create(item);
                }
                Commit();
                return enderecoLista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}