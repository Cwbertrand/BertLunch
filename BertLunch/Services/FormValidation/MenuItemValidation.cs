using Model;
using FluentValidation;

namespace BertLunch.Services.FormValidation
{
    public class MenuItemValidation : AbstractValidator<MenuItem>
    {
        public MenuItemValidation()
        {
            RuleFor(m => m.MenuName).NotEmpty().WithMessage("Please specify the name")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long");
                //.Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain letters, numbers and spaces");
            //RuleFor(m => m.Description).NotEmpty().WithMessage("Please write a description")
            //    .MinimumLength(15).WithMessage("Name must be at least 15 characters long")
            //    .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain letters, numbers and spaces");
            RuleFor(m => m.Ingredient).NotEmpty().WithMessage("Please specify the Ingredient")
                .MinimumLength(15).WithMessage("Name must be at least 15 characters long");
                //.Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain letters, numbers and spaces");
            RuleFor(m => m.Price).NotEmpty().WithMessage("Please specify the price");
            //RuleFor(m => m.Image).NotEmpty().WithMessage("Please input an image for the corresponding menu item");

        }
    }
}
