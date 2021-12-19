using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class AddToneRequest : RequestBase<Tone>
    {
        public string Name { get; set; }

        public double Frequency { get; set; }

        public double WaveLenght { get; set; }
    }
}
