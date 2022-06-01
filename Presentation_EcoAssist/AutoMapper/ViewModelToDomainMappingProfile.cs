using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EntitiesServices.Model;
using ERP_CRM_Solution.ViewModels;

namespace MvcMapping.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioViewModel, USUARIO_SUGESTAO>();
            CreateMap<UsuarioLoginViewModel, USUARIO_SUGESTAO>();
            CreateMap<LogViewModel, LOG>();
            CreateMap<ConfiguracaoViewModel, CONFIGURACAO>();
            CreateMap<NotificacaoViewModel, NOTIFICACAO>();
            CreateMap<TemplateViewModel, TEMPLATE>();
            CreateMap<AgendaViewModel, AGENDA>();
            CreateMap<NoticiaViewModel, NOTICIA>();
            CreateMap<NoticiaComentarioViewModel, NOTICIA_COMENTARIO>();
            CreateMap<TelefoneViewModel, TELEFONE>();
            CreateMap<TarefaViewModel, TAREFA>();
            CreateMap<TarefaAcompanhamentoViewModel, TAREFA_ACOMPANHAMENTO>();
            CreateMap<PrestadorViewModel, PRESTADOR>();
            CreateMap<PrestadorAjudanteViewModel, PRESTADOR_AJUDANTE>();
            CreateMap<PrestadorAnotacoesViewModel, PRESTADOR_ANOTACOES>();
            CreateMap<PrestadorBancoViewModel, PRESTADOR_BANCO>();
            CreateMap<PrestadorCertificadoViewModel, PRESTADOR_CERTIFICADO>();
            CreateMap<PrestadorContatoViewModel, PRESTADOR_CONTATO>();
            CreateMap<PrestadorEnderecoViewModel, PRESTADOR_ENDERECO>();
            CreateMap<PrestadorMotoristaViewModel, PRESTADOR_MOTORISTA>();
            CreateMap<PrestadorRegiaoViewModel, PRESTADOR_REGIAO>();
            CreateMap<PrestadorVeiculoViewModel, PRESTADOR_VEICULO>();

        }
    }
}