using MiBankWebsiteA2;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiBankWebsiteA3.Controllers.BusinessObjects
{    public class BackgroundServices : IHostedService, IDisposable
    {
        private readonly MiBankContext _context;
        private Timer _timer;
    
        public BackgroundServices(MiBankContext context)
        {
            _context = context;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            CheckBillpays();
            CheckLogins();
        }

        public void CheckBillpays()
        {
            var billpays = _context.BillPays.ToList();
            foreach (var bill in billpays)
            {
                if (bill.ScheduleDate <= DateTime.Now)
                {
                    if (bill.PayPeriod == BillPayPeriod.M || bill.PayPeriod == BillPayPeriod.Q || bill.PayPeriod == BillPayPeriod.Y)
                    {
                        if (!bill.Blocked)
                            bill.billPayTransaction();
                        bill.updateBillPay();
                    }
                }
            }
            _context.SaveChangesAsync();
        }

        public void CheckLogins()
        { 
            var CustomerAccounts = _context.Logins.ToList();
            foreach (Login login in CustomerAccounts)
            {
                if (login.Locked)
                {
                    if ((DateTime.Now - login.ModifyDate) >= TimeSpan.FromMinutes(1))
                    {
                        login.Locked = false;
                    }
                }
            }
            _context.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken stoppingToken)
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
