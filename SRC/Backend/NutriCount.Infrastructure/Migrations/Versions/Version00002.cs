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
                .WithColumn("ServingSize").AsDecimal().NotNullable()
                .WithColumn("Calories").AsDecimal().NotNullable()
                .WithColumn("Proteins").AsDecimal().NotNullable()
                .WithColumn("Carbohydrates").AsDecimal().NotNullable()
                .WithColumn("TotalFats").AsDecimal().NotNullable()
                .WithColumn("SaturatedFats").AsDecimal().NotNullable()
                .WithColumn("TransFats").AsDecimal().NotNullable()
                .WithColumn("DietaryFiber").AsDecimal().NotNullable()
                .WithColumn("Sodium").AsDecimal().NotNullable()
                .WithColumn("Calcium").AsDecimal().NotNullable()
                .WithColumn("Iron").AsDecimal().NotNullable()
                .WithColumn("VitaminA").AsDecimal().NotNullable()
                .WithColumn("VitaminC").AsDecimal().NotNullable()
                .WithColumn("Potassium").AsDecimal().NotNullable()
                .WithColumn("Magnesium").AsDecimal().NotNullable()
                .WithColumn("Zinc").AsDecimal().NotNullable()
                .WithColumn("VitaminD").AsDecimal().NotNullable()
                .WithColumn("VitaminE").AsDecimal().NotNullable()
                .WithColumn("FolicAcid").AsDecimal().NotNullable()
                .WithColumn("Choline").AsDecimal().NotNullable()
                .WithColumn("Phosphorus").AsDecimal().NotNullable()
                .WithColumn("DataSource").AsString(255).NotNullable()
                .WithColumn("UserId").AsInt64().Nullable().ForeignKey("FK_Food_User_Id", "Users", "Id"); //UserAssociation
        }
    }
}
