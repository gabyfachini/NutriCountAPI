namespace NutriCount.Domain.Entities
{
    public class EntityBase
    {
        public long Id { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow; //Aqui usou o 'UtsNow' ao invés de só 'Now' porque ele vai usar um fuso padrão para qualquer lugar do mundo, se fosse só o 'Now' pegaria a data e hora do computador que esta executando o programa

    }
}
