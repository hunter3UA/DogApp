using DogApp.Domain.Enums;

namespace DogApp.Application.Helpers
{
    public class SortingHelper
    {
        public static SortingOrder ConvertToEnum(string? orderString = "")
        {
            if (Enum.TryParse(orderString, true, out SortingOrder sortingOrder))
                return sortingOrder;

            return SortingOrder.Asc;
        }
    }
}