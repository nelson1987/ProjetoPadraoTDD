using System;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Application.Services.Process;

namespace WebForLink.Domain.Services.Adesao
{
    public class AdesaoWebForLinkAppService: AppService<WebForLinkContexto>, IAdesaoWebForLinkService
    {
        private readonly IUnitOfWork Processo;

        public void Dispose()
        {
            Processo.Finalizar();
        }

        public AdesaoWebForLinkAppService(IUnitOfWork processo)
        {
            try
            {
                Processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
        /// <summary>
        /// Ao criar uma nova adesão será criado uma string de criação de adesão junto ao PagSeguro
        /// </summary>
        public string CriarNovaAdesao(OrdemPagamento usuario)
        {
            try
            {
                int idSolicitacao = 1;
                usuario.Referencia = string.Format("REFPLANO0{0}{1}", usuario.TipoConta, idSolicitacao.ToString());
                Pagamento pagamento = new Pagamento(usuario);
                try
                {
                    pagamento.EfetuarPagamento((TipoPlano)usuario.TipoConta);
                    //throw new Exception();
                }
                catch (Exception ex)
                {
                    throw new ServiceWebForLinkException("Erro ao tentar criar url", ex);
                }
                return pagamento.UrlRetorno;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar processar pagamento", ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ConfirmarAdesao()
        {
            //SITE WFL - Registrar Empresa
            //SITE WFL - Escolher o plano
            //SITE WFL - Efetuar Pagamento
            //ADM CH - Confirmar o Pagamento
            //ADM CH - Liberar o Uso do WFL
            //Empresa - Receber a senha do usuário MASTER
            //Empresa - Acessar o APLICATIVO WFL
            //Pagamento pagamento = new Pagamento(usuario);
            //try
            //{
            //    pagamento.EfetuarPagamento ((TipoPlano)usuario.TipoConta);
            //}
        }
    }
}
