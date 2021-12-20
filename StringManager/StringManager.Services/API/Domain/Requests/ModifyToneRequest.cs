using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyToneRequest : RequestBase<Tone>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Frequency { get; set; }

        public double? WaveLenght { get; set; }
    }
}
