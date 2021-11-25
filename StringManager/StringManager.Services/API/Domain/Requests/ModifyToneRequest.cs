using StringManager.Core.Models;

namespace StringManager.Services.API.Domain.Requests
{
    public class ModifyToneRequest : RequestBase<Tone>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Frequency { get; set; }

        public int? WaveLenght { get; set; }
    }
}
