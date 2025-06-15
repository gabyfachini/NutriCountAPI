namespace NutriCount.Communication.Request
{
    public class RequestFoodRegisterJson
    {
        public string Name { get; set; } = string.Empty;
        public decimal ServingSize { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal TotalFats { get; set; }
        public decimal SaturatedFats { get; set; }
        public decimal TransFats { get; set; }
        public decimal DietaryFiber { get; set; }
        public decimal Sodium { get; set; }
        public decimal Calcium { get; set; }
        public decimal Iron { get; set; }
        public decimal VitaminA { get; set; }
        public decimal VitaminC { get; set; }
        public decimal Potassium { get; set; }
        public decimal Magnesium { get; set; }
        public decimal Zinc { get; set; }
        public decimal VitaminD { get; set; }
        public decimal VitaminE { get; set; }
        public decimal FolicAcid { get; set; }
        public decimal Choline { get; set; }
        public decimal Phosphorus { get; set; }
        public string DataSource { get; set; } = string.Empty; //The next step would be to leave this as a pre-registered option rather than a text to have better analysis of the data.

    }
}
