using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using ModelServices.Interfaces.Repositories;
using ApplicationServices.Services;
using ModelServices.EntitiesServices;
using DataServices.Repositories;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Presentation.Start.NinjectWebCommons), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Presentation.Start.NinjectWebCommons), "Stop")]

namespace Presentation.Start
{
    public class NinjectWebCommons
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();
            kernel.Bind<ILogAppService>().To<LogAppService>();
            kernel.Bind<IConfiguracaoAppService>().To<ConfiguracaoAppService>();
            kernel.Bind<INotificacaoAppService>().To<NotificacaoAppService>();
            kernel.Bind<ITemplateAppService>().To<TemplateAppService>();
            kernel.Bind<IAgendaAppService>().To<AgendaAppService>();
            kernel.Bind<INoticiaAppService>().To<NoticiaAppService>();
            kernel.Bind<ITarefaAppService>().To<TarefaAppService>();
            kernel.Bind<ITipoPessoaAppService>().To<TipoPessoaAppService>();
            kernel.Bind<ITelefoneAppService>().To<TelefoneAppService>();
            kernel.Bind<ICategoriaTelefoneAppService>().To<CategoriaTelefoneAppService>();
            kernel.Bind<IPrestadorAppService>().To<PrestadorAppService>();
            kernel.Bind<IPrestadorCnpjAppService>().To<PrestadorCnpjAppService>();
            kernel.Bind<IDepartamentoAppService>().To<DepartamentoAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<IConfiguracaoService>().To<ConfiguracaoService>();
            kernel.Bind<INotificacaoService>().To<NotificacaoService>();
            kernel.Bind<ITemplateService>().To<TemplateService>();
            kernel.Bind<IAgendaService>().To<AgendaService>();
            kernel.Bind<INoticiaService>().To<NoticiaService>();
            kernel.Bind<ITarefaService>().To<TarefaService>();
            kernel.Bind<ITipoPessoaService>().To<TipoPessoaService>();
            kernel.Bind<ITelefoneService>().To<TelefoneService>();
            kernel.Bind<ICategoriaTelefoneService>().To<CategoriaTelefoneService>();
            kernel.Bind<IPrestadorService>().To<PrestadorService>();
            kernel.Bind<IPrestadorCnpjService>().To<PrestadorCnpjService>();
            kernel.Bind<IDepartamentoService>().To<DepartamentoService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IConfiguracaoRepository>().To<ConfiguracaoRepository>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
            kernel.Bind<ILogRepository>().To<LogRepository>();
            kernel.Bind<IPerfilRepository>().To<PerfilRepository>();
            kernel.Bind<ITemplateRepository>().To<TemplateRepository>();
            kernel.Bind<ITipoPessoaRepository>().To<TipoPessoaRepository>();
            kernel.Bind<ICategoriaNotificacaoRepository>().To<CategoriaNotificacaoRepository>();
            kernel.Bind<INotificacaoRepository>().To<NotificacaoRepository>();
            kernel.Bind<INotificacaoAnexoRepository>().To<NotificacaoAnexoRepository>();
            kernel.Bind<IUsuarioAnexoRepository>().To<UsuarioAnexoRepository>();
            kernel.Bind<IUFRepository>().To<UFRepository>();
            kernel.Bind<ICargoRepository>().To<CargoRepository>();
            kernel.Bind<ICategoriaAgendaRepository>().To<CategoriaAgendaRepository>();
            kernel.Bind<IAgendaAnexoRepository>().To<AgendaAnexoRepository>();
            kernel.Bind<IAgendaRepository>().To<AgendaRepository>();
            kernel.Bind<INoticiaRepository>().To<NoticiaRepository>();
            kernel.Bind<INoticiaComentarioRepository>().To<NoticiaComentarioRepository>();
            kernel.Bind<ITarefaRepository>().To<TarefaRepository>();
            kernel.Bind<ITarefaAnexoRepository>().To<TarefaAnexoRepository>();
            kernel.Bind<ITarefaNotificacaoRepository>().To<TarefaNotificacaoRepository>();
            kernel.Bind<ITipoTarefaRepository>().To<TipoTarefaRepository>();
            kernel.Bind<ITelefoneRepository>().To<TelefoneRepository>();
            kernel.Bind<ICategoriaTelefoneRepository>().To<CategoriaTelefoneRepository>();
            kernel.Bind<IPeriodicidadeRepository>().To<PeriodicidadeRepository>();
            kernel.Bind<IBancoRepository>().To<BancoRepository>();
            kernel.Bind<ICategoriaCNHRepository>().To<CategoriaCNHRepository>();
            kernel.Bind<IMarcaVeiculoRepository>().To<MarcaVeiculoRepository>();
            kernel.Bind<IModeloVeiculoRepository>().To<ModeloVeiculoRepository>();
            kernel.Bind<IPrestadorAjudanteAnotacoesRepository>().To<PrestadorAjudanteAnotacoesRepository>();
            kernel.Bind<IPrestadorAjudanteRepository>().To<PrestadorAjudanteRepository>();
            kernel.Bind<IPrestadorAnexoRepository>().To<PrestadorAnexoRepository>();
            kernel.Bind<IPrestadorAnotacoesRepository>().To<PrestadorAnotacoesRepository>();
            kernel.Bind<IPrestadorBancoRepository>().To<PrestadorBancoRepository>();
            kernel.Bind<IPrestadorCertificadoRepository>().To<PrestadorCertificadoRepository>();
            kernel.Bind<IPrestadorCnpjRepository>().To<PrestadorCnpjRepository>();
            kernel.Bind<IPrestadorContatoRepository>().To<PrestadorContatoRepository>();
            kernel.Bind<IPrestadorEnderecoRepository>().To<PrestadorEnderecoRepository>();
            kernel.Bind<IPrestadorMotoristaAnotacoesRepository>().To<IPrestadorMotoristaAnotacoesRepository>();
            kernel.Bind<IPrestadorMotoristaRepository>().To<PrestadorMotoristaRepository>();
            kernel.Bind<IPrestadorRegiaoRepository>().To<PrestadorRegiaoRepository>();
            kernel.Bind<IPrestadorRepository>().To<PrestadorRepository>();
            kernel.Bind<IPrestadorVeiculoAnotacoesRepository>().To<PrestadorVeiculoAnotacoesRepository>();
            kernel.Bind<IPrestadorVeiculoCertificadoRepository>().To<PrestadorVeiculoCertificadoRepository>();
            kernel.Bind<IPrestadorVeiculoFuncaoRepository>().To<PrestadorVeiculoFuncaoRepository>();
            kernel.Bind<IPrestadorVeiculoRepository>().To<PrestadorVeiculoRepository>();
            kernel.Bind<IRegiaoCoberturaRepository>().To<RegiaoCoberturaRepository>();
            kernel.Bind<IRegiaoRepository>().To<RegiaoRepository>();
            kernel.Bind<ITipoCertificadoRepository>().To<TipoCertificadoRepository>();
            kernel.Bind<ITipoCertificadoVeiculoRepository>().To<TipoCertificadoVeiculoRepository>();
            kernel.Bind<ITipoContaRepository>().To<TipoContaRepository>();
            kernel.Bind<ITipoVeiculoRepository>().To<TipoVeiculoRepository>();
            kernel.Bind<IVeiculoFuncaoRepository>().To<VeiculoFuncaoRepository>();
            kernel.Bind<IDepartamentoRepository>().To<DepartamentoRepository>();

        }
    }
}