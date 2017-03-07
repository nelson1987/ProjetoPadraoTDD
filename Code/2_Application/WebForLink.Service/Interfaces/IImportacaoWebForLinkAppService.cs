using System;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.FiltrosDTO;

namespace WebForLink.Application.Interfaces
{
    public interface IImportacaoWebForLinkAppService
    {
        ImportacaoDTO ImportarComConvite(int idFornecedorBase, int idUsuario, string assuntoMensagem,
            string mensagemTexto);

        void ProrrogarPrazo(int solicitacaoId, int idUsuario, DateTime dataProrrogacao, string motivo);
        void AvaliarProrrogacao(int[] idsFornecedorBase, int idUsuario, string motivo, EnumTiposFuncionalidade avaliacao);
    }
}
