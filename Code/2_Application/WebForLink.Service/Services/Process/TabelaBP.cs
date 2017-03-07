using System.Collections.Generic;
using WebForDocs.Business.Manager;
using WebForDocs.Data.ModeloDB;

namespace WebForDocs.Business.Process
{
    /*
    public class QuestionarioAbaBP : BPBase
    {
        readonly QuestionarioAbaBM _questionarioAbaBM = new QuestionarioAbaBM();
        public QIC_QUEST_ABA BuscarPorID(int id)
        {
            return _questionarioAbaBM.BuscarPorID(id);
        }
    }

    public class QuestionarioAbaPerguntaBP : BPBase
    {
        readonly QuestionarioAbaPerguntaBM _questionarioAbaPerguntaBM = new QuestionarioAbaPerguntaBM();
        public QIC_QUEST_ABA_PERG BuscarPorID(int id)
        {
            return _questionarioAbaPerguntaBM.BuscarPorID(id);
        }
        public List<QIC_QUEST_ABA_PERG> BuscarPorPerguntasFilho(int id)
        {
            return _questionarioAbaPerguntaBM.BuscarPorPerguntasFilho(id);
        }
    }

    public class QuestionarioAbaPerguntaPapelBP : BPBase
    {
        readonly QuestionarioAbaPerguntaPapelBM _questionarioAbaPerguntaPapelBM = new QuestionarioAbaPerguntaPapelBM();
        public QIC_QUEST_ABA_PERG_PAPEL BuscarPorID(int id)
        {
            return _questionarioAbaPerguntaPapelBM.BuscarPorID(id);
        }
    }

    public class QuestionarioAbaPerguntaRespostaBP : BPBase
    {
        private readonly QuestionarioAbaPerguntaRespostaBM _questionarioAbaPerguntaRespostaBM = new QuestionarioAbaPerguntaRespostaBM();
        readonly QuestionarioAbaPerguntaBM _questionarioAbaPerguntaBM = new QuestionarioAbaPerguntaBM();
        
        public QIC_QUEST_ABA_PERG_RESP BuscarPorID(int id)
        {
            return _questionarioAbaPerguntaRespostaBM.BuscarPorID(id);
        }
    }

    public class BancoBP : BPBase
    {
        readonly BancoBM _bancoBM = new BancoBM();
        
        public T_BANCO BuscarPorID(int id)
        {
            return _bancoBM.BuscarPorID(id);
        }

        public List<T_BANCO> ListarTodosPorNome()
        {
            return _bancoBM.ListarTodosPorNome();
        }
    }

    public class UfBP : BPBase
    {
        readonly UfBM _ufBM = new UfBM();
        public T_UF BuscarPorID(string sigla)
        {
            return _ufBM.BuscarPorID(sigla);
        }
    }

    public class UnpscBP : BPBase
    {
        readonly UnpscBM _unpscBM = new UnpscBM();
        public T_UNSPSC BuscarPorID(int id)
        {
            return _unpscBM.BuscarPorID(id);
        }
    }

    public class AplicacaoBP : BPBase
    {
        readonly AplicacaoBM _aplicacaoBM = new AplicacaoBM();

        public WAC_APLICACAO BuscarPorID(int id)
        {
            return _aplicacaoBM.BuscarPorID(id);
        }

        public List<WAC_APLICACAO> ListarTodos()
        {
            return _aplicacaoBM.ListarTodos();
        }
    }

    public class FuncaoBP : BPBase
    {
        readonly FuncaoBM _funcaoBM = new FuncaoBM();
        public WAC_FUNCAO BuscarPorID(int id)
        {
            return _funcaoBM.BuscarPorID(id);
        }
    }

    public class ContrantantePJPFBancoBP : BPBase
    {
        readonly ContratantePJPFBancoBM _contratantePJPFBancoBM = new ContratantePJPFBancoBM();
        public WFD_CONTR_PJPF_BANCO BuscarPorID(int id)
        {
            return _contratantePJPFBancoBM.BuscarPorID(id);
        }
    }

    public class ContratanteConfiguracaoBP : BPBase
    {
        readonly ContratanteConfiguracaoBM _contratanteConfiguracaoBM = new ContratanteConfiguracaoBM();
        public WFD_CONTRATANTE_CONFIG BuscarPorID(int id)
        {
            return _contratanteConfiguracaoBM.BuscarPorID(id);
        }
    }

    public class ContratanteOrganizacaoComprasBP : BPBase
    {
        readonly ContratanteOrganizacaoComprasBM _contratanteOrganizacaoComprasBM = new ContratanteOrganizacaoComprasBM();
        public WFD_CONTRATANTE_ORG_COMPRAS BuscarPorID(int id)
        {
            return _contratanteOrganizacaoComprasBM.BuscarPorID(id);
        }
    }

    public class ContratantePjpfBP : BPBase
    {
        readonly ContratantePJPFBM _contratantePJPFBM = new ContratantePJPFBM();
        public WFD_CONTRATANTE_PJPF BuscarPorID(int id)
        {
            return _contratantePJPFBM.BuscarPorID(id);
        }
    }

    public class DescricaoDocumentosBP : BPBase
    {
        readonly DescricaoDocumentosBM _descricaoDocumentosBM = new DescricaoDocumentosBM();
        public WFD_DESCRICAO_DOCUMENTOS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class DescricaoDocumentosCHBP : BPBase
    {
        readonly DescricaoDocumentosCHBM _descricaoDocumentosCHBM = new DescricaoDocumentosCHBM();
        public WFD_DESCRICAO_DOCUMENTOS_CH BuscarPorID(int id)
        {
            return _descricaoDocumentosCHBM.BuscarPorID(id);
        }
    }

    public class DestinatarioBP : BPBase
    {
        readonly DestinatarioBM _destinatarioBM = new DestinatarioBM();
        public WFD_DESTINATARIO BuscarPorID(int id)
        {
            return _destinatarioBM.BuscarPorID(id);
        }
    }

    public class ExtensaoArquivoBP : BPBase
    {
        readonly ExtensaoArquivoBM _extensaoArquivoBM = new ExtensaoArquivoBM();
        public WFD_EXTENSAO_ARQUIVO BuscarPorID(string extensao)
        {
            return _extensaoArquivoBM.BuscarPorID(extensao);
        }
    }

    public class GrupoBP : BPBase
    {
        readonly GrupoBM _drupoBM = new GrupoBM();
        public WFD_GRUPO BuscarPorID(int id)
        {
            return _drupoBM.BuscarPorID(id);
        }
    }

    public class MeusCompartilhamentosBP : BPBase
    {
        readonly MeusCompartilhamentosBM _deusCompartilhamentosBM = new MeusCompartilhamentosBM();
        public WFD_MEUS_COMPARTILHAMENTOS BuscarPorID(int id)
        {
            return _deusCompartilhamentosBM.BuscarPorID(id);
        }
    }

    public class MeusDocumentosBP : BPBase
    {
        readonly MeusDocumentosBM _deusDocumentosBM = new MeusDocumentosBM();
        public WFD_MEUS_DOCUMENTOS BuscarPorID(int id)
        {
            return _deusDocumentosBM.BuscarPorID(id);
        }
    }

    public class MeusDocumentosCompartilhadosBP : BPBase
    {
        readonly MeusDocumentosCompartilhadosBM _deusDocumentosCompartilhadosBM = new MeusDocumentosCompartilhadosBM();
        public WFD_MEUS_DOCUMENTOS_COMPARTILHADOS BuscarPorID(int id)
        {
            return _deusDocumentosCompartilhadosBM.BuscarPorID(id);
        }
    }

    public class PJPFBP : BPBase
    {
        readonly PJPFBM _pjpfBM = new PJPFBM();
        public WFD_PJPF BuscarPorID(int id)
        {
            return _pjpfBM.BuscarPorID(id);
        }

        public List<WFD_PJPF> ListarTodosPorContratanteIdAtivoChave(int idContratante, string chave)
        {
            return _pjpfBM.ListarTodosPorContratanteIdAtivoChave(idContratante, chave);
        }

        public WFD_PJPF BuscarExibirPorID(int id)
        {
            return _pjpfBM.BuscarExibirPorID(id);
        }

    }

    public class PJPFBancoBP : BPBase
    {
        readonly PJPFBancoBM _pJPFBancoBM = new PJPFBancoBM();
        public WFD_PJPF_BANCO BuscarPorID(int id)
        {
            return _pJPFBancoBM.BuscarPorID(id);
        }
    }

    public class PJPFBaseBP : BPBase
    {
        readonly PJPFBaseBM _pJPFBaseBM = new PJPFBaseBM();
        public WFD_PJPF_BASE BuscarPorID(int id)
        {
            return _pJPFBaseBM.BuscarPorID(id);
        }
        public WFD_PJPF_BASE BuscarPorIDContratanteID(int id, int contratanteId)
        {
            return _pJPFBaseBM.BuscarPorIDContratanteID(id, contratanteId);
        }
    }

    public class PJPFBaseContatosBP : BPBase
    {
        readonly PJPFBaseContatosBM _descricaoDocumentosBM = new PJPFBaseContatosBM();
        public WFD_PJPF_BASE_CONTATOS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFCategoriaCHBP : BPBase
    {
        readonly PJPFCategoriaCHBM _descricaoDocumentosBM = new PJPFCategoriaCHBM();
        public WFD_PJPF_CATEGORIA_CH BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFContatosBP : BPBase
    {
        readonly PJPFContatosBM _descricaoDocumentosBM = new PJPFContatosBM();
        public WFD_PJPF_CONTATOS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFContratanteOrganizacaoComprasBP : BPBase
    {
        readonly PJPFContratanteOrganizacaoComprasBM _descricaoDocumentosBM = new PJPFContratanteOrganizacaoComprasBM();
        public WFD_PJPF_CONTRATANTE_ORG_COMPRAS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }
    
    public class PJPFDocumentosBP : BPBase
    {
        readonly PJPFDocumentosBM _descricaoDocumentosBM = new PJPFDocumentosBM();
        public WFD_PJPF_DOCUMENTOS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }
    
    public class PJPFListaDocumentosBP : BPBase
    {
        readonly PJPFListaDocumentosBM _descricaoDocumentosBM = new PJPFListaDocumentosBM();
        public WFD_PJPF_LISTA_DOCUMENTOS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFRoboBP : BPBase
    {
        readonly PJPFRoboBM _descricaoDocumentosBM = new PJPFRoboBM();
        public WFD_PJPF_ROBO BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFRoboLogBP : BPBase
    {
        readonly PJPFRoboLogBM _descricaoDocumentosBM = new PJPFRoboLogBM();
        public WFD_PJPF_ROBO_LOG BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFSolicitacaoBP : BPBase
    {
        readonly PJPFSolicitacaoBM _descricaoDocumentosBM = new PJPFSolicitacaoBM();
        public WFD_PJPF_SOLICITACAO BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFSolicitacaoDocumentosBP : BPBase
    {
        readonly PJPFSolicitacaoDocumentosBM _descricaoDocumentosBM = new PJPFSolicitacaoDocumentosBM();
        public WFD_PJPF_SOLICITACAO_DOCUMENTOS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFStatusBP : BPBase
    {
        readonly PJPFStatusBM _descricaoDocumentosBM = new PJPFStatusBM();
        public WFD_PJPF_STATUS BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class PJPFUnspscBP : BPBase
    {
        readonly PJPFUnspscBM _descricaoDocumentosBM = new PJPFUnspscBM();
        public WFD_PJPF_UNSPSC BuscarPorID(int id)
        {
            return _descricaoDocumentosBM.BuscarPorID(id);
        }
    }

    public class SolicitacaoBloqueioBP : BPBase
    {
        readonly SolicitacaoBloqueioBM _dolicitacaoBloqueioBM = new SolicitacaoBloqueioBM();
        public WFD_SOL_BLOQ BuscarPorID(int id)
        {
            return _dolicitacaoBloqueioBM.BuscarPorID(id);
        }
    }

    public class SolicitacaoCadastroPjpfbp : BPBase
    {
        readonly SolicitacaoCadastroPJPFBM _dolicitacaoCadastroPJPFBM = new SolicitacaoCadastroPJPFBM();
        public WFD_SOL_CAD_PJPF BuscarPorID(int id)
        {
            return _dolicitacaoCadastroPJPFBM.BuscarPorID(id);
        }
    }

    public class SolicitacaoCadastroPJPFMateriaisServicoBP : BPBase
    {
        readonly SolicitacaoCadastroPJPFMateriaisServicoBM _dolicitacaoCadastroPJPFMateriaisServicoBM = new SolicitacaoCadastroPJPFMateriaisServicoBM();
        public WFD_SOL_CAD_PJPF_MAT_SERV BuscarPorID(int id)
        {
            return _dolicitacaoCadastroPJPFMateriaisServicoBM.BuscarPorID(id);
        }
    }

    public class SolicitacaoDesbloqueioBP : BPBase
    {
        readonly SolicitacaoDesbloqueioBM _dolicitacaoDesbloqueioBM = new SolicitacaoDesbloqueioBM();
        public WFD_SOL_DESBLOQ BuscarPorID(int id)
        {
            return _dolicitacaoDesbloqueioBM.BuscarPorID(id);
        }
    }


    public class SolicitacaoModificacaoDadosGeraisSequenciaBP : BPBase
    {
        readonly SolicitacaoModificacaoDadosGeraisSequenciaBM _dolicitacaoModificacaoDadosGeraisSequenciaBM = new SolicitacaoModificacaoDadosGeraisSequenciaBM();
        public WFD_SOL_MOD_DGERAIS_SEQ BuscarPorID(int id)
        {
            return _dolicitacaoModificacaoDadosGeraisSequenciaBM.BuscarPorID(id);
        }
    }

    public class SolicitacaoStatusBP : BPBase
    {
        readonly SolicitacaoStatusBM _dolicitacaoStatusBM = new SolicitacaoStatusBM();
        public WFD_SOLICITACAO_STATUS BuscarPorID(int id)
        {
            return _dolicitacaoStatusBM.BuscarPorID(id);
        }
    }
    
    public class DescricaoBP : BPBase
    {
        readonly DescricaoBM _descricaoBM = new DescricaoBM();
        public WFD_T_DESCRICAO BuscarPorID(int id)
        {
            return _descricaoBM.BuscarPorID(id);
        }
        public List<WFD_T_DESCRICAO> ListarPorGrupoId(int grupoId)
        {
            return _descricaoBM.ListarPorGrupoId(grupoId);
        }
    }

    public class FuncaoBloqueioBP : BPBase
    {
        readonly FuncaoBloqueioBM _duncaoBloqueioBM = new FuncaoBloqueioBM();
        public WFD_T_FUNCAO_BLOQUEIO BuscarPorID(int id)
        {
            return _duncaoBloqueioBM.BuscarPorID(id);
        }
    }

    public class VisaoBP : BPBase
    {
        readonly VisaoBM _disaoBM = new VisaoBM();
        public WFD_T_VISAO BuscarPorID(int id)
        {
            return _disaoBM.BuscarPorID(id);
        }
    }

    public class TipoCadastroBP : BPBase
    {
        readonly TipoCadastroBM _dipoCadastroBM = new TipoCadastroBM();
        public WFD_TIPO_CADASTRO BuscarPorID(int id)
        {
            return _dipoCadastroBM.BuscarPorID(id);
        }
    }

    */
}

