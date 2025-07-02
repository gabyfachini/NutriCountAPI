using FluentMigrator;

namespace NutriCount.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_FOODS, "Create table to save the foods' information")]
    public class Version00002 : VersionBase
    {
        private const string FOOD_TABLE_NAME = "Foods";

        public override void Up()
        {
            CreateTable(FOOD_TABLE_NAME)
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Description").AsString(255).Nullable()
                .WithColumn("ServingSize").AsDouble().NotNullable()
                .WithColumn("Calories").AsDouble().NotNullable()
                .WithColumn("Protein").AsDouble().NotNullable()
                .WithColumn("Carbohydrates").AsDouble().NotNullable()
                .WithColumn("Fats").AsDouble().NotNullable()
                .WithColumn("UserId").AsInt64().Nullable().ForeignKey("FK_Food_User_Id", "Users", "Id"); //UserAssociation
        }
    }
}
