namespace WebForLink.Domain.Enums
{
    public enum CasosPreCadastroEnum
    {
        /// <summary>
        ///     Importado com outro contratante - Exibir em branco
        /// </summary>
        PreCadastradoOutroContratante = 1,

        /// <summary>
        ///     Pré-Cadastrado - Exibir dados Base
        /// </summary>
        PreCadastradoProprio = 2,

        /// <summary>
        ///     Cadastrado com outro contratante - Exibir Pjpf Outro Contratante
        /// </summary>
        CadastradoOutroContratante = 3,

        /// <summary>
        ///     Cadastrado com contratante atual - Exibir Dados Bloqueado
        /// </summary>
        CadastradoProprio = 4,

        /// <summary>
        ///     Além de ter dados incluídos na FORNECEDORBASE ou atualizados, terá a solicitação e passará por robô
        /// </summary>
        CadastradoPorContratante = 5
    }
}