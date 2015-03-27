using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace OwinWindowsService
{
    public partial class Service : ServiceBase
    {
        private IDisposable _webApp;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            InternalStart();
        }

        internal void InternalStart(string url = "http://localhost:8080")
        {
            _webApp = WebApp.Start<Startup>(url);
        }

        protected override void OnStop()
        {
            InternalStop();
        }

        internal void InternalStop()
        {
            if(_webApp!=null)
                _webApp.Dispose();
        }
    }
}
