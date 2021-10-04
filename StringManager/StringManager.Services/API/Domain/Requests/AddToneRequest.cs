namespace StringManager.Services.API.Domain.Requests
{
    public class AddToneRequest : RequestBase
    {
        public string Name { get; set; }

        public int Frequency { get; set; }

        public int WaveLenght { get; set; }
    }
}
