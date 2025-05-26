namespace NutriCount.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;//Ao invés de ser nulo, é uma string vazia
        public string Email { get; set; }  =string.Empty;
        public string Password { get; set; } = string.Empty; 
        public Guid  UserIdentifier { get; set; } = Guid.NewGuid();

    }
}
