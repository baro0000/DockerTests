using FluentValidation;
using SOTags.ApplicationServices.API.Domain;

namespace SOTags.ApplicationServices.API.Validators
{
    public class GetPagedTagsRequestValidator : AbstractValidator<GetPagedTagsRequest>
    {
        public GetPagedTagsRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .LessThan(Int32.MaxValue).WithMessage($"Wrong range! Value must be integer greater than 0 and less than {Int32.MaxValue}");

            RuleFor(x => x.PageSize)
                .Must(BeValidPagingOption).WithMessage("Allowed options are: 10 / 30 / 50");

            RuleFor(x => x.SortByName)
                .Must(BeValidSortOrder).WithMessage("Allowed options are: ASC / DESC");

            RuleFor(x => x.SortByCount)
                .Must(BeValidSortOrder).WithMessage("Allowed options are: ASC / DESC");
        }

        private bool BeValidSortOrder(string sortOrder)
        {
            return sortOrder == "ASC" || sortOrder == "DESC" || sortOrder == null;
        }
        private bool BeValidPagingOption(int pagingOption)
        {
            return pagingOption == 10 || pagingOption == 30 || pagingOption == 50;
        }
    }
}
