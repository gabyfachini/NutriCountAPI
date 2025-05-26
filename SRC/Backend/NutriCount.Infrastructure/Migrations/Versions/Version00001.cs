using FluentMigrator; //usada para versionamento de banco de dados via código

namespace NutriCount.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_USER, "Create table to save the user's information")]
    public class Version00001 : VersionBase
    {
        public override void Up() 
        {
            CreateTable("Users") //Cria uma tabela chamada Users
                .WithColumn("Name").AsString(255).NotNullable() //Adiciona uma coluna chamada Name, tipo VARCHAR(255), que não pode ser nula.
                .WithColumn("Email").AsString(255).NotNullable() //Coluna de e-mail, também string de até 255 caracteres, não nula.
                .WithColumn("Password").AsString(2000).NotNullable() //Coluna para senha, string de até 2000 caracteres (geralmente quando se usa hash de senha).
                .WithColumn("UserIdentifier").AsGuid().NotNullable(); //Uma coluna que guarda um GUID, provavelmente como identificador único do usuário.
        }
    }
}
