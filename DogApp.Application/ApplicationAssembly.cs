using System.Reflection;

namespace DogApp.Application
{
    public static class ApplicationAssembly
    {
        public static Assembly GetAssembly()
        {
            return typeof(ApplicationAssembly).Assembly;
        }
    }
}
