using DogApp.Domain.Enums;

namespace DogApp.Application.Helpers
{
    public class SortingHelper
    {
        public static SortingOrder ConvertToEnum(string? order)
        {
            return SortingOrder.Asc;
        }

    }
}
