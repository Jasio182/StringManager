namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyStringsSetRequest : RequestBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? NumberOfStrings { get; set; }
    }
}
