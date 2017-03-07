using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface IFichaCadastralAppService : IAppService<FichaCadastral>
    {
        ValidationResult IncluirFichaCadastral(FichaCadastral fichaCadastral);
        ValidationResult Incluir(FichaCadastral fichaCadastral, int idSolicitacao);

        ValidationResult IncluirArquivo(int idSolicitacao, int idDocumentoSolicitado, string nomeOriginal, int size,
            string url);
        List<Endereco> UpdateAdicionarEndereco(int fichaCadastral, List<Endereco> listaEndereco);
        List<Contato> UpdateAdicionarContato(int idFichaCadastral, List<Contato> enderecoLista);
        List<Banco> UpdateAdicionarBanco(int idFichaCadastral, List<Banco> enderecoLista);
    }
}