using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace bShop.Client.Models;

public class ShopValidation : ComponentBase
{
    [CascadingParameter] public EditContext? CurrentEditContext { get; set; }
    private ValidationMessageStore? MessageStore { get; set; }

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException(
                $"{nameof(ShopValidation)} requires a cascading " +
                $"parameter of type {nameof(EditContext)}. " +
                $"For example, you can use {nameof(ShopValidation)} " +
                $"inside an {nameof(EditForm)}.");
        }

        MessageStore = new(CurrentEditContext);
        CurrentEditContext.OnValidationRequested += (s, e) => MessageStore.Clear();
        CurrentEditContext.OnFieldChanged += (s, e) => MessageStore.Clear(e.FieldIdentifier);
    }

    public void DisplayErrors(Dictionary<string, List<string>> errors)
    {
        if (CurrentEditContext != null)
        {
            foreach (var err in errors)
            {
                MessageStore?.Add(CurrentEditContext.Field(err.Key), err.Value);
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
    }

    public void ClearErrors()
    {
        MessageStore?.Clear();
        CurrentEditContext?.NotifyValidationStateChanged();
    }
}
