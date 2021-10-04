namespace StringManager.Services.API.Domain.Requests
{
    public class AddStringsSetRequest : RequestBase
    {
        public string Name { get; set; }

        public int NumberOfStrings { get; set; }
    }
}
