using Microsoft.AspNetCore.SignalR;
using MomAndBaby.BusinessObject.Entity;
using TableDependency.SqlClient;

namespace MomAndBaby.Subscribe
{
    public class SubscribeNotification
    {
        SqlTableDependency<Notification> _tableDependency;
        NotificationHub _hubContext;

        public SubscribeNotification(NotificationHub hubContext)
        {
            _hubContext = hubContext;
        }

        public void SubscribeTableDependency()
        {
            _tableDependency = new SqlTableDependency<Notification>("server=(local);database=MomAndBaby;uid=sa;pwd=12345;TrustServerCertificate=true");
            _tableDependency.OnChanged += TableDependency_OnChanged;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                await _hubContext.SendNotification();
            }
        }

    }
}
