using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Administracao;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Repository.Repositories;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Service.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Domain.Services.PreConfiguracao.Contratante
{
    public interface IPreConfiguracaoContratanteService
    {
        void IncluirContratanteAncora(AdesaoContratante contratante);
        void IncluirFornecedorIndividual(AdesaoContratante contratante);
    }
    public class PreConfiguracaoContratanteService : PadraoService<IUnitOfWork>
    {
        private readonly IUnitOfWork _unitOfWok;
        private readonly IContratanteConfiguracaoRepository _contratanteConfig;
        private readonly IContratanteConfiguracaoEmailRepository _contratanteConfigEmail;
        private readonly IFuncaoRepository _funcao;
        private readonly IContratanteOrganizacaoCompraRepository _contratanteOrgCompras;
        private readonly IFluxoRepository _fluxo;
        private readonly IFluxoSequenciaRepository _fluxoSequencia;
        private readonly IContratanteRepository _contratante;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IPapelRepository _papelRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;
        private readonly IDescricaoDocumentosRepository _descricaoDocumentoRepository;
        private readonly IFornecedorCategoriaRepository _categoriaFornecedorRepository;

        public PreConfiguracaoContratanteService(IUnitOfWork processo, IContratanteConfiguracaoRepository contratanteConfig, IContratanteConfiguracaoEmailRepository contratanteConfigEmail, IFuncaoRepository funcao, IContratanteOrganizacaoCompraRepository contratanteOrgCompras, IFluxoRepository fluxo, IFluxoSequenciaRepository fluxoSequencia, IContratanteRepository contratante, IPerfilRepository perfilRepository, IPapelRepository papelRepository, IGrupoRepository grupoRepository, ITipoDocumentoRepository tipoDocumentoRepository, IDescricaoDocumentosRepository descricaoDocumentoRepository, IFornecedorCategoriaRepository categoriaFornecedorRepository)
        {
            _unitOfWok = processo;
            _contratanteConfig = contratanteConfig;
            _contratanteConfigEmail = contratanteConfigEmail;
            _funcao = funcao;
            _contratanteOrgCompras = contratanteOrgCompras;
            _fluxo = fluxo;
            _fluxoSequencia = fluxoSequencia;
            _contratante = contratante;
            _perfilRepository = perfilRepository;
            _papelRepository = papelRepository;
            _grupoRepository = grupoRepository;
            _tipoDocumentoRepository = tipoDocumentoRepository;
            _descricaoDocumentoRepository = descricaoDocumentoRepository;
            _categoriaFornecedorRepository = categoriaFornecedorRepository;
            try
            {       
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// CN.02 Pré - Configurar a Aplicação (Contratante)
        /// Os valores das tabelas abaixo são definidos pelo CONTRATANTE e devem ser especificadas nas implantações.
        /// Estes valores determinarão como a aplicação funcionará conforme as regras parametrizadas pelo Contratado.
        /// </summary>
        public void IncluirContratanteAncora(AdesaoContratante contratante)
        {
            //Incluir Contratante
            CONTRATANTE ancora = new CONTRATANTE();
            ancora.TIPO_CADASTRO_ID = (int)contratante.TipoEmpresa;
            ancora.TIPO_CONTRATANTE_ID = (int)contratante.TipoContratante;
            ancora.DATA_CADASTRO = DateTime.Now;
            ancora.ATIVO = true;
            ancora.ATIVO_DT = DateTime.Now;
            ancora.CNPJ = contratante.Documento;
            ancora.RAZAO_SOCIAL = contratante.RazaoSocial;
            ancora.NOME_FANTASIA = contratante.NomeFantasia;
            ancora.COD_WEBFORMAT = contratante.CodigoWebformat;
            ancora.CONTRANTE_COD_ERP = contratante.CodigoErp;
            ancora.ESTILO = contratante.CodigoErp;

            //•	WFD_CONTRATANTE_CONFIG - Define dados particulares do contratante(Manualmente)
            CONTRATANTE_CONFIGURACAO config = _contratanteConfig.BuscarPorId(1);
            ancora.WFD_CONTRATANTE_CONFIG = new CONTRATANTE_CONFIGURACAO()
            {
                SOLICITA_DOCS = config.SOLICITA_DOCS,
                SOLICITA_FICHA_CAD = config.SOLICITA_FICHA_CAD,
                LOGOTIPO = config.LOGOTIPO,
                TERMO_ACEITE = config.TERMO_ACEITE,
                ROBO_CICLO_ATU = config.ROBO_CICLO_ATU,
                ROBO_DT_PROX_EXEC = config.ROBO_DT_PROX_EXEC,
                BLOQUEIO_MANUAL = config.BLOQUEIO_MANUAL,
                BLOQUIEO_MANUAL_PRAZO = config.BLOQUIEO_MANUAL_PRAZO,
                TOTAL_TENTATIVA_ROBO = config.TOTAL_TENTATIVA_ROBO,
                NIVEIS_CATEGORIA = config.NIVEIS_CATEGORIA,
                QTD_ROBO_SIMULTANEA = config.QTD_ROBO_SIMULTANEA,
                PRAZO_ENTREGA_FICHA = config.PRAZO_ENTREGA_FICHA,
                FORNECEDOR_CARGA = config.FORNECEDOR_CARGA,
                FORNECEDOR_RETORNO = config.FORNECEDOR_RETORNO,
                CLIENTE_CARGA = config.CLIENTE_CARGA,
                CLIENTE_RETORNO = config.CLIENTE_RETORNO,
                CONTRATANTE_ID = 0
            };

            //•	WFD_CONTRATANTE_CONFIG_EMAIL - Define os emails que o contratante envia (Manualmente)
            var contratanteConfiguracaoEmails = _contratanteConfigEmail.ListarTodosPorIdContratante(1);
            contratanteConfiguracaoEmails.ForEach(x =>
            {
                ancora.WFD_CONTRATANTE_CONFIG_EMAIL.Add(
                new CONTRATANTE_CONFIGURACAO_EMAIL
                {
                    ASSUNTO = x.ASSUNTO,
                    CORPO = x.CORPO,
                    EMAIL_TP_ID = x.EMAIL_TP_ID
                });
            });

            //•	WFD_CONTRATANTE_FUNCAO - Define quais funções o contratante tem acesso.Se o contratante não for associado a uma função nesta tabela, 
            // esta função não parecerá na tela de cadastro de novos perfis. (Manualmente)
            List<FUNCAO> funcao = _funcao.Listar().Where(x => x.ID == 7 || x.FUNCAO_PAI == 7).ToList();
            ancora.WAC_FUNCAO = funcao;

            //•	WFD_CONTRATANTE_LOG – Não é usado

            //•	WFD_CONTRATANTE_ORG_COMPRAS – Define as organizações de compras que um contratante pode ter.
            //é necessário ao menos uma organização(Manualmente).
            var organizacoesDeCompras = _contratanteOrgCompras.ListarTodosPorIdContratante(1);
            organizacoesDeCompras.ForEach(x =>
            {
                ancora.WFD_CONTRATANTE_ORG_COMPRAS.Add(
                    new CONTRATANTE_ORGANIZACAO_COMPRAS
                    {
                        ORG_COMPRAS_COD = x.ORG_COMPRAS_COD,
                        ORG_COMPRAS_DSC = x.ORG_COMPRAS_DSC
                    });
            });

            //•	WFD_TIPO_DOCUMENTOS – Grupo de documentos que o contratante e o fornecedor irão usar para armazenar seus arquivos.
            //Inicialmente será populada com a tabela WFD_TIPO_DOCUMENTOS_CH Manualmente.
            var tipoDocumentos = _tipoDocumentoRepository.Listar(x => x.Valido(x) && x.CONTRATANTE_ID == 1, e => e.DESCRICAO).ToList();
            tipoDocumentos.ForEach(x =>
            {
                ancora.WFD_TIPO_DOCUMENTOS.Add(
                    new TIPO_DOCUMENTOS
                    {
                        ATIVO = x.ATIVO,
                        DESCRICAO = x.DESCRICAO,
                        TIPO_DOCUMENTOS_CH_ID = x.TIPO_DOCUMENTOS_CH_ID,
                    });
            });

            //•	WFD_DESCRICAO_DOCUMENTOS – São os documentos do grupo de documentos que o contratante e o fornecedor usarão 
            //para armazenar seus arquivos.Inicialmente será populada com a tabela WFD_DESCRICAO_DOCUMENTOS_CH Manualmente.
            var descricaoDocumentos = _descricaoDocumentoRepository.Listar(e => e.ATIVO && e.CONTRATANTE_ID == 1).ToList();
            descricaoDocumentos.ForEach(x =>
            {
                ancora.WFD_DESCRICAO_DOCUMENTOS.Add(
                    new WFD_DESCRICAO_DOCUMENTOS
                    {
                        ATIVO = x.ATIVO,
                        DESCRICAO = x.DESCRICAO,
                        DESCRICAO_DOCUMENTOS_CH_ID = x.DESCRICAO_DOCUMENTOS_CH_ID,
                        TIPO_DOCUMENTOS_ID = x.TIPO_DOCUMENTOS_ID
                    });
            });
            //•	WFD_GRUPO – Define o grupo de empresas que o contratante faz parte. 
            //Se o contratante não tive grupo é necessário criar ao menos um grupo pra ele. (Manualmente)
            //•	WFD_GRUPO_CONTRATANTE – Associa os contratantes ao grupo de empresa.
            var grupos = _grupoRepository.BuscarPorId(1);
            ancora.WFD_GRUPO.Add(new GRUPO
            {
                GRUPO_NM = contratante.Grupo
            });


            //•	WFD_PJPF_CATEGORIA – Inicialmente serão as categorias armazenadas na tabela WFD_PJPF_CATEGORIA_CH (Manualmente).
            var categorias = _categoriaFornecedorRepository.ListarPorContratanteId(1);
            categorias.ForEach(x =>
            {
                ancora.WFD_PJPF_CATEGORIA.Add(new FORNECEDOR_CATEGORIA
                {
                    ATIVO = x.ATIVO,
                    CATEGORIA_PAI_ID = x.CATEGORIA_PAI_ID,
                    CODIGO = x.CODIGO,
                    DESCRICAO = x.DESCRICAO,
                    ISENTO_CONTATOS = x.ISENTO_CONTATOS,
                    ISENTO_DADOSBANCARIOS = x.ISENTO_DADOSBANCARIOS,
                    ISENTO_DOCUMENTOS = x.ISENTO_DOCUMENTOS,
                    PJPF_CATEGORIA_CH_ID = x.PJPF_CATEGORIA_CH_ID,
                    QIC_QUESTIONARIO_CATEGORIA = x.QIC_QUESTIONARIO_CATEGORIA
                });
            });

            //•	WFD_USUARIO – Necessário criar um usuário Administrador Manualmente para o novo contratante.
            ancora.WFD_USUARIO.Add(new Domain.Models.USUARIO
            {
                ATIVO = true,
                CONTA_TENTATIVA = 0,
                DAT_NASCIMENTO = DateTime.Now,
                DT_CRIACAO = DateTime.Now,
                EXPIRA_EM_DIAS = 0,
                PRIMEIRO_ACESSO = true,
                CPF_CNPJ = contratante.UsuarioAdministrador.Documento,
                PRINCIPAL = contratante.UsuarioAdministrador.Administrador,
                CARGO = contratante.UsuarioAdministrador.Cargo,
                EMAIL = contratante.UsuarioAdministrador.Email,
                LOGIN = contratante.UsuarioAdministrador.Login,
                SENHA = "1000:GOVDjUgvS4PqbgHkr6Q3uRmFXy5EFPD5:V+GLrEZIIoGcY1h0gwFMVR+AiPdcSHy9",
                NOME = contratante.UsuarioAdministrador.Nome,
            });

            //•	WFL_FLUXO – Define todos os fluxos que o contratante âncora terá no sistema.
            //Inicialmente copiar os fluxos do contratante CH manualmente.
            var fluxos = _fluxo.BuscarPorContratanteId(1);
            fluxos.ForEach(x =>
            {
                ancora.WFL_FLUXO.Add(new FLUXO
                {
                    FLUXO_NM = x.FLUXO_NM,
                    CONTRATANTE_ID = x.CONTRATANTE_ID,
                    APLICACAO_ID = x.APLICACAO_ID,
                    PAPEL_INI_FLUXO = x.PAPEL_INI_FLUXO,
                    FLUXO_TP_ID = x.FLUXO_TP_ID
                });
            });

            //•	WFL_FLUXO_SEQ_PRE_REQUIS – por enquanto não é usado.

            //•	WFL_USUARIO_PAPEL – Associar o usuário Administrador aos papeis necessários ou a todos.
            var papeis = _papelRepository.ListarPorContratanteId(1);
            papeis.ForEach(x =>
            {
                ancora.WFL_PAPEL.Add(new WFL_PAPEL
                {
                    PAPEL_NM = x.PAPEL_NM,
                    PAPEL_SGL = x.PAPEL_SGL,
                    PAPEL_TP_ID = 50
                });
            });

            //•	WAC_PERFIL - É preciso criar um perfil Administrador Inicial (Manualmente)
            var perfis = _perfilRepository.ListarPorContratanteId(1);
            perfis.ForEach(x =>
            {
                ancora.WAC_PERFIL.Add(new Perfil
                {
                    PERFIL_DSC = x.PERFIL_DSC,
                    PERFIL_NM = x.PERFIL_NM
                });
            });
            //•	WFD_CONTRATANTE - Necessário Criar o Contratante(Manualmente)
            _contratante.Inserir(ancora);
            _unitOfWok.Finalizar();
            //•	WFL_FLUXO_SEQUENCIA – Define a sequência que cada Fluxo irá seguir no workflow. 
            //Inicialmente copiar do contratante CH Manualmente.
            var fluxosContratantenovo = _fluxo.BuscarPorContratanteId(ancora.ID);
            fluxosContratantenovo.ForEach(y =>
            {
                var fluxoSequenciais = _fluxoSequencia.ListarPorContratanteIdEFluxoTipoId(1, y.FLUXO_TP_ID);
                fluxoSequenciais.ForEach(x =>
                {
                    ancora.WFL_FLUXO_SEQUENCIA.Add(new FLUXO_SEQUENCIA
                    {
                        CONTRATANTE_ID = ancora.ID,
                        FLUXO_ID = y.ID,
                        SEQUENCIA = x.SEQUENCIA,
                        PAPEL_ID_INI = x.PAPEL_ID_INI,
                        PAPEL_ID_FIM = x.PAPEL_ID_FIM,
                        FLUXO_ETAPA_NM = x.FLUXO_ETAPA_NM,
                        FLUXO_ETAPA_DSC = x.FLUXO_ETAPA_DSC,
                        FLUXO_SEQ_ANTERIOR = x.FLUXO_SEQ_ANTERIOR,
                        GRUPO_ORIGEM = x.GRUPO_ORIGEM,
                        GRUPO_DESTINO = x.GRUPO_DESTINO,
                        EXECUCAO_MANUAL = x.EXECUCAO_MANUAL,
                        APROV_SEM_ROBO = x.APROV_SEM_ROBO,
                        BLOQ_INATIVO_RECEITA = x.BLOQ_INATIVO_RECEITA,
                    });
                });
            });
            //contratanteAncora.WFL_FLUXO_SEQUENCIA = fluxoSequenciais;


            _contratante.Alterar(ancora);
            _unitOfWok.Finalizar();

            ancora.USUARIO_ID = ancora.WFD_USUARIO.FirstOrDefault().ID;
            _contratante.Alterar(ancora);
            _unitOfWok.Finalizar();
        }
        /// <summary>
        /// Inclusão de contratante Individual ou Fornecedor-Contratante
        /// </summary>
        public void IncluirFornecedorIndividual(AdesaoContratante contratante)
        {
            CONTRATANTE contratanteAncora = new CONTRATANTE();
            contratanteAncora.TIPO_CADASTRO_ID = (int)EnumTiposFornecedor.EmpresaNacional;
            contratanteAncora.TIPO_CONTRATANTE_ID = (int)EnumTipoContratante.FornecedorIndividual;
            contratanteAncora.DATA_CADASTRO = DateTime.Now;
            contratanteAncora.DATA_NASCIMENTO = DateTime.Now;
            contratanteAncora.ATIVO = true;
            contratanteAncora.ATIVO_DT = DateTime.Now;

            //contratanteAncora.CNPJ = cnpj;
            //contratanteAncora.RAZAO_SOCIAL = razaoSocial;
            //contratanteAncora.NOME_FANTASIA = nomeFantasia;
            //contratanteAncora.COD_WEBFORMAT = codigoWebformat;
            //contratanteAncora.CONTRANTE_COD_ERP = codigoErp;
            contratanteAncora.ESTILO = "Azul";
            _contratante.Inserir(contratanteAncora);


            var papeis = _papelRepository.BuscarPorContratanteIdETipoPapelId(1, 50);
            contratanteAncora.WFL_PAPEL.Add(new WFL_PAPEL
            {
                PAPEL_NM = papeis.PAPEL_NM,
                PAPEL_SGL = papeis.PAPEL_SGL,
                PAPEL_TP_ID = papeis.PAPEL_TP_ID
            });

            contratanteAncora.WAC_PERFIL.Add(new Perfil
            {
                PERFIL_DSC = "Administrador do Sistema",
                PERFIL_NM = "Administrador"
            });
            contratanteAncora.WAC_PERFIL.Add(new Perfil
            {
                PERFIL_DSC = "Usuário do Sistema",
                PERFIL_NM = "Usuário"
            });
        }
        public void Dispose()
        {
            _unitOfWok.Finalizar();
        }
    }
}
