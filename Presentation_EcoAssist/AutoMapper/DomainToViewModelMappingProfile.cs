using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EntitiesServices.Model;
using ERP_CRM_Solution.ViewModels;

namespace MvcMapping.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<USUARIO_SUGESTAO, UsuarioViewModel>();
            CreateMap<USUARIO_SUGESTAO, UsuarioLoginViewModel>();
            CreateMap<LOG, LogViewModel>();
            CreateMap<CONFIGURACAO, ConfiguracaoViewModel>();
            CreateMap<NOTIFICACAO, NotificacaoViewModel>();
            CreateMap<TEMPLATE, TemplateViewModel>();
            CreateMap<AGENDA, AgendaViewModel>();
            CreateMap<NOTICIA, NoticiaViewModel>();
            CreateMap<NOTICIA_COMENTARIO, NoticiaComentarioViewModel>();
            CreateMap<TELEFONE, TelefoneViewModel>();
            CreateMap<TAREFA, TarefaViewModel>();
            CreateMap<TAREFA_ACOMPANHAMENTO, TarefaAcompanhamentoViewModel>();

        }
    }
}
