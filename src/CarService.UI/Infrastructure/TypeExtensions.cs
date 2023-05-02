namespace CarService.UI.Infrastructure
{
    public static class TypeExtension
    {
        public static string GetControllerName(this Type controller)
        {
            var name = controller.Name;
            return name.EndsWith("Controller") ? name[0..^10] : name;
        }
    }
}