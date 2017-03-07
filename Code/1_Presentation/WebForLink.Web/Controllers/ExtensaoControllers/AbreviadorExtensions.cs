using KeySupport.Ferramentas.Abreviacoes;
using KeySupport.Ferramentas.Abreviacoes.Extensoes;
using System;

namespace WebForLink.Web.Controllers.Extensoes
{
    public class AbreviadorExtensions
    {
        public string AbreviadorRobo(int meta, string nome)
        {
            CarregarDicionarios();
            AbreviaResultado AbreviaResultado = nome.Abrevia(AbreviacaoTipos.Nome, meta, true, true, true, true, true, true, true, true, "01195269000133-S1-CHC");
            return AbreviaResultado.Resultado;
        }

        private static void CarregarDicionarios()
        {
            string logradourosPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Dicionarios\Logradouros.dic");
            ConfiguracoesAbreviacao.Dicionarios.Carrega("Logradouros", logradourosPath, true);

            string namesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Dicionarios\Nomes.dic");
            ConfiguracoesAbreviacao.Dicionarios.Carrega("Nomes", namesPath, true);

            string maisPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Dicionarios\AbreviaMais.dic");
            ConfiguracoesAbreviacao.Dicionarios.Carrega("AbreviaMais", maisPath, true);

            string preposicoesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Dicionarios\Preposicoes.dic");
            ConfiguracoesAbreviacao.Dicionarios.Carrega("Preposicoes", preposicoesPath, false);

            string excecoesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Dicionarios\Excecoes.dic");
            ConfiguracoesAbreviacao.Dicionarios.Carrega("Excecoes", excecoesPath, true);

            string expressoesPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\Dicionarios\Expressoes.dic");
            ConfiguracoesAbreviacao.Dicionarios.Carrega("Expressoes", expressoesPath, true);
        }
    }
}