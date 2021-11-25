using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddToneRequest : RequestBase<Tone>
    {
        public string Name { get; set; }

        public int Frequency { get; set; }

        public int WaveLenght { get; set; }
    }
}
