using SpecificationPattern.Application.Commands.WineCommands;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Specifications
{
    public class GetWineByIdSpecification : BaseSpecification<Wine, WineOutput>
    {
        public GetWineByIdSpecification(Guid id)
        {
            WhereExpressions.Add(w => w.Id == id);
            IncludeExpressions.Add(w => w.Grapes);
            IncludeExpressions.Add(w => w.Region.Country);
            IncludeExpressions.Add(w => w.Winery);
            SelectExpression = SelectExpression = wine => WineOutput.FromEntity(wine);
        }
    }
}
