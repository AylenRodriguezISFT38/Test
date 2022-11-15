using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using DB;
using System.Linq;

namespace WebApplication1.Services
{
    public class IntervalTaskReport : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceScopeFactory _scopeFactory;
        public IConfiguration config;
        public IntervalTaskReport(IServiceScopeFactory scopeFactory, IConfiguration _config)
        {
            _scopeFactory = scopeFactory;
            config = _config;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(CreateReport, null, TimeSpan.Zero, TimeSpan.FromHours(6));

            return Task.CompletedTask;

        }
        public void CreateReport(object state)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _db = scope.ServiceProvider.GetRequiredService<TestContext>();

                    var bsq = _db.Stock.Where(x => x.Cantidad <= 10 && x.Cantidad > 0).ToList();
                    var bsq2 = _db.Stock.Where(x => x.Cantidad == 0).ToList();

                    if (bsq.Count() != 0)
                    {
                        foreach (var item in bsq)
                        {
                            StockReport.Report("El producto " + _db.Producto.Where(x => x.Id == item.IdProducto).FirstOrDefault().Nombre + " tiene " + item.Cantidad + " de stock disponible!");
                        }
                    }
                    else if (bsq2.Count() != 0)
                    {
                        foreach (var item in bsq)
                        {
                            StockReport.Report("El producto " + _db.Producto.Where(x => x.Id == item.IdProducto).FirstOrDefault().Nombre + " se quedó sin stock!");
                        }
                    }
                    

                }
            }
            catch (Exception ex)
            {
                LogService.Log("Error la clase IntervalTaskReport " + ex.Message);
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
    
}
