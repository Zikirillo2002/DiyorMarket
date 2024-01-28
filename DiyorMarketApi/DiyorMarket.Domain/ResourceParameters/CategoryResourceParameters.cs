namespace DiyorMarket.Domain.ResourceParameters
{
    public class CategoryResourceParameters : ResourceParametersBase
    {
        public override string OrderBy { get; set; } = "name";

        protected override int MaxPageSize { get; set; } = 50;
    }
}
