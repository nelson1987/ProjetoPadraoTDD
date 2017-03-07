using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.Areas.Externo.Models
{
    public class DadosExternoPreCadastro
    {
        public DadosExternoPreCadastro()
        {

        }
        public DadosExternoPreCadastro(List<Fornecedor> pjpfList, List<FORNECEDORBASE> pjpfBaseList, string documentoFornecedor, int contratanteId)
        {
            FornecedorBaseList = pjpfBaseList;
            ContratanteId = contratanteId;
            Documento = documentoFornecedor;
            IsFornecedorProprio = false;
            IsFornecedorOutroContratante = false;
            IsFornecedorBaseProprio = false;
            IsFornecedorBaseOutroContratante = false;
            FornecedorBaseProprio = FornecedorBaseList.FirstOrDefault(w => w.WFD_CONTRATANTE.ID == ContratanteId) != null
                ? FornecedorBaseList.FirstOrDefault(w => w.WFD_CONTRATANTE.ID == ContratanteId)
                : null;
            FornecedorList = pjpfList;
            FornecedorProprio = FornecedorList.FirstOrDefault(x => x.WFD_CONTRATANTE_PJPF.Any(y => y.CONTRATANTE_ID == ContratanteId)) != null
                ? FornecedorList.FirstOrDefault(x => x.WFD_CONTRATANTE_PJPF.Any(y => y.CONTRATANTE_ID == ContratanteId))
                : null;
        }
        public DadosExternoPreCadastro(List<FORNECEDORBASE> pjpfBaseList, string documentoFornecedor, int contratanteId)
        {
            FornecedorBaseList = pjpfBaseList;
            ContratanteId = contratanteId;
            Documento = documentoFornecedor;
            IsFornecedorProprio = false;
            IsFornecedorOutroContratante = false;
            IsFornecedorBaseProprio = false;
            IsFornecedorBaseOutroContratante = false;
            FornecedorBaseProprio = FornecedorBaseList.FirstOrDefault(w => w.WFD_CONTRATANTE.ID == ContratanteId) != null
                ? FornecedorBaseList.FirstOrDefault(w => w.WFD_CONTRATANTE.ID == ContratanteId)
                : null;
        }
        //
        public int ContratanteId { get; set; }
        public string Documento { get; set; }
        public bool IsFornecedorProprio { get; set; }
        public bool IsFornecedorOutroContratante { get; set; }
        public bool IsFornecedorBaseProprio { get; set; }
        public bool IsFornecedorBaseOutroContratante { get; set; }
        public CasosPreCadastroEnum PreCadastroEnum { get; set; }
        public List<FORNECEDORBASE> FornecedorBaseList { get; set; }
        public List<Fornecedor> FornecedorList { get; set; }
        public FORNECEDORBASE FornecedorBaseProprio { get; set; }
        public Fornecedor FornecedorProprio { get; set; }
        //
        public void PopularDados()
        {
            //if (FornecedorList.Any())
            //    if (FornecedorList.FirstOrDefault(x => x.WFD_CONTRATANTE_PJPF.Any(y => y.CONTRATANTE_ID == ContratanteId)) != null)
            //        IsFornecedorProprio = true;
            //    else
            //        IsFornecedorOutroContratante = true;

            if (FornecedorBaseList.Any())
                if (FornecedorBaseList.FirstOrDefault(w => w.WFD_CONTRATANTE.ID == ContratanteId) != null)
                    IsFornecedorBaseProprio = true;
                else
                    IsFornecedorBaseOutroContratante = true;
            else
                IsFornecedorBaseOutroContratante = true;

            if (IsFornecedorBaseOutroContratante)
                PreCadastroEnum = CasosPreCadastroEnum.PreCadastradoOutroContratante; // Exibir tela "em branco" para alteração
            //if (IsFornecedorOutroContratante)
            //    PreCadastroEnum = CasosPreCadastroEnum.CadastradoOutroContratante; //EXIBIR DADOS PJPF OUTRO
            if (IsFornecedorBaseProprio)
                PreCadastroEnum = CasosPreCadastroEnum.PreCadastradoProprio;// EXIBIR DADOS BASE
            //if (IsFornecedorProprio)
            //    PreCadastroEnum = CasosPreCadastroEnum.CadastradoProprio; //EXIBIR DADOS BLOQUEADOS
        }
    }
    
}