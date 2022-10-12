using ESESMT.Infra.Shared.Notifications;

namespace ESESMT.Domain.Entities
{
    public class BaseEntity<TEntityKey> : Notifiable
    {
        public TEntityKey Id { get; set; }
    }
}